/**
 * Operation object instance interface.
 */

using System;
using System.Collections.Generic;
using Ocl20.uml13.iface.foundation.datatypes;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Operation : BehavioralFeature {
        /**
     * Returns the value of attribute concurrency.
     * @return Value of attribute concurrency.
     */
        //CallConcurrencyKind getConcurrency();
        /**
     * Sets the value of concurrency attribute. See {@link #getConcurrency} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        //void setConcurrency(CallConcurrencyKind newValue);
        /**
     * Returns the value of attribute isRoot.
     * @return Value of attribute isRoot.
     */
        //bool isRoot();
        /**
     * Sets the value of isRoot attribute. See {@link #isRoot} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setRoot(bool newValue);
        /**
     * Returns the value of attribute isLeaf.
     * @return Value of attribute isLeaf.
     */
        //bool isLeaf();
        /**
     * Sets the value of isLeaf attribute. See {@link #isLeaf} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setLeaf(bool newValue);
        /**
     * Returns the value of attribute isAbstract.
     * @return Value of attribute isAbstract.
     */
        //bool isAbstract();
        /**
     * Sets the value of isAbstract attribute. See {@link #isAbstract} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        //void setAbstract(bool newValue);
        /**
     * Returns the value of attribute specification.
     * @return Value of attribute specification.
     */
        //String getSpecification();
        /**
     * Sets the value of specification attribute. See {@link #getSpecification} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        //void setSpecification(String newValue);
        /**
     * Returns the value of reference method.
     * @return Value of reference method.
     */
        //List<object> getMethod();
    }
}
