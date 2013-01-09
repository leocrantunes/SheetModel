using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.impl.common;
using Ocl20.uml13.iface.foundation.core;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.impl.foundation.core
{
    public abstract class OperationImpl : CoreOperationImpl, Operation {

        protected override CoreModelElement getSpecificOwnerElement() {
            return	(CoreModelElement) getOwner();
        }
	
        public override List<object> getSpecificOwnedElements() {
            return new List<object>();
        }
	
        public override bool getSpecificIsInstanceScope() {
            return (getOwnerScope() == ScopeKindEnum.SK_INSTANCE) && (super_getName()[0] != '$');
        }
	
        protected abstract bool super_isQuery();
	
        public override bool  getSpecificIsQuery() {
            return	super_isQuery();
        }

        protected override bool getSpecificHasStereotype(String name) {
            return	false;
        }
	
        protected override CoreClassifier getSpecificReturnParameterType() {
            List<object> parameters = getParameter();
		
            for (int i = 0; i < parameters.Count; i++) {
                Parameter param = (Parameter) parameters[i];
                if (isReturnParameter(param) && ! param.getType().getName().Equals("void"))
                    return  (CoreClassifier) param.getType();			
            }
            return	null;				
        }

        protected override List<object> getSpecificParameterTypesExceptReturn() {
            List<object> paramTypes = new List<object>();

            foreach (Parameter param in getParameter()) {
                if (! isReturnParameter(param)) {
                    paramTypes.Add(param.getType());
                }				
            }

            return paramTypes;
        }

        protected override List<object> getSpecificParameterNamesExceptReturn() {
            List<object> paramNames = new List<object>();
		
            foreach (Parameter param in getParameter()) {
                if (! isReturnParameter(param)) {
                    paramNames.Add(param.getName());
                }				
            }
		
            return paramNames;    	
        }

        public override String getName() {
            String name = super_getName();
		
            return (name[0] == '$') ? 
                       name.Substring(1, name.Length) :
                       name;
        }

        public abstract Namespace getNamespace();
        public abstract void setNamespace(Namespace newValue);
        public abstract List<object> getClientDependency();
        public abstract List<object> getSupplierDependency();

        private bool isReturnParameter(Parameter param) {
            return param.getKind() == ParameterDirectionKindEnum.PDK_RETURN;
        }

        public abstract List<object> getParameter();
        public abstract ScopeKind getOwnerScope();
        public abstract Classifier getOwner();
    }
}
