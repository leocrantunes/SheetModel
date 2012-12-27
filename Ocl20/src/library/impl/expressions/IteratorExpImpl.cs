using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
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

        public override String	ToString() {
            return	this.getSource().ToString() + "->" + this.getName() + "(" + getIteratorsAsString() + " | " + getBody().ToString() +  ")";
        }
	
    }
}
