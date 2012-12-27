/*
 * Created on Nov 20, 2003
 *
 * To change the template for this generated file go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */


/**
 * @author Administrator
 *
 * To change the template for this generated type comment go to
 * Window>Preferences>Java>Code Generation>Code and Comments
 */

using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
{
    public class CSTBodyDeclCS : CSTOperationConstraintCS {
        public CSTBodyDeclCS(
            OCLWorkbenchToken token,
            CSTNameCS name,
            CSTExpressionInOclCS expression) : base(token, name, expression)
        {}

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.context.CSTOperationConstraintCS#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            visitor.visitBodyDecl(this);
            base.accept(visitor);
        }
    }
}
