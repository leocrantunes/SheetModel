using System.Collections.Generic;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.expression
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

        public List<object> getArgumentsAst()
        {
            List<object> result = new List<object>();

            foreach (CSTArgumentCS argument in argumentsNodesCS)
            {
                result.Add(argument.getAst());
            }

            return result;
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
