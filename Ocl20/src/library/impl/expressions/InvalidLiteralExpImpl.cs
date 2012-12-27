using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class InvalidLiteralExpImpl : LiteralExpImpl, InvalidLiteralExp {

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitInvalidLiteralExp(this);
        }

        public override String	ToString() {
            return "invalid";
        }
	
        public override Object Clone() {
            NullLiteralExpImpl theClone = (NullLiteralExpImpl) base.Clone();
            return	theClone;
        }
    }
}
