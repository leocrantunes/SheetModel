using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.context
{
    public class CSTPreDeclCS : CSTOperationConstraintCS {
        public CSTPreDeclCS(
            OCLWorkbenchToken token,
            CSTNameCS name,
            CSTExpressionInOclCS expression) : base(token, name, expression) {
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            visitor.visitPreDecl(this);
            base.accept(visitor);
        }
    }
}
