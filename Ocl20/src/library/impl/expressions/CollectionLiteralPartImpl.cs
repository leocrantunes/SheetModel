using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;

namespace Ocl20.library.impl.expressions
{
    public abstract class CollectionLiteralPartImpl : OclModelElementImpl, CollectionLiteralPart, IASTOclVisited, ICloneable {

        private	CoreClassifier	type;
        private	CollectionLiteralExp		literalExp;
	
        public CollectionLiteralPartImpl() {
        }
	
        public abstract void accept(IASTOclVisitor visitor);
        public abstract bool conformsTo(CoreClassifier c);
	
	
	
        /**
	 * @return Returns the literalExp.
	 */
        public CollectionLiteralExp getLiteralExp() {
            return literalExp;
        }
        /**
	 * @param literalExp The literalExp to set.
	 */
        public void setLiteralExp(CollectionLiteralExp literalExp) {
            this.literalExp = literalExp;
        }
        /**
	 * @return Returns the type.
	 */
        public CoreClassifier getType() {
            return type;
        }
        /**
	 * @param type The type to set.
	 */
        public void setType(CoreClassifier type) {
            this.type = type;
        }
	
        /* (non-Javadoc)
	 * @see impl.ocl20.expressions.OclModelElementImpl#getElemOwner()
	 */
        public override OclModelElement getElemOwner() {
            return literalExp;
        }
	
        public override Object Clone() {
            try {
                CollectionLiteralPartImpl theClone = (CollectionLiteralPartImpl) base.Clone();
                theClone.type = type;
                return	theClone; 
            } catch (Exception) {
                return	null;
            }
        }
    }
}
