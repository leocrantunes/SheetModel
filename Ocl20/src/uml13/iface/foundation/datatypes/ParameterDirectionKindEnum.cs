/**
 * ParameterDirectionKind enumeration class implementation.
 */

using System;
using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.datatypes
{
    public class ParameterDirectionKindEnum : ParameterDirectionKind {
        /**
	 * Comment for <code>serialVersionUID</code>
	 */
        private static long serialVersionUID = 1L;
        /**
     * Enumeration constant corresponding to literal pdk_in.
     */
        public static ParameterDirectionKindEnum PDK_IN = new ParameterDirectionKindEnum("pdk_in");
        /**
     * Enumeration constant corresponding to literal pdk_inout.
     */
        public static ParameterDirectionKindEnum PDK_INOUT = new ParameterDirectionKindEnum("pdk_inout");
        /**
     * Enumeration constant corresponding to literal pdk_out.
     */
        public static ParameterDirectionKindEnum PDK_OUT = new ParameterDirectionKindEnum("pdk_out");
        /**
     * Enumeration constant corresponding to literal pdk_return.
     */
        public static ParameterDirectionKindEnum PDK_RETURN = new ParameterDirectionKindEnum("pdk_return");

        private static List<object> typeName;
        private readonly String literalName;

        static ParameterDirectionKindEnum() {
            List<object> temp = new List<object>();
            temp.Add("Foundation");
            temp.Add("Data_Types");
            temp.Add("ParameterDirectionKind");
            typeName = new List<object>(temp);
        }

        private ParameterDirectionKindEnum(String literalName) {
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
        public override int GetHashCode() {
            return literalName.GetHashCode();
        }

        /**
     * Indicates whether some other object is equal to this enumeration value.
     * @param o The reference object with which to compare.
     * @return true if the other object is the enumeration of the same type and 
     * of the same value.
     */
        public bool equals(Object o) {
            if (o.GetType() == typeof(ParameterDirectionKindEnum)) return (o == this);
            else if (o.GetType() == typeof (ParameterDirectionKind)) return (o.ToString().Equals(literalName));
            else return false;
        }

        /**
     * Translates literal name to correspondent enumeration value.
     * @param name Enumeration literal.
     * @return Enumeration value corresponding to the passed literal.
     */
        public static ParameterDirectionKind forName(String name) {
            if (name.Equals("pdk_in")) return PDK_IN;
            if (name.Equals("pdk_inout")) return PDK_INOUT;
            if (name.Equals("pdk_out")) return PDK_OUT;
            if (name.Equals("pdk_return")) return PDK_RETURN;
            throw new Exception("Unknown literal name '" + name + "' for enumeration 'Foundation.Data_Types.ParameterDirectionKind'");
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
