using System;
using System.Globalization;
using OclLibrary.iface.expressions;
using OclLibrary.impl;
using OclLibrary.impl.expressions;

public class IntegerLiteralExpImpl : NumericLiteralExpImpl, IntegerLiteralExp {

	private	long integerSymbol;	
	
	public IntegerLiteralExpImpl() {
	}

	public override void accept(IASTOclVisitor visitor) {
		visitor.visitIntegerLiteralExp(this);
	}
	
	public override String ToString() {
		return	this.getIntegerSymbol().ToString(CultureInfo.InvariantCulture);
	}
	
	/**
	 * @return Returns the integerSymbol.
	 */
	public long getIntegerSymbol() {
		return integerSymbol;
	}
	/**
	 * @param integerSymbol The integerSymbol to set.
	 */
	public void setIntegerSymbol(long integerSymbol) {
		this.integerSymbol = integerSymbol;
	}
	
	public override Object Clone() {
		IntegerLiteralExpImpl theClone = (IntegerLiteralExpImpl) base.Clone();
		theClone.integerSymbol = integerSymbol;
		return	theClone;
	}

}
