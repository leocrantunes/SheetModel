

using Ocl20.library.iface.expressions;

namespace Ocl20.parser.cst.expression
{
    abstract public class CSTOclExpressionCS : CSTNode {
        private OclExpression ast;

        /**
     * @return
     */
        public OclExpression getAst()
        {
            return ast;
        }

        /**
     * @param expression
     */
        public void setAst(OclExpression expression)
        {
            ast = expression;
        }
    }
}
