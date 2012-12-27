using Ocl20.library.iface.constraints;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
{
    public class CSTNamedConstraintCS : CSTConstraintDefinitionCS {
        protected CSTNameCS nameNodeCS;
        protected CSTExpressionInOclCS expressionNodeCS;
        protected OCLWorkbenchToken token;

        public CSTNamedConstraintCS(
            OCLWorkbenchToken token,
            CSTNameCS name,
            CSTExpressionInOclCS expression) {
            this.token = token;
            this.nameNodeCS = name;
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
        public CSTNameCS getNameNodeCS() {
            return nameNodeCS;
        }

        public ExpressionInOcl getExpressionInOCL()
        {
            return this.getExpressionNodeCS().getAst();
        }

        /**
     * @return
     */
        public string getNameAsString() {
            return (nameNodeCS != null) ? nameNodeCS.ToString()
                       : null;
        }

        public override OCLWorkbenchToken getToken() {
            return this.token;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (expressionNodeCS != null) {
                expressionNodeCS.accept(visitor);
            }
        }
    }
}
