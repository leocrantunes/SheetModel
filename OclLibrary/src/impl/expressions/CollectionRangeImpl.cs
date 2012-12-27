using System;
using OclLibrary.iface.common;
using OclLibrary.iface.expressions;
using OclLibrary.impl;
using OclLibrary.impl.expressions;

public class CollectionRangeImpl : CollectionLiteralPartImpl, CollectionRange {

	private	OclExpression		first;
	private	OclExpression		last;

	
	/**
	 * @param object
	 */
	public CollectionRangeImpl() {
	}

	public override void accept(IASTOclVisitor visitor) {
		((OclExpressionImpl) getFirst()).accept(visitor);
		((OclExpressionImpl) getLast()).accept(visitor);
	}

	public	String	toString() {
		return	this.getFirst().ToString() + " .. " + this.getLast().ToString();
	}
	
	public override bool conformsTo(CoreClassifier c) {
		return	this.getFirst().getType().conformsTo(c);
	}

	/**
	 * @return Returns the first.
	 */
	public OclExpression getFirst() {
		return first;
	}
	/**
	 * @param first The first to set.
	 */
	public void setFirst(OclExpression first) {
		this.first = first;
	}
	/**
	 * @return Returns the last.
	 */
	public OclExpression getLast() {
		return last;
	}
	/**
	 * @param last The last to set.
	 */
	public void setLast(OclExpression last) {
		this.last = last;
	}
	
	public override Object Clone() {
		CollectionRangeImpl theClone = (CollectionRangeImpl) base.Clone();
		theClone.first = (OclExpression) first.Clone();
		theClone.last = (OclExpression) last.Clone();
		
    	((OclExpressionImpl) theClone.first).setCollectionRange(theClone);
    	((OclExpressionImpl) theClone.last).setCollectionRange(theClone);

		return theClone;
	}
}
