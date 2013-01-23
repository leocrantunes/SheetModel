using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.impl.environment;
using Ocl20.library.impl.util;
using Ocl20.parser.semantics.types;
using CorePackage = Ocl20.library.iface.common.CorePackage;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.library.impl.common
{
    public class CoreModelImpl : CorePackageImpl, CoreModel
    {
        private Dictionary<string, CoreAssociation> associations = null;
        private	Object mainPackage;
        private Ocl20Package oclPackage;

        public CoreModelImpl()
        {
            associations = new Dictionary<string, CoreAssociation>();
            mainPackage = null;
            oclPackage = new Ocl20PackageImpl();
        }
	
        public CoreClassifier toOclType(CoreClassifier classifier) {
            CoreClassifier	oclType = getPrimitiveType(classifier);
            return	oclType == null ? classifier : oclType;
        }

        public bool isPrimitiveType(CoreModelElement element) {
            return	getPrimitiveType(element) != null;
        }

        private	CoreClassifier getPrimitiveType(CoreModelElement element) {
            return  (element.GetType() == typeof(CoreClassifierImpl))
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
                if (element.GetType() == typeof(CoreStereotypeImpl))
                    result.Add(element);
				
                if (element.GetType() == typeof(CorePackageImpl))
                    result.AddRange(getAllStereotypesOfNamespace((CoreNamespace) element));	
            }
		
            return	result;
        }

        public List<object> getAssociationClassesForClassifier(
            CoreClassifier classifier) {
            List<object> associationClassesCollection = new List<object>();
            List<object> associations = getAllAssociations();

            foreach (CoreAssociation association in associations) {
                if (association.GetType() == typeof(CoreAssociationClassImpl) &&
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

        public override List<object> getElementsForEnvironment()
        {
            throw new NotImplementedException();
        }

        public override void setDirty(bool value) {
            this.isDirty = value;
        }

        public override void populateEnvironment(Environment environment) {
            bool isFirstLevel = true;

            addAllClassifiersFromInnerPackages(isFirstLevel, this, environment);
        }

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
                else if (element.GetType() == typeof (CorePackageImpl))
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
            return	element.GetType() == typeof(CoreClassifierImpl) && !isPrimitiveType(element);
        }
    }
}
