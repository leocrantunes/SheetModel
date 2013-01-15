using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.iface.util;
using Ocl20.library.impl.common;
using Ocl20.library.impl.expressions;
using Ocl20.library.impl.types;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.impl.util
{
    public class AstOclModelElementFactoryImpl : AstOclModelElementFactory {

        private Dictionary<object,object> allTypesCreated = new Dictionary<object,object>();
        private List<object> allTuplesCreated = new List<object>();
        private	Ocl20Package oclPackage;
        private Dictionary<String, CollectionOperation> allCollectionOperations = new Dictionary<String, CollectionOperation>();
        private List<object> allTupleParts = new List<object>();
    

        protected String getCurrentMethodName()
        {
            return new StackTrace().GetFrame(1).GetMethod().Name;
        }
        
        public void setOclPackage(Ocl20Package oclPackage) {
            this.oclPackage = oclPackage;
        }
    
        public void resetTypes() {
            allTypesCreated.Clear();
            allTuplesCreated.Clear();
        }

        public CollectionType createSpecificCollectionType(
            CollectionKind collectionKind,
            CoreClassifier elementType) {
            if (collectionKind.Equals(CollectionKindEnum.BAG))
                return createBagType(elementType);

            if (collectionKind.Equals(CollectionKindEnum.SET))
                return createSetType(elementType);

            if (collectionKind.Equals(CollectionKindEnum.SEQUENCE))
                return createSequenceType(elementType);

            if (collectionKind.Equals(CollectionKindEnum.ORDERED_SET))
                return createOrderedSetType(elementType);

            if (collectionKind.Equals(CollectionKindEnum.COLLECTION))
                return createCollectionType(elementType);

        
            return null;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createBagType(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
     */
        public BagType createBagType(CoreClassifier elementType) {
            BagType result = (BagType) getCollectionType("Bag(" + elementType.getName() + ")");
            if (result == null) {
                result = oclPackage.getTypes().getBagType();
                result.setFactory(this);
                result.setElementType(elementType);
                allTypesCreated.Add(result.getName(), result);
            } 
        
            return	result;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createSetType(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
     */
        public SetType createSetType(CoreClassifier elementType) {

            SetType result = (SetType) getCollectionType("Set(" + elementType.getName() + ")");
            if (result == null) {
                result = oclPackage.getTypes().getSetType();
                result.setFactory(this);
                result.setElementType(elementType);
                allTypesCreated.Add(result.getName(), result);
            }
        
            return	result;
        }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createOrderedSetType(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
     */
        public OrderedSetType createOrderedSetType(
            CoreClassifier elementType) {

            OrderedSetType result = (OrderedSetType) getCollectionType("OrderedSet(" + elementType.getName() + ")");
            if (result == null) {
                result = oclPackage.getTypes().getOrderedSetType();
                result.setFactory(this);
                result.setElementType(elementType);
                allTypesCreated.Add(result.getName(), result);
            }
        
            return	result;
            }

        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createSequenceType(br.cos.ufrj.lens.odyssey.tools.psw.metamodels.core.interfaces.CoreClassifier)
     */
        public SequenceType createSequenceType(CoreClassifier elementType) {

            SequenceType result = (SequenceType) getCollectionType("Sequence(" + elementType.getName() + ")");
            if (result == null) {
                result = oclPackage.getTypes().getSequenceType();
                result.setFactory(this);
                result.setElementType(elementType);
                allTypesCreated.Add(result.getName(), result);
            }
        
            return	result;
        }

        public CollectionType createCollectionType(CoreClassifier elementType) {

            CollectionType result = (CollectionType) getCollectionType("Collection(" + elementType.getName() + ")");
            if (result == null) {
                result = oclPackage.getTypes().getCollectionType();
                result.setFactory(this);
                result.setElementType(elementType);
                allTypesCreated.Add(result.getName(), result);
            }
        
            return	result;
        }

    
        private CollectionType getCollectionType(
            String name) {
            //CollectionType typeAlreadyCreated = (CollectionType) allTypesCreated.get(name);
            return	null;
            //return	typeAlreadyCreated;
            }

        public CollectionType createCollectionType(
            String name,
            CoreClassifier elementType) {
            if (name.StartsWith("Bag")) {
                return createBagType(elementType);
            } else if (name.StartsWith("Set")) {
                return createSetType(elementType);
            } else if (name.StartsWith("Sequence")) {
                return createSequenceType(elementType);
            } else if (name.StartsWith("OrderedSet")) {
                return createOrderedSetType(elementType);
            } else if (name.StartsWith("Collection")) {
                return createCollectionType(elementType);
            } else {
                return null;
            }
            }
    
        /* (non-Javadoc)
     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createTupleType()
     */
        public TupleType createTupleType() {
            TupleType type = oclPackage.getTypes().getTupleType();
            type.setFactory(this);
            allTuplesCreated.Add(type);
            return	type;
        }
    
        public TuplePartType  createTuplePartType(TupleType tupleType, String name, CoreClassifier type) {
            TuplePartType partType = oclPackage.getTypes().getTuplePartType();
    	
            partType.setTupleType(tupleType);
            partType.setFeatureType(type);
            partType.setName(name);

            allTupleParts.Add(partType);
    	
            return	partType;
        }

        public VariableDeclaration createVariableDeclaration(
            String varName,
            CoreClassifier varType,
            OclExpression varInitialization) {
        	
            VariableDeclaration varDecl = new VariableDeclarationImpl();
            varDecl.setFactory(this);
        	
            varDecl.setVarName(varName);
            varDecl.setType(varType);
            varDecl.setInitExpression(varInitialization);
            if (varInitialization != null)
                ((OclExpressionImpl) varInitialization).setInitializedVariable(varDecl);
        	
            return	varDecl;
            }

        public BooleanLiteralExp createBooleanLiteralExp(
            bool symbol,
            CoreClassifier type) {
            BooleanLiteralExpImpl exp = new BooleanLiteralExpImpl();
            exp.setFactory(this);
        	
            exp.setBooleanSymbol(symbol);
            exp.setType(type);
        	
            return	exp;
            }

        public IntegerLiteralExp createIntegerLiteralExp(
            long symbol,
            CoreClassifier type) {
            IntegerLiteralExp exp = new IntegerLiteralExpImpl();
            exp.setFactory(this);
        	
            exp.setIntegerSymbol(symbol);
            exp.setType(type);
        	
            return	exp;
            }

        public RealLiteralExp createRealLiteralExp(
            String symbol,
            CoreClassifier type) {
            RealLiteralExp exp = new RealLiteralExpImpl();
            exp.setFactory(this);
        	
            exp.setRealSymbol(symbol);
            exp.setType(type);

            return	exp;

            }

        public StringLiteralExp createStringLiteralExp(
            String symbol,
            CoreClassifier type) {

            StringLiteralExp exp = new StringLiteralExpImpl();
            exp.setFactory(this);
        	
            exp.setStringSymbol(symbol);
            exp.setType(type);

            return	exp;
            }

        /* (non-Javadoc)
		 * @see AstOclModelElementFactory#createInvalidLiteralExp(CoreClassifier)
		 */
        public InvalidLiteralExp createInvalidLiteralExp(CoreClassifier type) {
            InvalidLiteralExp exp = new InvalidLiteralExpImpl();
            exp.setFactory(this);
            exp.setType(type);
            return	exp;
        }
		
        /* (non-Javadoc)
		 * @see AstOclModelElementFactory#createNullLiteralExp(CoreClassifier)
		 */
        public NullLiteralExp createNullLiteralExp(CoreClassifier type) {
            NullLiteralExp exp = new NullLiteralExpImpl();
            exp.setFactory(this);
            exp.setType(type);
            return	exp;
        }
        
        
        public CollectionRange createCollectionRange(
            OclExpression from,
            OclExpression to) {
            CollectionRange exp = new CollectionRangeImpl();
            exp.setFactory(this);
        	
            exp.setFirst(from);
            ((OclExpressionImpl) from).setCollectionRange(exp);
            exp.setLast(to);
            ((OclExpressionImpl) to).setCollectionRange(exp);
        	
            exp.setType(from.getType());

            return	exp;
            }

        public CollectionItem createCollectionItem(
            OclExpression expression) {
            CollectionItem exp = new CollectionItemImpl();
            exp.setFactory(this);
        	
            exp.setItem(expression);
            ((OclExpressionImpl) expression).setCollectionItem(exp);
            exp.setType(expression.getType());
            return	exp;
            }

        public CollectionLiteralExp createCollectionLiteralExp(
            List<object> parts,
            CollectionType type) {
            CollectionLiteralExp	exp = new CollectionLiteralExpImpl();
            exp.setFactory(this);
        	
            exp.setKind(((CollectionTypeImpl) type).getCollectionKind());
            exp.setType(type);
            if (parts != null) {
                foreach (CollectionLiteralPartImpl part in parts) {
                    ((CollectionLiteralExpImpl) exp).addPart(part);
                    part.setLiteralExp(exp);
                }
            }
            return exp;
            }

        public TupleLiteralExp createTupleLiteralExp(
            List<object> tupleParts,
            CoreClassifier tupleType) {
            TupleLiteralExp exp = new TupleLiteralExpImpl();
            exp.setFactory(this);
            
            exp.setType(tupleType);
            
            foreach (VariableDeclarationImpl variable in tupleParts) {
                ((TupleLiteralExpImpl) exp).addTuplePart(variable);
                variable.setTupleLiteralExp(exp);
            }
            
            return	exp;
            }

        /* (non-Javadoc)
         * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createLetExp(List<object>, br.cos.ufrj.lens.odyssey.tools.psw.metamodels.OclExpression)
         */
        public LetExp createLetExp(
            VariableDeclaration variable,
            OclExpression expression) {
            LetExp exp = new LetExpImpl();
            exp.setFactory(this);
            
            exp.setVariable(variable);
            exp.setIn(expression);
            
            variable.setLetExp(exp);
            ((OclExpressionImpl) expression).setLetExp(exp);
            
            exp.setType(expression.getType());
            
            return	exp;
            }

        public IfExp createIfExp(
            OclExpression conditionExp,
            OclExpression thenExp,
            OclExpression elseExp) {
            IfExp exp = new IfExpImpl();
            exp.setFactory(this);

            exp.setCondition(conditionExp);
            exp.setThenExpression(thenExp);
            exp.setElseExpression(elseExp);
        	
            ((OclExpressionImpl) conditionExp).setIfExp(exp);
            ((OclExpressionImpl) thenExp).setIfExp(exp);
            ((OclExpressionImpl) elseExp).setIfExp(exp);
        	
            exp.setType(thenExp.getType().getMostSpecificCommonSuperType(elseExp.getType()));
            return	exp;
            }

        public EnumLiteralExp createEnumLiteralExp(
            CoreEnumLiteral enumLiteral) {
            EnumLiteralExp exp = new EnumLiteralExpImpl();
            exp.setFactory(this);

            exp.setType(enumLiteral.getTheEnumeration());
            exp.setReferredEnumLiteral(enumLiteral);
            return	exp;
            }

        public OclTypeLiteralExp createOclTypeLiteralExp(OclModelElementType oclModelElementType) {
            OclTypeLiteralExp exp = new OclTypeLiteralExpImpl();
            exp.setFactory(this);
            exp.setType(oclModelElementType);
            return exp;	
        }

        public OclModelElementType createOclModelElementType(CoreModelElement referredElement) {
            OclModelElementType modelElement = oclPackage.getTypes().getOclModelElementType();
            modelElement.setReferredModelElement(referredElement);
            return	modelElement;
        }

        public VariableExp createVariableExp(VariableDeclaration variable) {
            VariableExp exp = new VariableExpImpl();
            exp.setFactory(this);
        	
            exp.setReferredVariable(variable);
            ((VariableDeclarationImpl) variable).addVariableExp(exp);
            exp.setType(variable.getType());
            return	exp;
        }

        public AttributeCallExp createAttributeCallExp(
            OclExpression source,
            CoreAttribute attribute,
            bool isMarkedPre) {
            AttributeCallExp exp = new AttributeCallExpImpl();
            exp.setFactory(this);
        	
            exp.setSource(isMarkedPre ? createAtPreOperation(source) : source);
            exp.setReferredAttribute(attribute);
        	
            CoreClassifier expType;
            if (attribute.isOclDefined())
                expType = attribute.getFeatureType();
            else
                expType = attribute.getModel() != null ? attribute.getModel().toOclType(attribute.getFeatureType()) : attribute.getFeatureType();
            exp.setType(((AttributeCallExpImpl) exp).getExpressionType(source, expType));
        	
            if (source != null)
                ((OclExpressionImpl) source).setAppliedProperty(exp);
        	
            return	exp;
            }	

        public AssociationEndCallExp createAssociationEndCallExp(
            OclExpression source,
            CoreAssociationEnd referredAssociationEnd,
            CoreAssociationEnd navigationSource,
            List<object> qualifiers,
            bool isMarkedPre) {
            AssociationEndCallExpImpl exp = new AssociationEndCallExpImpl();
            exp.setFactory(this);
        	
            exp.setReferredAssociationEnd(referredAssociationEnd);
            exp.setNavigationSource(navigationSource);
            exp.setSource(isMarkedPre ? createAtPreOperation(source) : source);
            if (qualifiers != null) {
                foreach (OclExpression qualifier in qualifiers) {
                    ((OclExpressionImpl) qualifier).setNavigationCallExp(exp);
                    ((AssociationEndCallExpImpl) exp).addQualifier(qualifier);
                }
            }	
        	
            exp.setType(((AssociationEndCallExpImpl) exp).getExpressionType(source, referredAssociationEnd));

            ((OclExpressionImpl) source).setAppliedProperty(exp);
        	
            return	exp;
            }

        public AssociationClassCallExp createAssociationClassCallExp(
            OclExpression source,
            CoreAssociationClass referredAssociationClass,
            CoreAssociationEnd navigationSource,
            List<object> qualifiers,
            bool isMarkedPre) {
            AssociationClassCallExpImpl exp = new AssociationClassCallExpImpl();
            exp.setFactory(this);
        	
            exp.setReferredAssociationClass(referredAssociationClass);
            exp.setNavigationSource(navigationSource);
            exp.setSource(isMarkedPre ? createAtPreOperation(source) : source);
            if (qualifiers != null) {
                foreach (OclExpression qualifier in qualifiers) {
                    ((OclExpressionImpl) qualifier).setNavigationCallExp(exp);
                    ((AssociationClassCallExpImpl) exp).addQualifier(qualifier);
                }
            }
            CoreAssociationEnd	assocEnd = navigationSource != null ? navigationSource : referredAssociationClass.lookupAssociationEnd(source.getType()); 

            exp.setType(((AssociationClassCallExpImpl) exp).getExpressionType(source, assocEnd, referredAssociationClass));
        	
            ((OclExpressionImpl) source).setAppliedProperty(exp);
            return	exp;
            }

        public OperationCallExp createOperationCallExp(
            OclExpression source,
            CoreOperation operation,
            List<object> arguments,
            CoreClassifier returnType,
            bool isMarkedPre){
            OperationCallExp exp = new OperationCallExpImpl();
            exp.setFactory(this);
            	
            exp.setReferredOperation(operation);
            exp.setType(returnType);
            exp.setSource((isMarkedPre ? createAtPreOperation(source) : source));
            if (arguments != null) {
                foreach (OclExpression argument in arguments) {
                    ((OclExpressionImpl) argument).setParentOperation(exp);
                    ((OperationCallExpImpl) exp).addArgument(argument);
                }
            }
            ((OclExpressionImpl) source).setAppliedProperty(exp);

            return exp;
            }

        public OperationCallExp createOperationCallExp(
            CoreClassifier returnType,
            OclExpression source,
            String opName,
            List<object> arguments, 
            bool isMarkedPre) {
            OperationCallExp exp = new OperationCallExpImpl();
            exp.setFactory(this);
            	
            exp.setName(opName);
            exp.setType(returnType);
            exp.setSource((isMarkedPre ? createAtPreOperation(source) : source));
            foreach (OclExpression argument in arguments) {
                ((OclExpressionImpl) argument).setParentOperation(exp);
                ((OperationCallExpImpl) exp).addArgument(argument);
            }
            ((OclExpressionImpl) source).setAppliedProperty(exp);
            	
            return exp;
            }

        public IteratorExp createIteratorExp(
            String name,
            CoreClassifier type,
            OclExpression source,
            OclExpression body,
            List<object> iterators) {
            IteratorExp exp = new IteratorExpImpl();
            exp.setFactory(this);
        	
            exp.setType(type);
            exp.setSource(source);
            exp.setBody(body);
            exp.setName(name);
        	
            if (iterators != null) {
                foreach (VariableDeclaration var in iterators) {
                    var.setLoopExp(exp);
                    ((IteratorExpImpl) exp).addIterator(var); 
                }
            }
            if (body != null)
                ((OclExpressionImpl) body).setLoopExp(exp);
            if (source != null)
                ((OclExpressionImpl) source).setAppliedProperty(exp);
        	
            return	exp;
            }

        public IterateExp createIterateExp(
            CoreClassifier type,
            OclExpression source,
            OclExpression body,
            List<object> iterators,
            VariableDeclaration result) {
            IterateExp exp = new IterateExpImpl();
            exp.setFactory(this);
        	
            exp.setType(type);
            exp.setSource(source);
            exp.setBody(body);
            exp.setResult(result);
            exp.setName("iterate");
        	
            if (iterators != null) {
                foreach (VariableDeclaration var in iterators) {
                    var.setLoopExp(exp);
                    ((IterateExpImpl) exp).addIterator(var); 
                }
            }
            result.setBaseExp(exp);
            if (body != null)
                ((OclExpressionImpl) body).setLoopExp(exp);
            if (source != null)
                ((OclExpressionImpl) source).setAppliedProperty(exp);

            return	exp;
            }

        public OperationCallExp createAtPreOperation(OclExpression source) {
            OperationCallExp exp = createOperationCallExp(source.getType(), source, "atPre",
                                                          new List<object>(), false);
            exp.setFactory(this);
            return	exp;
        }

        public OperationCallExp createAsSetOperation(OclExpression source) {
            OperationCallExp exp = createOperationCallExp(createSpecificCollectionType(CollectionKindEnum.SET, source.getType()),
                                                          source, "asSet", new List<object>(), false);
            exp.setFactory(this);
            return	exp;
        }
        
        /* (non-Javadoc)
    	 * @see AstOclModelElementFactory#createCollectionOperation(CoreOperation, CoreClassifier)
    	 */
        public CollectionOperation createCollectionOperation(
            CoreOperation jmiOperation, CoreClassifier owner) {
    		
//    		String key = owner.getFullPathName() + jmiOperation.getName();
    		
            String key = owner.getFullPathName() + CoreModelElementNameGeneratorImpl.getInstance().generateName(jmiOperation);
            CollectionOperation operation;
            allCollectionOperations.TryGetValue(key, out operation);
    		
            if (operation == null) {
                operation = oclPackage.getTypes().getCollectionOperation();
                operation.setFeatureOwner(owner);
                operation.setJmiOperation(jmiOperation);
                allCollectionOperations.Add(key, operation);
            }

            operation.setFeatureOwner(owner);
    		
            return operation;
            }


//    public VariableDeclaration createVariableDeclaration(
//        String varName,
//        CoreClassifier varType,
//        OclExpression varInitialization) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	VariableDeclaration varDecl = oclPackage.getExpressions().getVariableDeclaration().createVariableDeclaration();
//    	varDecl.setFactory(this);
//    	
//    	varDecl.setVarName(varName);
//    	varDecl.setType(varType);
//    	varDecl.setInitExpression(varInitialization);
//    	mon1.stop();
//    	
//    	return	varDecl;
//    }
//
//    public BooleanLiteralExp createBooleanLiteralExp(
//        bool symbol,
//        CoreClassifier type) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	BooleanLiteralExp exp = oclPackage.getExpressions().getboolLiteralExp().createBooleanLiteralExp();
//    	exp.setFactory(this);
//    	
//    	exp.setboolSymbol(symbol);
//    	exp.setType(type);
//    	
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public IntegerLiteralExp createIntegerLiteralExp(
//        long symbol,
//        CoreClassifier type) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	IntegerLiteralExp exp = oclPackage.getExpressions().getIntegerLiteralExp().createIntegerLiteralExp();
//    	exp.setFactory(this);
//    	
//    	exp.setIntegerSymbol(symbol);
//    	exp.setType(type);
//    	
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public RealLiteralExp createRealLiteralExp(
//        double symbol,
//        CoreClassifier type) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	RealLiteralExp exp = oclPackage.getExpressions().getRealLiteralExp().createRealLiteralExp();
//    	exp.setFactory(this);
//    	
//    	exp.setRealSymbol(symbol);
//    	exp.setType(type);
//
//    	mon1.stop();
//
//    	return	exp;
//
//    }
//
//    public StringLiteralExp createStringLiteralExp(
//        String symbol,
//        CoreClassifier type) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//
//    	StringLiteralExp exp = oclPackage.getExpressions().getStringLiteralExp().createStringLiteralExp();
//    	exp.setFactory(this);
//    	
//    	exp.setStringSymbol(symbol);
//    	exp.setType(type);
//
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public CollectionRange createCollectionRange(
//        OclExpression from,
//        OclExpression to) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	CollectionRange exp = oclPackage.getExpressions().getCollectionRange().createCollectionRange();
//    	exp.setFactory(this);
//    	
//    	exp.setFirst(from);
//    	exp.setLast(to);
//    	exp.setType(from.getType());
//
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public CollectionItem createCollectionItem(
//        OclExpression expression) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	CollectionItem exp = oclPackage.getExpressions().getCollectionItem().createCollectionItem();
//    	exp.setFactory(this);
//    	
//    	exp.setItem(expression);
//    	exp.setType(expression.getType());
//    	mon1.stop();
//        return	exp;
//    }
//
//    public CollectionLiteralExp createCollectionLiteralExp(
//        List<object> parts,
//        CollectionType type) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	CollectionLiteralExp	exp = oclPackage.getExpressions().getCollectionLiteralExp().createCollectionLiteralExp();
//    	exp.setFactory(this);
//    	
//    	exp.setKind(((CollectionTypeImpl) type).getCollectionKind());
//    	exp.setType(type);
//    	if (parts != null) {
//        	for (Iterator iter = parts.iterator(); iter.hasNext();) {
//        		oclPackage.getExpressions().getCollectionLiteralExpLiteralPart().add(exp, (CollectionLiteralPart) iter.next());
////        		((CollectionLiteralPart) iter.next()).setLiteralExp(exp);
//        	}
//    	}
//    	mon1.stop();
//        return exp;
//    }
//
//    public TupleLiteralExp createTupleLiteralExp(
//        List<object> tupleParts,
//        CoreClassifier tupleType) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//        TupleLiteralExp exp = oclPackage.getExpressions().getTupleLiteralExp().createTupleLiteralExp();
//    	exp.setFactory(this);
//        
//        exp.setType(tupleType);
//        
//        for (Iterator iter = tupleParts.iterator(); iter.hasNext(); ) {
//    		((VariableDeclaration) iter.next()).setTupleLiteralExp(exp);
//        }
//        
//        mon1.stop();
//    	return	exp;
//    }
//
//    /* (non-Javadoc)
//     * @see br.cos.ufrj.lens.odyssey.tools.psw.metamodels.ocl20.Factory#createLetExp(List<object>, br.cos.ufrj.lens.odyssey.tools.psw.metamodels.OclExpression)
//     */
//    public LetExp createLetExp(
//        VariableDeclaration variable,
//        OclExpression expression) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//        LetExp exp = oclPackage.getExpressions().getLetExp().createLetExp();
//    	exp.setFactory(this);
//        
//        exp.setVariable(variable);
//        exp.setIn(expression);
//        exp.setType(expression.getType());
//        
//        mon1.stop();
//        return	exp;
//    }
//
//    public IfExp createIfExp(
//        OclExpression conditionExp,
//        OclExpression thenExp,
//        OclExpression elseExp) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	IfExp exp = oclPackage.getExpressions().getIfExp().createIfExp();
//    	exp.setFactory(this);
//
//    	exp.setCondition(conditionExp);
//    	exp.setThenExpression(thenExp);
//    	exp.setElseExpression(elseExp);
//    	
//    	exp.setType(thenExp.getType().getMostSpecificCommonSuperType(elseExp.getType()));
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public EnumLiteralExp createEnumLiteralExp(
//        CoreEnumLiteral enumLiteral) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	EnumLiteralExp exp = oclPackage.getExpressions().getEnumLiteralExp().createEnumLiteralExp();
//    	exp.setFactory(this);
//
//    	exp.setType(enumLiteral.getTheEnumeration());
//        exp.setReferredEnumLiteral(enumLiteral);
//        mon1.stop();
//        return	exp;
//    }
//
//    public OclTypeLiteralExp createOclTypeLiteralExp(OclModelElementType oclModelElementType) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	OclTypeLiteralExp exp = oclPackage.getExpressions().getOclTypeLiteralExp().createOclTypeLiteralExp();
//    	exp.setFactory(this);
//    	exp.setType(oclModelElementType);
//    	mon1.stop();
//        return exp;	
//    }
//
//    public OclModelElementType createOclModelElementType(CoreModelElement referredElement) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	OclModelElementType modelElement = oclPackage.getTypes().getOclModelElementType().createOclModelElementType();
//    	modelElement.setReferredModelElement(referredElement);
//    	mon1.stop();
//    	return	modelElement;
//    }
//
//    public VariableExp createVariableExp(VariableDeclaration variable) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	VariableExp exp = oclPackage.getExpressions().getVariableExp().createVariableExp();
//    	exp.setFactory(this);
//    	
//    	exp.setReferredVariable(variable);
//    	exp.setType(variable.getType());
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public AttributeCallExp createAttributeCallExp(
//        OclExpression source,
//        CoreAttribute attribute,
//        bool isMarkedPre) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	AttributeCallExp exp = oclPackage.getExpressions().getAttributeCallExp().createAttributeCallExp();
//    	exp.setFactory(this);
//    	
//    	exp.setSource(isMarkedPre ? createAtPreOperation(source) : source);
//    	exp.setReferredAttribute(attribute);
//    	
//    	CoreClassifier expType;
//    	if (attribute.isOclDefined())
//    		expType = attribute.getFeatureType();
//    	else
//    		expType = attribute.getModel() != null ? attribute.getModel().toOclType(attribute.getFeatureType()) : attribute.getFeatureType();
//    	exp.setType(((AttributeCallExpImpl) exp).getExpressionType(source, expType));
//    	mon1.stop();
//    	return	exp;
//    }	
//
//    public AssociationEndCallExp createAssociationEndCallExp(
//        OclExpression source,
//        CoreAssociationEnd referredAssociationEnd,
//        CoreAssociationEnd navigationSource,
//        List<object> qualifiers,
//        bool isMarkedPre) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	AssociationEndCallExp exp = oclPackage.getExpressions().getAssociationEndCallExp().createAssociationEndCallExp();
//    	exp.setFactory(this);
//    	
//    	exp.setReferredAssociationEnd(referredAssociationEnd);
//    	exp.setNavigationSource(navigationSource);
//    	exp.setSource(isMarkedPre ? createAtPreOperation(source) : source);
//    	if (qualifiers != null) {
//    		for (Iterator iter = qualifiers.iterator(); iter.hasNext();) {
//    			((OclExpression) iter.next()).setNavigationCallExp(exp);
//    		}
//    	}	
//    	
//    	exp.setType(((AssociationEndCallExpImpl) exp).getExpressionType(source, referredAssociationEnd));
//
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public AssociationClassCallExp createAssociationClassCallExp(
//        OclExpression source,
//        CoreAssociationClass referredAssociationClass,
//        CoreAssociationEnd navigationSource,
//        List<object> qualifiers,
//        bool isMarkedPre) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	AssociationClassCallExp exp = oclPackage.getExpressions().getAssociationClassCallExp().createAssociationClassCallExp();
//    	exp.setFactory(this);
//    	
//    	exp.setReferredAssociationClass(referredAssociationClass);
//    	exp.setNavigationSource(navigationSource);
//    	exp.setSource(isMarkedPre ? createAtPreOperation(source) : source);
//    	if (qualifiers != null) {
//        	for (Iterator iter = qualifiers.iterator(); iter.hasNext();) {
//        		((OclExpression) iter.next()).setNavigationCallExp(exp);
//        	}
//    	}
//		CoreAssociationEnd	assocEnd = navigationSource != null ? navigationSource : referredAssociationClass.lookupAssociationEnd(source.getType()); 
//
//    	exp.setType(((AssociationClassCallExpImpl) exp).getExpressionType(source, assocEnd, referredAssociationClass));
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public OperationCallExp createOperationCallExp(
//            OclExpression source,
//            CoreOperation operation,
//            List<object> arguments,
//            CoreClassifier returnType,
//            bool isMarkedPre){
//    		Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//        	OperationCallExp exp = oclPackage.getExpressions().getOperationCallExp().createOperationCallExp();
//        	exp.setFactory(this);
//        	
//        	exp.setReferredOperation(operation);
//        	exp.setType(returnType);
//        	exp.setSource((isMarkedPre ? createAtPreOperation(source) : source));
//        	if (arguments != null) {
//        		for (Iterator iter = arguments.iterator(); iter.hasNext(); ) {
//        			((OclExpression) iter.next()).setParentOperation(exp);
//        		}
//        	}
//        	mon1.stop();
//            return exp;
//    }
//
//    public OperationCallExp createOperationCallExp(
//            CoreClassifier returnType,
//            OclExpression source,
//            String opName,
//            List<object> arguments, 
//            bool isMarkedPre) {
//    		Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//        	OperationCallExp exp = oclPackage.getExpressions().getOperationCallExp().createOperationCallExp();
//        	exp.setFactory(this);
//        	
//        	exp.setName(opName);
//        	exp.setType(returnType);
//        	exp.setSource((isMarkedPre ? createAtPreOperation(source) : source));
//            for (Iterator iter = arguments.iterator(); iter.hasNext(); ) {
//        		OclExpression argument = (OclExpression) iter.next();
//        		argument.setParentOperation(exp);
//            }
//        	
//            mon1.stop();
//            return exp;
//    }
//
//    public IteratorExp createIteratorExp(
//    	String name,
//        CoreClassifier type,
//        OclExpression source,
//        OclExpression body,
//        List<object> iterators) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	IteratorExp exp = oclPackage.getExpressions().getIteratorExp().createIteratorExp();
//    	exp.setFactory(this);
//    	
//    	exp.setType(type);
//    	exp.setSource(source);
//    	exp.setBody(body);
//    	exp.setName(name);
//    	
//    	if (iterators != null) {
//            for (Iterator iter = iterators.iterator(); iter.hasNext(); ) {
//        		((VariableDeclaration) iter.next()).setLoopExp(exp);
//            }
//    	}
//    	
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public IterateExp createIterateExp(
//        CoreClassifier type,
//        OclExpression source,
//        OclExpression body,
//        List<object> iterators,
//        VariableDeclaration result) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//    	IterateExp exp = oclPackage.getExpressions().getIterateExp().createIterateExp();
//    	exp.setFactory(this);
//    	
//    	exp.setType(type);
//    	exp.setSource(source);
//    	exp.setBody(body);
//    	exp.setResult(result);
//    	exp.setName("iterate");
//    	
//    	if (iterators != null) {
//            for (Iterator iter = iterators.iterator(); iter.hasNext(); ) {
//        		((VariableDeclaration) iter.next()).setLoopExp(exp);
//            }
//    	}
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public OperationCallExp createAtPreOperation(OclExpression source) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//        OperationCallExp exp = createOperationCallExp(source.getType(), source, "atPre",
//            new ArrayList(), false);
//    	exp.setFactory(this);
//    	mon1.stop();
//    	return	exp;
//    }
//
//    public OperationCallExp createAsSetOperation(OclExpression source) {
//    	Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//        OperationCallExp exp = createOperationCallExp(createSpecificCollectionType(CollectionKindEnum.SET, source.getType()),
//                source, "asSet", new ArrayList(), false);
//    	exp.setFactory(this);
//    	mon1.stop();
//    	return	exp;
//    }
//    
//    /* (non-Javadoc)
//	 * @see AstOclModelElementFactory#createCollectionOperation(CoreOperation, CoreClassifier)
//	 */
//	public CollectionOperation createCollectionOperation(
//			CoreOperation jmiOperation, CoreClassifier owner) {
//		Monitor mon1 = MonitorFactory.start(this.getCurrentMethodName());
//		
////		String key = owner.getFullPathName() + jmiOperation.getName();
//		
//		String key = CoreModelElementNameGeneratorImpl.getInstance().generateName(jmiOperation);
//		CollectionOperation operation = (CollectionOperation) allCollectionOperations.get(key);
//		
//		if (operation == null) {
//			operation = oclPackage.getTypes().getCollectionOperation().createCollectionOperation();
//			operation.setFeatureOwner(owner);
//			operation.setJmiOperation(jmiOperation);
//			allCollectionOperations.put(key, operation);
//		}
//
//		operation.setFeatureOwner(owner);
//		
//		mon1.stop();	
//
//		return operation;
//	}
    }
}
