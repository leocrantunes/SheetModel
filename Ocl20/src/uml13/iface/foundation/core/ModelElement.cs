/**
 * ModelElement object instance interface.
 */

using System;
using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface ModelElement : Element {
        /**
     * Returns the value of attribute name.
     * @return Value of attribute name.
     */
        String getName();
        /**
     * Sets the value of name attribute. See {@link #getName} for description 
     * on the attribute.
     * @param newValue New value to be set.
     */
        void setName(String newValue);
        /**
     * Returns the value of attribute visibility.
     * @return Value of attribute visibility.
     */
        //VisibilityKind getVisibility();
        /**
     * Sets the value of visibility attribute. See {@link #getVisibility} for 
     * description on the attribute.
     * @param newValue New value to be set.
     */
        //void setVisibility(VisibilityKind newValue);
        /**
     * Returns the value of attribute isSpecification.
     * @return Value of attribute isSpecification.
     */
        //bool isSpecification();
        /**
     * Sets the value of isSpecification attribute. See {@link #isSpecification} 
     * for description on the attribute.
     * @param newValue New value to be set.
     */
        //void setSpecification(bool newValue);
        /**
     * Returns the value of reference namespace.
     * @return Value of reference namespace.
     */
        Namespace getNamespace();
        /**
     * Sets the value of reference namespace. See {@link #getNamespace} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        void setNamespace(Namespace newValue);
        /**
     * Returns the value of reference clientDependency.
     * @return Value of reference clientDependency.
     */
        List<object> getClientDependency();
        /**
     * Returns the value of reference constraint.
     * @return Value of reference constraint.
     */
        //List<object> getConstraint();
        /**
     * Returns the value of reference supplierDependency.
     * @return Value of reference supplierDependency.
     */
        List<object> getSupplierDependency();
        /**
     * Returns the value of reference presentation.
     * @return Value of reference presentation.
     */
        //List<object> getPresentation();
        /**
     * Returns the value of reference targetFlow.
     * @return Value of reference targetFlow.
     */
        //List<object> getTargetFlow();
        /**
     * Returns the value of reference sourceFlow.
     * @return Value of reference sourceFlow.
     */
        //List<object> getSourceFlow();
        /**
     * Returns the value of reference templateParameter3.
     * @return Value of reference templateParameter3.
     */
        //List<object> getTemplateParameter3();
        /**
     * Returns the value of reference binding.
     * @return Value of reference binding.
     */
        //Binding getBinding();
        /**
     * Sets the value of reference binding. See {@link #getBinding} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
        //void setBinding(Binding newValue);
        /*/*
     * Returns the value of reference comment.
     * @return Value of reference comment.
     */
        //List<object> getComment();
        /**
     * Returns the value of reference elementResidence.
     * @return Value of reference elementResidence.
     */
        //List<object> getElementResidence();
        /**
     * Returns the value of reference templateParameter.
     * @return Value of reference templateParameter.
     */
        //List<object> getTemplateParameter();
        /**
     * Returns the value of reference templateParameter2.
     * @return Value of reference templateParameter2.
     */
        //List<object> getTemplateParameter2();
    }
}
