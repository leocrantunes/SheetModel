using OclLibrary.iface.expressions;

namespace OclParser.cst.expression
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
