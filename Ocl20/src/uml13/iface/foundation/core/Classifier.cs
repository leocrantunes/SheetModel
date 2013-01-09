/**
 * Classifier object instance interface.
 */

using System.Collections.Generic;

namespace Ocl20.uml13.iface.foundation.core
{
    public interface Classifier : GeneralizableElement, Namespace {
        /**
     * Returns the value of reference feature.
     * @return Value of reference feature.
     */
        List<object> getFeature();
        /**
     * Returns the value of reference participant.
     * @return Value of reference participant.
     */
        List<object> getParticipant();
        /**
     * Returns the value of reference powertypeRange.
     * @return Value of reference powertypeRange.
     */
        //List<object> getPowertypeRange();
    }
}
