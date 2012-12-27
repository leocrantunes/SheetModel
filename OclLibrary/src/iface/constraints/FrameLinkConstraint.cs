using System;
using System.Reflection;
using OclLibrary.iface.common;
using CoreAssociationEnd = OclLibrary.iface.expressions.CoreAssociationEnd;

namespace OclLibrary.iface.constraints
{
    public class FrameLinkConstraint : ICloneable {

        private	String	source;
        private	object		sourceNodeCS;
	
        private	ExpressionInOcl sourceElements;
        private	CoreAssociationEnd	associationEnd;
        private	ExpressionInOcl targetElements;
	

        /**
	 * @param object
	 */
        public FrameLinkConstraint() {
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
        public object getSourceNodeCS() {
            return sourceNodeCS;
        }
        /**
	 * @param sourceNodeCS The sourceNodeCS to set.
	 */
        public void setSourceNodeCS(object sourceNodeCS) {
            this.sourceNodeCS = sourceNodeCS;
        }

	
	
        public CoreAssociationEnd getAssociationEnd() {
            return associationEnd;
        }

        public void setAssociationEnd(CoreAssociationEnd associationEnd) {
            this.associationEnd = associationEnd;
        }

        public ExpressionInOcl getSourceElements() {
            return sourceElements;
        }

        public void setSourceElements(ExpressionInOcl sourceElements) {
            this.sourceElements = sourceElements;
        }

        public ExpressionInOcl getTargetElements() {
            return targetElements;
        }

        public void setTargetElements(ExpressionInOcl targetElements) {
            this.targetElements = targetElements;
        }

        public object Clone() {
            try {
                FrameLinkConstraint theClone = (FrameLinkConstraint) Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, this.GetType().FullName).Unwrap();
                theClone.source = source;
                theClone.sourceNodeCS = sourceNodeCS;
                theClone.sourceElements = (ExpressionInOcl) sourceElements.Clone();
                theClone.targetElements = (ExpressionInOcl) targetElements.Clone();
                theClone.associationEnd = associationEnd;
//			theClone.expression.setConstraint(theClone);
                return	theClone;
            } catch (Exception) {
                return	null;
            }
        }

    }
}
