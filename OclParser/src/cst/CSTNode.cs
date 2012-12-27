using System.Collections.Generic;
using OclParser.controller;
using OclParser.exception;

namespace OclParser.cst
{
    abstract public class CSTNode : VisitableElement {
    
        public virtual void accept(CSTVisitor visitor) {
            throw new OCLSemanticException(this, "node not expected : " + this.GetType().Name);
        }

        protected void accept(List<object> parts, CSTVisitor visitor)
        {
            OCLMultipleSemanticExceptions exceptions = new OCLMultipleSemanticExceptions(this, "");

            foreach (CSTNode part in parts)
            {
                try
                {
                    if (part != null)
                    {
                        part.accept(visitor);
                    }
                }
                catch (OCLSemanticException e)
                {
                    exceptions.add(e);
                }
            }

            if (exceptions.getAllExceptions().Count > 0)
                throw exceptions;

        }

        public virtual OCLWorkbenchToken getToken() {
            return new OCLWorkbenchToken("unknown", 0, 0);
        }
    }
}
