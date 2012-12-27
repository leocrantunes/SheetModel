using System;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class NullLiteralExpImpl : LiteralExpImpl, NullLiteralExp {

        public override void accept(IASTOclVisitor visitor) {
            visitor.visitNullLiteralExp(this);
        }

        public override String	ToString() {
            return "null";
        }
	
        public override Object Clone() {
            NullLiteralExpImpl theClone = (NullLiteralExpImpl) base.Clone();
            return	theClone;
        }

    }
}
