using System;
using System.Collections.Generic;
using OclParser.controller;
using OclParser.cst.type;

namespace OclParser.cst.context
{
    public class CSTOperationContextCS : CSTContextDeclarationCS {
        private CSTOperationCS operationNodeCS;
        private List<object> constraintsNodesCS;

        public CSTOperationContextCS(
            CSTOperationCS operation,
            List<object> constraints) {
            this.operationNodeCS = operation;
            this.constraintsNodesCS = constraints;
            }

        /**
     * @return
     */
        public List<object> getConstraintsNodesCS() {
            return constraintsNodesCS;
        }

        /**
     * @return
     */
        public CSTOperationCS getOperationNodeCS() {
            return operationNodeCS;
        }

        public List<object> getPreConditionsNodesCS() {
            return getConstraints(typeof(CSTPreDeclCS));
        }

        public List<object> getPostConditionsNodesCS() {
            return getConstraints(typeof(CSTPostDeclCS));
        }

        public List<object> getBodyDefinitionNodesCS() {
            return getConstraints(typeof(CSTBodyDeclCS));
        }

        //public List<object> getActionBodyDefinitionNodesCS() {
        //    return getConstraints(typeof(CSTActionBodyDeclCS));
        //}
    
        public List<object> getLocalDefinitionNodesCS() {
            return	getConstraints(typeof(CSTDefVarExpressionCS));
        }

        //public List<object>	getModifiableDefinitionNodesCS() {
        //    return getConstraints(typeof(CSTModifiableDeclCS));
        //}
    
        private List<object> getConstraints(Type constraintClass) {
            List<object> result = new List<object>();

            foreach (var constraint in constraintsNodesCS)
            {
                if (constraint != null && constraint.GetType() == constraintClass) {
                    result.Add(constraint);
                }
            }

            return result;
        }

        public List<string> getParameterNames() {
            List<string> names = new List<string>();

            foreach (CSTVariableDeclarationCS parameter in operationNodeCS.getParametersNodesCS())
            {
                if (parameter != null)
                    names.Add(parameter.getNameNodeCS().toString());
            }

            return names;
        }

        public override void accept(CSTVisitor visitor) {
            if (operationNodeCS != null) {
                operationNodeCS.accept(visitor);
                visitor.visitOperationContextDeclCSBegin(this);
                accept(this.constraintsNodesCS, visitor);
                visitor.visitOperationContextDeclCSEnd(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return operationNodeCS.getToken();
        }
    }
}
