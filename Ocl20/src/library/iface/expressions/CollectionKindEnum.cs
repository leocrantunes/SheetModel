/**
 * CollectionKind enumeration class implementation.
 */

using System;
using System.Collections.Generic;

namespace Ocl20.library.iface.expressions
{
    public class CollectionKindEnum : CollectionKind {
        /**
	 * Comment for <code>serialVersionUID</code>
	 */
        private static long serialVersionUID = 1L;
        /**
     * Enumeration constant corresponding to literal Collection.
     */
        public static CollectionKindEnum COLLECTION = new CollectionKindEnum("Collection");
        /**
     * Enumeration constant corresponding to literal Set.
     */
        public static CollectionKindEnum SET = new CollectionKindEnum("Set");
        /**
     * Enumeration constant corresponding to literal Bag.
     */
        public static CollectionKindEnum BAG = new CollectionKindEnum("Bag");
        /**
     * Enumeration constant corresponding to literal Sequence.
     */
        public static CollectionKindEnum SEQUENCE = new CollectionKindEnum("Sequence");
        /**
     * Enumeration constant corresponding to literal OrderedSet.
     */
        public static CollectionKindEnum ORDERED_SET = new CollectionKindEnum("OrderedSet");

        private static List<object> typeName;
        private readonly string literalName;

        static CollectionKindEnum(){
            List<object> temp = new List<object>();
            temp.Add("OCL20");
            temp.Add("expressions");
            temp.Add("CollectionKind");
            typeName = new List<object>(temp);
        }

        private CollectionKindEnum(string literalName) {
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
        public override string ToString() {
            return literalName;
        }

        /**
     * Returns a hash code for this the enumeration value.
     * @return A hash code for this enumeration value.
     */
        public override int GetHashCode() {
            return literalName.GetHashCode();
        }

        /**
     * Indicates whether some other object is equal to this enumeration value.
     * @param o The reference object with which to compare.
     * @return true if the other object is the enumeration of the same type and 
     * of the same value.
     */
        public override bool Equals(object o) {
            if (o.GetType() == typeof(CollectionKindEnum)) return (o == this);
            else if (o.GetType() == typeof(CollectionKind)) return (o.ToString().Equals(literalName));
            else return (o.ToString().Equals(literalName));
        }

        /**
     * Translates literal name to correspondent enumeration value.
     * @param name Enumeration literal.
     * @return Enumeration value corresponding to the passed literal.
     */
        public static CollectionKind forName(string name) {
            if (name.Equals("Collection")) return COLLECTION;
            if (name.Equals("Set")) return SET;
            if (name.Equals("Bag")) return BAG;
            if (name.Equals("Sequence")) return SEQUENCE;
            if (name.Equals("OrderedSet")) return ORDERED_SET;
            throw new ArgumentException("Unknown literal name '" + name + "' for enumeration 'CollectionKind'");
        }
        /**
     * Resolves serialized instance of enumeration value.
     * @return Resolved enumeration value.
     */
        protected object readResolve() {
            try {
                return forName(literalName);
            } catch (ArgumentException e) {
                throw new Exception(e.Message);
            }
        }
    }
}
