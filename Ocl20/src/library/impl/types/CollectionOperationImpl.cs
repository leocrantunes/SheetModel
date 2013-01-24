using System;
using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.impl.common;
using Ocl20.parser.semantics.types;
using Environment = Ocl20.library.iface.environment.Environment;

namespace Ocl20.library.impl.types
{
    public class CollectionOperationImpl  : CollectionOperation
    {
        private CoreModelElement elemOwner;
        private CoreOperation jmiOperation;

        public bool isInvalidSumOperation() {
            CollectionType	collection = (CollectionType) getFeatureOwner();

            if (getName().Equals("sum")) {
                Environment env  = collection.getElementType().getEnvironmentWithoutParents();
                if (env != null && env.lookupOperationLocal("+").Count == 0)
                    return	true;
            }
            return	false;
        }

        public 	CoreClassifier getReturnType(List<object> argumentTypes) {
            CollectionTypeImpl	collection = (CollectionTypeImpl) getFeatureOwner();
				
            CoreClassifier	returnType = getJmiOperation().getReturnType();
		
            if (getName().Equals("collect")) {
                CoreClassifier elementType = (CoreClassifier) argumentTypes[0];
                if (elementType is CollectionTypeImpl) {
                    elementType = ((CollectionTypeImpl) elementType).getInnerMostElementType();
                }
                CoreClassifier type = collection.getFactory().createCollectionType(collection.getReturnTypeForCollect(), elementType);
                return	type;
            } 	else  if (getName().Equals("collectNested")) {
                return collection.getFactory().createCollectionType(returnType.getName(), (CoreClassifier) argumentTypes[0]);
            } 	else if (returnType.getName().Equals("<T>")) {
                return	collection.getElementType();			
            } else if (OclTypesDefinition.isOclGenericCollectionType(returnType.getName())) {
                if (returnType.getName().IndexOf("<T>") >= 0) {
                    if (((CollectionType) getFeatureOwner()).getElementType().getName().Equals("OclVoid"))  {
                        CoreClassifier	argumentElementType;
                        if (argumentTypes.Count == 0)
                            argumentElementType = collection.getElementType();
                        else if (argumentTypes[0] is CollectionTypeImpl)
                            argumentElementType = ((CollectionType) argumentTypes[0]).getElementType();
                        else
                            argumentElementType = (CoreClassifier) argumentTypes[0];
                        return collection.getFactory().createCollectionType(returnType.getName(), argumentElementType);
                    }
                    else
                        return collection.getFactory().createCollectionType(returnType.getName(), collection.getElementType());
                }
                else if (returnType.getName().IndexOf("<T2>") >= 0 )
                    return collection.getFactory().createCollectionType(returnType.getName(), collection.getInnerMostElementType());
                else
                    return	null;	
            } else  if (returnType.getName().StartsWith("Set(Tuple")) {
                if (argumentTypes.Count > 0) {
                    CollectionType collectionParameter = (CollectionType) argumentTypes[0];
				
                    TupleTypeImpl	tupleType = (TupleTypeImpl) collection.getFactory().createTupleType();
                    tupleType.addElement("first", collection.getElementType());
                    tupleType.addElement("second", collectionParameter.getElementType());
                    return collection.getFactory().createCollectionType("Set", tupleType);
                }
                else
                    return	null;
            }
            else	
                return	returnType;	
        }

        public 	CoreClassifier getReturnType() {
            List<object> args = new List<object>();
            return getReturnType(args);
        }
	
        public bool hasMatchingSignature(List<object> pParamTypes) {
            CollectionType	ownerCollection = (CollectionType) getFeatureOwner();
            List<object> parameters = new List<object>(getParametersTypesExceptReturn());

            if (parameters.Count == 0 && pParamTypes == null)
                return	true;

            if (parameters.Count != pParamTypes.Count) 
                return	false;
			
            List<object> paramTypes = new List<object>(pParamTypes);
		
            for (int i = 0; i < parameters.Count; i++) {
                CoreClassifier parameter = (CoreClassifier) parameters[i];
                CoreClassifier typeToBeMatched = (CoreClassifier) paramTypes[i];
			
                if (parameter.getName().Equals("<T>")) {
                    if (! typeToBeMatched.conformsTo(ownerCollection.getElementType()) && ! ownerCollection.getElementType().getName().Equals("OclVoid"))
                        return	false;
                }				
                else if  (OclTypesDefinition.isOclGenericCollectionType(parameter.getName())) {
                    parameter = getParameterRealType(parameter, typeToBeMatched);
				
                    if (parameter == null || (! typeToBeMatched.conformsTo(parameter) && ! parameter.getName().Equals("OclVoid")))
                        return	false;
                }
                else if (! typeToBeMatched.conformsTo(parameter) &&  ! ownerCollection.getElementType().getName().Equals("OclVoid")) {
                    return false;
                }
            }
            return true;
        }

        private	CoreClassifier getParameterRealType(CoreClassifier parameter, CoreClassifier typeToBeMatched) {
            CollectionType	ownerCollection = (CollectionType) getFeatureOwner();
		
            if (parameter.getName().IndexOf("<T>") >= 0)
                return ownerCollection.getFactory().createCollectionType(parameter.getName(), ownerCollection.getElementType());
            else {
                if (typeToBeMatched is CollectionTypeImpl) {
                    CollectionType collection = (CollectionType) typeToBeMatched;
                    return ownerCollection.getFactory().createCollectionType(parameter.getName(), collection.getElementType());
                }	
                else
                    return	null;
            }
        }

        /**
	 * @return
	 */
        public ICollection<object> getElemOwnedElements()
        {
            return getJmiOperation().getElemOwnedElements();
        }

        public void setElemOwnedElements(List<object> newValue)
        {
            getJmiOperation().setElemOwnedElements(newValue);
        }

        /**
	 * @return
	 */
        public CoreModelElement getElemOwner() {
            return elemOwner;
        }

        public void setElemOwner(CoreModelElement newValue)
        {
            elemOwner = newValue;
        }

        /**
	 * @return
	 */
        public String getFullSignatureAsString() {
            return getJmiOperation().getFullSignatureAsString();
        }
        /**
	 * @return
	 */
        public CoreModel getModel() {
            return getJmiOperation().getModel();
        }
        /**
	 * @return
	 */
        public String getName() {
            return getJmiOperation().getName();
        }
        /**
	 * @return
	 */
        public List<object> getParametersNamesExceptReturn()
        {
            return getJmiOperation().getParametersNamesExceptReturn();
        }
        /**
	 * @return
	 */
        public List<object> getParametersTypesExceptReturn() {
            return getJmiOperation().getParametersTypesExceptReturn();
        }
        /**
	 * @return
	 */
        public List<object> getParametersTypesNamesExceptReturn() {
            return getJmiOperation().getParametersTypesNamesExceptReturn();
        }
        /**
	 * @return
	 */
        public List<CoreStereotype> getTheStereotypes() {
            return getJmiOperation().getTheStereotypes();
        }

        public void setTheStereotypes(List<CoreStereotype> newValue)
        {
            throw new NotImplementedException();
        }
        
        public CoreNamespace getNamespace()
        {
            throw new NotImplementedException();
        }

        public void setNamespace(CoreNamespace newValue)
        {
            throw new NotImplementedException();
        }

        public List<object> getConnection()
        {
            throw new NotImplementedException();
        }

        public void setConnection(List<object> newValue)
        {
            throw new NotImplementedException();
        }

        public List<object> getClientDependency()
        {
            throw new NotImplementedException();
        }

        public void setClientDependency(List<object> newValue)
        {
            throw new NotImplementedException();
        }

        /**
	 * @param paramTypes
	 * @param returnType
	 * @return
	 */
        public bool hasSameSignature(List<object> paramTypes,
                                     CoreClassifier returnType) {
            return getJmiOperation().hasSameSignature(paramTypes, returnType);
                                     }
        /**
	 * @param name
	 * @return
	 */
        public bool hasStereotype(String name) {
            return getJmiOperation().hasStereotype(name);
        }
        /**
	 * @return
	 */
        public bool isInstanceScope() {
            return getJmiOperation().isInstanceScope();
        }
        /**
	 * @return
	 */
        public bool getIsQuery() {
            return getJmiOperation().getIsQuery();
        }

        public void setIsQuery(bool newValue)
        {
            getJmiOperation().setIsQuery(newValue);
        }

        public List<Parameter> getParameter()
        {
            throw new NotImplementedException();
        }

        public List<object> setParameter()
        {
            throw new NotImplementedException();
        }

        public void setParameter(List<Parameter> newValue)
        {
            throw new NotImplementedException();
        }

        /**
	 * @param name
	 * @return
	 */
        public bool operationNameMatches(String name) {
            return getJmiOperation().operationNameMatches(name);
        }
        /**
	 * @param newValue
	 */
        public void setName(String newValue) {
            getJmiOperation().setName(newValue);
        }
        /* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
        public override String ToString() {
            return getJmiOperation().ToString();
        }
	
        /**
	 * @return
	 */
        public List<object> getConstraintExpressionInOcl() {
            return getJmiOperation().getConstraintExpressionInOcl();
        }
	
	
	
        /* (non-Javadoc)
	 * @see ocl20.common.CoreOperation#getConstraint()
	 */
        public List<object> getConstraint() {
            return new List<object>();
        }

        public void setActionBody(OclActionBodyConstraint body)
        {
            throw new NotImplementedException();
        }

        public OclActionBodyConstraint getActionBody()
        {
            throw new NotImplementedException();
        }

        public void addLocalVariable(string source, VariableDeclaration variable)
        {
            throw new NotImplementedException();
        }

        public List<VariableDeclaration> getLocalVariables()
        {
            throw new NotImplementedException();
        }

        public VariableDeclaration lookupLocalVariable(string name)
        {
            throw new NotImplementedException();
        }

        public List<object> getModifiableConstraints()
        {
            throw new NotImplementedException();
        }

        /**
	 * @param operationSpec
	 */
        public void addOperationSpecification(OclOperationConstraint operationSpec) {
            getJmiOperation().addOperationSpecification(operationSpec);
        }
        /**
	 * @param source
	 */
        public void deleteAllConstraintsForSource(String source) {
            getJmiOperation().deleteAllConstraintsForSource(source);
        }
        /**
	 * @return
	 */
        public OclBodyConstraint getBodyDefinition() {
            return getJmiOperation().getBodyDefinition();
        }
        /**
	 * @return
	 */
        public List<OclOperationConstraint> getSpecifications()
        {
            return getJmiOperation().getSpecifications();
        }
        /**
	 * @return
	 */
        public bool isOclDefined() {
            return getJmiOperation().isOclDefined();
        }

        public CoreClassifier getFeatureOwner()
        {
            return (CoreClassifier) getElemOwner();
        }

        public void setFeatureOwner(CoreClassifier newValue)
        {
            setElemOwner(newValue);
        }

        public ScopeKind getOwnerScope()
        {
            return getJmiOperation().getOwnerScope();
        }

        public void setOwnerScope(ScopeKind newValue)
        {
            getJmiOperation().setOwnerScope(newValue);
        }

        /**
	 * @param body
	 */
        public void setBodyDefinition(OclBodyConstraint body) {
            getJmiOperation().setBodyDefinition(body);
        }

        public CoreOperation getJmiOperation()
        {
            return jmiOperation;
        }

        public void setJmiOperation(CoreOperation newValue)
        {
            jmiOperation = newValue;
        }
    }
}
