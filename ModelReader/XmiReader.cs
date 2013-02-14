using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.datatypes;

namespace ModelReader
{
    public class XmiReader : ModelReader
    {
        private Dictionary<string, CoreModelElement> lookup;
        private Dictionary<string, string> idToType; 
        private XDocument doc;
        private CoreModel coreModel;

        private readonly XNamespace xnamespaceUmlMetamodel = "org.omg.xmi.namespace.UML";
        private readonly XNamespace xnamespaceUmlModel = "href://org.omg/UML/1.3";

        public XmiReader(string modelPath)
        {
            lookup = new Dictionary<string, CoreModelElement>();
            idToType = new Dictionary<string, string>();
            doc = XDocument.Load(modelPath);
        }

        public CoreModel getMetamodel()
        {
            return getModel(xnamespaceUmlMetamodel);
        }

        public CoreModel getModel()
        {
            return getModel(xnamespaceUmlModel);
        }

        public override CoreModel getModel(XNamespace xnamespace)
        {
            if (coreModel != null)
                return coreModel;

            var xcoreModel = doc.Descendants(xnamespace + "Model").FirstOrDefault();

            if (xcoreModel != null)
            {
                coreModel = new CoreModelImpl();
                var xcoreModelNamespace = xcoreModel.Element(xnamespace + "Namespace.ownedElement");

                if (xcoreModelNamespace != null)
                {
                    CoreNamespace coreNamespace = new CoreNamespaceImpl();

                    // all model datatypes
                    var xdatatypes = xcoreModelNamespace.Descendants(xnamespace + "DataType");
                    foreach (var xdatatype in xdatatypes)
                        createDataType(coreNamespace, coreModel, xdatatype);

                    var xmodelClasses = xcoreModelNamespace.Elements(xnamespace + "Class");
                    createModelClasses(xnamespace, coreNamespace, coreModel, xmodelClasses);

                    var xpackages = xcoreModelNamespace.Elements(xnamespace + "Package");
                    foreach (var xpackage in xpackages)
                        createPackage(xnamespace, coreNamespace, coreModel, xpackage);
                    
                    // all model generalizations
                    var xgeneralizations = getAllAvailableGeneralizations(xnamespace, xcoreModelNamespace);
                    foreach (var xgeneralization in xgeneralizations)
                        createGeneralization(xnamespace, xgeneralization);

                    // all model abstractions
                    var xabstractions = xcoreModelNamespace.Descendants(xnamespace + "Abstraction");
                    foreach (var xabstraction in xabstractions)
                        createAbstraction(xabstraction);

                    // fill model types
                    fillModelElementTypes();

                    // all associations classes
                    var xassociationclasses = xcoreModelNamespace.Descendants(xnamespace + "AssociationClass");
                    foreach (var xassociationclass in xassociationclasses)
                    {
                        CoreAssociationClass associationClass = (CoreAssociationClass) createAssociation(xnamespace, xassociationclass, new CoreAssociationClassImpl());
                        fillModelElementTypes();
                        updateElemOwnedElements(coreModel, associationClass);

                        associationClass.setElemOwner(coreModel);
                        updateElemOwnedElements(coreModel, associationClass);
                        associationClass.setNamespace(coreNamespace);
                        updateNamespaceElemOwnedElements(coreNamespace, associationClass);

                        var xclassifierfeature = xassociationclass.Element(xnamespace + "Classifier.feature");
                        if (xclassifierfeature != null)
                        {
                            var xoperations = xclassifierfeature.Elements(xnamespace + "Operation");
                            foreach (var xoperation in xoperations)
                                createOperation(xnamespace, coreNamespace, associationClass, xoperation);

                            var xattributes = xclassifierfeature.Elements(xnamespace + "Attribute");
                            foreach (var xattribute in xattributes)
                                createAttribute(coreNamespace, associationClass, xattribute);
                        }
                    }

                    // all model associations
                    var xassociations = xcoreModelNamespace.Descendants(xnamespace + "Association");
                    foreach (var xassociation in xassociations)
                    {
                        CoreAssociation coreAssociation = createAssociation(xnamespace, xassociation, new CoreAssociationImpl());
                        fillModelElementTypes();
                        updateElemOwnedElements(coreModel, coreAssociation);
                    }
                }
            }

            return coreModel;
        }

        #region auxiliary methods

        private IEnumerable<XElement> getAllAvailableGeneralizations(XNamespace xnamespace, XElement xcoreModelNamespace)
        {
            return xcoreModelNamespace.Descendants(xnamespace + "Generalization").Where(x => x.Attribute("xmi.id") != null);
        }

        private CoreAssociation createAssociation(XNamespace xnamespace, XElement xassociation, CoreAssociation coreAssociation)
        {
            coreAssociation.setName(xassociation.Attribute("name").Value);
            
            XElement xelementconnection = xassociation.Element(xnamespace + "Association.connection");
            if (xelementconnection != null)
            {
                var xassociationends = xelementconnection.Elements(xnamespace + "AssociationEnd");
                foreach (var xassociationend in xassociationends)
                    createAssociationEnd(xnamespace, coreAssociation, xassociationend);
            }
            
            lookup.Add(xassociation.Attribute("xmi.id").Value, coreAssociation);

            return coreAssociation;
        }

        private void createAssociationEnd(XNamespace xnamespace, CoreAssociation coreAssociation, XElement xassociationend)
        {
            CoreAssociationEnd coreAssociationEnd = new CoreAssociationEndImpl();
            coreAssociationEnd.setName(xassociationend.Attribute("name").Value);
            coreAssociationEnd.setOrdering(getOrderingKind(xassociationend.Attribute("ordering").Value));

            coreAssociationEnd.setAssociation(coreAssociation);
            updateConnection(coreAssociation, coreAssociationEnd);

            var xqualifiernamespace = xassociationend.Element(xnamespace + "AssociationEnd.qualifier");
            if (xqualifiernamespace != null)
            {
                var xqualifiers = xqualifiernamespace.Elements(xnamespace + "Attribute");
                foreach (var xqualifier in xqualifiers)
                {
                    CoreAttribute qualifier = createAttribute(null, coreAssociationEnd, xqualifier);
                    updateQualifiers(coreAssociationEnd, qualifier);
                }
            }

            var multiplicity = createMultiplicity(xnamespace, xassociationend);
            coreAssociationEnd.setMultiplicity(multiplicity);

            var id = xassociationend.Attribute("xmi.id").Value;
            lookup.Add(id, coreAssociationEnd);
            var xtyperefid = xassociationend.Attribute("type").Value;
            idToType.Add(id, xtyperefid);
        }

        private Multiplicity createMultiplicity(XNamespace xnamespace, XElement xassociationend)
        {
            var xmultiplicitynamespace = xassociationend.Element(xnamespace + "AssociationEnd.multiplicity");
            if (xmultiplicitynamespace != null)
            {

                var xmultiplicity = xmultiplicitynamespace.Element(xnamespace + "Multiplicity");
                if (xmultiplicity != null)
                {
                    Multiplicity multiplicity = new MultiplicityImpl();
                    MultiplicityRange range = createMultiplicityRange(xnamespace, xmultiplicity, multiplicity);
                    updateRanges(multiplicity, range);

                    return multiplicity;
                }
            }

            return null;
        }

        private MultiplicityRange createMultiplicityRange(XNamespace xnamespace, XElement xmultiplicity, Multiplicity multiplicity)
        {
            var xrangenamespace = xmultiplicity.Element(xnamespace + "Multiplicity.range");
            if (xrangenamespace != null)
            {
                var xranges = xrangenamespace.Elements(xnamespace + "MultiplicityRange");
                foreach (var xrange in xranges)
                {
                    MultiplicityRange range = new MultiplicityRangeImpl();
                    int lower = Convert.ToInt32(xrange.Attribute("lower").Value);
                    range.setLower(lower);
                    int upper = Convert.ToInt32(xrange.Attribute("upper").Value);
                    range.setUpper(upper);
                    range.setMultiplicity(multiplicity);
                    return range;
                }
            }

            return null;
        }

        private OrderingKind getOrderingKind(string skind)
        {
            switch (skind)
            {
                case "ordered":
                    return OrderingKindEnum.OK_ORDERED;
                case "unordered":
                    return OrderingKindEnum.OK_UNORDERED;
                case "sorted":
                    return OrderingKindEnum.OK_SORTED;
                default:
                    return OrderingKindEnum.OK_UNORDERED;
            }
        }

        private void createAbstraction(XElement xabstraction)
        {
            CoreModelElement client = null;
            CoreModelElement supplier = null;

            XAttribute xattributeclient = xabstraction.Attribute("client");
            if (xattributeclient != null)
            {
                string xclientidref = xattributeclient.Value;
                lookup.TryGetValue(xclientidref, out client);
            }

            XAttribute xattributesupplier = xabstraction.Attribute("supplier");
            if (xattributesupplier != null)
            {
                string xsupplieridref = xattributesupplier.Value;
                lookup.TryGetValue(xsupplieridref, out supplier);
            }

            CoreClassifier coreClassifier = (CoreClassifier) client;
            CoreInterface coreInterface = (CoreInterface) supplier;

            Dependency dependency = new DependencyImpl();
            updateClient(dependency, coreClassifier);
            updateSupplier(dependency, coreInterface);

            updateClientDependency(coreInterface, dependency);
            updateSupplierDependency(coreClassifier, dependency);
        }


        private Generalization createGeneralization(XNamespace xnamespace, XElement xgeneralization)
        {
            Generalization generalization = new GeneralizationImpl();

            XAttribute xattributechild = xgeneralization.Attribute("child");
            if (xattributechild != null)
                fillGeneralizationMode1(xgeneralization, generalization, xattributechild);
            else
            {
                XElement xchild = xgeneralization.Element(xnamespace + "Generalization.child");
                if (xchild != null)
                    fillGeneralizationMode2(xnamespace, xgeneralization, generalization, xchild);
            }

            CoreClassifier child = generalization.getChild();
            updateChildGeneralizations(child, generalization);

            CoreClassifier parent = generalization.getParent();
            updateParentSpecializations(parent, generalization);

            lookup.Add(xgeneralization.Attribute("xmi.id").Value, generalization);

            return generalization;
        }

        private void updateChildGeneralizations(CoreClassifier owner, Generalization newGeneralization)
        {
            List<Generalization> generalizations = owner.getGeneralization();
            generalizations.Add(newGeneralization);
            owner.setGeneralization(generalizations);
        }

        private void updateParentSpecializations(CoreClassifier owner, Generalization newSpecialization)
        {
            List<Generalization> specializations = owner.getSpecialization();
            specializations.Add(newSpecialization);
            owner.setSpecialization(specializations);
        }

        private void fillGeneralizationMode1(XElement xgeneralization, Generalization generalization, XAttribute xattributechild)
        {
            string xchildidref = xattributechild.Value;
            CoreModelElement child;
            lookup.TryGetValue(xchildidref, out child);
            generalization.setChild((CoreClassifier)child);

            XAttribute xattributeparent = xgeneralization.Attribute("parent");
            if (xattributeparent != null)
            {
                string xparentidref = xattributeparent.Value;
                CoreModelElement parent;
                lookup.TryGetValue(xparentidref, out parent);
                generalization.setParent((CoreClassifier)parent);
            }
        }

        private void fillGeneralizationMode2(XNamespace xnamespace, XElement xgeneralization, Generalization generalization, XElement xchild)
        {
            var xclass = xchild.Element(xnamespace + "Class");
            if (xclass != null)
            {
                string xchildidref = xclass.Attribute("xmi.idref").Value;
                CoreModelElement child;
                lookup.TryGetValue(xchildidref, out child);
                generalization.setChild((CoreClassifier) child);

                XElement xparent = xgeneralization.Element(xnamespace + "Generalization.parent");
                if (xparent != null)
                {
                    var xpclass = xparent.Element(xnamespace + "Class");
                    if (xpclass != null)
                    {
                        string xparentidref = xpclass.Attribute("xmi.idref").Value;
                        CoreModelElement parent;
                        lookup.TryGetValue(xparentidref, out parent);
                        generalization.setParent((CoreClassifier) parent);
                    }
                }
            }
        }

        private CorePackage createPackage(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xpackage)
        {
            CorePackage corePackage = new CorePackageImpl();
            corePackage.setName(xpackage.Attribute("name").Value);
            corePackage.setElemOwner(owner);
            updateElemOwnedElements(owner, corePackage);
            corePackage.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, corePackage);

            lookup.Add(xpackage.Attribute("xmi.id").Value, corePackage);

            var xcoreNamespace = xpackage.Element(xnamespace + "Namespace.ownedElement");
            
            if (xcoreNamespace != null)
            {
                CoreNamespace coreNamespace = new CoreNamespaceImpl();

                var xmodelClasses = xcoreNamespace.Elements(xnamespace + "Class");
                createModelClasses(xnamespace, coreNamespace, corePackage, xmodelClasses);

                var xpackages = xcoreNamespace.Elements(xnamespace + "Package");
                foreach (var xinnerpackage in xpackages)
                    createPackage(xnamespace, coreNamespace, coreModel, xinnerpackage);

                var xstereotypes = xcoreNamespace.Elements(xnamespace + "Stereotype");
                foreach (var xstereotype in xstereotypes)
                    createStereotype(coreNamespace, corePackage, xstereotype);

                var xinterfaces = xcoreNamespace.Elements(xnamespace + "Interface");
                foreach (var xinterface in xinterfaces)
                    createInterface(coreNamespace, corePackage, xinterface);
            }

            return corePackage;
        }

        private CoreInterface createInterface(CoreNamespace ownerNamespace, CoreModelElement owner, XElement xinterface)
        {
            CoreInterface coreInterface = new CoreInterfaceImpl();
            coreInterface.setName(xinterface.Attribute("name").Value);
            coreInterface.setElemOwner(owner);
            updateElemOwnedElements(owner, coreInterface);
            coreInterface.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreInterface);

            lookup.Add(xinterface.Attribute("xmi.id").Value, coreInterface);

            return coreInterface;
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

            var id = xdatatype.Attribute("xmi.id");
            lookup.Add(id.Value , coreDataType);

            return coreDataType;
        }

        private CoreStereotype createStereotype(CoreNamespace ownerNamespace, CoreModelElement owner, XElement xstereotype)
        {
            CoreStereotype coreStereotype = new CoreStereotypeImpl();
            coreStereotype.setName(xstereotype.Attribute("name").Value);
            coreStereotype.setElemOwner(owner);
            updateElemOwnedElements(owner, coreStereotype);
            coreStereotype.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreStereotype);

            string xidref = xstereotype.Attribute("extendedElement").Value;
            CoreModelElement extendedElement;
            lookup.TryGetValue(xidref, out extendedElement);

            if (extendedElement != null)
            {
                updateExtendedElements(coreStereotype, extendedElement);
                updateStereotypes(extendedElement, coreStereotype);
            }
            
            lookup.Add(xstereotype.Attribute("xmi.id").Value, coreStereotype);

            return coreStereotype;
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

                    var xclassifierfeature = xmodelClass.Element(xnamespace + "Classifier.feature");
                    if (xclassifierfeature != null)
                    {
                        var xoperations = xclassifierfeature.Elements(xnamespace + "Operation");
                        foreach (var xoperation in xoperations)
                            createOperation(xnamespace, ownerNamespace, modelClass, xoperation);

                        var xattributes = xclassifierfeature.Elements(xnamespace + "Attribute");
                        foreach (var xattribute in xattributes)
                            createAttribute(ownerNamespace, modelClass, xattribute);
                    }

                    var id = xmodelClass.Attribute("xmi.id").Value;
                    lookup.Add(id, modelClass);
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

            coreAttribute.setOwnerScope(getScopeKind(xattribute.Attribute("ownerScope").Value));

            var id = xattribute.Attribute("xmi.id").Value;
            lookup.Add(id, coreAttribute);
            string xidref = xattribute.Attribute("type").Value;
            idToType.Add(id, xidref);

            return coreAttribute;
        }

        private ScopeKindEnum getScopeKind(string scopeKindString)
        {
            switch (scopeKindString)
            {
                case "instance" :
                    return ScopeKindEnum.SK_INSTANCE;
                case "classifier":
                    return ScopeKindEnum.SK_CLASSIFIER;
                default:
                    return ScopeKindEnum.SK_INSTANCE;
            }
        }

        private void fillModelElementTypes()
        {
            foreach (KeyValuePair<string, string> pair in idToType)
            {
                CoreModelElement type;
                lookup.TryGetValue(pair.Value, out type);
                
                CoreModelElement modelElement;
                lookup.TryGetValue(pair.Key, out modelElement);

                if (modelElement != null)
                {
                    if (modelElement is CoreAssociationEndImpl)
                        ((CoreAssociationEnd) modelElement).setType((CoreClassifier) type);
                    else if (modelElement is CoreAttributeImpl)
                        ((CoreAttribute) modelElement).setFeatureType((CoreClassifier) type);
                    else if (modelElement is ParameterImpl)
                        ((Parameter) modelElement).setType((CoreClassifier)type);
                }
            }

            idToType.Clear();
        }

        private CoreOperation createOperation(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xoperation)
        {
            CoreOperation coreOperation = new CoreOperationImpl();
            coreOperation.setName(xoperation.Attribute("name").Value);
            coreOperation.setElemOwner(owner);
            updateElemOwnedElements(owner, coreOperation);
            coreOperation.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreOperation);

            coreOperation.setOwnerScope(getScopeKind(xoperation.Attribute("ownerScope").Value));

            var xbehavioralFeature = xoperation.Element(xnamespace + "BehavioralFeature.parameter");
            if (xbehavioralFeature != null)
            {
                var xparameters = xbehavioralFeature.Elements(xnamespace + "Parameter");
                foreach (var xparameter in xparameters)
                    createParameter(xnamespace, ownerNamespace, coreOperation, xparameter);
            }

            var isQuery = xoperation.Attribute("isQuery").Value;
            coreOperation.setIsQuery(bool.Parse(isQuery));

            lookup.Add(xoperation.Attribute("xmi.id").Value, coreOperation);

            return coreOperation;
        }

        private Parameter createParameter(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreBehavioralFeature owner, XElement xparameter)
        {
            Parameter parameter = new ParameterImpl();
            parameter.setName(xparameter.Attribute("name").Value);
            parameter.setBehavioralFeature(owner);
            updateOperationParameters(owner, parameter);
            parameter.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, parameter);

            var id = xparameter.Attribute("xmi.id").Value;
            lookup.Add(id, parameter);
            
            // mode 1
            var xptype = xparameter.Element(xnamespace + "Parameter.type");
            if (xptype != null)
            {
                var xpclass = xptype.Element(xnamespace + "Class");
                if (xpclass != null)
                {
                    var xtyperefid = xpclass.Attribute("xmi.idref").Value;
                    idToType.Add(id, xtyperefid);
                }
            }
            else // mode 2
            {
                var xtype = xparameter.Attribute("type");
                if (xtype != null)
                {
                    string xtypeidref = xtype.Value;
                    idToType.Add(id, xtypeidref);
                }
            }

            string skind = xparameter.Attribute("kind").Value;
            CoreModelElement kind;
            lookup.TryGetValue(skind, out kind);
            parameter.setKind(getParameterDirectionKind(skind));

            return parameter;
        }

        private ParameterDirectionKind getParameterDirectionKind(string skind)
        {
            switch (skind)
            {
                case "in":
                    return ParameterDirectionKindEnum.PDK_IN;
                case "inout":
                    return ParameterDirectionKindEnum.PDK_INOUT;
                case "out":
                    return ParameterDirectionKindEnum.PDK_OUT;
                case "return":
                    return ParameterDirectionKindEnum.PDK_RETURN;
                default:
                    return ParameterDirectionKindEnum.PDK_IN;
            }
        }
        
        private void updateOperationParameters(CoreBehavioralFeature owner, Parameter newParameter)
        {
            List<Parameter> parameters = owner.getParameter();
            parameters.Add(newParameter);
            owner.setParameter(parameters);
        }

        private void updateElemOwnedElements(CoreModelElement owner, CoreModelElement newOwnedElement)
        {
            List<object> ownedElements = (List<object>) owner.getElemOwnedElements();
            ownedElements.Add(newOwnedElement);
            owner.setElemOwnedElements(ownedElements);
        }

        private void updateNamespaceElemOwnedElements(CoreNamespace coreNamespace, CoreModelElement newOwnedElement)
        {
            if (coreNamespace != null)
            {
                List<object> ownedElements = (List<object>) coreNamespace.getElemOwnedElements();
                ownedElements.Add(newOwnedElement);
                coreNamespace.setElemOwnedElements(ownedElements);
            }
        }

        private void updateExtendedElements(CoreStereotype owner, CoreModelElement newExtendedElement)
        {
            List<object> extendedElements = (List<object>)owner.getExtendedElement();
            extendedElements.Add(newExtendedElement);
            owner.setExtendedElement(extendedElements);
        }

        private void updateStereotypes(CoreModelElement owner, CoreStereotype newStereotype)
        {
            List<CoreStereotype> stereotypes = owner.getTheStereotypes();
            stereotypes.Add(newStereotype);
            owner.setTheStereotypes(stereotypes);
        }

        private void updateQualifiers(CoreAssociationEnd owner, CoreAttribute newQualifier)
        {
            List<object> qualifiers = owner.getQualifier();
            qualifiers.Add(newQualifier);
            owner.setQualifier(qualifiers);
        }

        private void updateRanges(Multiplicity owner, MultiplicityRange newRange)
        {
            List<object> ranges = owner.getRange();
            ranges.Add(newRange);
            owner.setRange(ranges);
        }

        private void updateConnection(CoreAssociation owner, CoreAssociationEnd newConnection)
        {
            List<object> connections = owner.getConnection();
            connections.Add(newConnection);
            owner.setConnection(connections);
        }

        private void updateClient(Dependency owner, CoreModelElement newConnection)
        {
            List<object> dependency = owner.getClient();
            dependency.Add(newConnection);
            owner.setClient(dependency);
        }

        private void updateSupplier(Dependency owner, CoreModelElement newConnection)
        {
            List<object> supplierDependency = owner.getSupplier();
            supplierDependency.Add(newConnection);
            owner.setSupplier(supplierDependency);
        }

        private void updateClientDependency(CoreInterface owner, Dependency newConnection)
        {
            List<object> dependency = owner.getClientDependency();
            dependency.Add(newConnection);
            owner.setClientDependency(dependency);
        }

        private void updateSupplierDependency(CoreClassifier owner, Dependency newConnection)
        {
            List<Dependency> supplierDependency = owner.getSupplierDependency();
            supplierDependency.Add(newConnection);
            owner.setSupplierDependency(supplierDependency);
        }

        #endregion
    }
}
