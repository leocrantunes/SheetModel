using System;
using OclLibrary.iface.expressions;

namespace OclLibrary.impl.expressions
{
    public class IteratorExpImpl : LoopExpImpl, IteratorExp {

        /**
	 * 
	 */
        public IteratorExpImpl() {
        }
        public override void accept(IASTOclVisitor visitor) {
            base.accept(visitor);
            visitor.visitIteratorExp(this);
        }

        public	String	toString() {
            return	this.getSource().ToString() + "->" + this.getName() + "(" + getIteratorsAsString() + " | " + getBody().ToString() +  ")";
        }
	
    }
}
