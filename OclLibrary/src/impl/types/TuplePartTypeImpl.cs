using System;
using OclLibrary.iface.common;
using OclLibrary.iface.types;
using OclLibrary.impl.common;

namespace OclLibrary.impl.types
{
    public abstract class TuplePartTypeImpl : CoreAttributeImpl, TuplePartType {

        private	CoreClassifier	featureType;
	
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
        public int compareTo(Object arg0) {
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


        public abstract TupleType getTupleType();
        public abstract void setTupleType(TupleType newValue);
    }
}
