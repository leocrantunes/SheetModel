using OclParser.controller;
using OclParser.cst.name;
using OclParser.cst.type;

namespace OclParser.cst.context
{
    public class CSTDefVarExpressionCS : CSTDefExpressionCS {
        private CSTNameCS nameNodeCS;
        private CSTTypeCS typeNodeCS;
        private CSTExpressionInOclCS expressionNodeCS;

        public CSTDefVarExpressionCS(
            CSTNameCS name,
            CSTTypeCS type,
            CSTExpressionInOclCS expression) {
            this.nameNodeCS = name;
            this.typeNodeCS = type;
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

        /**
     * @return
     */
        public CSTTypeCS getTypeNodeCS() {
            return typeNodeCS;
        }

        public string getNameAsString() {
            return (nameNodeCS != null) ? nameNodeCS.toString()
                       : null;
        }

        public override void accept(CSTVisitor visitor) {
            if ((typeNodeCS != null) && (expressionNodeCS != null)) {
                visitor.visitDefVarExpressionBegin(this);
                typeNodeCS.accept(visitor);
                expressionNodeCS.accept(visitor);
                visitor.visitDefVarExpressionEnd(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
