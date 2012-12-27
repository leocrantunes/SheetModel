using System.Collections.Generic;
using OclParser.cst;

namespace OclParser.exception
{
    public class OCLMultipleSemanticExceptions : OCLSemanticException {
        private  const long serialVersionUID = 1L;
        private	List<OCLSemanticException>	exceptions;
	
        public OCLMultipleSemanticExceptions(CSTNode node, string message) : base(node, message) {
            exceptions = new List<OCLSemanticException>();
        }

        public void	add(OCLSemanticException e) {
            exceptions.Add(e);
        }
	
        public	List<OCLSemanticException>	getAllExceptions() {
            List<OCLSemanticException>	result = new List<OCLSemanticException>();
		
            foreach (OCLSemanticException e in exceptions)
            {
                var semanticExceptions = e as OCLMultipleSemanticExceptions;
                if (semanticExceptions != null) 
                {
                    result.AddRange(semanticExceptions.getAllExceptions());
                } 
                else 
                {
                    result.Add(e);
                }
            }

            return	result;
        }
    }
}
