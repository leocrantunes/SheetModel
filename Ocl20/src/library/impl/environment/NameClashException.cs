using System;

namespace Ocl20.library.impl.environment
{
    public class NameClashException : Exception {
        /**
	 * Comment for <code>serialVersionUID</code>
	 */
        private static long serialVersionUID = 1L;

        public NameClashException(string message) : base(message) {
        }
    }
}
