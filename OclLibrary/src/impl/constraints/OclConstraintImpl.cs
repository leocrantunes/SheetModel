using System;
using System.Reflection;
using OclLibrary.iface.constraints;

public abstract class OclConstraintImpl : OclConstraint, ICloneable {
	private	String	source;
	private	ExpressionInOcl	expression;
	private	Object		sourceNodeCS;
	
	/**
	 * @param object
	 */
	public OclConstraintImpl() {
	}
	
	/**
	 * @return Returns the expression.
	 */
	public ExpressionInOcl getExpression() {
		return expression;
	}
	/**
	 * @param expression The expression to set.
	 */
	public void setExpression(ExpressionInOcl expression) {
		this.expression = expression;
	}
	/**
	 * @return Returns the source.
	 */
	public String getSource() {
		return source;
	}
	/**
	 * @param source The source to set.
	 */
	public void setSource(String source) {
		this.source = source;
	}
	
	/**
	 * @return Returns the sourceNodeCS.
	 */
	public Object getSourceNodeCS() {
		return sourceNodeCS;
	}
	/**
	 * @param sourceNodeCS The sourceNodeCS to set.
	 */
	public void setSourceNodeCS(Object sourceNodeCS) {
		this.sourceNodeCS = sourceNodeCS;
	}
	
	public virtual Object Clone() {
		try {
            OclConstraintImpl theClone = (OclConstraintImpl)Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, this.GetType().FullName).Unwrap();
            theClone.source = source;
			theClone.sourceNodeCS = sourceNodeCS;
			if (expression != null) {
				theClone.expression = (ExpressionInOcl) expression.Clone();
				theClone.expression.setConstraint(theClone);
			}
			
			return	theClone;
		} catch (Exception) {
			return	null;
		}
	}
}
