using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.iface.types;
using Ocl20.library.impl.types;
using Ocl20.library.utils;

namespace Ocl20.library.impl.expressions
{
    public class OperationCallExpImpl : ModelPropertyCallExpImpl, OperationCallExp {

        private	CoreOperation	referredOperation;
        private List<OclExpression> arguments;
	
        /**
	 * @param object
	 */
        public OperationCallExpImpl() {
            arguments = new List<OclExpression>();
        }

        public override void accept(IASTOclVisitor visitor) {
            if (this.getSource() != null) {
                ((OclExpressionImpl) this.getSource()).accept(visitor);
            }
		
            visitor.visitOperationCalllExpBegin(this);
            foreach (OclExpressionImpl oclExpression in this.getArguments())
            {
                oclExpression.accept(visitor);
                visitor.visitOperationArgumentExpEnd(this);
            }
            visitor.visitOperationCalllExpEnd(this);
        }

        protected override String getSpecificString() {
            return	this.getReferredOperation().getName() + "(" + this.getArgumentsAsString() + ")";
        }
	
        public override String ToString() {
            if (this.getReferredOperation() != null) {
                if (isUnaryOperator(this.getReferredOperation().getName()))
                    return	this.getReferredOperation().getName() + " " + getSource().ToString();
                if (isBasicOperator(this.getReferredOperation().getName())) {
                    return	getSource().ToString() + " " + this.getReferredOperation().getName() + " " + this.getArgumentsAsString();
                } else {
                    String	sourceAsString = getSource().ToString();
	    		
                    if (sourceAsString.EndsWith("@pre")) {
                        int indexPre = sourceAsString.LastIndexOf("@pre");
                        return	sourceAsString.Substring(0, indexPre) + "."  + this.getReferredOperation().getName() + "@pre" + "(" + this.getArgumentsAsString() + ")";
                    } else if (getSource().getType() is CollectionTypeImpl) {
                        return	getSource().ToString() + "->" + getSpecificString();
                    } else {
                        return	getSource().ToString() + "." + getSpecificString();
                    }
                }
            } else {
                if ("atPre".Equals(this.getName())) {
                    return		getSource().ToString() + "@pre";
                } else {
                    return	 	getSource().ToString();
                }
            }
        }

        public	String	getArgumentsAsString() {
            StringBuilder	result = new StringBuilder();
		
            int length = getArguments().Count;
            int index = 0;
            foreach (OclExpression argument in getArguments())
            {
                result.Append(argument.ToString());
                if (index < length - 1)
                    result.Append(", ");
                index++;
            }

            if (isBasicOperator(this.getReferredOperation().getName()) && this.getArguments().Count == 1) {
                OclExpression argument = (OclExpression) this.getArguments()[0];
                if (argument is OperationCallExpImpl && isBasicOperator(((OperationCallExp) argument).getReferredOperation().getName())) {
                    return	"(" + result.ToString() + ")";
                }
            }
		
            return	result.ToString();
        }

        public bool	isBasicOperator(String operation) {
            String[] basicOperations = { "=", "<>", ">", "<", ">=", "<=", "+", "-", "*", "/", "and", "or", "xor" };
		
            for (int i = 0; i < basicOperations.Length; i++)
                if (basicOperations[i].Equals(operation))
                    return	true;
				
            return	false;
        }

        public bool	isUnaryOperator(String operation) {
            String[] unaryOperations = { "-", "not" };
		
            for (int i = 0; i < unaryOperations.Length; i++)
                if (unaryOperations[i].Equals(operation) && this.getArguments().Count == 0)
                    return	true;
				
            return	false;
        }

        /**
	 * @return Returns the arguments.
	 */
        public List<OclExpression> getArguments()
        {
            return arguments;
        }

        public void addArgument(OclExpression argument) {
            this.arguments.Add(argument);
        }
	
        public void removeArgument(OclExpression argument) {
            this.arguments.Remove(argument);
        }
	
        /**
	 * @return Returns the referredOperation.
	 */
        public CoreOperation getReferredOperation() {
            return referredOperation;
        }
        /**
	 * @param referredOperation The referredOperation to set.
	 */
        public void setReferredOperation(CoreOperation referredOperation) {
            this.referredOperation = referredOperation;
        }
	
        public override Object Clone() {
            OperationCallExpImpl theClone = (OperationCallExpImpl) base.Clone();
		
            theClone.referredOperation = referredOperation;
            if (arguments != null) {
                theClone.arguments = new List<OclExpression>(arguments.Cast<OclExpressionImpl>().ToList().Clone());
                foreach (OclExpression argument in theClone.arguments)
                    ((OclExpressionImpl)argument).setParentOperation(theClone);
            }
		
            return	theClone;
        }
    }
}
