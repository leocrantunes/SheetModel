// TODO : Rever ExpressionInOcl

using Ocl20.library.iface.constraints;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;
using Ocl20.parser.cst.type;

namespace Ocl20.parser.cst.context
{
    public class CSTAttrOrAssocContextCS : CSTContextDeclarationCS {
        private CSTPathNameCS pathNameNodeCS;
        private CSTTypeCS typeNodeCS;
        private CSTInitDerivedValueCS valueExpressionNodeCS;

        public CSTAttrOrAssocContextCS(
            CSTPathNameCS pathName,
            CSTTypeCS type,
            CSTInitDerivedValueCS value) {
            this.pathNameNodeCS = pathName;
            this.typeNodeCS = type;
            this.valueExpressionNodeCS = value;
            }

        /**
     * @return
     */
        public CSTPathNameCS getPathNameNodeCS() {
            return pathNameNodeCS;
        }

        /**
     * @return
     */
        public CSTTypeCS getTypeNodeCS() {
            return typeNodeCS;
        }

        /**
     * @return
     */
        public CSTInitDerivedValueCS getValueExpressionNodeCS() {
            return valueExpressionNodeCS;
        }

        public string getClassifierName() {
            return (getPathNameNodeCS() != null)
                       ? getPathNameNodeCS()
                             .getAllButLastName()
                       : null;
        }

        public string getFeatureName() {
            return (getPathNameNodeCS() != null)
                       ? getPathNameNodeCS()
                             .getLastName()
                       : null;
        }

        public bool isInitConstraint() {
            return (getValueExpressionNodeCS() != null)
                       ? getValueExpressionNodeCS()
                             .getToken()
                             .getText()
                             .Equals("init")
                       : false;
        }

        public ExpressionInOcl getExpressionInOCL()
        {
            return (getValueExpressionNodeCS() != null)
            ? this.getValueExpressionNodeCS()
                  .getExpressionNodeCS()
                  .getAst()
            : null;
        }

        public override void accept(CSTVisitor visitor) {
            visitor.visitAttrOrAssocContextDeclCSBegin(this);

            if (typeNodeCS != null) {
                typeNodeCS.accept(visitor);
            }

            if (valueExpressionNodeCS != null) {
                valueExpressionNodeCS.accept(visitor);
            }

            visitor.visitAttrOrAssocContextDeclCSEnd(this);
        }

        public override OCLWorkbenchToken getToken() {
            return pathNameNodeCS.getToken();
        }
    }
}
