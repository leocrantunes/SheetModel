/**
 * Expression object instance interface.
 */

using System;

namespace Ocl20.library.iface.common
{
    public interface Expression {
        /**
     * Returns the value of attribute language.
     * @return Value of attribute language.
     */
        String getLanguage();
        /**
     * Sets the value of language attribute. See {@link #getLanguage} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setLanguage(String newValue);
        /**
     * Returns the value of attribute body.
     * @return Value of attribute body.
     */
        String getBody();
        /**
     * Sets the value of body attribute. See {@link #getBody} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setBody(String newValue);
    }
}
