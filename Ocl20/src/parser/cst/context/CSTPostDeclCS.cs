using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
{
    public class CSTPostDeclCS : CSTOperationConstraintCS {
        public CSTPostDeclCS(
            OCLWorkbenchToken token,
            CSTNameCS name,
            CSTExpressionInOclCS expression) : base(token, name, expression) {
            }

        public override void accept(CSTVisitor visitor) {
            visitor.visitPostDecl(this);
            base.accept(visitor);
        }
    }
}
