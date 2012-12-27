using System.Collections.Generic;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.impl.util
{
    public interface AstOclConstraintFactory {
        OclConstraint createModelElementInitConstraint(
            string source, CoreClassifier contextualClassifier,
            CoreModelElement element, ExpressionInOcl initialValue);

        OclConstraint createModelElementDeriveConstraint(
            string source, CoreClassifier contextualClassifier,
            CoreModelElement element, ExpressionInOcl initialValue);

        OclConstraint createAttributeInitConstraint(string source,
                                                    CoreClassifier contextualClassifier, CoreAttribute attribute,
                                                    ExpressionInOcl initialValue);

        OclConstraint createAssociationEndInitConstraint(
            string source, CoreClassifier contextualClassifier,
            CoreAssociationEnd assocEnd, ExpressionInOcl initialValue);

        OclConstraint createAttributeDeriveConstraint(
            string source, CoreClassifier contextualClassifier,
            CoreAttribute attribute, ExpressionInOcl initialValue);

        OclConstraint createAssociationEndDeriveConstraint(
            string source, CoreClassifier contextualClassifier,
            CoreAssociationEnd assocEnd, ExpressionInOcl initialValue);

        OclConstraint createInvariantConstraint(string source,
                                                string name, CoreClassifier contextualClassifier,
                                                ExpressionInOcl initialValue);

        OclBodyConstraint createBodyConstraint(string source,
                                               CoreOperation contextualOperation, ExpressionInOcl body,
                                               List<string> parameterNames);
	
        //OclActionBodyConstraint createActionBodyConstraint(string source,
        //                                                   CoreOperation contextualOperation, Action body,
        //                                                   List<string> parameterNames);

        OclPreConstraint createPreConstraint(
            OclPrePostConstraint owner, string source, string name,
            CoreOperation contextualOperation, ExpressionInOcl preCondition);

        OclPostConstraint createPostConstraint(
            OclPrePostConstraint owner, string source, string name,
            CoreOperation contextualOperation, ExpressionInOcl postCondition);

        OclPrePostConstraint createPrePostConstraint(string source,
                                                     CoreOperation contextualOperation);
	
        ExpressionInOcl createExpressionInOcl(string name, CoreModelElement contextualElement, OclExpression bodyExpression);
	
        //OclModifiableTypeDefinition createOclModifiableTypeDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, CoreClassifier type);
	
        //OclModifiableInstancesDefinition createOclModifiableInstancesDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, OclExpression instancesSource);
	
        //OclModifiableAttributesDefinition createOclModifiableAttributesDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, OclExpression instancesSource, CoreAttribute attribute);

        //OclModifiableLinksDefinition createOclModifiableLinksDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, OclExpression sourceExp, CoreAssociationEnd role, OclExpression targetExp);

        //OclModifiableDeclarationConstraint createOclModifiableDeclarationConstraint(string name, string source, CoreOperation contextualOperation);
    }
}