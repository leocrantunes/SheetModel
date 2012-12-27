using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.context
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
