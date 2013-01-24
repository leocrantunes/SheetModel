/**
 * OrderingKind enumeration class implementation.
 */

using System;
using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.datatypes
{
    public class OrderingKindEnum : OrderingKind {
        /**
	 * Comment for <code>serialVersionUID</code>
	 */
        private static readonly long serialVersionUID = 1L;
        /**
     * Enumeration constant corresponding to literal ok_unordered.
     */
        public static readonly OrderingKindEnum OK_UNORDERED = new OrderingKindEnum("ok_unordered");
        /**
     * Enumeration constant corresponding to literal ok_ordered.
     */
        public static readonly OrderingKindEnum OK_ORDERED = new OrderingKindEnum("ok_ordered");
        /**
     * Enumeration constant corresponding to literal ok_sorted.
     */
        public static readonly OrderingKindEnum OK_SORTED = new OrderingKindEnum("ok_sorted");

        private static readonly List<object> typeName;
        private readonly String literalName;

        static OrderingKindEnum() {
            List<object> temp = new List<object>();
            temp.Add("Foundation");
            temp.Add("Data_Types");
            temp.Add("OrderingKind");
            typeName = new List<object>(temp);
        }

        private OrderingKindEnum(String literalName) {
            this.literalName = literalName;
        }

        /**
     * Returns fully qualified name of the enumeration type.
     * @return List containing all parts of the fully qualified name.
     */
        public List<object> refTypeName() {
            return typeName;
        }

        /**
     * Returns a string representation of the enumeration value.
     * @return A string representation of the enumeration value.
     */
        public String toString() {
            return literalName;
        }

        /**
     * Returns a hash code for this the enumeration value.
     * @return A hash code for this enumeration value.
     */
        public int hashCode() {
            return literalName.GetHashCode();
        }

        /**
     * Indicates whether some other object is equal to this enumeration value.
     * @param o The reference object with which to compare.
     * @return true if the other object is the enumeration of the same type and 
     * of the same value.
     */
        public bool equals(Object o) {
            if (o is OrderingKindEnum) return (o == this);
            else if (o is OrderingKind) return (o.ToString().Equals(literalName));
            else return false;
        }

        /**
     * Translates literal name to correspondent enumeration value.
     * @param name Enumeration literal.
     * @return Enumeration value corresponding to the passed literal.
     */
        public static OrderingKind forName(String name) {
            if (name.Equals("ok_unordered")) return OK_UNORDERED;
            if (name.Equals("ok_ordered")) return OK_ORDERED;
            if (name.Equals("ok_sorted")) return OK_SORTED;
            throw new Exception("Unknown literal name '" + name + "' for enumeration 'Foundation.Data_Types.OrderingKind'");
        }
        /**
     * Resolves serialized instance of enumeration value.
     * @return Resolved enumeration value.
     */
        protected object readResolve() {
            try {
                return forName(literalName);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
