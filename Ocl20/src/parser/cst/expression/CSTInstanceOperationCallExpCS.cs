using System.Collections.Generic;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.expression
{
    public class CSTInstanceOperationCallExpCS : CSTOperationCallExpCS {
        private CSTSimpleNameCS simpleName;

        public CSTInstanceOperationCallExpCS(
            CSTSimpleNameCS simpleName,
            List<object> arguments,
            bool isMarkedPre) : base(arguments, isMarkedPre) {
            this.simpleName = simpleName;
            }

        public CSTSimpleNameCS getSimpleNameNodeCS() {
            return simpleName;
        }

        public override string getOperationName() {
            return simpleName.ToString();
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (getSimpleNameNodeCS() != null) {
                base.accept(visitor);
                visitor.visitInstanceOperationCallExp(this);
            }
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#getToken()
     */
        public override OCLWorkbenchToken getToken() {
            return simpleName.getToken();
        }
    }
}
