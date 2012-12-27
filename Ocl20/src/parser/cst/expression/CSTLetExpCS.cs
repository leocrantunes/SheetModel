using System.Collections.Generic;

namespace Ocl20.parser.cst.expression
{
    public class CSTLetExpCS : CSTOclExpressionCS {
        private List<object> varDeclarations;
        private CSTOclExpressionCS expression;

        public CSTLetExpCS(
            List<object> varDeclarations,
            CSTOclExpressionCS expression) {
            this.varDeclarations = varDeclarations;
            this.expression = expression;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if ((expression != null) && (varDeclarations != null)) {
                visitor.visitLetExp(this);
            }
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getExpressionNodeCS() {
            return expression;
        }

        /**
     * @return
     */
        public List<object> getVarDeclarationsNodesCS() {
            return varDeclarations;
        }
    }
}
