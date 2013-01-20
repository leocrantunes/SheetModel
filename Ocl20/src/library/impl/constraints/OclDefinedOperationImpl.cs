using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.common;

namespace Ocl20.library.impl.constraints
{
    public abstract class OclDefinedOperationImpl : CoreOperationImpl, OclDefinedOperation {

        private String name;
        private List<object> paramTypes;
        private List<object> paramNames;
        private CoreClassifier returnType;
        private String source;
        private	CoreClassifier owner;

        public override bool isOclDefined() {
            return	true;
        }

        public override bool isInstanceScope() {
            return true;
        }

        /* (non-Javadoc)
	 * @see impl.ocl20.common.CoreOperationImpl#isQuery()
	 */
        public override bool getIsQuery() {
            return true;
        }
    
        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.uml14Bridge.UMLOperationBridge#getReturnParameter()
     */
        protected CoreClassifier getReturnParameter() {
            return returnType;
        }
    
        public override CoreClassifier getReturnType() {
            return	returnType;
        }

        public override bool operationNameMatches(String name) {
            ModelElementNameGenerator nameGenerator = CoreModelElementNameGeneratorImpl.getInstance();

            return nameGenerator.operationNameMatches(nameGenerator.generateNameForOperation(
                this.name, this.paramTypes), name);
        }

        public override String getName() {
            return this.name;
        }

        public String getSource() {
            return this.source;
        }

        protected override CoreClassifier getSpecificReturnParameterType() {
            return	returnType;
        }
    
        protected override List<object> getSpecificParameterTypesExceptReturn() {
            return	paramTypes;
        }

        protected override List<object> getSpecificParameterNamesExceptReturn() {
            return	paramNames;
        }

        /**
	 * @param name The name to set.
	 */
        public override void setName(String name) {
            this.name = name;
        }
        /**
	 * @param paramNames The paramNames to set.
	 */
        public void setParamNames(List<object> paramNames) {
            this.paramNames = paramNames;
        }
        /**
	 * @param paramTypes The paramTypes to set.
	 */
        public void setParamTypes(List<object> paramTypes) {
            this.paramTypes = paramTypes;
        }
        /**
	 * @param returnType The returnType to set.
	 */
        public void setReturnType(CoreClassifier returnType) {
            this.returnType = returnType;
        }
        /**
	 * @param source The source to set.
	 */
        public void setSource(String source) {
            this.source = source;
        }
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreModelElement#setElemOwner(ocl20.common.CoreModelElement)
	 */
        public override void setElemOwner(CoreModelElement newValue) {
            this.owner = (CoreClassifier) newValue;
        }
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreFeature#setFeatureOwner(ocl20.common.CoreClassifier)
	 */
        public override void setFeatureOwner(CoreClassifier newValue) {
            this.owner = newValue;
        }
	
        public override CoreClassifier getFeatureOwner() {
            return	this.owner;
        }

        public CoreClassifier getElementOwner() {
            return	this.owner;
        }
    }
}
