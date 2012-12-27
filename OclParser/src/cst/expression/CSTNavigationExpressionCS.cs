using System.Collections.Generic;
using OclParser.controller;

namespace OclParser.cst.expression
{
    public class CSTNavigationExpressionCS : CSTOclExpressionCS {
        private CSTOclExpressionCS callExp;
        private List<object> innerNavigation;
        private List<object> operators;

        public CSTNavigationExpressionCS(CSTOclExpressionCS callExp) {
            this.callExp = callExp;
            this.innerNavigation = new List<object>();
            this.operators = new List<object>();
        }

        public void addInnerNavigation(
            CSTNavigationOperatorCS operat,
            CSTOclExpressionCS innerExp) {
            this.innerNavigation.Add(innerExp);
            this.operators.Add(operat);
            }

        /**
     * @return
     */
        public CSTOclExpressionCS getCallExpNodeCS() {
            return callExp;
        }

        /**
     * @return
     */
        public List<object> getInnerNavigationNodesCS() {
            return innerNavigation;
        }

        public List<object> getOperatorsNodesCS() {
            return operators;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            //if (callExp != null) {
            //    callExp.accept(visitor);
            //    visitor.visitNavigationExpressionBegin(this, callExp);

            //    for (Iterator iter = innerNavigation.iterator(), iterOperator = operators.iterator();
            //            iter.hasNext() && iterOperator.hasNext();) {
            //        CSTOclExpressionCS innerExp = (CSTOclExpressionCS) iter.next();
            //        CSTNavigationOperatorCS operat = (CSTNavigationOperatorCS) iterOperator.next();

            //        if ((innerExp != null) && (operat != null)) {
            //            operat.accept(visitor);
            //            innerExp.accept(visitor);
            //            visitor.visitNavigationExpression(this, innerExp);
            //        }
            //    }

            //    visitor.visitNavigationExpressionEnd(this, innerNavigation, callExp);
            //}
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return callExp.getToken();
        }
    }
}
