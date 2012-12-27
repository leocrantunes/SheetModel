using System;
using System.Collections.Generic;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.impl.environment;
using Ocl20.library.impl.util;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.library.impl.common
{
    public abstract class CoreModelImpl : CorePackageImpl, CoreModel {
        private	Dictionary<string,CoreAssociation> associations = null;
        private	Object mainPackage;
        private Ocl20Package oclPackage;
	
        /* (non-Javadoc)
	 * @see ocl20.CoreModel#toOclType(ocl20.CoreClassifier)
	 */
        public CoreClassifier toOclType(CoreClassifier classifier) {
            CoreClassifier	oclType = getPrimitiveType(classifier);
            return	oclType == null ? classifier : oclType;
        }

        public abstract void populateEnvironment(EnvironmentImpl environment);

        public bool isPrimitiveType(CoreModelElement element) {
            return	getPrimitiveType(element) != null;
        }

        private	CoreClassifier getPrimitiveType(CoreModelElement element) {
            return  (element.GetType() == typeof(CoreClassifier))
                        ? (CoreClassifier) OclTypesDefinition.getEnvironment().lookup(element.getName())
                        : null;		
        }

        public   CoreStereotype  getStereotype(String stereotypeName) {
            List<object> allStereotypes = getAllStereotypes();
		
            foreach (CoreStereotype stereotype in allStereotypes){
                if (stereotype.getName().ToUpper().Equals(stereotypeName.ToUpper()))
                    return	stereotype;
            }
		
            return	null;
        }
	
        public List<object> getAllStereotypes() {
            return	getAllStereotypesOfNamespace(this);
        }
	
        protected List<object> getAllStereotypesOfNamespace(CoreNamespace ns) {
            List<object> result = new List<object>();
				
            foreach (CoreModelElement element in ns.getElemOwnedElements()) {
                if (element.GetType() == typeof(CoreStereotype))
                    result.Add(element);
				
                if (element.GetType() == typeof(CorePackage))
                    result.AddRange(getAllStereotypesOfNamespace((CoreNamespace) element));	
            }
		
            return	result;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreModel#getAssociationClassesForClassifier(ocl20.CoreClassifier)
	 */
        public List<object> getAssociationClassesForClassifier(
            CoreClassifier classifier) {
            List<object> associationClassesCollection = new List<object>();
            List<object> associations = getAllAssociations();

            foreach (CoreAssociation association in associations) {
                if (association.GetType() == typeof(CoreAssociationClass) &&
                    association.isClassifierInAssociation(classifier)) {
                        associationClassesCollection.Add(association);
                    }
            }

            return associationClassesCollection;
            }
	
	
        public List<object> getAssociationEndsForClassifier(CoreClassifier classifier) {
            List<object> associationEndsCollection = new List<object>();

            foreach (CoreAssociation association in getAllAssociations()) {
                if (association.isClassifierInAssociation(classifier) || association == classifier) {
                    associationEndsCollection.AddRange(association.getTheAssociationEnds(classifier));
                }
            }

            return associationEndsCollection;
        }

        public void addAssociation(CoreAssociation association) {
            associations.Add(association.getFullPathName(), association);
        }	

        public CoreAssociation	getAssociationForName(string associationName) {
            if (associations == null) {
                associations = new Dictionary<string, CoreAssociation>();
                foreach (CoreAssociation association in getAllAssociations()) {
                    this.addAssociation(association);
                }    
            }
            CoreAssociation coreAssociation;
            associations.TryGetValue(associationName, out coreAssociation);
            return coreAssociation;
        }
    
        public void setMainPackage(object mainPackage) {
            this.associations = null;
            this.mainPackage = mainPackage;
            this.setDirty(true);
        }
    
        public object getMainPackage() {
            return	this.mainPackage;
        }
    
        /* (non-Javadoc)
	 * @see ocl20.common.CoreModel#setOclPackage(ocl20.Ocl20Package)
	 */
        public void setOclPackage(Ocl20Package oclPackage) {
            this.associations = null;
            this.oclPackage = oclPackage;
            AstOclModelElementFactoryManager.getInstance(oclPackage)
                                            .resetTypes();
            this.setDirty(true);
        }
    
        public Ocl20Package getOclPackage() {
            return	this.oclPackage;
    	
        }
	
        public override Environment getEnvironmentWithoutParents() {
            if (getIsDirty() || this.environmentWithoutParents == null) {
                try {
                    this.environmentWithoutParents = EnvironmentFactoryManager.getInstance(this.getOclPackage()).getEnvironmentInstance();
                    this.environmentWithoutParents = this.environmentWithoutParents.addEnvironment(OclTypesDefinition.getEnvironment());

                    populateEnvironment(this.environmentWithoutParents);
                    isDirty = false;
                } catch (NameClashException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }

            return this.environmentWithoutParents;
        }

        public override void setDirty(bool value) {
            this.isDirty = value;
    	
        }

        public override void populateEnvironment(Environment environment) {
            bool isFirstLevel = true;

            addAllClassifiersFromInnerPackages(isFirstLevel, this, environment);
        }

        /* (non-Javadoc)
	 * @see implocl20.CorePackageImpl#elementShouldBeAddedToEnvironment(ocl20.CoreModelElement)
	 */
        protected override bool elementShouldBeAddedToEnvironment(CoreModelElement element) {
            return base.elementShouldBeAddedToEnvironment(element) && 
                   ! isPrimitiveType(element);
        }

        protected void addAllClassifiersFromInnerPackages(
            bool firstLevel,
            CorePackage aPackage,
            Environment environment)
        {
            foreach (CoreModelElement element in aPackage.getElemOwnedElements())
            {
                if (element.getName() == null)
                {
                    continue;
                }

                if (isClassifierToBeAdded(element))
                {
                    addElementToEnvironment(element.getName(), element, environment);
                }
                else if (element.GetType() == typeof (CorePackage))
                {
                    if (firstLevel)
                    {
                        addElementToEnvironment(element.getName(), element, environment);
                    }

                    bool notFirstLevel = true;
                    addAllClassifiersFromInnerPackages(notFirstLevel, (CorePackage) element, environment);
                }
            }
        }

        protected bool isClassifierToBeAdded(CoreModelElement element) {
            return	element.GetType() == typeof(CoreClassifier) && !isPrimitiveType(element);
        }
    }
}
