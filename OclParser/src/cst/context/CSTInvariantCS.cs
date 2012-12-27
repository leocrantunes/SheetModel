using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.context
{
    public class CSTInvariantCS : CSTNamedConstraintCS {
        public CSTInvariantCS(
            OCLWorkbenchToken token,
            CSTSimpleNameCS simpleName) : base(token, simpleName, null) {
            }

        public void setExpressionNodeCS(CSTExpressionInOclCS expression) {
            this.expressionNodeCS = expression;
        }

        public override void accept(CSTVisitor visitor) {
            if (expressionNodeCS != null)
            {
                visitor.visitInvariantCSBegin(this);

                base.accept(visitor);

                if (getExpressionInOCL() != null)
                {
                    visitor.visitInvariantCSEnd(this);
                }
            }
        }
    }
}
