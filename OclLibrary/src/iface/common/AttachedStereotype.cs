/**
 * AttachedStereotype association proxy interface.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using OclLibrary.iface.common;

public interface AttachedStereotype {
    /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param theStereotypes Value of the first association end.
     * @param theExtendedElement Value of the second association end.
     * @return Returns true if the queried link exists.
     */
    bool exists(CoreStereotype theStereotypes, CoreModelElement theExtendedElement);
    /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param theStereotypes Required value of the first association end.
     * @return Collection of related objects.
     */
    List<object> getTheStereotypes(CoreModelElement theExtendedElement);
    /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param theExtendedElement Required value of the second association end.
     * @return Collection of related objects.
     */
    List<object> getTheExtendedElement(CoreStereotype theStereotypes);
    /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param theStereotypes Value of the first association end.
     * @param theExtendedElement Value of the second association end.
     */
    bool add(CoreStereotype theStereotypes, CoreModelElement theExtendedElement);
    /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param theStereotypes Value of the first association end.
     * @param theExtendedElement Value of the second association end.
     */
    bool remove(CoreStereotype theStereotypes, CoreModelElement theExtendedElement);
}
