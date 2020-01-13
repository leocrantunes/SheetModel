using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;

namespace Ocl20.modelreader
{
    public abstract class ModelReader
    {
        protected Dictionary<string, CoreModelElement> lookup;
        protected Dictionary<string, string> idToType;
        protected XDocument doc;
        protected CoreModel coreModel;

        protected ModelReader(string modelPath)
        {
            lookup = new Dictionary<string, CoreModelElement>();
            idToType = new Dictionary<string, string>();
            doc = XDocument.Load(modelPath);
        }

        public abstract CoreModel getModel();

        public abstract string tryFindCoreModelElementType(string id);

        protected void fillModelElementTypes()
        {
            foreach (KeyValuePair<string, string> pair in idToType)
            {
                CoreModelElement type;
                lookup.TryGetValue(pair.Value, out type);

                if (type == null)
                {
                    var newId = tryFindCoreModelElementType(pair.Value);
                    lookup.TryGetValue(newId, out type);
                }

                CoreModelElement modelElement;
                lookup.TryGetValue(pair.Key, out modelElement);
                
                if (modelElement != null)
                {
                    if (modelElement is CoreAssociationEndImpl)
                        ((CoreAssociationEnd)modelElement).setType((CoreClassifier)type);
                    else if (modelElement is CoreAttributeImpl)
                        ((CoreAttribute)modelElement).setFeatureType((CoreClassifier)type);
                    else if (modelElement is ParameterImpl)
                        ((Parameter)modelElement).setType((CoreClassifier)type);
                }
            }

            idToType.Clear();
        }

        protected ScopeKindEnum getScopeKind(string scopeKindString)
        {
            switch (scopeKindString)
            {
                case "instance":
                    return ScopeKindEnum.SK_INSTANCE;
                case "classifier":
                    return ScopeKindEnum.SK_CLASSIFIER;
                default:
                    return ScopeKindEnum.SK_INSTANCE;
            }
        }

        protected ParameterDirectionKind getParameterDirectionKind(string skind)
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

        protected void updateOperationParameters(CoreBehavioralFeature owner, Parameter newParameter)
        {
            List<Parameter> parameters = owner.getParameter();
            parameters.Add(newParameter);
            owner.setParameter(parameters);
        }

        protected void updateElemOwnedElements(CoreModelElement owner, CoreModelElement newOwnedElement)
        {
            List<object> ownedElements = (List<object>)owner.getElemOwnedElements();
            ownedElements.Add(newOwnedElement);
            owner.setElemOwnedElements(ownedElements);
        }

        protected void updateNamespaceElemOwnedElements(CoreNamespace coreNamespace, CoreModelElement newOwnedElement)
        {
            if (coreNamespace != null)
            {
                List<object> ownedElements = (List<object>)coreNamespace.getElemOwnedElements();
                ownedElements.Add(newOwnedElement);
                coreNamespace.setElemOwnedElements(ownedElements);
            }
        }

        protected void updateExtendedElements(CoreStereotype owner, CoreModelElement newExtendedElement)
        {
            List<object> extendedElements = (List<object>)owner.getExtendedElement();
            extendedElements.Add(newExtendedElement);
            owner.setExtendedElement(extendedElements);
        }

        protected void updateStereotypes(CoreModelElement owner, CoreStereotype newStereotype)
        {
            List<CoreStereotype> stereotypes = owner.getTheStereotypes();
            stereotypes.Add(newStereotype);
            owner.setTheStereotypes(stereotypes);
        }

        protected void updateQualifiers(CoreAssociationEnd owner, CoreAttribute newQualifier)
        {
            List<object> qualifiers = owner.getQualifier();
            qualifiers.Add(newQualifier);
            owner.setQualifier(qualifiers);
        }

        protected void updateRanges(Multiplicity owner, MultiplicityRange newRange)
        {
            List<object> ranges = owner.getRange();
            ranges.Add(newRange);
            owner.setRange(ranges);
        }

        protected void updateConnection(CoreAssociation owner, CoreAssociationEnd newConnection)
        {
            List<object> connections = owner.getConnection();
            connections.Add(newConnection);
            owner.setConnection(connections);
        }

        protected void updateClient(Dependency owner, CoreModelElement newConnection)
        {
            List<object> dependency = owner.getClient();
            dependency.Add(newConnection);
            owner.setClient(dependency);
        }

        protected void updateSupplier(Dependency owner, CoreModelElement newConnection)
        {
            List<object> supplierDependency = owner.getSupplier();
            supplierDependency.Add(newConnection);
            owner.setSupplier(supplierDependency);
        }

        protected void updateClientDependency(CoreInterface owner, Dependency newConnection)
        {
            List<object> dependency = owner.getClientDependency();
            dependency.Add(newConnection);
            owner.setClientDependency(dependency);
        }

        protected void updateSupplierDependency(CoreClassifier owner, Dependency newConnection)
        {
            List<Dependency> supplierDependency = owner.getSupplierDependency();
            supplierDependency.Add(newConnection);
            owner.setSupplierDependency(supplierDependency);
        }
        
        protected void updateChildGeneralizations(CoreClassifier owner, Generalization newGeneralization)
        {
            List<Generalization> generalizations = owner.getGeneralization();
            generalizations.Add(newGeneralization);
            owner.setGeneralization(generalizations);
        }

        protected void updateParentSpecializations(CoreClassifier owner, Generalization newSpecialization)
        {
            List<Generalization> specializations = owner.getSpecialization();
            specializations.Add(newSpecialization);
            owner.setSpecialization(specializations);
        }
    }
}
