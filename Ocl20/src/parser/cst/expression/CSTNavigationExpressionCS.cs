using System.Collections.Generic;
using Ocl20.parser.controller;
using System.Linq;

namespace Ocl20.parser.cst.expression
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
            if (callExp != null)
            {
                callExp.accept(visitor);
                visitor.visitNavigationExpressionBegin(this, callExp);

                //var expAndOperators = innerNavigation.Zip(operators, (e, o) => new { Expression = e, Operator = o });            
                //foreach (var e in expAndOperators)
                //{
                //    CSTOclExpressionCS innerExp = (CSTOclExpressionCS) e.Expression;
                //    CSTNavigationOperatorCS operat = (CSTNavigationOperatorCS) e.Operator;

                //    if ((innerExp != null) && (operat != null))
                //    {
                //        operat.accept(visitor);
                //        innerExp.accept(visitor);
                //        visitor.visitNavigationExpression(this, innerExp);
                //    }
                //}

                var iter = innerNavigation.GetEnumerator();
                var iterOperator = operators.GetEnumerator();
                while (iter.MoveNext() && iterOperator.MoveNext())
                {
                    CSTOclExpressionCS innerExp = (CSTOclExpressionCS)iter.Current;
                    CSTNavigationOperatorCS operat = (CSTNavigationOperatorCS)iterOperator.Current;

                    if ((innerExp != null) && (operat != null))
                    {
                        operat.accept(visitor);
                        innerExp.accept(visitor);
                        visitor.visitNavigationExpression(this, innerExp);
                    }
                }

                visitor.visitNavigationExpressionEnd(this, innerNavigation, callExp);
            }
        }

        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return callExp.getToken();
        }
    }
}
