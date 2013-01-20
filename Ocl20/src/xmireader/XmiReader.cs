using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;

namespace Ocl20.xmireader
{
    public class XmiReader
    {
        private Dictionary<string, CoreModelElement> lookup;
        private XDocument doc;
        private CoreModel coreModel;

        private readonly XNamespace xnamespaceUmlMetamodel = "org.omg.xmi.namespace.UML";
        private readonly XNamespace xnamespaceUmlModel = "href://org.omg/UML/1.3";

        public XmiReader(string modelPath)
        {
            lookup = new Dictionary<string, CoreModelElement>();
            doc = XDocument.Load(modelPath);
        }

        #region model

        public CoreModel getMetamodel()
        {
            return getModel(xnamespaceUmlMetamodel);
        }

        public CoreModel getModel()
        {
            return getModel(xnamespaceUmlModel);
        }

        public CoreModel getModel(XNamespace xnamespace)
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
                }
            }

            return coreModel;
        }

        #endregion

        #region metamodel

        

        #endregion

        #region auxiliary methods

        private IEnumerable<XElement> getAllAvailableGeneralizations(XNamespace xnamespace, XElement xcoreModelNamespace)
        {
            return xcoreModelNamespace.Descendants(xnamespace + "Generalization").Where(x => x.Attribute("xmi.id") != null);
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
            coreDataType.setName(xdatatype.Attribute("name").Value);
            coreDataType.setElemOwner(owner);
            updateElemOwnedElements(owner, coreDataType);
            coreDataType.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreDataType);
            
            lookup.Add(xdatatype.Attribute("xmi.id").Value, coreDataType);

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

                    var id = xmodelClass.Attribute("xmi.id").Value;
                    lookup.Add(id, modelClass);
                }

                // second iteration to reference classes that are types
                foreach (var xmodelClass in iEnumerable)
                {
                    var id = xmodelClass.Attribute("xmi.id").Value;
                    CoreModelElement modelClass;
                    lookup.TryGetValue(id, out modelClass);
                    var modelClassFeature = xmodelClass.Element(xnamespace + "Classifier.feature");
                    if (modelClassFeature != null)
                    {
                        var xoperations = modelClassFeature.Elements(xnamespace + "Operation");
                        foreach (var xoperation in xoperations)
                            createOperation(xnamespace, ownerNamespace, modelClass, xoperation);

                        var xattributes = modelClassFeature.Elements(xnamespace + "Attribute");
                        foreach (var xattribute in xattributes)
                            createAttribute(ownerNamespace, modelClass, xattribute);
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

            string xidref = xattribute.Attribute("type").Value;
            CoreModelElement featureType;
            lookup.TryGetValue(xidref, out featureType);
            coreAttribute.setFeatureType((CoreClassifier) featureType);

            // bug : when type is enum, maybe it's not on the list

            lookup.Add(xattribute.Attribute("xmi.id").Value, coreAttribute);

            return coreAttribute;
        }

        private CoreOperation createOperation(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xoperation)
        {
            CoreOperation coreOperation = new CoreOperationImpl();
            coreOperation.setName(xoperation.Attribute("name").Value);
            coreOperation.setElemOwner(owner);
            updateElemOwnedElements(owner, coreOperation);
            coreOperation.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreOperation);

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

            bool mode1 = fillParameterTypeMode1(xnamespace, xparameter, parameter);
            if (!mode1)
                fillParameterTypeMode2(xparameter, parameter);

            string skind = xparameter.Attribute("kind").Value;
            CoreModelElement kind;
            lookup.TryGetValue(skind, out kind);
            parameter.setKind(getParameterDirectionKind(skind));

            lookup.Add(xparameter.Attribute("xmi.id").Value, parameter);

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

        private bool fillParameterTypeMode1(XNamespace xnamespace, XElement xparameter, Parameter parameter)
        {
            var xptype = xparameter.Element(xnamespace + "Parameter.type");
            if (xptype != null)
            {
                var xpclass = xptype.Element(xnamespace + "Class");
                if (xpclass != null)
                {
                    string xtypeidref = xpclass.Attribute("xmi.idref").Value;
                    CoreModelElement type;
                    lookup.TryGetValue(xtypeidref, out type);
                    parameter.setType((CoreClassifier) type);

                    return true;
                }
            }

            return false;
        }

        private void fillParameterTypeMode2(XElement xparameter, Parameter parameter)
        {
            var xtype = xparameter.Attribute("type");
            if (xtype != null)
            {
                string xtypeidref = xtype.Value;
                CoreModelElement type;
                lookup.TryGetValue(xtypeidref, out type);
                parameter.setType((CoreClassifier)type);
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
            List<object> ownedElements = (List<object>)coreNamespace.getElemOwnedElements();
            ownedElements.Add(newOwnedElement);
            coreNamespace.setElemOwnedElements(ownedElements);
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

        #endregion
    }
}
