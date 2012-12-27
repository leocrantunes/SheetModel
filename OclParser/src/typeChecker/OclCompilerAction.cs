using OclLibrary.iface.environment;
using OclParser.cst;

namespace OclParser.typeChecker
{
    public interface OclCompilerAction
    {
        void doAction(OCLSemanticAnalyzer analyzer, Environment environment, CSTNode node);
    }
}
