using System.Collections.Generic;
using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.expression
{
    public class CSTSimpleNameExpCS : CSTOclExpressionCS {
        private CSTSimpleNameCS nameNodeCS;
        private List<object> argumentsNodesCS;
        private bool isMarkedPre;

        public CSTSimpleNameExpCS(
            CSTSimpleNameCS name,
            List<object> arguments,
            bool isMarkedPre) {
            this.nameNodeCS = name;
            this.argumentsNodesCS = arguments;
            this.isMarkedPre = isMarkedPre;
            }

        /**
     * @return
     */
        public bool getIsMarkedPre() {
            return isMarkedPre;
        }

        /**
     * @return
     */
        public string getNameAsString() {
            return nameNodeCS.ToString();
        }

        public override void accept(CSTVisitor visitor) {
            if (argumentsNodesCS != null) {
                accept(argumentsNodesCS, visitor);
                visitor.visitSimpleNameExp(this);
            }
        }

        /**
     * @return
     */
        public List<object> getArgumentsNodesCS() {
            return argumentsNodesCS;
        }

        //public List<object> getArgumentsAst() {
        //    List<object> result = new List<object>();

        //    for (Iterator iter = argumentsNodesCS.iterator(); iter.hasNext();) {
        //        CSTArgumentCS argument = (CSTArgumentCS) iter.next();
        //        result.add(argument.getAst());
        //    }

        //    return result;
        //}

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
