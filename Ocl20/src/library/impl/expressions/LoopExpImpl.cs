using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ocl20.library.iface.expressions;
using Ocl20.library.utils;

namespace Ocl20.library.impl.expressions
{
    public abstract class LoopExpImpl : PropertyCallExpImpl, LoopExp {

        private	OclExpression	body;
        private List<VariableDeclaration> iterators;
	

        /**
	 * 
	 */
        public LoopExpImpl() {
            iterators = new List<VariableDeclaration>();
        }

        public override void accept(IASTOclVisitor visitor) {
            base.accept(visitor);
        }
	
        protected String	getIteratorsAsString() {
            StringBuilder result = new StringBuilder();

            int length = this.getIterators().Count;
            int index = 0;
            foreach (VariableDeclaration iterator in this.getIterators()) {
                result.Append(iterator.ToString());
                if (index < length - 1) {
                    result.Append(", ");
                }
                index++;
            }
            return	result.ToString();
        }

        /**
	 * @return Returns the body.
	 */
        public OclExpression getBody() {
            return body;
        }
        /**
	 * @param body The body to set.
	 */
        public void setBody(OclExpression body) {
            this.body = body;
        }
        /**
	 * @return Returns the iterators.
	 */
        public List<VariableDeclaration> getIterators()
        {
            return iterators;
        }
        /**
	 * @param iterators The iterators to set.
	 */
        public void setIterators(List<VariableDeclaration> iterators)
        {
            this.iterators = iterators;
        }
	
        public void addIterator(VariableDeclaration iterator) {
            this.iterators.Add(iterator);
        }
	
        public void removeIterator(VariableDeclaration iterator) {
            this.iterators.Remove(iterator);
        }

        public override Object Clone() {
            LoopExpImpl theClone = (LoopExpImpl) base.Clone();
		
            if (iterators != null) {
                theClone.iterators = new List<VariableDeclaration>(iterators.Cast<VariableDeclarationImpl>().ToList().Clone());
                foreach (VariableDeclaration var in theClone.iterators) {
                    var.setLoopExp(theClone);
                }
            } else 
                theClone.iterators = null;
    	
            if (body != null) {
                theClone.body = (OclExpression) body.Clone();
                ((OclExpressionImpl) theClone.body).setLoopExp(theClone);
            } else 
                theClone.body = null;

            return	theClone;
        }
    }
}
