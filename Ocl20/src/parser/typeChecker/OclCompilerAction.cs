using Ocl20.library.iface.environment;
using Ocl20.parser.cst;

namespace Ocl20.parser.typeChecker
{
    public interface OclCompilerAction
    {
        void doAction(OCLSemanticAnalyzer analyzer, Environment environment, CSTNode node);
    }
}
