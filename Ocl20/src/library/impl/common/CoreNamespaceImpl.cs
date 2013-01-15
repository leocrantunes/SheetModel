using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.environment;
using Ocl20.parser.semantics.types;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.library.impl.common
{
    public class CoreNamespaceImpl : CoreModelElementImpl, CoreNamespace {
        protected Environment environmentWithParents;
        protected Environment environmentWithoutParents;
        protected bool	isDirty = false;
	
        public List<object> getAllAssociations() {
            List<object> associations = new List<object>();
		
            foreach (CoreModelElement element in getElemOwnedElements()) {
                if (element.GetType() == typeof(CoreAssociation)) {
                    associations.Add(element);
                }
			
                if (element.GetType() ==  typeof(CorePackage)) {
                    associations.AddRange( ((CoreNamespace) element).getAllAssociations());
                }
            }
		
            return	associations;
        }

        public Environment getGlobalEnvironment() {
            return	this.getModel().getEnvironmentWithoutParents();
        }
    
        protected virtual void resetEnvironments() {
            this.environmentWithoutParents = null;
            this.environmentWithParents = null;
        }

        public Environment getEnvironmentWithParents() {
            if (isDirty || this.environmentWithParents == null) {
                try {
                    this.environmentWithParents = EnvironmentFactoryManager.getInstance(this.getModel().getOclPackage()).getEnvironmentInstance(); 
                    this.environmentWithParents = this.environmentWithParents.addEnvironment(getEnvironmentWithoutParents());
                } catch(NameClashException) {
                    // in this particular situation, there will be no name clash
                }

                CoreModelElement owner = this.getElemOwner();
                if (owner != null && owner.GetType() == typeof(CoreNamespace)) {
                    this.environmentWithParents.setParent(((CoreNamespace) owner).getEnvironmentWithParents());
                }
    		
                isDirty = false;
            }

            return this.environmentWithParents;
        }

        public virtual Environment getEnvironmentWithoutParents() {
            if (isDirty || this.environmentWithoutParents == null) {
                this.environmentWithoutParents = EnvironmentFactoryManager.getInstance(this.getModel().getOclPackage()).getEnvironmentInstance();
                populateEnvironment(this.environmentWithoutParents);
                isDirty = false;
            }

            return this.environmentWithoutParents;
        }

        public virtual List<object> getElementsForEnvironment()
        {
            throw new NotImplementedException();
        }

        public virtual void populateEnvironment(Environment environment) {
            foreach (CoreModelElement element in getElemOwnedElements()) {

                if (elementShouldBeAddedToEnvironment(element)) {
                    addElementToEnvironment(element.getName(), element, environment);
                }
            }
        }

        protected void addElementToEnvironment(
            String name,
            CoreModelElement wrapper,
            Environment environment) {
            try {
                environment.addElement(name, wrapper, false);
            } catch (NameClashException e) {
                Object element = environment.lookupLocal(name);
                try {
                    if (element.GetType() == typeof(CoreClassifier) && 
                        wrapper.GetType() == typeof(CoreClassifier) &&
                        ! OclTypesDefinition.isOclPrimitiveType(wrapper.getName())) {
                            environment.removeElement(name);
                            CoreClassifier cls = (CoreClassifier) element;
                            environment.addElement(cls.getFullPathName(), cls, false);
                            environment.addElement(((CoreClassifier) wrapper).getFullPathName(), cls, false);
                        }
                } catch (NameClashException e2) {
                }
            }
            }

        public virtual void setDirty(bool value) {
            this.isDirty = value;
            if (isDirty) {
                resetEnvironments();
            }
        }

        public bool getIsDirty() {
            return this.isDirty;
        }

        protected virtual bool elementShouldBeAddedToEnvironment(CoreModelElement element)
        {
            throw new NotImplementedException();
        }
    }
}
