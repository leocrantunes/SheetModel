/**
 * OclModelElementTypeCoreModelElement association proxy interface.
 */

using System.Collections.Generic;
using Ocl20.library.iface.common;

namespace Ocl20.library.iface.types
{
    public interface OclModelElementTypeCoreModelElement {
        /**
     * Queries whether a link currently exists between a given pair of instance 
     * objects in the associations link set.
     * @param oclModelElement Value of the first association end.
     * @param referredModelElement Value of the second association end.
     * @return Returns true if the queried link exists.
     */
        bool exists(OclModelElementType oclModelElement, CoreModelElement referredModelElement);
        /**
     * Queries the instance objects that are related to a particular instance 
     * object by a link in the current associations link set.
     * @param oclModelElement Required value of the first association end.
     * @return Collection of related objects.
     */
        List<object> getOclModelElement(CoreModelElement referredModelElement);
        /**
     * Queries the instance object that is related to a particular instance object 
     * by a link in the current associations link set.
     * @param referredModelElement Required value of the second association end.
     * @return Related object or <code>null</code> if none exists.
     */
        CoreModelElement getReferredModelElement(OclModelElementType oclModelElement);
        /**
     * Creates a link between the pair of instance objects in the associations 
     * link set.
     * @param oclModelElement Value of the first association end.
     * @param referredModelElement Value of the second association end.
     */
        bool add(OclModelElementType oclModelElement, CoreModelElement referredModelElement);
        /**
     * Removes a link between a pair of instance objects in the current associations 
     * link set.
     * @param oclModelElement Value of the first association end.
     * @param referredModelElement Value of the second association end.
     */
        bool remove(OclModelElementType oclModelElement, CoreModelElement referredModelElement);
    }
}
