using System.Collections.Generic;
using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.expression
{
    public class CSTClassOperationCallExpCS : CSTOperationCallExpCS {
        private CSTPathNameCS pathName;

        public CSTClassOperationCallExpCS(
            CSTPathNameCS pathName,
            List<object> arguments,
            bool isMarkedPre) : base(arguments, isMarkedPre) {
            this.pathName = pathName;
            }

        /**
     * @return
     */
        public CSTPathNameCS getPathNameNodeCS() {
            return pathName;
        }

        public string getClassifierName() {
            return pathName.getAllButLastName();
        }

        public override string getOperationName() {
            return pathName.getLastName();
        }

        public override void accept(CSTVisitor visitor) {
            if (getPathNameNodeCS() != null) {
                base.accept(visitor);
                visitor.visitClassOperationCallExp(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return pathName.getToken();
        }
    }
}
