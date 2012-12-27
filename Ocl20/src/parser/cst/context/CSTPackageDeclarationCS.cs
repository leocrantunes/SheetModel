using System.Collections.Generic;
using Ocl20.parser.controller;
using Ocl20.parser.cst.name;

namespace Ocl20.parser.cst.context
{
    public class CSTPackageDeclarationCS : CSTNode, VisitableElement {
        private CSTNameCS nameNodeCS;
        private List<object> contextDeclList;

        public CSTPackageDeclarationCS(CSTNameCS name) {
            this.nameNodeCS = name;
            contextDeclList = new List<object>();
        }

        public void addContextDeclaration(CSTContextDeclarationCS contextDecl) {
            this.contextDeclList.Add(contextDecl);
        }

        public CSTNameCS getNameNodeCS() {
            return this.nameNodeCS;
        }

        public List<object> getContextDeclarations() {
            return this.contextDeclList;
        }

        public string getFullPathName() {
            return this.nameNodeCS.ToString();
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.VisitableElement#accept(br.cos.ufrj.lens.odyssey.tools.psw.parser.ast.ASTVisitor)
     */
        public override void accept(CSTVisitor visitor) {
            visitor.visitPackageDeclarationCS(this);
            accept(contextDeclList, visitor);
        }

        public override OCLWorkbenchToken getToken() {
            return nameNodeCS.getToken();
        }
    }
}
