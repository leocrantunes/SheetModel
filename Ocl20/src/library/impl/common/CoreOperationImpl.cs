using System;
using System.Collections.Generic;
using System.Text;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.iface.expressions;
using Ocl20.parser.semantics.types;

namespace Ocl20.library.impl.common
{
    public abstract class CoreOperationImpl : CoreBehavioralFeatureImpl, CoreOperation
    {
        private List<OclOperationConstraint> specifications = new List<OclOperationConstraint>();
        //private List<OclModifiableDeclarationConstraint> modifiableConstraints = new List<OclModifiableDeclarationConstraint>();
        private OclBodyConstraint body = null;
        private OclActionBodyConstraint actionBody = null;
        private Dictionary<string, List<VariableDeclaration>> localVariablesBySource = new Dictionary<string, List<VariableDeclaration>>();

        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#getFullSignatureAsString()
	 */

        public String getFullSignatureAsString()
        {
            return getName() +
                   "(" + getParametersAsString() + ")" +
                   ((getReturnType() != null)
                        ? " : " + getReturnType().getName()
                        : "");
        }

        protected String getParametersAsString()
        {
            StringBuilder result = new StringBuilder();

            Object[] parameterNames = this.getParametersNamesExceptReturn().ToArray();
            Object[] parameterTypes = this.getParametersTypesExceptReturn().ToArray();

            if (parameterNames.Length > 0)
                result.Append(getParameterAsString((String) parameterNames[0], (CoreClassifier) parameterTypes[0]));
            for (int i = 1; i < parameterNames.Length; i++)
            {
                result.Append(", ");
                result.Append(getParameterAsString((String) parameterNames[i], (CoreClassifier) parameterTypes[i]));
            }

            return result.ToString();
        }

        protected String getParameterAsString(String parameterName, CoreClassifier parameterType)
        {
            StringBuilder result = new StringBuilder();

            result.Append(parameterName);
            result.Append(" : ");
            result.Append(parameterType.getName());

            return result.ToString();
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#getParametersNamesExceptReturn()
	 */

        public List<object> getParametersNamesExceptReturn()
        {
            return adjustCollectionResult(getSpecificParameterNamesExceptReturn());
        }


        public List<object> getParametersTypesNamesExceptReturn()
        {
            List<object> result = new List<object>();

            foreach (CoreClassifier type in getParametersTypesExceptReturn())
            {
                result.Add(type.getName());
            }

            return result;
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#getParametersTypesExceptReturn()
	 */

        public List<object> getParametersTypesExceptReturn()
        {
            List<object> parameters = getSpecificParameterTypesExceptReturn();
            List<object> result = new List<object>();

            if (parameters != null)
            {
                foreach (CoreClassifier paramType in parameters)
                {
                    if (OclTypesDefinition.typeNeedsToBeParsed(paramType.getName()))
                        result.Add(OclTypesFactory.createTypeFromString(paramType.getName()));
                    else
                        result.Add(paramType);
                }
            }
            return result;


//		return	adjustCollectionResult(getSpecificParameterTypesExceptReturn());
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#getReturnType()
	 */

        public virtual CoreClassifier getReturnType()
        {
            CoreClassifier returnType = getSpecificReturnParameterType();

            if (returnType != null && OclTypesDefinition.typeNeedsToBeParsed(returnType))
                return OclTypesFactory.createTypeFromString(returnType.getName());
            else
                return returnType;

//		return	getSpecificReturnParameterType();
        }

//  if (returnType != null) {
//  if (OCLTypesDefinition.typeNeedsToBeParsed(name)) {
//  return OCLTypesDefinition.parseType(classifier.getGlobalEnvironment(),
//      name);
//    else
//    return	 returnType;
//} else {
//    return null;
//}
//}


        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#hasMatchingSignature(java.util.Collection)
	 */

        public bool hasMatchingSignature(List<object> argumentTypes)
        {
            return evaluateMatch(argumentTypes, new ConformsToEvaluator());
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#hasSameSignature(java.util.Collection, ocl20.CoreClassifier)
	 */

        public bool hasSameSignature(List<object> argumentTypes, CoreClassifier returnType)
        {
            if (evaluateMatch(argumentTypes, new SameTypeEvaluator()) == true)
            {

                if (isVoidType(getReturnType()) ||
                    sameType(returnType, getReturnType()))
                {
                    return true;
                }
            }

            return false;
        }


        protected bool evaluateMatch(List<object> argumentTypes, ParameterRuleEvaluator evaluator)
        {
            List<object> parameters = new List<object>();
            parameters.AddRange(getParametersTypesExceptReturn());

            List<object> listArgumentTypes = new List<object>();
            if (argumentTypes != null)
                listArgumentTypes.AddRange(argumentTypes);

            if ((parameters.Count == 0) && (listArgumentTypes.Count == 0))
            {
                return true;
            }

            if (parameters.Count == listArgumentTypes.Count)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    CoreClassifier parameterType = (CoreClassifier) parameters[i];
                    CoreClassifier argumentType = (CoreClassifier) listArgumentTypes[i];

                    if (! evaluator.evaluate(argumentType, parameterType))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        protected interface ParameterRuleEvaluator
        {
            bool evaluate(CoreClassifier argumentType, CoreClassifier parameterType);
        }

        private class ConformsToEvaluator : ParameterRuleEvaluator
        {
            /* (non-Javadoc)
		 * @see implocl20.CoreOperationImpl.ParameterRuleEvaluator#evaluate(ocl20.CoreClassifier, ocl20.CoreClassifier)
		 */

            public bool evaluate(CoreClassifier argumentType, CoreClassifier parameterType)
            {
                return argumentType.conformsTo(parameterType);
            }
        }

        public class SameTypeEvaluator : ParameterRuleEvaluator
        {
            /* (non-Javadoc)
		 * @see implocl20.CoreOperationImpl.ParameterRuleEvaluator#evaluate(ocl20.CoreClassifier, ocl20.CoreClassifier)
		 */

            public bool evaluate(CoreClassifier argumentType, CoreClassifier parameterType)
            {
                return sameType(argumentType, parameterType);
            }
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreOperation#operationNameMatches(java.lang.String)
	 */

        public virtual bool operationNameMatches(String name)
        {
            ModelElementNameGenerator nameGenerator = CoreModelElementNameGeneratorImpl.getInstance();

            return nameGenerator.operationNameMatches(nameGenerator.generateName(this), name);
        }

        protected static bool sameType(
            CoreClassifier typeToBeMatched,
            CoreClassifier returnParameter)
        {
            return typeToBeMatched == returnParameter || typeToBeMatched.getName().Equals(returnParameter.getName());
        }

        protected bool isVoidType(CoreClassifier returnParameter)
        {
            return (returnParameter == null) ||
                   ("void".Equals(returnParameter.getName()));
        }

        /* (non-Javadoc)
	 * @see ocl20.CoreBehavioralFeature#isQuery()
	 */

        public override bool isQuery()
        {
            // default of most case tools is to return isQuery as false.
            // so we adopted the stereotype modifier as an indicator that the operation isQuery
            // return	getSpecificIsQuery(); // && ! hasStereotype("modifier");
            return !hasStereotype("modifier");
        }

        public void addOperationSpecification(OclOperationConstraint operationSpec)
        {
            specifications.Add(operationSpec);
        }

        public List<OclOperationConstraint> getSpecifications()
        {
            return specifications;
        }

        //public void addOperationModifiableDefinition(OclModifiableDeclarationConstraint modifiableDefinition)
        //{
        //    modifiableConstraints.Add(modifiableDefinition);
        //}

        //public List<object> getModifiableConstraints()
        //{
        //    return modifiableConstraints;
        //}

        public void setBodyDefinition(OclBodyConstraint body)
        {
            this.body = body;
        }

        public OclBodyConstraint getBodyDefinition()
        {
            return this.body;
        }

        public abstract List<object> getConstraint();
        public abstract void setActionBody(OclActionBodyConstraint body);
        public abstract OclActionBodyConstraint getActionBody();

        //public void setActionBody(OclActionBodyConstraint body)
        //{
        //    this.actionBody = body;
        //}

        //public OclActionBodyConstraint getActionBody()
        //{
        //    return this.actionBody;
        //}


        /* (non-Javadoc)
     * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.constraints.OCLConstraintOwner#deleteAllConstraintsForSource(java.lang.String)
     */

        public void deleteAllConstraintsForSource(String sourceName)
        {
            if ((body != null) && body.getSource().Equals(sourceName))
            {
                body = null;
            }

            if ((actionBody != null) && actionBody.getSource().Equals(sourceName))
            {
                actionBody = null;
            }

            specifications.RemoveAll(s => s.getSource().Equals(sourceName));
            //modifiableConstraints.RemoveAll(m => m.element.getSource().Equals(sourceName));

            localVariablesBySource.Remove(sourceName);
        }

        public void addLocalVariable(String source, VariableDeclaration variable)
        {
            List<VariableDeclaration> targetList;
            this.localVariablesBySource.TryGetValue(source, out targetList);
            if (targetList == null)
            {
                targetList = new List<VariableDeclaration>();
            }
            targetList.Add(variable);
            this.localVariablesBySource.Add(source, targetList);
        }

        public VariableDeclaration lookupLocalVariable(string name)
        {
            List<VariableDeclaration> localVariables = getLocalVariables();
            foreach (VariableDeclaration variable in localVariables)
            {
                if (variable.getVarName().Equals(name))
                {
                    return variable;
                }
            }
            return null;
        }

        public abstract List<object> getModifiableConstraints();

        public List<VariableDeclaration> getLocalVariables()
        {
            List<VariableDeclaration> result = new List<VariableDeclaration>();
            foreach (KeyValuePair<string, List<VariableDeclaration>> pair in localVariablesBySource)
            {
                result.AddRange(pair.Value);
            }
            return result;
        }


        public override ICollection<object> getElemOwnedElements()
        {
            List<object> allOwnedElements = new List<object>();
            OclConstraint constraint;

            if (this.body != null)
                allOwnedElements.Add(this.body);
            if (this.actionBody != null)
                allOwnedElements.Add(this.actionBody);

            foreach (OclPrePostConstraint prePost in specifications)
            {
                allOwnedElements.AddRange(prePost.getPreConditions());
                allOwnedElements.AddRange(prePost.getPostConditions());
            }

            return allOwnedElements;
        }


        protected bool getSpecificHasStereotype(String name)
        {
            return false;
        }

        protected virtual CoreClassifier getSpecificReturnParameterType()
        {
            return null;
        }

        protected virtual List<object> getSpecificParameterTypesExceptReturn()
        {
            return new List<object>();
        }

        protected virtual List<object> getSpecificParameterNamesExceptReturn()
        {
            return new List<object>();
        }

        public bool getSpecificIsQuery()
        {
            return true;
        }
    }
}