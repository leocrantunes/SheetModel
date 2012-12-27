using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OclLibrary.iface.common;
using OclLibrary.iface.constraints;
using OclLibrary.impl.environment;
using OclLibrary.utils;
using Environment = OclLibrary.iface.environment.Environment;

namespace OclLibrary.impl.common
{
    public abstract class CoreClassifierImpl : CoreNamespaceImpl, CoreClassifier, CoreEnumeration
    {
        private Dictionary<String, CoreModelElement> featuresMap = null;
        private ClassifierConstraintsHolder constraintsHolder;
        private static String OCLANY = "OclAny";
        private static String OCLVOID = "OclVoid";
        private static String OCLINVALID = "OclInvalid";
        private Environment envWithoutAncestors = null;

        protected CoreClassifierImpl()
        {
            constraintsHolder = new ClassifierConstraintsHolder(this);
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#isEnumeration()
	 */

        public virtual bool isEnumeration()
        {
            return getSpecificIsEnumeration();
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreEnumeration#lookupEnumLiteral(java.lang.String)
	 */

        public CoreEnumLiteral lookupEnumLiteral(String name)
        {
            foreach (CoreEnumLiteral literal in getTheLiterals())
            {
                if (literal.getName() != null && literal.getName().Equals(name))
                    return literal;
            }
            return null;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreEnumeration#getTheLiterals()
	 */

        public virtual List<object> getTheLiterals()
        {
            if (isEnumeration())
            {
                List<object> result = new List<object>();
                result.AddRange(getAllAttributes());
                return result;
            }
            else
            {
                return new List<object>();
            }
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#conformsTo(ocl20.CoreClassifier)
	 */

        public virtual bool conformsTo(CoreClassifier c)
        {
            CoreClassifier thisClassifier = getModel() != null ? getModel().toOclType(this) : this;
            CoreClassifier theOtherClassifier = c.getModel() != null ? (CoreClassifier) c.getModel().toOclType(c) : c;

            return ((thisClassifier == theOtherClassifier) ||
                    (thisClassifier.getFullPathName().Equals(theOtherClassifier.getFullPathName())) ||
                    (OCLANY.Equals(theOtherClassifier.getName())) ||
                    (OCLVOID.Equals(thisClassifier.getName())) ||
                    (OCLINVALID.Equals(thisClassifier.getName())) ||
                    (isClassifierDescendantOf(theOtherClassifier)) ||
                    (classifierRealizesInterface(theOtherClassifier)));
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllAttributes()
	 */

        public virtual ICollection<object> getAllAttributes()
        {
//		List result = new ArrayList();

            HashSet<object> result = new HashSet<object>();
            Environment env = this.getEnvironmentWithoutAncestors();
            result.AddRange(env.getAllOfType(typeof (CoreAttribute)));
            return result;

//		for (Iterator iter = getClassifierFeatures().iterator(); iter.hasNext();) {
//			CoreModelElement element = (CoreModelElement) iter.next();
//			if (element instanceof CoreAttribute)
//				result.add(element);
//		}
//        return result;
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllAttributes()
	 */

        public ICollection<object> getAllAttributesTransitiveClosure()
        {
//		List result = new ArrayList();

            HashSet<object> result = new HashSet<object>();
            Environment env = this.getEnvironmentWithParents();
            result.AddRange(env.getAllOfType(typeof (CoreAttribute)));
            return result;

//		for (Iterator iter = getClassifierFeatures().iterator(); iter.hasNext();) {
//			CoreModelElement element = (CoreModelElement) iter.next();
//			if (element instanceof CoreAttribute)
//				result.add(element);
//		}
//        return result;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllAttributes()
	 */

        public ICollection<object> getAllAssociationEndsTransitiveClosure()
        {
//		List result = new ArrayList();

            HashSet<object> result = new HashSet<object>();
            Environment env = this.getEnvironmentWithParents();
            result.AddRange(env.getAllOfType(typeof (CoreAssociationEnd)));
            return result;

//		for (Iterator iter = getClassifierFeatures().iterator(); iter.hasNext();) {
//			CoreModelElement element = (CoreModelElement) iter.next();
//			if (element instanceof CoreAttribute)
//				result.add(element);
//		}
//        return result;
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllClassifierScopeAttributes()
	 */

        public List<object> getAllClassifierScopeAttributes()
        {
            List<object> result = new List<object>();

            foreach (CoreAttribute attribute in getAllAttributes())
            {
                if (! attribute.isInstanceScope() && (attribute.getFeatureOwner() == this))
                {
                    result.Add(attribute);
                }
            }

            return result;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getClassifierFeatures()
	 */

        public abstract List<object> getConstraint();

        public List<object> getClassifierFeatures()
        {
            return adjustCollectionResult(getSpecificClassifierFeatures());
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllAssociationClasses()
	 */

        public List<object> getAllAssociationClasses()
        {
            CoreModel model = getModel();
            return model.getAssociationClassesForClassifier(this);
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllAssociationEnds()
	 */

        public List<object> getAllAssociationEnds()
        {
            CoreModel model = getModel();
            return model.getAssociationEndsForClassifier(this);
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllAncestors()
	 */

        public List<object> getAllAncestors()
        {
            return (new AncestorsCollector()).getAllAncestors(this);
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllDirectSubClasses()
	 */

        public List<object> getAllDirectSubClasses()
        {
            List<object> result = new List<object>();

            List<object> allSubClasses = adjustCollectionResult(getSpecificSubClasses());

            foreach (CoreClassifier subClass in allSubClasses)
            {
                if ((subClass != null) && !subClass.getName().Equals(this.getName()))
                {
                    result.Add(subClass);
                }
            }

            return result;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllSubClasses()
	 */

        public List<object> getAllSubClasses()
        {
            List<object> result = new List<object>();

            List<object> directSubClasses = this.getAllDirectSubClasses();
            result.AddRange(directSubClasses);

            foreach (CoreClassifier cls in directSubClasses)
            {
                result.AddRange(cls.getAllSubClasses());
            }

            return result;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllInterfaces()
	 */

        public List<object> getAllInterfaces()
        {
            return adjustCollectionResult(getSpecificClassifierInterfaces());
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getAllImplementedInterfaces()
	 */

        public List<object> getAllImplementedInterfaces()
        {
            return (new ImplementedInterfacesCollector()).getAllImplementedInterfaces(
                this, this.getAllAncestors());
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getFullPathName()
	 */

        public String getFullPathName()
        {
            StringBuilder result = new StringBuilder();
            CoreNamespace ns = (CoreNamespace) getElemOwner();

            while ((ns != null) && !(ns.GetType() == typeof (CoreModel)))
            {
                result.Insert(0, ns.getName() + "::");
                ns = (CoreNamespace) ns.getElemOwner();
            }

            result.Append(getName());

            return result.ToString();
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#getMostSpecificCommonSuperType(ocl20.CoreClassifier)
	 */

        public virtual CoreClassifier getMostSpecificCommonSuperType(
            CoreClassifier otherClassifier)
        {
            List<object> myAncestors = this.getAllAncestors();
            List<object> otherAncestors = otherClassifier.getAllAncestors();

            if ((this == otherClassifier) || otherAncestors.Contains(this) ||
                OCLVOID.Equals(otherClassifier.getName()) ||
                OCLINVALID.Equals(otherClassifier.getName()))
            {
                return this;
            }
            else if (myAncestors.Contains(otherClassifier))
            {
                return otherClassifier;
            }
            else if (OCLVOID.Equals(this.getName()) ||
                     OCLINVALID.Equals(this.getName()))
            {
                return otherClassifier;
            }
            else
            {
                foreach (CoreClassifier aSuperClass in otherAncestors)
                {
                    if (myAncestors.Contains(aSuperClass) || (aSuperClass == this))
                    {
                        return aSuperClass;
                    }
                }
            }

            return (CoreClassifier) getGlobalEnvironment().lookup(OCLANY);
        }

        /* (non-Javadoc)
	 * @see implocl20.CoreModelElementImpl#getElemOwnedElements()
	 */

        public override ICollection<object> getElemOwnedElements()
        {
            HashSet<object> resultAttr = new HashSet<object>();
            HashSet<object> resultOper = new HashSet<object>();
            HashSet<object> resultAssocEnd = new HashSet<object>();

            Environment env = this.getEnvironmentWithoutAncestors();

            resultAttr.AddRange(env.getAllOfType(typeof (CoreAttribute)));
            resultOper.AddRange(env.getAllOfType(typeof (CoreOperation)));
            resultAssocEnd.AddRange(env.getAllOfType(typeof (CoreAssociationEnd)));

            List<object> allOwnedElements = new List<object>();

            allOwnedElements.AddRange(resultAttr);
            allOwnedElements.AddRange(resultOper);
            allOwnedElements.AddRange(resultAssocEnd);

            allOwnedElements.AddRange(this.getAllInvariants());

            return allOwnedElements;
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#isConcrete()
	 */

        public virtual bool isConcrete()
        {
            return getSpecificIsConcrete();
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#lookupAttribute(java.lang.String)
	 */

        public virtual CoreAttribute lookupAttribute(String name)
        {
            return (CoreAttribute) this.lookupFeature(name, typeof (CoreAttribute));
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#lookupAssociationClass(java.lang.String)
	 */

        public CoreAssociationClass lookupAssociationClass(String name)
        {
            return (CoreAssociationClass) this.lookupFeature(name, typeof (CoreAssociationClass));
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#lookupAssociationEnd(java.lang.String)
	 */

        public CoreAssociationEnd lookupAssociationEnd(String name)
        {
            CoreAssociationEnd result;
            result = (CoreAssociationEnd) this.lookupFeature(name, typeof (CoreAssociationEnd));
            if (result == null)
            {
                StringBuilder upperCaseName = new StringBuilder(name);
                char firstChar = name.Substring(0, 1).ToUpper()[0];
                upperCaseName.Insert(0, firstChar);
                result = (CoreAssociationEnd) this.lookupFeature(upperCaseName.ToString(), typeof (CoreAssociationEnd));
            }
            return result;
        }

        private CoreModelElement lookupFeature(
            String name,
            Type desiredType)
        {
            Environment env = this.getEnvironmentWithoutParents();

            Object element = env.lookupLocal(name);

            if ((element != null) && desiredType.IsInstanceOfType(element))
            {
                return (CoreModelElement) element;
            }
            else
            {
                return null;
            }
        }



        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#lookupOperation(java.lang.String, java.util.Collection)
	 */

        public virtual CoreOperation lookupOperation(String name, List<object> paramTypes)
        {
            Environment env = this.getEnvironmentWithoutParents();
            List<object> operations = env.lookupOperationLocal(name);

            if (operations != null)
            {
                List<object> matchingOperations = new List<object>();

                foreach (CoreOperation operation in operations)
                {
                    if (operation.hasMatchingSignature(paramTypes))
                    {
                        matchingOperations.Add(operation);
                    }
                }

                if (matchingOperations.Count == 1)
                {
                    return (CoreOperation) matchingOperations[0];
                }
                else
                {
                    CoreOperation result = operationOwnerMatchingDesiredOwner(matchingOperations,
                                                                              this);

                    if (result == null)
                    {
                        List<object> ancestors = getAllAncestors();

                        for (IEnumerator iter = ancestors.GetEnumerator(); iter.MoveNext() && (result == null);)
                        {
                            CoreClassifier ancestor = (CoreClassifier) iter.Current;
                            result = operationOwnerMatchingDesiredOwner(matchingOperations, ancestor);
                        }
                    }

                    return result;
                }
            }

            return null;
        }


        /* (non-Javadoc)
	 * @see ocl20.CoreClassifier#lookupSameSignatureOperation(java.lang.String, java.util.Collection, ocl20.CoreClassifier)
	 */

        public CoreOperation lookupSameSignatureOperation(String name, List<object> paramTypes, CoreClassifier returnType)
        {
            Environment env = this.getEnvironmentWithoutAncestors();
            List<object> operations = env.lookupOperationLocal(name);

            if (operations != null)
            {
                foreach (CoreOperation operation in operations)
                {
                    if (operation.hasSameSignature(paramTypes, returnType))
                    {
                        return operation;
                    }
                }
            }

            return null;
        }

        public bool isClassifierDescendantOf(CoreClassifier superClass)
        {
            if (superClass != null)
            {
                // recursively try to find superclass in all direct and indirect ancestors 
                foreach (CoreClassifier ancestor in getSpecificClassifierAncestors())
                {
                    if ((ancestor.Equals(superClass)) || (ancestor.isClassifierDescendantOf(superClass)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private CoreOperation operationOwnerMatchingDesiredOwner(
            List<object> matchingOperations,
            CoreClassifier desiredOwner)
        {
            for (int i = 0; i < matchingOperations.Count; i++)
            {
                CoreOperation oper = (CoreOperation) matchingOperations[i];
                if (oper.getElemOwner() == desiredOwner)
                {
                    return oper;
                }
            }

            return null;
        }



        public override void populateEnvironment(Environment environment)
        {
            populateWithFeatures(environment);
            populateWithAssociationEnds(environment);
            populateWithAssociationClasses(environment);
            populateWithDefinedFeatures(environment);
            populateWithAncestorsFeatures(this, environment);
            populateWithOclAnyFeatures(environment);
        }

        private void populateWithFeatures(Environment environment)
        {
            foreach (CoreModelElement element in getClassifierFeatures())
            {
                if (elementShouldBeAddedToEnvironment(element))
                {
                    ModelElementNameGenerator nameGenerator = new CoreModelElementNameGeneratorImpl();
                    addElementToEnvironment(nameGenerator.generateName(element), element, environment);
                }
            }
        }

        protected override bool elementShouldBeAddedToEnvironment(CoreModelElement element)
        {
            return (element.GetType() == typeof (CoreAttribute) || element.GetType() == typeof (CoreOperation));
        }

        private void populateWithAssociationEnds(Environment environment)
        {
            List<object> associationEnds = this.getAllAssociationEnds();

            foreach (CoreAssociationEnd associationEnd in associationEnds)
            {
                ModelElementNameGenerator nameGenerator = new CoreModelElementNameGeneratorImpl();
                addElementToEnvironment(nameGenerator.generateName(associationEnd), associationEnd, environment);
            }
        }

        private void populateWithAssociationClasses(Environment environment)
        {
            List<object> associationClasses = this.getAllAssociationClasses();

            foreach (CoreAssociationClass associationClass in associationClasses)
            {
                ModelElementNameGenerator nameGenerator = new CoreModelElementNameGeneratorImpl();
                addElementToEnvironment(nameGenerator.generateName(associationClass), associationClass, environment);
            }
        }

        private void populateWithAncestorsFeatures(CoreClassifier classifier, Environment environment)
        {
            AncestorsPopulateEnvironmentStrategy visitor = new AncestorsPopulateEnvironmentStrategy(classifier, environment);
            visitor.traverseAllAncestors();
        }

        private void populateWithDefinedFeatures(Environment environment)
        {
            foreach (CoreFeature feature in constraintsHolder.getDefinedFeatures())
            {
                ModelElementNameGenerator nameGenerator = new CoreModelElementNameGeneratorImpl();
                addElementToEnvironment(nameGenerator.generateName(feature), feature, environment);
            }
        }

        private void populateWithOclAnyFeatures(Environment environment)
        {
            if (! OCLANY.Equals(this.getName()))
            {
                CoreClassifier oclAnyType = (CoreClassifier) getGlobalEnvironment().lookup(OCLANY);

                if (oclAnyType != null)
                {
                    environment.addNamespace(oclAnyType);
                }
            }
        }


        protected override void resetEnvironments()
        {
            this.envWithoutAncestors = null;
            base.resetEnvironments();
        }

        private Environment getEnvironmentWithoutAncestors()
        {
            if (isDirty || envWithoutAncestors == null)
            {
                this.envWithoutAncestors =
                    EnvironmentFactoryManager.getInstance(this.getModel().getOclPackage()).getEnvironmentInstance();

                populateWithFeatures(envWithoutAncestors);
                populateWithAssociationEnds(envWithoutAncestors);
                populateWithAssociationClasses(envWithoutAncestors);
                populateWithDefinedFeatures(envWithoutAncestors);

                if (isDirty)
                {
                    base.resetEnvironments();
                }
                isDirty = false;
            }

            return envWithoutAncestors;
        }


        protected bool classifierRealizesInterface(
            CoreClassifier umlInterface)
        {
            if ((umlInterface != null))
            {
                return getAllImplementedInterfaces().Contains(umlInterface);
            }
            else
            {
                return false;
            }
        }

        public void addInitConstraint(
            String elementName,
            OclInitConstraint constraint)
        {
            constraintsHolder.addInitConstraint(elementName, constraint);
        }

        /* (non-Javadoc)
         * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getInitOrDerivedConstraint(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement)
         */

        public OclInitConstraint getInitConstraint(String elementName)
        {
            return constraintsHolder.getInitConstraint(elementName);
        }

        /* (non-Javadoc)
         * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getLocalInitConstraint(java.lang.String)
         */

        public OclInitConstraint getLocalInitConstraint(String elementName)
        {
            return constraintsHolder.getLocalInitConstraint(elementName);
        }

        /* (non-Javadoc)
         * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#addDeriveConstraint(java.lang.String, br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.constraints.OCLConstraint)
         */

        public void addDeriveConstraint(
            String elementName,
            OclDeriveConstraint constraint)
        {
            constraintsHolder.addDeriveConstraint(elementName, constraint);
        }


        /* (non-Javadoc)
         * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getInitOrDerivedConstraint(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement)
         */

        public OclDeriveConstraint getDeriveConstraint(String elementName)
        {
            return constraintsHolder.getDeriveConstraint(elementName);
        }

        public OclDeriveConstraint getLocalDeriveConstraint(String elementName)
        {
            return constraintsHolder.getLocalDeriveConstraint(elementName);
        }

        public void addInvariantConstraint(
            String name,
            OclInvariantConstraint invariant)
        {
            constraintsHolder.addInvariantConstraint(name, invariant);
        }

        public List<object> getAllInvariants()
        {
            return constraintsHolder.getAllInvariants();
        }

        public List<object> getAllInvariantsTransitiveClosure()
        {
            List<object> result = new List<object>();
            result.AddRange(constraintsHolder.getAllInvariants());
            foreach (CoreClassifier ancestor in this.getAllAncestors())
            {
                List<object> invariants = ancestor.getAllInvariants();
                if (invariants != null)
                {
                    result.AddRange(invariants);
                }
            }
            return result;
        }

        public OclInvariantConstraint getInvariant(String name)
        {
            return constraintsHolder.getInvariant(name);
        }

        public CoreAttribute addDefinedElement(
            String source,
            String name,
            CoreClassifier type)
        {
            CoreModelElement element = constraintsHolder.addDefinedElement(source, name, type);
            setDirty(true);
            return (CoreAttribute) element;
        }

        public CoreOperation addDefinedOperation(
            String source,
            String name,
            List<object> paramNames,
            List<object> paramTypes,
            CoreClassifier returnType)
        {

            CoreModelElement element = constraintsHolder.addDefinedOperation(source, name, new List<object>(paramNames),
                                                                             new List<object>(paramTypes), returnType);
            setDirty(true);
            return (CoreOperation) element;
        }

        public void deleteAllConstraintsForSource(String sourceName)
        {
            if (constraintsHolder.deleteAllConstraintsForSource(sourceName) > 0)
            {
                setDirty(true);
            }
        }


        public override void setDirty(bool dirty)
        {
            foreach (CoreClassifier cls in getAllDirectSubClasses())
            {
                cls.setDirty(true);
            }
            base.setDirty(true);
        }

        protected bool getSpecificIsEnumeration()
        {
            return false;
        }

        public List<object> getSpecificClassifierAncestors()
        {
            return new List<object>();
        }

        public List<object> getSpecificClassifierInterfaces()
        {
            return new List<object>();
        }

        protected List<object> getSpecificClassifierFeatures()
        {
            return new List<object>();
        }

        protected bool getSpecificIsConcrete()
        {
            return false;
        }

        protected List<object> getSpecificSubClasses()
        {
            return new List<object>();
        }

        public virtual List<object> getSpecificAssociationEnds()
        {
            return new List<object>();
        }

    }

    internal abstract class AncestorsTraversalStrategy
    {
        private CoreClassifier cls = null;

        protected AncestorsTraversalStrategy(CoreClassifier cls)
        {
            this.cls = cls;
        }

        public abstract void doSpecificTask(CoreClassifier superClass);

        public void traverseAllAncestors()
        {
            List<object> superClasses = ((CoreClassifierImpl) cls).getSpecificClassifierAncestors();

            foreach (CoreClassifier superClass in superClasses)
            {
                if (superClass != null)
                {
                    doSpecificTask(superClass);
                }
            }
        }
    }

    class AncestorsPopulateEnvironmentStrategy : AncestorsTraversalStrategy
    {
        private Environment env = null;

        public AncestorsPopulateEnvironmentStrategy(CoreClassifier cls, Environment env) : base(cls)
        {
            this.env = env;
        }

        public override void doSpecificTask(CoreClassifier superClass)
        {
            ((CoreClassifierImpl) superClass).populateEnvironment(env);
        }
    }

    class AncestorsCollector {
        public List<object> getAllAncestors(
            CoreClassifier cls) {
            List<object> allAncestors = new List<object>();
            List<object> ancestors = ((CoreClassifierImpl) cls).getSpecificClassifierAncestors();

            allAncestors.AddRange(ancestors);
            
            foreach (CoreClassifier superClass in ancestors) {
                allAncestors.AddRange(superClass.getAllAncestors());
            }

            return allAncestors;
            }
    }

    internal class ImplementedInterfacesCollector
    {
        public List<object> getAllImplementedInterfaces(
            CoreClassifier cls,
            List<object> allAncestors)
        {

            HashSet<object> allInterfaces = new HashSet<object>();
            List<object> interfaces = ((CoreClassifierImpl) cls).getSpecificClassifierInterfaces();

            // add all interfaces implemented by this classifier
            foreach (CoreClassifier interfaceClassifier in interfaces)
            {
                allInterfaces.Add(interfaceClassifier);
                allInterfaces.AddRange(interfaceClassifier.getAllAncestors());
            }

            //	add all interfaces implemented by the ancestors of this classifier
            foreach (CoreClassifier ancestor in allAncestors)
            {
                allInterfaces.AddRange(ancestor.getAllImplementedInterfaces());
            }

            return new List<object>(allInterfaces);
        }
    }
}