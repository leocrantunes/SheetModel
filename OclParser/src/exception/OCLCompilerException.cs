using System;
using OclParser.cst;

namespace OclParser.exception
{
    public class OCLCompilerException : Exception, IComparable {
	
        private const long serialVersionUID = 1L;
        private	SourceLocation	sourcePosition;		

        public OCLCompilerException(String message, SourceLocation source) : base (message) 
        {	
            sourcePosition = source;
        }
	
        public override	String ToString() {
            return this.sourcePosition + " : " + this.Message;
        }

        public int CompareTo(object obj)
        {
            OCLCompilerException other = (OCLCompilerException)obj;
            return (this.sourcePosition.CompareTo(other.sourcePosition));
        }
	
        public override bool Equals(object arg0) {
            OCLCompilerException	other = (OCLCompilerException) arg0;
		
            return this.sourcePosition.Equals(other.sourcePosition) &&
                   this.Message.Equals(other.Message);
        }

        public override int GetHashCode()
        {
            return this.sourcePosition.GetHashCode();
        }

        public SourceLocation getSourceLocation()
        {
            return this.sourcePosition;
        }
    }
}
