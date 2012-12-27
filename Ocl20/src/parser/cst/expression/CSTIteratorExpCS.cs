using System.Collections.Generic;
using Ocl20.parser.controller;

namespace Ocl20.parser.cst.expression
{
    public class CSTIteratorExpCS : CSTOclExpressionCS {
        private CSTIteratorOperationCS iteratorOperation;
        private List<object> variables;
        private CSTOclExpressionCS bodyExpression;

        public CSTIteratorExpCS(
            CSTIteratorOperationCS iteratorOperation,
            List<object> variables,
            CSTOclExpressionCS bodyExpression) {
            this.iteratorOperation = iteratorOperation;
            this.variables = variables;
            this.bodyExpression = bodyExpression;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getBodyExpressionNodeCS() {
            return bodyExpression;
        }

        /**
     * @return
     */
        public CSTIteratorOperationCS getIteratorOperationNodeCS() {
            return iteratorOperation;
        }

        /**
     * @return
     */
        public List<object> getVariablesNodesCS() {
            return variables;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (bodyExpression != null) {
                accept(variables, visitor);
                visitor.visitBeginIteratorExp(this);
                bodyExpression.accept(visitor);
                visitor.visitEndIteratorExp(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return iteratorOperation.getToken();
        }
    }
}
