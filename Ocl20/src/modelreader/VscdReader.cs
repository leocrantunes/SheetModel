using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;

namespace Ocl20.modelreader
{
    public class VscdReader : ModelReader
    {
        private readonly XNamespace xnamespaceUmlModel = "http://schemas.microsoft.com/dsltools/LogicalClassDesigner";
        private XElement xcoreModel;

        public VscdReader(string modelPath)
            : base(modelPath)
        {}

        public override CoreModel getModel()
        {
            return getModel(xnamespaceUmlModel);
        }

        public override string tryFindCoreModelElementType(string id)
        {
            var xreferencedtypes = xcoreModel.Descendants(xnamespaceUmlModel + "referencedType");
            var xtargetelement = xreferencedtypes.FirstOrDefault(x => x.Attribute("Id") != null && x.Attribute("Id").Value == id);
            if (xtargetelement != null)
            {
                var name = xtargetelement.Attribute("name").Value;
                var xotherelement =
                    xcoreModel.Descendants()
                              .FirstOrDefault(x => x.Attribute("name") != null && x.Attribute("name").Value == name);
                if (xotherelement != null)
                    return xotherelement.Attribute("Id").Value;
            }
            return "";
        }

        public CoreModel getModel(XNamespace xnamespace)
        {
            if (coreModel != null)
                return coreModel;

            xcoreModel = doc.Element(xnamespace + "logicalClassDesignerModel");

            if (xcoreModel != null)
            {
                coreModel = new CoreModelImpl();
                var name = xcoreModel.Attribute("name");
                coreModel.setName(name != null ? name.Value : "");
                var xcoreModelNamespace = xcoreModel.Element(xnamespace + "packagedElements");

                if (xcoreModelNamespace != null)
                {
                    CoreNamespace coreNamespace = new CoreNamespaceImpl();
                    
                    var xhaspackages = xcoreModelNamespace.Element(xnamespace + "logicalClassDesignerModelHasPackages");
                    if (xhaspackages != null)
                    {
                        var xpackages = xhaspackages.Elements(xnamespace + "package");

                        // all model datatypes
                        var xdatatypes = xcoreModelNamespace.Descendants(xnamespace + "referencedType");
                        foreach (var xdatatype in xdatatypes.Where(x => !belongsPackages(x,xpackages)))
                            createDataType(coreNamespace, coreModel, xdatatype);

                        foreach (var xpackage in xpackages)
                            createPackage(xnamespace, coreNamespace, coreModel, xpackage);
                    }
                    
                    // fill model types
                    fillModelElementTypes();
                }
            }

            return coreModel;
        }

        private bool belongsPackages(XElement x, IEnumerable<XElement> packages)
        {
            foreach (var p in packages)
            {
                if (x.Attribute("cachedFullName").Value.Contains(p.Attribute("name").Value))
                    return true;
            }

            return false;
        }
        
        private CorePackage createPackage(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xpackage)
        {
            CorePackage corePackage = new CorePackageImpl();
            corePackage.setName(xpackage.Attribute("name").Value);
            corePackage.setElemOwner(owner);
            updateElemOwnedElements(owner, corePackage);
            corePackage.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, corePackage);

            lookup.Add(xpackage.Attribute("Id").Value, corePackage);

            var xcoreNamespace = xpackage.Element(xnamespace + "packagedElements");
            if (xcoreNamespace != null)
            {
                CoreNamespace coreNamespace = new CoreNamespaceImpl();

                var xhasnamedelements = xcoreNamespace.Element(xnamespace + "packageHasNamedElement");
                if (xhasnamedelements != null)
                {
                    var xenumerations = xcoreNamespace.Descendants(xnamespace + "enumeration");
                    var stereotype = createStereotype(ownerNamespace, owner, "Enumeration");
                    foreach (var xenumeration in xenumerations)
                        createEnumeration(xnamespace, coreNamespace, corePackage, xenumeration, stereotype);

                    var stereotypeId = createStereotype(ownerNamespace, owner, "Id");
                    var xmodelClasses = xcoreNamespace.Descendants(xnamespace + "class");
                    createModelClasses(xnamespace, coreNamespace, corePackage, xmodelClasses, stereotypeId);
                    
                    var xpackages = xcoreNamespace.Descendants(xnamespace + "package");
                    foreach (var xinnerpackage in xpackages)
                        createPackage(xnamespace, coreNamespace, coreModel, xinnerpackage);
                }
            }

            return corePackage;
        }

        private CoreClassifier createEnumeration(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xenumeration, CoreStereotype coreStereotype)
        {
            CoreClassifier modelClass = new CoreClassifierImpl();
            modelClass.setName(xenumeration.Attribute("name").Value);

            modelClass.setElemOwner(owner);
            updateElemOwnedElements(owner, modelClass);
            modelClass.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, modelClass);

            updateExtendedElements(coreStereotype, modelClass);
            updateStereotypes(modelClass, coreStereotype);

            var xattributes = xenumeration.Descendants(xnamespace + "enumerationLiteral");
            foreach (var xattribute in xattributes)
            {
                CoreAttribute coreAttribute = new CoreAttributeImpl();
                coreAttribute.setName(xattribute.Attribute("name").Value);
                coreAttribute.setElemOwner(modelClass);
                updateElemOwnedElements(modelClass, coreAttribute);
                coreAttribute.setNamespace(ownerNamespace);
                updateNamespaceElemOwnedElements(ownerNamespace, coreAttribute);

                var id2 = xattribute.Attribute("Id").Value;
                lookup.Add(id2, coreAttribute);
            }

            var id = xenumeration.Attribute("Id").Value;
            lookup.Add(id, modelClass);
            
            return modelClass;
        }

        private CoreStereotype createStereotype(CoreNamespace ownerNamespace, CoreModelElement owner, string name)
        {
            CoreStereotype coreStereotype = new CoreStereotypeImpl();
            coreStereotype.setName(name);
            coreStereotype.setElemOwner(owner);
            updateElemOwnedElements(owner, coreStereotype);
            coreStereotype.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreStereotype);
            
            return coreStereotype;
        }

        private void createModelClasses(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, IEnumerable<XElement> xmodelClasses, CoreStereotype coreStereotype)
        {
            if (xmodelClasses != null)
            {
                var iEnumerable = xmodelClasses as IList<XElement> ?? xmodelClasses.ToList();
                foreach (var xmodelClass in iEnumerable)
                {
                    createClassifier(xnamespace, xmodelClass, ownerNamespace, owner, coreStereotype);
                }
                foreach (XElement xmodelClass in iEnumerable)
                {
                    var xassociations = xmodelClass.Descendants(xnamespace + "association");
                    foreach (var xassociation in xassociations)
                    {
                        CoreAssociation coreAssociation = createAssociation(xnamespace, xassociation, new CoreAssociationImpl());
                        fillModelElementTypes();
                        updateElemOwnedElements(coreModel, coreAssociation);
                    }
                }
            }
        }

        private CoreClassifier createClassifier(XNamespace xnamespace, XElement xmodelClass, CoreNamespace ownerNamespace, CoreModelElement owner, CoreStereotype coreStereotype)
        {
            CoreClassifier modelClass = new CoreClassifierImpl();
            modelClass.setName(xmodelClass.Attribute("name").Value);

            modelClass.setElemOwner(owner);
            updateElemOwnedElements(owner, modelClass);
            modelClass.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, modelClass);

            var xoperations = xmodelClass.Descendants(xnamespace + "operation");
            foreach (var xoperation in xoperations)
                createOperation(xnamespace, ownerNamespace, modelClass, xoperation);

            var xattributes = xmodelClass.Descendants(xnamespace + "property");
            foreach (var xattribute in xattributes)
                createAttribute(ownerNamespace, modelClass, xattribute, coreStereotype);

            var id = xmodelClass.Attribute("Id").Value;
            lookup.Add(id, modelClass);

            return modelClass;
        }

        private CoreAttribute createAttribute(CoreNamespace ownerNamespace, CoreModelElement owner, XElement xattribute, CoreStereotype coreStereotype)
        {
            CoreAttribute coreAttribute = new CoreAttributeImpl();
            coreAttribute.setName(xattribute.Attribute("name").Value);
            coreAttribute.setElemOwner(owner);
            updateElemOwnedElements(owner, coreAttribute);
            coreAttribute.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreAttribute);

            //coreAttribute.setOwnerScope(getScopeKind(xattribute.Attribute("ownerScope").Value));

            var id = xattribute.Attribute("Id").Value;
            lookup.Add(id, coreAttribute);
            var xtype = xattribute.Descendants(xnamespaceUmlModel + "referencedTypeMoniker").FirstOrDefault();
            if (xtype != null)
            {
                string xidref = xtype.Attribute("Id").Value;
                idToType.Add(id, xidref);
            }

            var isUnique = xattribute.Attribute("isUnique");
            if (isUnique == null || isUnique.Value == "true")
            {
                updateExtendedElements(coreStereotype, coreAttribute);
                updateStereotypes(coreAttribute, coreStereotype);
            }

            return coreAttribute;
        }

        private CoreDataType createDataType(CoreNamespace ownerNamespace, CoreModelElement owner, XElement xdatatype)
        {
            CoreDataType coreDataType = new CoreDataTypeImpl();
            var name = xdatatype.Attribute("name");
            coreDataType.setName(name.Value);
            coreDataType.setElemOwner(owner);
            updateElemOwnedElements(owner, coreDataType);
            coreDataType.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreDataType);

            var id = xdatatype.Attribute("Id");
            lookup.Add(id.Value, coreDataType);

            return coreDataType;
        }

        private CoreAssociation createAssociation(XNamespace xnamespace, XElement xassociation, CoreAssociation coreAssociation)
        {
            var name = xassociation.Attribute("name");
            coreAssociation.setName(name != null ? name.Value : "");

            List<CoreAssociationEnd> associationEnds = new List<CoreAssociationEnd>();
            var xassociationends = xassociation.Descendants(xnamespace + "memberEnd");
            foreach (var xassociationend in xassociationends)
                associationEnds.Add(createAssociationEnd(xnamespace, coreAssociation, xassociationend));

            XElement xparent = xassociation.Parent;
            const string xname = "class";
            while ((xparent != null && xparent.Name.LocalName != null) && !xparent.Name.LocalName.Equals(xname))
                xparent = xparent.Parent;
            var idAssocEnd0 = lookup.FirstOrDefault(e => e.Value == associationEnds[0]).Key;
            var idtype = xparent.Attribute("Id").Value;
            idToType.Add(idAssocEnd0, idtype);

            var idAssocEnd1 = lookup.FirstOrDefault(e => e.Value == associationEnds[1]).Key;
            var idtype1 = xassociation.Element(xnamespace + "classMoniker").Attribute("Id").Value;
            idToType.Add(idAssocEnd1, idtype1);

            lookup.Add(xassociation.Attribute("Id").Value, coreAssociation);

            return coreAssociation;
        }

        private CoreAssociationEnd createAssociationEnd(XNamespace xnamespace, CoreAssociation coreAssociation, XElement xassociationend)
        {
            CoreAssociationEnd coreAssociationEnd = new CoreAssociationEndImpl();
            var name = xassociationend.Attribute("name");
            coreAssociationEnd.setName(name != null ? name.Value : "");

            coreAssociationEnd.setAssociation(coreAssociation);
            //coreAssociationEnd.setOrdering(getOrderingKind(xassociationend.Attribute("ordering").Value));

            updateConnection(coreAssociation, coreAssociationEnd);

            //var xqualifiernamespace = xassociationend.Element(xnamespace + "AssociationEnd.qualifier");
            //if (xqualifiernamespace != null)
            //{
            //    var xqualifiers = xqualifiernamespace.Elements(xnamespace + "Attribute");
            //    foreach (var xqualifier in xqualifiers)
            //    {
            //        CoreAttribute qualifier = createAttribute(null, coreAssociationEnd, xqualifier);
            //        updateQualifiers(coreAssociationEnd, qualifier);
            //    }
            //}

            var multiplicity = createMultiplicity(xnamespace, xassociationend);
            coreAssociationEnd.setMultiplicity(multiplicity);

            var id = xassociationend.Attribute("Id").Value;
            lookup.Add(id, coreAssociationEnd);
            
            return coreAssociationEnd;
        }

        private Multiplicity createMultiplicity(XNamespace xnamespace, XElement xassociationend)
        {
            Multiplicity multiplicity = new MultiplicityImpl();
            MultiplicityRange range = createMultiplicityRange(xnamespace, xassociationend, multiplicity);
            updateRanges(multiplicity, range);

            return multiplicity;
        }

        private MultiplicityRange createMultiplicityRange(XNamespace xnamespace, XElement xmultiplicity, Multiplicity multiplicity)
        {
            MultiplicityRange range = new MultiplicityRangeImpl();

            var xlowerinternal = xmultiplicity.Element(xnamespace + "lowerValueInternal");
            if (xlowerinternal != null)
            {
                var xlower = xlowerinternal.Element(xnamespace + "literalString");
                if (xlower != null)
                {
                    var vlower = xlower.Attribute("value").Value;
                    vlower = vlower.Equals("*") ? "-1" : vlower;
                    int lower = Convert.ToInt32(vlower);
                    range.setLower(lower);
                }
            }
            else // xlowerinternal == null (valor default)
                range.setLower(1);

            var xupperinternal = xmultiplicity.Element(xnamespace + "upperValueInternal");
            if (xupperinternal != null)
            {
                var xupper = xupperinternal.Element(xnamespace + "literalString");
                if (xupper != null)
                {
                    var vupper = xupper.Attribute("value").Value;
                    vupper = vupper.Equals("*") ? "-1" : vupper;
                    int upper = Convert.ToInt32(vupper);
                    range.setUpper(upper);
                }
            }
            else // xupperinternal == null (valor default)
                range.setUpper(1);
            
            range.setMultiplicity(multiplicity);
            return range;
        }

        private CoreOperation createOperation(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xoperation)
        {
            CoreOperation coreOperation = new CoreOperationImpl();
            coreOperation.setName(xoperation.Attribute("name").Value);
            coreOperation.setElemOwner(owner);
            updateElemOwnedElements(owner, coreOperation);
            coreOperation.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreOperation);

            //coreOperation.setOwnerScope(getScopeKind(xoperation.Attribute("ownerScope").Value));

            var xparameters = xoperation.Descendants(xnamespace + "parameter");
            foreach (var xparameter in xparameters)
                createParameter(xnamespace, ownerNamespace, coreOperation, xparameter);
           
            var isQuery = xoperation.Attribute("isQuery").Value;
            coreOperation.setIsQuery(bool.Parse(isQuery));

            lookup.Add(xoperation.Attribute("Id").Value, coreOperation);

            return coreOperation;
        }

        private Parameter createParameter(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreBehavioralFeature owner, XElement xparameter)
        {
            Parameter parameter = new ParameterImpl();
            var name = xparameter.Attribute("name");
            parameter.setName(name != null ? name.Value : "");
            parameter.setBehavioralFeature(owner);
            updateOperationParameters(owner, parameter);
            parameter.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, parameter);

            var id = xparameter.Attribute("Id").Value;
            lookup.Add(id, parameter);

            var xptype = xparameter.Descendants(xnamespace + "referencedTypeMoniker").FirstOrDefault();
            if (xptype != null)
            {
                var xtyperefid = xptype.Attribute("Id").Value;
                idToType.Add(id, xtyperefid);
            }

            string skind = xparameter.Attribute("direction").Value.ToLower();
            parameter.setKind(getParameterDirectionKind(skind));

            return parameter;
        }
    }
}
