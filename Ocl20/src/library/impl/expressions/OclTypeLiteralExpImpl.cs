using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class OclTypeLiteralExpImpl : LiteralExpImpl, OclTypeLiteralExp {

        /**
	 * 
	 */
        public OclTypeLiteralExpImpl() {
        }

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitOclTypeLiteralExp(this);
        }

        public override String ToString() {
            return	this.getType().ToString();
        }
    }
}
