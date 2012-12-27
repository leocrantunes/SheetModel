using System.Collections.Generic;
using OclParser.controller;
using OclParser.cst.type;

namespace OclParser.cst.context
{
    public class CSTDefOperationExpressionCS : CSTDefExpressionCS {
        private CSTOperationCS operationNodeCS;
        private CSTExpressionInOclCS expressionNodeCS;

        public CSTDefOperationExpressionCS(
            CSTOperationCS operation,
            CSTExpressionInOclCS expression) {
            this.operationNodeCS = operation;
            this.expressionNodeCS = expression;
            }

        /**
     * @return
     */
        public CSTExpressionInOclCS getExpressionNodeCS() {
            return expressionNodeCS;
        }

        /**
     * @return
     */
        public CSTOperationCS getOperationNodeCS() {
            return operationNodeCS;
        }

        public List<string> getParameterNames() {
            List<string> names = new List<string>();

            foreach (CSTVariableDeclarationCS parameter in operationNodeCS.getParametersNodesCS())
            {
                names.Add(parameter.getNameNodeCS().toString());
            }

            return names;
        }

        public override void accept(CSTVisitor visitor) {
            if ((operationNodeCS != null) && (expressionNodeCS != null)) {
                visitor.visitDefOperationExpressionBegin(this);
                operationNodeCS.accept(visitor);
                expressionNodeCS.accept(visitor);
                visitor.visitDefOperationExpressionEnd(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return operationNodeCS.getToken();
        }
    }
}
