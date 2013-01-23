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
    public class TupleLiteralExpImpl : LiteralExpImpl, TupleLiteralExp {

        private List<VariableDeclaration> tuplePart;
	
        /**
	 * @param object
	 */
        public TupleLiteralExpImpl() {
            tuplePart = new List<VariableDeclaration>();
        }

        public override bool conformsTo(CoreClassifier type) {
            if (type.GetType() != typeof(TupleTypeImpl))
                return	false;
		
            TupleType targetType = (TupleType) type;
		
            foreach (VariableDeclaration declaration in getTuplePart()) {
                CoreAttribute targetElementType =  targetType.lookupAttribute(declaration.getVarName());
                if ((targetElementType == null) || (! declaration.getType().conformsTo(targetElementType.getFeatureType())))	
                    return	false;
            }
		
            return true;
        }

        public VariableDeclaration getTuplePart(String name) {
            foreach (VariableDeclaration declaration in getTuplePart()) {
                if (declaration.getVarName().Equals(name)) {
                    return declaration;
                }
            }
            return	null; 
        }
	

        /* (non-Javadoc)
	 * @see br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.expressions.ASTOclExpression#accept(br.ufrj.cos.lens.odyssey.tools.psw.metamodels.ocl20.IASTOclVisitor)
	 */
        public override void accept(IASTOclVisitor visitor) {
            List<object> tupleParts = new List<object>(getTuplePart());
            for (int i = 0; i < tupleParts.Count; i++) {
                ((OclExpressionImpl) ((VariableDeclaration) tupleParts[i]).getInitExpression()).accept(visitor);
                visitor.visitVariableDeclaration(((VariableDeclaration) tupleParts[i]));
            }
		
            visitor.visitTupleLiteralExp(this);
        }

        public override String ToString() {
            return	"Tuple{ " + getTuplePartsAsString() + " }";
        }

        public	String	getTuplePartsAsString() {
            StringBuilder result = new StringBuilder();

            int length = getTuplePart().Count;
            int index = 0;
            foreach (VariableDeclaration part in getTuplePart())
            {
                result.Append(getTuplePartAsString(part));
                if (index < length - 1)
                    result.Append(", ");
                index++;
            }
		
            return	result.ToString();
        }

        public	String	getTuplePartAsString(VariableDeclaration part) {
            return	part.ToString();
        }
	
        /**
	 * @return Returns the tuplePart.
	 */
        public List<VariableDeclaration> getTuplePart()
        {
            return tuplePart;
        }
        /**
	 * @param tuplePart The tuplePart to set.
	 */
        public void setTuplePart(List<VariableDeclaration> tuplePart)
        {
            this.tuplePart = tuplePart;
        }
	
        public void addTuplePart(VariableDeclaration part) {
            tuplePart.Add(part);
        }
	
        public void removeTuplePart(VariableDeclaration part) {
            tuplePart.Remove(part);
        }
	
        public override Object Clone() {
            TupleLiteralExpImpl theClone = (TupleLiteralExpImpl) base.Clone();
            if (tuplePart != null)
            {
                theClone.tuplePart = new List<VariableDeclaration>(tuplePart.Cast<VariableDeclarationImpl>().ToList().Clone());
                foreach (VariableDeclarationImpl variable in theClone.tuplePart) {
                    variable.setTupleLiteralExp(theClone);
                }
            } else 
                theClone.tuplePart = null;

            return	theClone;
        }
    }
}
