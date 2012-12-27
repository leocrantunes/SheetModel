/**
 * CollectionType object instance interface.
 */

using OclLibrary.iface.common;
using OclLibrary.iface.util;

public interface CollectionType : CoreDataType {
    /**
     * Returns the value of reference factory.
     * @return Value of reference factory.
     */
    AstOclModelElementFactory getFactory();
    /**
     * Sets the value of reference factory. See {@link #getFactory} for description 
     * on the reference.
     * @param newValue New value to be set.
     */
    void setFactory(AstOclModelElementFactory newValue);
    /**
     * Returns the value of reference elementType.
     * @return Value of reference elementType.
     */
    CoreClassifier getElementType();
    /**
     * Sets the value of reference elementType. See {@link #getElementType} for 
     * description on the reference.
     * @param newValue New value to be set.
     */
    void setElementType(CoreClassifier newValue);
}
