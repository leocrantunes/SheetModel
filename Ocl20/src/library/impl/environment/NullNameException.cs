using Microsoft.VisualStudio.Tools.Applications.Runtime;

namespace Ocl20.library.impl.environment
{
    public class NullNameException : RuntimeException {
        /**
	 * Comment for <code>serialVersionUID</code>
	 */
        private static long serialVersionUID = 1L;

        public NullNameException(string message, long errorCode) : base(message, errorCode) {
        }
    }
}
