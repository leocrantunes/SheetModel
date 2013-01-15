/**
 * ScopeKind enumeration class implementation.
 */

using System;
using System.Collections.Generic;
using Ocl20.library.impl.common;

namespace Ocl20.library.iface.common
{
    public class ScopeKindEnum : ScopeKind {
        /**
	 * Comment for <code>serialVersionUID</code>
	 */
        private static readonly long serialVersionUID = 1L;
        /**
     * Enumeration constant corresponding to literal sk_instance.
     */
        public static readonly ScopeKindEnum SK_INSTANCE = new ScopeKindEnum("sk_instance");
        /**
     * Enumeration constant corresponding to literal sk_classifier.
     */
        public static readonly ScopeKindEnum SK_CLASSIFIER = new ScopeKindEnum("sk_classifier");

        private static readonly List<object> typeName;
        private readonly String literalName;

        static ScopeKindEnum() {
            List<object> temp = new List<object>();
            temp.Add("Foundation");
            temp.Add("Data_Types");
            temp.Add("ScopeKind");
            typeName = new List<object>(temp);
        }

        private ScopeKindEnum(String literalName) {
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
            if (o.GetType() == typeof(ScopeKindEnum)) return (o == this);
            else if (o.GetType() == typeof(ScopeKind)) return (o.ToString().Equals(literalName));
            else return false;
        }

        /**
     * Translates literal name to correspondent enumeration value.
     * @param name Enumeration literal.
     * @return Enumeration value corresponding to the passed literal.
     */
        public static ScopeKind forName(String name) {
            if (name.Equals("sk_instance")) return SK_INSTANCE;
            if (name.Equals("sk_classifier")) return SK_CLASSIFIER;
            throw new Exception("Unknown literal name '" + name + "' for enumeration 'Foundation.Data_Types.ScopeKind'");
        }
        /**
     * Resolves serialized instance of enumeration value.
     * @return Resolved enumeration value.
     */
        protected Object readResolve() {
            try {
                return forName(literalName);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
