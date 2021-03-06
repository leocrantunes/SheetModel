using System.Collections.Generic;
using OclLibrary.iface;

namespace OclLibrary.impl.environment
{
    public class EnvironmentFactoryManager {
        private static Dictionary<Ocl20Package, EnvironmentFactory> envFactoryMap = new Dictionary<Ocl20Package, EnvironmentFactory>();

        public static EnvironmentFactory getInstance(Ocl20Package oclPackage) {
            EnvironmentFactory factory;
            envFactoryMap.TryGetValue(oclPackage, out factory);
            if (factory == null) {
                factory = new EnvironmentFactory(oclPackage);
                envFactoryMap.Add(oclPackage, factory);
            }
            return	factory;
        }
    }
}
