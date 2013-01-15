using System.Collections.Generic;
using System.Xml.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;

namespace Ocl20.uml13.impl.foundation.core
{
    /*public abstract class AbstractClassifier : CoreClassifierImpl, Classifier
    {
        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getNamespace();
        }
		
        protected override bool getSpecificIsEnumeration() {
            return	hasStereotype("Enumeration");
        }

        public override List<object> getSpecificClassifierAncestors() {
            List<object> result = new List<object>();

            List<object> superClasses = getGeneralizations();
            foreach (Generalization generalization in superClasses) {
                result.Add((generalization.getParent()));
            }

            return	result;
        }
    
        public override List<object> getSpecificClassifierInterfaces() {
            List<object> result = new List<object>();
			
            foreach (Dependency realization in getClientDependency()) {
                foreach (object i in realization.getSupplier()) {
                    result.Add(i);
                }			
            }
			
            return result;
        }

        protected override List<object> getSpecificClassifierFeatures() {
            return	getFeature();
        }

        protected override bool getSpecificIsConcrete() {
            return	! isAbstract();
        }
        
        protected override List<object> getSpecificSubClasses() {
            List<object> result = new List<object>();

            List<object> subClasses = getSpecialization();
            foreach (Generalization generalization in subClasses) {
                result.Add(generalization.getChild());
            }

            return	result;
        }

        public abstract List<object> getFeature();
        public abstract List<object> getParticipant();
        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();
        public abstract List<object> getOwnedElement();
        public abstract bool isAbstract();
        public abstract List<object> getGeneralizations();
        public abstract List<object> getSpecialization();
    }*/
}
