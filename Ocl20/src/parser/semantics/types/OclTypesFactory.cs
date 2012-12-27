using System;
using Ocl20.library.iface.common;
using Ocl20.library.iface.types;
using Ocl20.library.impl.types;
using Ocl20.library.impl.util;
using Ocl20.parser.typeChecker;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.parser.semantics.types
{
    public class OclTypesFactory {
        private	static Environment environment = null;
        private	static	CoreModel	model = null;
        private	static CoreClassifier	result;

	
        public	static		CoreClassifier createOclIntegerType() {
            return	getType("Integer");
        }
	
        public	static		CoreClassifier createOclRealType() {
            return	getType("Real");
        }

        public	static		CoreClassifier createOclBooleanType() {
            return	getType("Boolean");
        }

        public	static		CoreClassifier createOclStringType() {
            return	getType("String");
        }
	
        public	static		CoreClassifier	createOclDateType() {
            return	getType("Date");
        }

        public	static		CoreClassifier	createOclDateTimeType() {
            return	getType("DateTime");
        }

        public static CoreClassifier getType(String typeName) {
            if (environment != null)
                return	(CoreClassifier) environment.lookup(typeName);
            else
                return	null;
        }

        public static void setEnvironment(CoreModel m) {
            model = m;
            environment = m.getEnvironmentWithoutParents();
        }
	
        public static CoreClassifier createTypeFromString(String classifierName) {
            result = null;
	    
            if (! OclTypesDefinition.isOclCollectionType(classifierName) && 
                ! OclTypesDefinition.isOclTupleType(classifierName)) 
                result = getType(classifierName);
		
            if (result == null) {
                PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, new ConstraintSourceTrackerImpl());
                result = oclCompiler.parseType(environment, classifierName);
                if (result.GetType() == typeof(CollectionType)) {
                    ((CollectionTypeImpl) result).setInnerMostElementType(getType(((CollectionTypeImpl) result).getInnerMostElementType().getName()));
                }
            }
	    
            return	result;
        }
	
        public static CoreAssociation	getAssociation(String associationName) {
            return	 model.getAssociationForName(associationName);
        }
	
        public static SetType createSetType(CoreClassifier elementType) {
            if (model != null)
                return	AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createSetType(elementType);
            else
                return	null;
        }
	
        public static BagType createBagType(CoreClassifier elementType) {
            if (model != null)
                return	AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createBagType(elementType);
            else
                return	null;
        }

        public static SequenceType createSequenceType(CoreClassifier elementType) {
            if (model != null)
                return	AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createSequenceType(elementType);
            else
                return	null;
        }

        public static OrderedSetType createOrderedSetType(CoreClassifier elementType) {
            if (model != null)
                return	AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createOrderedSetType(elementType);
            else
                return	null;
        }

        public static TupleType	createTupleType() {
            if (model != null)
                return	AstOclModelElementFactoryManager.getInstance(model.getOclPackage()).createTupleType();
            else
                return	null;
        }

    }
}
