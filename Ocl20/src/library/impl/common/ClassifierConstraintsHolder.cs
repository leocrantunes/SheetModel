using System.Collections;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.environment;
using Ocl20.library.impl.constraints;
using Ocl20.library.impl.environment;

namespace Ocl20.library.impl.common
{
    public class ClassifierConstraintsHolder {

        private	CoreClassifier classifier;
	
        public ClassifierConstraintsHolder(CoreClassifier classifier) {
            this.classifier = classifier;
        }

        private	List<object> createdFeatures = new List<object>();
	
        private Dictionary<string, OclConstraint> initConstraints = new Dictionary<string, OclConstraint>();
        private Dictionary<string, OclConstraint> deriveConstraints = new Dictionary<string, OclConstraint>();
        private Dictionary<string, OclConstraint> namedInvariantConstraints = new Dictionary<string, OclConstraint>();
        private List<object> annonymousInvariantConstraints = new List<object>();
        private Dictionary<string, CoreFeature> definedFeatures = new Dictionary<string, CoreFeature>();
        private Dictionary<string, List<CoreFeature>> definedFeaturesBySource = new Dictionary<string, List<CoreFeature>>();

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#addInitConstraint(java.lang.String, br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.constraints.OCLConstraint)
     */
        public void addInitConstraint(
            string elementName,
            OclInitConstraint constraint) {
            this.initConstraints.Add(elementName, constraint);
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getInitOrDerivedConstraint(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement)
     */
        public OclInitConstraint getInitConstraint(string elementName) {
            OclInitConstraint result = this.getLocalInitConstraint(elementName);

            if (result == null) {
                List<object> allSuperTypes = classifier.getAllAncestors();

                foreach (CoreClassifier superType in allSuperTypes)
                {
                    result = superType.getLocalInitConstraint(elementName);
                    if (result != null) break;
                }
            }

            return result;
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getLocalInitConstraint(java.lang.String)
     */
        public OclInitConstraint getLocalInitConstraint(string elementName)
        {
            OclConstraint init;
            initConstraints.TryGetValue(elementName, out init);
            return (OclInitConstraint) init;
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#addDeriveConstraint(java.lang.String, br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.constraints.OCLConstraint)
     */
        public void addDeriveConstraint(
            string elementName,
            OclDeriveConstraint constraint) {
            this.deriveConstraints.Add(elementName, constraint);
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getInitOrDerivedConstraint(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement)
     */
        public OclDeriveConstraint getDeriveConstraint(string elementName) {
            OclConstraint result;
            this.deriveConstraints.TryGetValue(elementName, out result);

            if (result == null) {
                List<object> allSuperTypes = classifier.getAllAncestors();

                foreach (CoreClassifier superType in allSuperTypes)
                {
                    result = superType.getLocalDeriveConstraint(elementName);
                    if (result != null) break;
                }
            }

            return (OclDeriveConstraint) result;
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getLocalDeriveConstraint(java.lang.String)
     */
        public OclDeriveConstraint getLocalDeriveConstraint(string elementName)
        {
            OclConstraint derive;
            deriveConstraints.TryGetValue(elementName, out derive);
            return (OclDeriveConstraint) derive;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#addInvariantConstraint(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreExpression)
     */

        public void addInvariantConstraint(
            string name,
            OclInvariantConstraint invariant)
        {
            if (name != null)
            {
                this.namedInvariantConstraints.Add(name, invariant);
            }
            else
            {
                this.annonymousInvariantConstraints.Add(invariant);
            }
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getAllInvariants()
     */
        public List<object> getAllInvariants() {
            List<object> allInvariants = new List<object>();
        
            allInvariants.AddRange(namedInvariantConstraints.Values);
            allInvariants.AddRange(annonymousInvariantConstraints);

            return allInvariants;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getInvariant(java.lang.String)
     */
        public OclInvariantConstraint getInvariant(string name)
        {
            OclConstraint invariant;
            namedInvariantConstraints.TryGetValue(name, out invariant);
            return (OclInvariantConstraint) invariant;
        }

        //	private void getAllInvariants(Map allInvariants) {
        //		InvariantsCollector collector = new InvariantsCollector(this.getJmiClassifier(), allInvariants);
        //		collector.traverseAllAncestors();
        //	}
        public CoreModelElement addDefinedElement(
            string source,
            string name,
            CoreClassifier type) {
    	
            List<object> allSubClasses = classifier.getAllSubClasses();
            foreach (CoreClassifier	subClass in allSubClasses) {
                Environment env = subClass.getEnvironmentWithoutParents();
    		
                if (env.lookup(name) != null) {
                    string errorMessage = "<{0}> already defined in a derived classifier";
                    throw new NameClashException(string.Format(errorMessage, new object[] { name }));
                }
            }
    	
            CoreFeature element = createOclDefinedAttribute(source, name, type);
            element.setFeatureOwner(classifier);
            element.setElemOwner(classifier);
        
            this.definedFeatures.Add(name, element);
            addElementToDefinedFeaturesBySource(source, element);

            return element;
            }

        private void addElementToDefinedFeaturesBySource(string source, CoreFeature element) {
            List<CoreFeature> targetList; 
            this.definedFeaturesBySource.TryGetValue(source, out targetList);
            if (targetList == null) {
                targetList = new List<CoreFeature>();
            }
            targetList.Add(element);
            this.definedFeaturesBySource.Add(source, targetList);
        }
    
    
        public CoreModelElement addDefinedOperation(
            string source,
            string name,
            List<object> paramNames,
            List<object> paramTypes,
            CoreClassifier returnType) {
            CoreFeature element = createOclDefinedOperation(source, name, paramNames, paramTypes, returnType);
            element.setFeatureOwner(classifier);
            element.setElemOwner(classifier);
            ModelElementNameGenerator nameGenerator = CoreModelElementNameGeneratorImpl.getInstance();
        
            string mangledName = nameGenerator.generateNameForOperation(name, paramTypes);
        
            this.definedFeatures.Add(mangledName, element);
            addElementToDefinedFeaturesBySource(source, element);

            return element;
            }

        public int deleteAllConstraintsForSource(string sourceName) {
            deleteAllConstraintsForHashMap(sourceName, this.initConstraints);
            deleteAllConstraintsForHashMap(sourceName, this.deriveConstraints);
            deleteAllConstraintsForHashMap(sourceName, this.namedInvariantConstraints);
            deleteAllConstraintsForList(sourceName, this.annonymousInvariantConstraints);
            return deleteAllDefinedElements(sourceName);
        }

        private void deleteAllConstraintsForHashMap(
            string sourceName,
            Dictionary<string, OclConstraint> map) {
            List<object> toBeRemoved = new List<object>();
        
            foreach (KeyValuePair<string, OclConstraint> element in map) {
                OclConstraint constraint = element.Value;

                if (constraint.getSource().Equals(sourceName)) {
                    toBeRemoved.Add(element.Key);
                }
            }

            foreach (string element in toBeRemoved) {
                map.Remove(element);
            }
            }

        private void deleteAllConstraintsForList(
            string sourceName,
            List<object> list)
        {
            list.RemoveAll(constraint => ((OclConstraint) constraint).getSource().Equals(sourceName));
        }

        private int deleteAllDefinedElements(string sourceName) {
            List<object> namesToBeRemovedFromEnvironment = new List<object>();
            int result = 0;

            List<CoreFeature> elementsToBeRemoved;
            this.definedFeaturesBySource.TryGetValue(sourceName, out elementsToBeRemoved);
            if (elementsToBeRemoved != null) {
                foreach(CoreFeature modelElement in elementsToBeRemoved) {
                    if (modelElement.GetType() == typeof(CoreAttributeImpl)) {
                        CoreAttribute attr = (CoreAttribute) modelElement;
                        namesToBeRemovedFromEnvironment.Add(attr.getName());
                    } else {
                        CoreOperation oper = (CoreOperation) modelElement;
                        namesToBeRemovedFromEnvironment.Add(
                            CoreModelElementNameGeneratorImpl.getInstance().generateNameForOperation(oper.getName(), 
                            new List<object>(oper.getParametersTypesExceptReturn())));
                    }
                }

                result = elementsToBeRemoved.Count;
            
                foreach (string element in namesToBeRemovedFromEnvironment)
                {
                    CoreFeature coreFeature;
                    bool foudbefore = this.definedFeatures.TryGetValue(element, out coreFeature);
                    this.definedFeatures.Remove(element);
                    bool foundafter = this.definedFeatures.TryGetValue(element, out coreFeature);
                }
            
                this.definedFeaturesBySource.Add(sourceName, new List<CoreFeature>());
            }
        
            return result;	
        }

        public List<object> getDefinedFeatures() {
            return new List<object>(definedFeatures.Values);
        }
    
        public CoreAttribute createOclDefinedAttribute(string source, string name, CoreClassifier type) {
            CoreAttribute oclDefinedAttribute = classifier.getModel().getOclPackage().getConstraints().getOclDefinedAttribute().createOclDefinedAttribute();
    	
            oclDefinedAttribute.setFeatureOwner(classifier);
            oclDefinedAttribute.setFeatureType(type);
            oclDefinedAttribute.setName(name);
            ((OclDefinedAttributeImpl) oclDefinedAttribute).setSource(source);
    
            createdFeatures.Add(oclDefinedAttribute);
    	
            return	oclDefinedAttribute;
        } 

        public CoreOperation createOclDefinedOperation(string source, string name, List<object> paramNames, List<object> paramTypes, CoreClassifier returnType) {
            CoreOperation oclDefinedOperation = classifier.getModel().getOclPackage().getConstraints().getOclDefinedOperation().createOclDefinedOperation();
    	
            ((OclDefinedOperationImpl) oclDefinedOperation).setName(name);
            ((OclDefinedOperationImpl) oclDefinedOperation).setParamNames(paramNames);
            ((OclDefinedOperationImpl) oclDefinedOperation).setParamTypes(paramTypes);
            ((OclDefinedOperationImpl) oclDefinedOperation).setReturnType(returnType);
            ((OclDefinedOperationImpl) oclDefinedOperation).setSource(source);
    	
            createdFeatures.Add(oclDefinedOperation);
    	
            return	oclDefinedOperation;
        }

    }
}
