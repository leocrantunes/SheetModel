using System.Text;
using System.Collections.Generic;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;
using Ocl20.parser.cst.type;

namespace Ocl20.parser.cst.context
{
    public class CSTOperationCS : CSTNode {
        private CSTNameCS nameNodeCS;
        private List<object> parametersNodesCS;
        private CSTTypeCS returnTypeNodeCS;

        public CSTOperationCS(
            CSTNameCS name,
            List<object> parameters,
            CSTTypeCS returnType) {
            this.nameNodeCS = name;
            this.parametersNodesCS = parameters;
            this.returnTypeNodeCS = returnType;
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
        public List<object> getParametersNodesCS()
        {
            return parametersNodesCS;
        }

        /**
     * @return
     */
        public CSTTypeCS getTypeNodeCS() {
            return returnTypeNodeCS;
        }

        public string getFullName() {
            StringBuilder result = new StringBuilder();

            if (nameNodeCS == null) {
                return null;
            }

            result.Append(getOperationName());

            result.Append("(");
            result.Append(getParametersTypes());
            result.Append(")");

            if (returnTypeNodeCS != null) {
                result.Append(getResultType());
            }

            return result.ToString();
        }

        public string getOperationName() {
            return nameNodeCS.getLastName();
        }

        private string getParametersTypes() {
            StringBuilder result = new StringBuilder();

            if (parametersNodesCS.Count > 0) {
                result.Append(getParameterTypeName(0));
            }

            for (int i = 1; i < parametersNodesCS.Count; i++) {
                result.Append(", ");
                result.Append(getParameterTypeName(i));
            }

            return result.ToString();
        }

        private string getResultType() {
            return " : " + returnTypeNodeCS.getName();
        }

        private string getParameterTypeName(int index) {
            CSTVariableDeclarationCS parameter = (CSTVariableDeclarationCS) parametersNodesCS[index];

            return parameter.getTypeNodeCS().getName();
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            base.accept(parametersNodesCS, visitor);

            if (returnTypeNodeCS != null) {
                returnTypeNodeCS.accept(visitor);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
