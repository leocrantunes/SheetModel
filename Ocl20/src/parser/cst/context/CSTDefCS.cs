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
    public class CSTDefCS : CSTConstraintDefinitionCS {
        private CSTNameCS nameNodeCS;
        private CSTDefExpressionCS expressionNodeCS;
        private OCLWorkbenchToken token;

        public CSTDefCS(
            OCLWorkbenchToken token,
            CSTNameCS name,
            CSTDefExpressionCS expression) {
            this.nameNodeCS = name;
            this.expressionNodeCS = expression;
            this.token = token;
            }

        /**
     * @return
     */
        public CSTDefExpressionCS getExpressionNodeCS() {
            return expressionNodeCS;
        }

        /**
     * @return
     */
        public CSTNameCS getNameNodeCS() {
            return nameNodeCS;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (expressionNodeCS != null) {
                expressionNodeCS.accept(visitor);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return this.token;
        }
    }
}
