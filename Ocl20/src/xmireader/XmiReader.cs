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

                    var xpackages = xcoreModelNamespace.Elements(xnamespace + "Package");
                    foreach (var xpackage in xpackages)
                        createPackage(xnamespace, coreNamespace, coreModel, xpackage);

                    var xmodelClasses = xcoreModelNamespace.Elements(xnamespace + "Class");
                    foreach (var xmodelClass in xmodelClasses)
                        createModelClass(xnamespace, coreNamespace, coreModel, xmodelClass);
                }
            }

            return coreModel;
        }

        #endregion

        #region metamodel

        

        #endregion

        #region auxiliary methods

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
                foreach (var xmodelClass in xmodelClasses)
                    createModelClass(xnamespace, coreNamespace, corePackage, xmodelClass);

                var xstereotypes = xcoreNamespace.Elements(xnamespace + "Stereotype");
                foreach (var xstereotype in xstereotypes)
                    createStereotype(coreNamespace, corePackage, xstereotype);

                var xdatatypes = xcoreNamespace.Elements(xnamespace + "DataType");
                foreach (var xdatatype in xdatatypes)
                    createDataType(coreNamespace, corePackage, xdatatype);
            }

            return corePackage;
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

        private CoreClassifier createModelClass(XNamespace xnamespace, CoreNamespace ownerNamespace, CoreModelElement owner, XElement xmodelClass)
        {
            CoreClassifier modelClass = new CoreClassifierImpl();
            modelClass.setName(xmodelClass.Attribute("name").Value);

            modelClass.setElemOwner(owner);
            updateElemOwnedElements(owner, modelClass);
            modelClass.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, modelClass);

            var id = xmodelClass.Attribute("xmi.id").Value;
            lookup.Add(id, modelClass);

            var modelClassFeature = xmodelClass.Element(xnamespace + "Classifier.feature");
            if (modelClassFeature != null)
            {
                var xoperations = modelClassFeature.Elements(xnamespace + "Operation");
                foreach (var xoperation in xoperations)
                    createOperation(ownerNamespace, modelClass, xoperation);

                var xattributes = modelClassFeature.Elements(xnamespace + "Attribute");
                foreach (var xattribute in xattributes)
                    createAttribute(ownerNamespace, modelClass, xattribute);
            }

            return modelClass;
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

            lookup.Add(xattribute.Attribute("xmi.id").Value, coreAttribute);

            return coreAttribute;
        }

        private CoreOperation createOperation(CoreNamespace ownerNamespace, CoreModelElement owner, XElement xoperation)
        {
            CoreOperation coreOperation = new CoreOperationImpl();
            coreOperation.setName(xoperation.Attribute("name").Value);
            coreOperation.setElemOwner(owner);
            updateElemOwnedElements(owner, coreOperation);
            coreOperation.setNamespace(ownerNamespace);
            updateNamespaceElemOwnedElements(ownerNamespace, coreOperation);

            lookup.Add(xoperation.Attribute("xmi.id").Value, coreOperation);

            return coreOperation;
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
