using System.Collections.Generic;
using System.Diagnostics;
using Ocl20.library.iface;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl.common;
using Ocl20.library.impl.constraints;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace Ocl20.library.impl.util
{
    public class AstOclConstraintFactoryImpl : AstOclConstraintFactory {

        private	Ocl20Package	oclPackage;
	
//	private List cachedObjects = new ArrayList();

        public AstOclConstraintFactoryImpl(Ocl20Package oclPackage) {
            this.oclPackage = oclPackage;
        }

        protected string getCurrentMethodName()
        {
            return new StackTrace().GetFrame(1).GetMethod().Name;
        }

        public	OclConstraint	createModelElementInitConstraint(string source, CoreClassifier contextualClassifier, CoreModelElement element, ExpressionInOcl initialValue) {
            if (element is CoreAttributeImpl) {
                return	createAttributeInitConstraint(source, contextualClassifier, (CoreAttribute) element, initialValue);
            } else if (element is CoreAssociationEndImpl) {
                return	createAssociationEndInitConstraint(source, contextualClassifier, (CoreAssociationEnd) element, initialValue);
            } else
                return	null;
        }

        public	OclConstraint	createModelElementDeriveConstraint(string source, CoreClassifier contextualClassifier, CoreModelElement element, ExpressionInOcl initialValue) {
            if (element is CoreAttributeImpl) {
                return	createAttributeDeriveConstraint(source, contextualClassifier, (CoreAttribute) element, initialValue);
            } else if (element is CoreAssociationEndImpl) {
                return	createAssociationEndDeriveConstraint(source, contextualClassifier, (CoreAssociationEnd) element, initialValue);
            } else
                return	null;
        }

	
	
        public	OclConstraint	createAttributeInitConstraint(string source, CoreClassifier contextualClassifier, CoreAttribute attribute, ExpressionInOcl initialValue) {
            OclAttributeInitConstraint constraint = new OclAttributeInitConstraintImpl();
		
            constraint.setSource(source);
            constraint.setContextualClassifier(contextualClassifier);
            constraint.setInitializedAttribute(attribute);
            constraint.setExpression(initialValue);
		
            contextualClassifier.addInitConstraint(attribute.getName(), constraint); 
            attribute.setInitialValueExpression(initialValue);
		
//		cachedObjects.add(constraint);
		
            return	constraint;
        }

        public	OclConstraint	createAssociationEndInitConstraint(string source, CoreClassifier contextualClassifier, CoreAssociationEnd assocEnd, ExpressionInOcl initialValue) {
            OclAssocEndInitConstraint constraint = new OclAssocEndInitConstraintImpl();
		
            constraint.setSource(source);
            constraint.setContextualClassifier(contextualClassifier);
            constraint.setInitializedAssocEnd(assocEnd);
            constraint.setExpression(initialValue);
		
            contextualClassifier.addInitConstraint(assocEnd.getName(), constraint);
		
//		cachedObjects.add(constraint);
		
            return	constraint;
        }

        public	OclConstraint	createAttributeDeriveConstraint(string source, CoreClassifier contextualClassifier, CoreAttribute attribute, ExpressionInOcl initialValue) {
            OclAttributeDeriveConstraint constraint = new OclAttributeDeriveConstraintImpl();
		
            constraint.setSource(source);
            constraint.setContextualClassifier(contextualClassifier);
            constraint.setDerivedAttribute(attribute);
            constraint.setExpression(initialValue);

            contextualClassifier.addDeriveConstraint(attribute.getName(), constraint);
            attribute.setDerivedValueExpression(initialValue);
		
//		cachedObjects.add(constraint);
		
            return	constraint;
        }

        public	OclConstraint	createAssociationEndDeriveConstraint(string source, CoreClassifier contextualClassifier, CoreAssociationEnd assocEnd, ExpressionInOcl initialValue) {
            OclAssocEndDeriveConstraint constraint = new OclAssocEndDeriveConstraintImpl();
		
            constraint.setSource(source);
            constraint.setContextualClassifier(contextualClassifier);
            constraint.setDerivedAssocEnd(assocEnd);
            constraint.setExpression(initialValue);
		
            contextualClassifier.addDeriveConstraint(assocEnd.getName(), constraint);
		
//		cachedObjects.add(constraint);
		
            return	constraint;
        }

        public	OclConstraint	createInvariantConstraint(string source, string name, CoreClassifier contextualClassifier, ExpressionInOcl initialValue) {
            OclInvariantConstraint constraint = new OclInvariantConstraintImpl();

            constraint.setSource(source);
            constraint.setContextualClassifier(contextualClassifier);
            constraint.setExpression(initialValue);
            constraint.setName(name);
		
            contextualClassifier.addInvariantConstraint(name, constraint);
		
            return	constraint;	
        }

        public	OclBodyConstraint	createBodyConstraint(string source, CoreOperation contextualOperation, ExpressionInOcl body, List<string> parameterNames) {
            OclBodyConstraint constraint = new OclBodyConstraintImpl();
		
            constraint.setSource(source);
            constraint.setContextualOperation(contextualOperation);
            constraint.setExpression(body);
            constraint.setParameterNames(parameterNames);

            contextualOperation.setBodyDefinition(constraint);
	
//		cachedObjects.add(constraint);
		
            return	constraint;
        }

//        public	OclActionBodyConstraint	createActionBodyConstraint(string source, CoreOperation contextualOperation, Action body, List<string> parameterNames) {
//            OclActionBodyConstraint constraint = new OclActionBodyConstraintImpl();
		
//            constraint.setSource(source);
//            constraint.setContextualOperation(contextualOperation);
//            constraint.setAction(body);
//            constraint.setParameterNames(parameterNames);

//            contextualOperation.setActionBody(constraint);
	
////		cachedObjects.add(constraint);
		
//            return	constraint;
//        }

        public	OclPreConstraint createPreConstraint(OclPrePostConstraint owner, string source, string name, CoreOperation contextualOperation, ExpressionInOcl preCondition) {
            OclPreConstraint constraint = new OclPreConstraintImpl();

            constraint.setSource(source);
            constraint.setContextualOperation(contextualOperation);
            constraint.setExpression(preCondition);
            constraint.setName(name);
            constraint.setOwner(owner);
            owner.addPreCondition(constraint);

//		cachedObjects.add(constraint);
		
            return	constraint;
        }

        public	OclPostConstraint		createPostConstraint(OclPrePostConstraint owner, string source, string name, CoreOperation contextualOperation, ExpressionInOcl postCondition) {
            OclPostConstraint constraint = new OclPostConstraintImpl();

            constraint.setSource(source);
            constraint.setContextualOperation(contextualOperation);
            constraint.setExpression(postCondition);
            constraint.setName(name);
            constraint.setOwner(owner);
            owner.addPostCondition(constraint);

//		cachedObjects.add(constraint);
		
            return	constraint;
        }

        public	OclPrePostConstraint	createPrePostConstraint(string source, CoreOperation contextualOperation) {
            OclPrePostConstraint constraint = new OclPrePostConstraintImpl();

            constraint.setSource(source);
            constraint.setContextualOperation(contextualOperation);
	
            contextualOperation.addOperationSpecification(constraint);
		
            return	constraint;
        }
	
        public ExpressionInOcl createExpressionInOcl(string name, CoreModelElement contextualElement, OclExpression bodyExpression) {
            ExpressionInOcl exp = new ExpressionInOclImpl();
            ((ExpressionInOclImpl) exp).setBodyExpression(bodyExpression);
            exp.setContextualElement(contextualElement);
            exp.setName(name);
		
            return	exp;
        }

        //public	OclModifiableTypeDefinition createOclModifiableTypeDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, CoreClassifier type) {
        //    OclModifiableTypeDefinition constraint = new OclModifiableTypeDefinition();
		
        //    constraint.setSource(source);
        //    constraint.setContextualOperation(contextualOperation);
        //    constraint.setClassifier(type);
        //    owner.addTypeDefinitionConstraint(constraint);
		
        //    return	constraint;
        //}
	
        //public	OclModifiableInstancesDefinition createOclModifiableInstancesDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, OclExpression instancesSource) {
        //    OclModifiableInstancesDefinition constraint = new OclModifiableInstancesDefinition();
		
        //    constraint.setSource(source);
        //    constraint.setContextualOperation(contextualOperation);
        //    constraint.setInstancesExpression(instancesSource);
        //    owner.addInstancesDefinitionConstraint(constraint);
		
        //    return	constraint;
        //}
	
        //public	OclModifiableAttributesDefinition createOclModifiableAttributesDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, OclExpression instancesSource, CoreAttribute attribute) {
        //    OclModifiableAttributesDefinition constraint = new OclModifiableAttributesDefinition();
		
        //    constraint.setSource(source);
        //    constraint.setContextualOperation(contextualOperation);
        //    constraint.setInstancesExpression(instancesSource);
        //    constraint.setAttribute(attribute);
        //    owner.addAttributesDefinitionConstraint(constraint);
		
        //    return	constraint;
        //}

        //public	OclModifiableLinksDefinition createOclModifiableLinksDefinition(CoreOperation contextualOperation, OclModifiableDeclarationConstraint owner, string source, OclExpression sourceExp, CoreAssociationEnd role, OclExpression targetExp) {
        //    OclModifiableLinksDefinition constraint = new OclModifiableLinksDefinition();
		
        //    constraint.setSource(source);
        //    constraint.setContextualOperation(contextualOperation);
        //    constraint.setSourceInstancesExpression(sourceExp);
        //    constraint.setAssociationEnd(role);
        //    constraint.setTargetInstancesExpression(targetExp);
        //    owner.addLinksDefinitionConstraint(constraint);
		
        //    return	constraint;
        //}

        //public	OclModifiableDeclarationConstraint createOclModifiableDeclarationConstraint(string name, string source, CoreOperation contextualOperation) {
        //    OclModifiableDeclarationConstraint constraint = new OclModifiableDeclarationConstraint();
		
        //    constraint.setName(name);
        //    constraint.setSource(source);
        //    constraint.setContextualOperation(contextualOperation);

        //    contextualOperation.addOperationModifiableDefinition(constraint);
		
        //    return	constraint;
        //}

	
	
//	public	OclConstraint	createAttributeInitConstraint(string source, CoreClassifier contextualClassifier, CoreAttribute attribute, ExpressionInOcl initialValue) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclAttributeInitConstraint constraint = oclPackage.getConstraints().getOclAttributeInitConstraint().createOclAttributeInitConstraint();
//		
//		constraint.setSource(source);
//		constraint.setContextualClassifier(contextualClassifier);
//		constraint.setInitializedAttribute(attribute);
//		constraint.setExpression(initialValue);
//		
//		contextualClassifier.addInitConstraint(attribute.getName(), constraint); 
//		attribute.setInitialValueExpression(initialValue);
//		
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclConstraint	createAssociationEndInitConstraint(string source, CoreClassifier contextualClassifier, CoreAssociationEnd assocEnd, ExpressionInOcl initialValue) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclAssocEndInitConstraint constraint = oclPackage.getConstraints().getOclAssocEndInitConstraint().createOclAssocEndInitConstraint();
//		
//		constraint.setSource(source);
//		constraint.setContextualClassifier(contextualClassifier);
//		constraint.setInitializedAssocEnd(assocEnd);
//		constraint.setExpression(initialValue);
//		
//		contextualClassifier.addInitConstraint(assocEnd.getName(), constraint);
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclConstraint	createAttributeDeriveConstraint(string source, CoreClassifier contextualClassifier, CoreAttribute attribute, ExpressionInOcl initialValue) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclAttributeDeriveConstraint constraint = oclPackage.getConstraints().getOclAttributeDeriveConstraint().createOclAttributeDeriveConstraint();
//		
//		constraint.setSource(source);
//		constraint.setContextualClassifier(contextualClassifier);
//		constraint.setDerivedAttribute(attribute);
//		constraint.setExpression(initialValue);
//
//		contextualClassifier.addDeriveConstraint(attribute.getName(), constraint);
//		attribute.setDerivedValueExpression(initialValue);
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclConstraint	createAssociationEndDeriveConstraint(string source, CoreClassifier contextualClassifier, CoreAssociationEnd assocEnd, ExpressionInOcl initialValue) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclAssocEndDeriveConstraint constraint = oclPackage.getConstraints().getOclAssocEndDeriveConstraint().createOclAssocEndDeriveConstraint();
//		
//		constraint.setSource(source);
//		constraint.setContextualClassifier(contextualClassifier);
//		constraint.setDerivedAssocEnd(assocEnd);
//		constraint.setExpression(initialValue);
//		
//		contextualClassifier.addDeriveConstraint(assocEnd.getName(), constraint);
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclConstraint	createInvariantConstraint(string source, string name, CoreClassifier contextualClassifier, ExpressionInOcl initialValue) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclInvariantConstraint constraint = oclPackage.getConstraints().getOclInvariantConstraint().createOclInvariantConstraint();
//
//		constraint.setSource(source);
//		constraint.setContextualClassifier(contextualClassifier);
//		constraint.setExpression(initialValue);
//		constraint.setName(name);
//		
//		contextualClassifier.addInvariantConstraint(name, constraint);
//		
//		mon.stop();
//		return	constraint;	
//	}
//
//	public	OclBodyConstraint	createBodyConstraint(string source, CoreOperation contextualOperation, ExpressionInOcl body, List parameterNames) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclBodyConstraint constraint = oclPackage.getConstraints().getOclBodyConstraint().createOclBodyConstraint();
//		
//		constraint.setSource(source);
//		constraint.setContextualOperation(contextualOperation);
//		constraint.setExpression(body);
//		constraint.setParameterNames(parameterNames);
//
//		contextualOperation.setBodyDefinition(constraint);
//		
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclPreConstraint createPreConstraint(OclPrePostConstraint owner, string source, string name, CoreOperation contextualOperation, ExpressionInOcl preCondition) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclPreConstraint constraint = oclPackage.getConstraints().getOclPreConstraint().createOclPreConstraint();
//
//		constraint.setSource(source);
//		constraint.setContextualOperation(contextualOperation);
//		constraint.setExpression(preCondition);
//		constraint.setName(name);
//		constraint.setOwner(owner);
//
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclPostConstraint		createPostConstraint(OclPrePostConstraint owner, string source, string name, CoreOperation contextualOperation, ExpressionInOcl postCondition) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclPostConstraint constraint = oclPackage.getConstraints().getOclPostConstraint().createOclPostConstraint();
//
//		constraint.setSource(source);
//		constraint.setContextualOperation(contextualOperation);
//		constraint.setExpression(postCondition);
//		constraint.setName(name);
//		constraint.setOwner(owner);
//
//		mon.stop();
//		return	constraint;
//	}
//
//	public	OclPrePostConstraint	createPrePostConstraint(string source, CoreOperation contextualOperation) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		OclPrePostConstraint constraint = oclPackage.getConstraints().getOclPrePostConstraint().createOclPrePostConstraint();
//
//		constraint.setSource(source);
//		constraint.setContextualOperation(contextualOperation);
//	
//		contextualOperation.addOperationSpecification(constraint);
//		
//		mon.stop();
//		return	constraint;
//	}
//	
//	public ExpressionInOcl createExpressionInOcl(string name, CoreModelElement contextualElement, OclExpression bodyExpression) {
//		Monitor mon = MonitorFactory.start(getCurrentMethodName());
//		ExpressionInOcl exp = oclPackage.getConstraints().getExpressionInOcl().createExpressionInOcl();
//		((ExpressionInOclImpl) exp).setBodyExpression(bodyExpression);
//		exp.setContextualElement(contextualElement);
//		exp.setName(name);
//		
//		allExpressionsInOcl.add(exp);
//		
//		mon.stop();
//		return	exp;
//	}
    }
}
