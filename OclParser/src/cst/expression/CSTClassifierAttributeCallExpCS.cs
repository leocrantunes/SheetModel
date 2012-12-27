using OclParser.controller;
using OclParser.cst.name;

namespace OclParser.cst.expression
{
    public class CSTClassifierAttributeCallExpCS : CSTOclExpressionCS {
        private CSTPathNameCS pathName;
        private bool isMarkedPre;

        public CSTClassifierAttributeCallExpCS(
            CSTPathNameCS pathName,
            bool isMarkedPre) {
            this.pathName = pathName;
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
        public CSTPathNameCS getPathNameNodeCS() {
            return pathName;
        }

        /**
     * @return
     */
        public string getClassifierName() {
            return pathName.getAllButLastName();
        }

        /**
     * @return
     */
        public string getFeatureName() {
            return pathName.getLastName();
        }

        public override void accept(CSTVisitor visitor) {
            if (getPathNameNodeCS() != null) {
                visitor.visitClassifierAttributeCall(this);
            }
        }

        public override OCLWorkbenchToken getToken() {
            return pathName.getToken();
        }
    }
}
