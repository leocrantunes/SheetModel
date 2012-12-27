using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public class CollectionItemImpl : CollectionLiteralPartImpl, CollectionItem
    {
        private OclExpression item;

        public CollectionItemImpl()
        {
        }

        public override bool conformsTo(CoreClassifier c)
        {
            return this.getItem().getType().conformsTo(c);
        }

        public override void accept(IASTOclVisitor visitor)
        {
            ((OclExpressionImpl) getItem()).accept(visitor);
        }


        public override String ToString()
        {
            return this.getItem().ToString();
        }


        /**
         * @return Returns the item.
         */

        public OclExpression getItem()
        {
            return item;
        }

        /**
         * @param item The item to set.
         */

        public void setItem(OclExpression item)
        {
            this.item = item;
        }

        public override Object Clone()
        {
            CollectionItemImpl theClone = (CollectionItemImpl) base.Clone();
            theClone.item = (OclExpression) item.Clone();

            ((OclExpressionImpl) theClone.item).setCollectionItem(theClone);

            return theClone;
        }
    }
}