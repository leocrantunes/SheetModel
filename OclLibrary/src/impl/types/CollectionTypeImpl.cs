using System;
using System.Collections.Generic;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.iface.util;
using OclLibrary.impl.common;
using Environment = OclLibrary.iface.environment.Environment;

namespace OclLibrary.impl.types
{
    public abstract class CollectionTypeImpl : CoreDataTypeImpl, CollectionType {

        public virtual CollectionKind getCollectionKind() {
            return	CollectionKindEnum.COLLECTION;
        }

        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreNamespace#getEnvironmentWithoutParents()
	 */
        public override Environment getEnvironmentWithoutParents() {
            return	getInnerMostElementType().getEnvironmentWithoutParents();
        }
	
        protected Environment getGenericTypeEnvironment() {
            CoreClassifier genericCollectionType = (CoreClassifier) OclTypesDefinition.getType(getCollectionTypeName() + "<T>");
            if (genericCollectionType != null) {
                return	genericCollectionType.getEnvironmentWithoutParents();
            } else {
                return	null;
            }
        }

        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#lookupOperation(java.lang.String, java.util.List)
	 */
        public override CoreOperation lookupOperation(String name, List<object> paramTypes) {
            Environment environment = getGenericTypeEnvironment();
		
            if (environment != null) {
                List<object> operations = environment.lookupOperationLocal(name);
                if (operations != null) {		
                    foreach (CoreOperation operation in operations) {
                        CollectionOperationImpl result = (CollectionOperationImpl) getFactory().createCollectionOperation(operation, this);
                        if (result.hasMatchingSignature(paramTypes) && (! result.isInvalidSumOperation()))
                            return	result;
                    }
                }			
            }
		
            return null;
        }
    
        /* (non-Javadoc)
	 * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreModelElement#getName()
	 */
        public override String getName() {
            return getCollectionTypeName() + getElementTypeName();
        }

        public override bool conformsTo(CoreClassifier c) {
            return areCollectionsCompatible(c) || isOclAnyType(c);
        }

        public CoreClassifier getInnerMostElementType() {
            CoreClassifier	innerElementType = this.getElementType();
		 
            while (innerElementType.GetType() == typeof(CollectionType)) {
                innerElementType = ((CollectionType) innerElementType).getElementType();
            }
		
            return	innerElementType;
        }

        public void setInnerMostElementType(CoreClassifier classifier) {
            CoreClassifier	innerElementType = this.getElementType();
            CollectionType	collectionType = this;
		 
            while (innerElementType.GetType() == typeof(CollectionType)) {
                collectionType = (CollectionType) innerElementType;
                innerElementType = ((CollectionType) innerElementType).getElementType();
            }
		
            collectionType.setElementType(classifier);
        }

	
        protected virtual String getCollectionTypeName() {
            return "Collection";
        }

        protected virtual String getReturnTypeForCollect() {
            return	"Bag"; 
        }
	
        protected String getElementTypeName() {
            return "(" + getElementType().getName() +  ")"; 
        }

        protected virtual bool areCollectionsCompatible(CoreClassifier c) {
            if (c.GetType() == this.GetType() || (c.getName().Equals(this.getName())) ||  (c.GetType() == typeof(CollectionType) && c.getName().StartsWith("Collection"))) 
                return elementTypesConformance(c);
            else
                return false;
        }

        protected bool elementTypesConformance(CoreClassifier c) {
            if ( ! (this.getElementType().GetType() == typeof(CollectionType)) && (((CollectionType) c).getElementType().GetType() == typeof(CollectionType))) 
                return	false;
		
            return 	this.getElementType().conformsTo(((CollectionType) c).getElementType()) || isVoidType(((CollectionType) c).getElementType());
        }

        private bool isVoidType(CoreClassifier c) {
            return 	"OclVoid".Equals(c.getName());
        }

        private bool isOclAnyType(CoreClassifier c) {
            return 	"OclAny".Equals(c.getName());	 
        }
	
        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier#getMostSpecificCommonSuperType(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
	 */
        public override CoreClassifier getMostSpecificCommonSuperType(CoreClassifier otherClassifier) {
            if (this.GetType() == otherClassifier.GetType()) {
                CollectionType	otherCollection = (CollectionType) otherClassifier;
                return	createSpecificCollectionType(getElementType().getMostSpecificCommonSuperType(otherCollection.getElementType()));
            } else if (otherClassifier.GetType() == typeof(CollectionType)) {
                CollectionType	otherCollection = (CollectionType) otherClassifier;
                return	createGenericCollectionType(getElementType().getMostSpecificCommonSuperType(otherCollection.getElementType()));
            } else {
                return	(CoreClassifier) OclTypesDefinition.getType("OclAny");			
            }
        }

        public virtual CollectionType createSpecificCollectionType(CoreClassifier elementType) {
            return	createGenericCollectionType(elementType);
        }

        public	CollectionType	createGenericCollectionType(CoreClassifier elementType) {
            return	getFactory().createCollectionType(elementType);
        }
	
        public override String ToString() {
            return this.getName();
        }

        public abstract AstOclModelElementFactory getFactory();
        public abstract void setFactory(AstOclModelElementFactory newValue);
        public abstract CoreClassifier getElementType();
        public abstract void setElementType(CoreClassifier newValue);
    }
}
