using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.context
{
    public abstract class CSTOperationConstraintCS : CSTNamedConstraintCS {
        public CSTOperationConstraintCS(
            OCLWorkbenchToken token,
            CSTNameCS name,
            CSTExpressionInOclCS expression) : base(token, name, expression) {
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            base.accept(visitor);
            visitor.visitOperationConstraint(this);
        }
    }
}
