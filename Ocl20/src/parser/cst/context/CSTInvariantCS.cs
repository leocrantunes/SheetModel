using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
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
