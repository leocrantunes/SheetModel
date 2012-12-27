using System.Collections.Generic;
using System.Text;
using OclParser.controller;

namespace OclParser.cst.name
{
    public class CSTPathNameCS : CSTNameCS {
        private const string PACKAGE_NAME_SEPARATOR = "::";
        private List<OCLWorkbenchToken> names;

        public CSTPathNameCS() {
            this.names = new List<OCLWorkbenchToken>();
        }

        public void addName(OCLWorkbenchToken token) {
            this.names.Add(token);
        }

        public override OCLWorkbenchToken getToken() {
            return (this.names.Count > 0) ? (OCLWorkbenchToken) this.names[0] : null;
        }

        public IEnumerator<OCLWorkbenchToken> getNames() {
            return this.names.GetEnumerator();
        }

        public override string getName() {
            return this.getAllButLastName() + PACKAGE_NAME_SEPARATOR +
                   this.getLastName();
        }

        public override string getLastName() {
            if (this.hasLastName()) {
                return this.getTextForToken(this.getIndexOfLastToken());
            } else {
                return null;
            }
        }

        public string getAllButLastName() {
            StringBuilder result = new StringBuilder();

            if (names.Count > 1) {
                result.Append(this.getTextForToken(0));
            }

            for (int i = 1; i < getIndexOfLastToken(); i++) {
                result.Append(PACKAGE_NAME_SEPARATOR);
                result.Append(this.getTextForToken(i));
            }

            return result.ToString();
        }

        private bool hasLastName() {
            return this.getIndexOfLastToken() >= 0;
        }

        private int getIndexOfLastToken() {
            return this.names.Count - 1;
        }

        private string getTextForToken(int tokenIndex) {
            return this.getTokenForIndex(tokenIndex).getText();
        }

        private OCLWorkbenchToken getTokenForIndex(int tokenIndex) {
            return (OCLWorkbenchToken) this.names[tokenIndex];
        }
    }
}
