using System.Collections.Generic;

namespace OclParser.cst.expression
{
    public abstract class CSTOperationCallExpCS : CSTOclExpressionCS {
        private List<object> arguments;
        private bool isMarkedPre;

        public CSTOperationCallExpCS(
            List<object> arguments,
            bool isMarkedPre) {
            this.arguments = arguments;
            this.isMarkedPre = isMarkedPre;
            }

        public override void accept(CSTVisitor visitor) {
            accept(arguments, visitor);
        }

        public abstract string getOperationName();

        /**
             * @return
             */
        public List<object> getArgumentsNodesCS() {
            return arguments;
        }

        //public List<object> getArgumentsAst() {
        //    List<object> argumentsAst = new List<object>();

        //    foreach (CSTArgumentCS argument in arguments)
        //        argumentsAst.Add(argument.getAst());
        
        //    return argumentsAst;
        //}

        /**
             * @return
             */
        public bool getIsMarkedPre() {
            return isMarkedPre;
        }

        //public List<object> getArgumentsTypes() {
        //    List<object> argumentsTypes = new List<object>();

        //    foreach (CSTArgumentCS argument in arguments)
        //        argumentsTypes.Add(argument.getAst().getType());
        
        //    return argumentsTypes;
        //}
    }
}
