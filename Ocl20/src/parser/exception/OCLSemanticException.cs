using Ocl20.parser.cst;

namespace Ocl20.parser.exception
{
    public class OCLSemanticException : OCLCompilerException {
	
        private const long serialVersionUID = 1L;
        private CSTNode node;

        public OCLSemanticException(CSTNode node, string message)
            : base(message, new SourceLocation(node.getToken()))
        {
            this.node = node;
        }
	
        public CSTNode getNode() {
            return node;
        }
    }
}
