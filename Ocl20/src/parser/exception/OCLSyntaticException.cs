using Ocl20.parser.cst;

namespace Ocl20.parser.exception
{
    public class OCLSyntaticException : OCLCompilerException {
    
        private const long serialVersionUID = 1L;

        public OCLSyntaticException(string message, SourceLocation source) : base(message, source)
        {}
	
    }
}
