using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.types;
using Ocl20.library.impl.common;

namespace Ocl20.library.impl.types
{
    public class TuplePartTypeImpl : CoreAttributeImpl, TuplePartType {

        private	CoreClassifier	featureType;
        private TupleType tupleType;
	
        /* (non-Javadoc)
	 * @see impl.ocl20.common.CoreStructuralFeatureImpl#getFeatureType()
	 */
        public override CoreClassifier getFeatureType() {
            return this.featureType;
        }
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreStructuralFeature#setFeatureType(ocl20.common.CoreClassifier)
	 */
        public override void setFeatureType(CoreClassifier newValue) {
            this.featureType = newValue;
        }
	
        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreAttribute#isInstanceScope()
	 */
        public override bool isInstanceScope() {
            return true;
        }

        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreAttribute#isClassifierScope()
	 */
        public bool isClassifierScope() {
            return false;
        }

        /* (non-Javadoc)
 	* @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreAttribute#getOwner()
 	*/
        public override CoreModelElement getElemOwner() {
            return getTupleType();
        }
	
        /* (non-Javadoc)
	 * @see java.lang.Comparable#compareTo(java.lang.Object)
	 */
        public override int CompareTo(Object arg0) {
            return 0;
        }

        public	Object[] getOwnedElements() {
            return	null;
        }
	
        /* (non-Javadoc)
	 * @see impl.ocl20.common.CoreFeatureImpl#isOclDefined()
	 */
        public override bool isOclDefined() {
            // TODO Auto-generated method stub
            return base.isOclDefined();
        }

        public TupleType getTupleType()
        {
            return tupleType;
        }

        public void setTupleType(TupleType newValue)
        {
            tupleType = newValue;
        }
    }
}
