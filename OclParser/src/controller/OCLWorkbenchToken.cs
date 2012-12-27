
/**
 * Token class derived from the ANTLR CommonToken class.
 * It is responsible for source position information.
 *
 */
namespace OclParser.controller
{
    public class OCLWorkbenchToken : antlr.CommonToken {
        private string srcName;

        public OCLWorkbenchToken(
            string srcName,
            int line,
            int column) {
            this.srcName = srcName;
            setFilename(srcName);
            setLine(line);
            setColumn(column);
            }

        public string getSrcName() {
            return this.srcName;
        }

        public override string getFilename() {
            return this.srcName;
        }
    }
}
