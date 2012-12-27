using System.Collections.Generic;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
{
    public class CSTClassifierContextDeclCS : CSTContextDeclarationCS {
        private CSTNameCS nameNodeCS;
        private List<object> constraintsNodesCS;

        public CSTClassifierContextDeclCS(CSTNameCS name) {
            this.nameNodeCS = name;
        }

        public void addConstraints(List<object> constraintsList) {
            this.constraintsNodesCS = constraintsList;
        }

        /**
     * @return
     */
        public List<object> getConstraintsNodesCS() {
            return constraintsNodesCS;
        }

        /**
     * @return
     */
        public CSTNameCS getNameNodeCS() {
            return nameNodeCS;
        }

        public string getNameAsString() {
            return (nameNodeCS != null) ? nameNodeCS.getName()
                       : null;
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitClassifierContextDeclCS(this);

            accept(this.constraintsNodesCS, visitor);
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
