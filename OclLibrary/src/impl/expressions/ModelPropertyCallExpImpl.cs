using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.iface.types;

namespace OclLibrary.impl.expressions
{
    public abstract class ModelPropertyCallExpImpl : PropertyCallExpImpl, ModelPropertyCallExp {

        /**
	 * @param object
	 */
        public ModelPropertyCallExpImpl() {
        }
	
        public CoreClassifier getExpressionType(OclExpression source, CoreClassifier propertyType) {
            if (source != null && source.getType().GetType() == typeof(CollectionType)) {
                CoreClassifier	elementType;
			
                if (propertyType.GetType() == typeof(CollectionType)) 
                    elementType = ((CollectionType) propertyType).getElementType();
                else
                    elementType = propertyType;
			
                if ( (source.getType().GetType() == typeof(SetType) || source.getType().GetType() == typeof(BagType)) ) {
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
