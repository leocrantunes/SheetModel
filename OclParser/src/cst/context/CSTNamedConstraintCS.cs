using OclLibrary.iface.constraints;
using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.context
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
            return (nameNodeCS != null) ? nameNodeCS.toString()
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
