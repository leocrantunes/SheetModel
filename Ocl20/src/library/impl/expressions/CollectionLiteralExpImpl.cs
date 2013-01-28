using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.impl.types;
using Ocl20.library.utils;

namespace Ocl20.library.impl.expressions
{
    public class CollectionLiteralExpImpl : LiteralExpImpl, CollectionLiteralExp {

        private	CollectionKind	kind;
        private List<CollectionLiteralPart> parts;
	
        /**
	 * @param object
	 */
        public CollectionLiteralExpImpl() {
            parts = new List<CollectionLiteralPart>();
        }

        public override void accept(IASTOclVisitor visitor) {
            foreach (CollectionLiteralPartImpl part in this.getParts()) {
                part.accept(visitor);
            }

            visitor.visitCollectionLiteralExp(this);
        }



        public override bool conformsTo(CoreClassifier type) {
            if (!(type is CollectionTypeImpl))
                return	false;
	
            if (((CollectionTypeImpl) type).getCollectionKind() == CollectionKindEnum.COLLECTION ||
                ((CollectionTypeImpl) type).getCollectionKind() == this.getKind()) {
                    CoreClassifier targetType = ((CollectionType) type).getElementType();
		
                    foreach (CollectionLiteralPartImpl collectionItem in getParts()) {
                        if (! collectionItem.conformsTo(targetType))
                            return	false;
                    }
                }
            else
                return	false;	
		
            return true;
        }

        public override String	ToString() {
            return	this.getCollectionTypeNameWithoutElementType() + "{ " + getCollectionPartsAsString() +  " }"; 
        }
	
        public String	getCollectionTypeNameWithoutElementType() {
            return	this.getType().getName().MySubstring(0, this.getType().getName().IndexOf('('));
        }

        private	String	getCollectionPartsAsString() {
            StringBuilder result = new StringBuilder();

            int length = getParts().Count;
            int index = 0;
            foreach (CollectionLiteralPartImpl part  in this.getParts()) {
                result.Append(part.ToString());
                if (index < length - 1) {
                    result.Append(", ");
                }
                index++;
            }
            return	result.ToString();
        }
	
	
        /**
	 * @return Returns the kind.
	 */
        public CollectionKind	getKind() {
            return kind;
        }
        /**
	 * @param kind The kind to set.
	 */
        public void setKind(CollectionKind	kind) {
            this.kind = kind;
        }
	
        /**
	 * @return Returns the parts.
	 */
        public List<CollectionLiteralPart> getParts()
        {
            return parts;
        }
        /**
	 * @param parts The parts to set.
	 */
        public void setParts(List<CollectionLiteralPart> parts)
        {
            this.parts = parts;
        }
	
        public void addPart(CollectionLiteralPart part) {
            this.parts.Add(part);
        }
	
        public void removePart(CollectionLiteralPart part) {
            this.parts.Remove(part);
        }

        public override Object Clone() {
            CollectionLiteralExpImpl theClone = (CollectionLiteralExpImpl) base.Clone();
            theClone.kind = kind;
		
            if (parts != null) {
                theClone.parts = new List<CollectionLiteralPart>(parts.Cast<CollectionLiteralPartImpl>().ToList().Clone());

                foreach (CollectionLiteralPartImpl part in theClone.parts) {
                    part.setLiteralExp(theClone);
                }
            }
            return	theClone;
        }
	
    }
}
