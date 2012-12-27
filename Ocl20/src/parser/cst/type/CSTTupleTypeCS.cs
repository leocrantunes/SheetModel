using System.Collections.Generic;
using System.Text;
using Ocl20.parser.controller;

namespace Ocl20.parser.cst.type
{
    public class CSTTupleTypeCS : CSTTypeCS {

        private List<object> variableDeclarationNodesCS;
        private OCLWorkbenchToken token;

        public CSTTupleTypeCS(OCLWorkbenchToken token, List<object> variableDeclarationList)
        {
            this.token = token;
            this.variableDeclarationNodesCS = variableDeclarationList;
        }

        /**
     * @return
     */
        public List<object> getVariableDeclarationNodesCS()
        {
            return variableDeclarationNodesCS;
        }

        public override string getName() {
            return "Tuple" + "(" + getVariableTypeNames() + ")";
        }

        public override List<object> getAllSimpleTypesNodesCS()
        {
            List<object> simpleTypes = new List<object>();

            for (int i = 0; i < variableDeclarationNodesCS.Count; i++) {
                CSTVariableDeclarationCS variableDecl = getVariableDeclaration(i);

                if (variableDecl.getTypeNodeCS() != null) {
                    simpleTypes.AddRange(variableDecl.getTypeNodeCS().getAllSimpleTypesNodesCS());
                }
            }

            return simpleTypes;
        }

        public override void accept(CSTVisitor visitor) {
            this.accept(variableDeclarationNodesCS, visitor);
            visitor.visitTupleTypeCS(this);
        }

        public override OCLWorkbenchToken getToken() {
            return	token;
        }

        private string getVariableTypeNames() {
            StringBuilder result = new StringBuilder();

            if (variableDeclarationNodesCS.Count > 0) {
                getTuplePartName(result, getVariableDeclaration(0));
            }

            for (int i = 1; i < variableDeclarationNodesCS.Count; i++) {
                result.Append(",");
                getTuplePartName(result, getVariableDeclaration(i));
            }

            return result.ToString();
        }

        private void getTuplePartName(
            StringBuilder result,
            CSTVariableDeclarationCS variableDecl) {
            result.Append(variableDecl.getNameNodeCS());
            result.Append(":");
            result.Append(getTypeNameForVariableDeclaration(variableDecl));
            }

        private CSTVariableDeclarationCS getVariableDeclaration(int index) {
            return (CSTVariableDeclarationCS) variableDeclarationNodesCS[index];
        }

        private string getTypeNameForVariableDeclaration(
            CSTVariableDeclarationCS variableDecl) {
            if (variableDecl.getTypeNodeCS() != null) {
                return variableDecl.getTypeNodeCS()
                                   .getName();
            } else {
                return "";
            }
            }
    }
}
