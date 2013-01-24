using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.impl.types;

namespace Ocl20.library.impl.expressions
{
    public abstract class ModelPropertyCallExpImpl : PropertyCallExpImpl, ModelPropertyCallExp {

        /**
	 * @param object
	 */
        public ModelPropertyCallExpImpl() {
        }
	
        public CoreClassifier getExpressionType(OclExpression source, CoreClassifier propertyType) {
            if (source != null && source.getType() is CollectionTypeImpl) {
                CoreClassifier	elementType;
			
                if (propertyType is CollectionTypeImpl) 
                    elementType = ((CollectionType) propertyType).getElementType();
                else
                    elementType = propertyType;
			
                if ( (source.getType() is SetTypeImpl || source.getType() is BagTypeImpl) ) {
                    return  getFactory().createBagType(elementType); 
                } else {
                    return  getFactory().createSequenceType(elementType);
                }
            }
            else
                return	propertyType;
        }
	
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.expressions.ASTOclExpression#accept(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.IASTOclVisitor)
	 */
        public override void accept(IASTOclVisitor visitor) {
            base.accept(visitor);
        }

        protected abstract String getSpecificString();
	
        public override String ToString() {
            String	sourceAsString = getSource().ToString();
	
            if (sourceAsString.EndsWith("@pre")) {
                int indexPre = sourceAsString.LastIndexOf("@pre", StringComparison.Ordinal);
                return sourceAsString.Substring(0, indexPre) + "." + getSpecificString() + "@pre";
            } else {
                return	getSource().ToString() + "." + getSpecificString();
            }	
        }	


    }
}
