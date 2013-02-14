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

        public VscdReader(string modelPath)
            : base(modelPath)
        {}

        public override CoreModel getModel()
        {
            return getModel(xnamespaceUmlModel);
        }

        public CoreModel getModel(XNamespace xnamespace)
        {
            if (coreModel != null)
                return coreModel;

            var xcoreModel = doc.Element(xnamespace + "logicalClassDesignerModel");

            if (xcoreModel != null)
            {
                coreModel = new CoreModelImpl();
                var xcoreModelNamespace = xcoreModel.Element(xnamespace + "packagedElements");

                if (xcoreModelNamespace != null)
                {
                    CoreNamespace coreNamespace = new CoreNamespaceImpl();

                    // all model datatypes
                    var xdatatypes = xcoreModelNamespace.Descendants(xnamespace + "referencedType");
                    foreach (var xdatatype in xdatatypes)
                        createDataType(coreNamespace, coreModel, xdatatype);

                    //var xmodelClasses = xcoreModelNamespace.Elements(xnamespace + "Class");
                    //createModelClasses(xnamespace, coreNamespace, coreModel, xmodelClasses);

                    var xhaspackages = xcoreModelNamespace.Element(xnamespace + "logicalClassDesignerModelHasPackages");
                    if (xhaspackages != null)
                    {
                        var xpackages = xhaspackages.Elements(xnamespace + "package");
                        foreach (var xpackage in xpackages)
                            createPackage(xnamespace, coreNamespace, coreModel, xpackage);
                    }
                    
                    //// all model generalizations
                    //var xgeneralizations = getAllAvailableGeneralizations(xnamespace, xcoreModelNamespace);
                    //foreach (var xgeneralization in xgeneralizations)
                    //    createGeneralization(xnamespace, xgeneralization);

                    //// create all model abstractions
                    //var xabstractions = xcoreModelNamespace.Descendants(xnamespace + "Abstraction");
                    //foreach (var xabstraction in xabstractions)
                    //    createAbstraction(xabstraction);

                    // fill model types
                    fillModelElementTypes();

                    //// all associations classes
                    //var xassociationclasses = xcoreModelNamespace.Descendants(xnamespace + "AssociationClass");
                    //foreach (var xassociationclass in xassociationclasses)
                    //{
                    //    CoreAssociationClass associationClass = (CoreAssociationClass)createAssociation(xnamespace, coreNamespace, coreModel, xassociationclass, new CoreAssociationClassImpl());
                    //    fillModelElementTypes();
                    //    updateElemOwnedElements(coreModel, associationClass);

                    //    associationClass.setElemOwner(coreModel);
                    //    updateElemOwnedElements(coreModel, associationClass);
                    //    associationClass.setNamespace(coreNamespace);
                    //    updateNamespaceElemOwnedElements(coreNamespace, associationClass);

                    //    var xclassifierfeature = xassociationclass.Element(xnamespace + "Classifier.feature");
                    //    if (xclassifierfeature != null)
                    //    {
                    //        var xoperations = xclassifierfeature.Elements(xnamespace + "Operation");
                    //        foreach (var xoperation in xoperations)
                    //            createOperation(xnamespace, coreNamespace, associationClass, xoperation);

                    //        var xattributes = xclassifierfeature.Elements(xnamespace + "Attribute");
                    //        foreach (var xattribute in xattributes)
                    //            createAttribute(coreNamespace, associationClass, xattribute);
                    //    }

                    //}
                }
            }

            return coreModel;
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
                    var xmodelClasses = xcoreNamespace.Descendants(xnamespace + "class");
                    createModelClasses(xnamespace, coreNamespace, corePackage, xmodelClasses);

                    var xpackages = xcoreNamespace.Descendants(xnamespace + "package");
                    foreach (var xinnerpackage in xpackages)
                        createPackage(xnamespace, coreNamespace, coreModel, xinnerpackage);

                    //var xstereotypes = xcoreNamespace.Elements(xnamespace + "Stereotype");
                    //foreach (var xstereotype in xstereotypes)
                    //    createStereotype(coreNamespace, corePackage, xstereotype);

                    //var xinterfaces = xcoreNamespace.Elements(xnamespace + "Interface");
                    //foreach (var xinterface in xinterfaces)
                    //    createInterface(coreNamespace, corePackage, xinterface);
                }
            }

            return corePackage;
        }

        private void createModelClasses(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, IEnumerable<XElement> xmodelClasses)
        {
            if (xmodelClasses != null)
            {
                var iEnumerable = xmodelClasses as IList<XElement> ?? xmodelClasses.ToList();
                foreach (var xmodelClass in iEnumerable)
                {
                    CoreClassifier modelClass = new CoreClassifierImpl();
                    modelClass.setName(xmodelClass.Attribute("name").Value);

                    modelClass.setElemOwner(owner);
                    updateElemOwnedElements(owner, modelClass);
                    modelClass.setNamespace(ownerNamespace);
                    updateNamespaceElemOwnedElements(ownerNamespace, modelClass);

                    //var xoperations = xclassifierfeature.Elements(xnamespace + "Operation");
                    //foreach (var xoperation in xoperations)
                    //    createOperation(xnamespace, ownerNamespace, modelClass, xoperation);

                    var xattributes = xmodelClass.Descendants(xnamespace + "property");
                    foreach (var xattribute in xattributes)
                        createAttribute(ownerNamespace, modelClass, xattribute);
                    
                    var id = xmodelClass.Attribute("Id").Value;
                    lookup.Add(id, modelClass);
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

        private CoreAttribute createAttribute(CoreNamespace ownerNamespace, CoreModelElement owner, XElement xattribute)
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
            
            range.setMultiplicity(multiplicity);
            return range;
        }
    }
}
