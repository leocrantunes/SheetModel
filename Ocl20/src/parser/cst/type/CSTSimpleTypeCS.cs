using System.Collections.Generic;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.type
{
    public class CSTSimpleTypeCS : CSTTypeCS {
        private CSTNameCS nameNodeCS;

        public CSTSimpleTypeCS(CSTNameCS name) {
            this.nameNodeCS = name;
        }

        /**
     * @return
     */
        public override string getName() {
            return nameNodeCS.ToString();
        }

        public override List<object> getAllSimpleTypesNodesCS() {
            List<object> simpleTypes = new List<object>();
            simpleTypes.Add(this);

            return simpleTypes;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTNode#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.cst.CSTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            if (nameNodeCS != null)
                visitor.visitSimpleTypeCS(this);
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
