using System;
using System.Collections.Generic;
using System.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.utils;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.impl.expressions
{
    public abstract class NavigationCallExpImpl : ModelPropertyCallExpImpl, NavigationCallExp {

        private	CoreAssociationEnd	navigationSource;
        private List<OclExpression> qualifiers;
	
        /**
	 * @param object
	 */
        public NavigationCallExpImpl() {
            qualifiers = new List<OclExpression>();
        }

        public CoreClassifier getExpressionType(CoreClassifier sourceType, CoreAssociationEnd associationEnd, CoreClassifier elementType) {
            if (sourceType.GetType() == typeof(CollectionType)) {
                if ( (sourceType.GetType() == typeof(SetType) || sourceType.GetType() == typeof(BagType)) && (! associationEnd.isOrdered()) 
                    ) {
                        return  getFactory().createBagType(elementType);
                    } else {
                        return  getFactory().createSequenceType(elementType);
                    }
            }
            else {
                if (isSingleElementAccess(sourceType, associationEnd)) {
                    return elementType;			
                } else if (associationEnd.isOrdered()){
                    return  getFactory().createOrderedSetType(elementType);
                }	else {
                    return  getFactory().createSetType(elementType);
                }
            }
        }

        private	bool isSingleElementAccess(CoreClassifier sourceType, CoreAssociationEnd associationEnd) {
            return 
                (sourceType.GetType() == typeof(CoreAssociationClass)) ||
                (associationEnd.isOneMultiplicity() && ! associationEndHasQualifiers(associationEnd)) ||
                (associationEnd.isOneMultiplicity() && associationEndHasQualifiers(associationEnd) && getQualifiers().Count > 0);
        }

        private	bool associationEndHasQualifiers(CoreAssociationEnd associationEnd) {
            return	associationEnd.getTheQualifiers() != null && associationEnd.getTheQualifiers().Count > 0;
        }

	
        /**
	 * @return Returns the navigationSource.
	 */
        public CoreAssociationEnd getNavigationSource() {
            return navigationSource;
        }
        /**
	 * @param navigationSource The navigationSource to set.
	 */
        public void setNavigationSource(CoreAssociationEnd navigationSource) {
            this.navigationSource = navigationSource;
        }
	
	
	
        /**
	 * @return Returns the qualifiers.
	 */
        public List<OclExpression> getQualifiers()
        {
            return qualifiers;
        }
        /**
	 * @param qualifiers The qualifiers to set.
	 */
        public void setQualifiers(List<OclExpression> qualifiers)
        {
            this.qualifiers = qualifiers;
        }
	
        public void addQualifier(OclExpression qualifier) {
            this.qualifiers.Add(qualifier);
        }
	
        public void removeQualifier(OclExpression qualifier) {
            this.qualifiers.Remove(qualifier);
        }
	
        public override Object Clone() {
            NavigationCallExpImpl theClone = (NavigationCallExpImpl) base.Clone();
		
            theClone.navigationSource = navigationSource;
            if (qualifiers != null)
                theClone.qualifiers = new List<OclExpression>(qualifiers.Cast<OclExpressionImpl>().ToList().Clone());
                
            else
                theClone.qualifiers = null;
		
            return	theClone;
        }
	
    }
}
