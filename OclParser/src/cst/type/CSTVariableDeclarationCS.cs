// TODO : Rever VariableDeclaration

using OclParser.controller;
using OclParser.cst.expression;
using OclParser.cst.name;

namespace OclParser.cst.type
{
    public class CSTVariableDeclarationCS : CSTNode {
        private CSTNameCS nameNodeCS;
        private CSTTypeCS typeNodeCS;
        private CSTOclExpressionCS expressionNodeCS;
        //private VariableDeclaration ast;

        public CSTVariableDeclarationCS(
            CSTNameCS name,
            CSTTypeCS type,
            CSTOclExpressionCS expression) {
            this.nameNodeCS = name;
            this.typeNodeCS = type;
            this.expressionNodeCS = expression;
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getExpressionNodeCS() {
            return expressionNodeCS;
        }

        /**
     * @return
     */
        public CSTNameCS getNameNodeCS() {
            return nameNodeCS;
        }

        /**
     * @return
     */
        public CSTTypeCS getTypeNodeCS() {
            return typeNodeCS;
        }

        /**
     * @return
     */
        //public VariableDeclaration getAst() {
        //    return ast;
        //}

        /**
     * @param declaration
     */
        //public void setAst(VariableDeclaration declaration) {
        //    ast = declaration;
        //}

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (typeNodeCS != null) {
                typeNodeCS.accept(visitor);
            }

            if (expressionNodeCS != null) {
                expressionNodeCS.accept(visitor);
            }

            if (getNameNodeCS() != null) {
                visitor.visitVariableDeclaration(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
