/**
 * CoreOperation object instance interface.
 */

using System.Collections.Generic;
using OclLibrary.iface.constraints;
using OclLibrary.iface.expressions;
using OclLibrary.impl.constraints;

namespace OclLibrary.iface.common
{
    public interface CoreOperation : CoreBehavioralFeature, OclConstraintOwner {
        /**
     * @param name 
     * @return 
     */
        //bool hasStereotype(string name);
        /**
     * @param paramTypes 
     * @return 
     */
        bool hasMatchingSignature(List<object> paramTypes);
        /**
     * @param paramTypes 
     * @param returnType 
     * @return 
     */
        bool hasSameSignature(List<object> paramTypes, CoreClassifier returnType);
        /**
     * @param name 
     * @return 
     */
        bool operationNameMatches(string name);
        /**
     * @return 
     */
        List<object> getParametersTypesExceptReturn();
        /**
     * @return 
     */
        List<object> getParametersNamesExceptReturn();
        /**
     * @return 
     */
        CoreClassifier getReturnType();
        /**
     * @return 
     */
        string getFullSignatureAsString();
        /**
     * @return 
     */
        List<object> getParametersTypesNamesExceptReturn();
        /**
     * @return 
     */
        //bool isQuery();
        /**
     * @param operationSpec 
     */
        void addOperationSpecification(OclOperationConstraint operationSpec);
        /**
     * @return 
     */
        List<OclOperationConstraint> getSpecifications();
        /**
     * @param body 
     */
        void setBodyDefinition(OclBodyConstraint body);
        /**
     * @return 
     */
        OclBodyConstraint getBodyDefinition();
        /**
     * @param source 
     */
        //void deleteAllConstraintsForSource(string source);
        /**
     * Returns the value of reference constraint.
     * @return Value of reference constraint.
     */
        List<object> getConstraint();
    
        void setActionBody(OclActionBodyConstraint body);
    
        OclActionBodyConstraint getActionBody();
    
        void addLocalVariable(string source, VariableDeclaration variable);

        List<VariableDeclaration> getLocalVariables();

        VariableDeclaration lookupLocalVariable(string name);
    
        //void addOperationModifiableDefinition(OclModifiableDeclarationConstraint modifiableDefinition);
    
        List<object> getModifiableConstraints();

    }
}
