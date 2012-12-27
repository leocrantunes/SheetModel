using Ocl20.library.iface.constraints;
using Ocl20.parser.controller;
using Ocl20.parser.cst.expression;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
{
    public class CSTExpressionInOclCS : CSTNode {
        private CSTNameCS nameNodeCS;
        private CSTOclExpressionCS oclExpressionNodeCS;
        private ExpressionInOcl ast;

        public CSTExpressionInOclCS(CSTOclExpressionCS oclExpression) {
            this.oclExpressionNodeCS = oclExpression;
        }

        /**
     * @return
     */
        public string getName()
        {
            return nameNodeCS.ToString();
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (oclExpressionNodeCS != null) {
                visitor.visitExpressionInOcl(this);
                oclExpressionNodeCS.accept(visitor);
                visitor.visitExpressionInOclEnd(this);
            }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return oclExpressionNodeCS.getToken();
        }

        /**
     * @return
     */
        public CSTOclExpressionCS getOclExpressionNodeCS() {
            return oclExpressionNodeCS;
        }

        /**
     * @return
     */
        public ExpressionInOcl getAst()
        {
            return ast;
        }

        /**
     * @param inOCL
     */
        public void setAst(ExpressionInOcl expInOCL)
        {
            ast = expInOCL;
        }
    }
}
