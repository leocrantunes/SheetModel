using System.Collections.Generic;
using OclLibrary.iface.environment;
using OclParser.cst;

namespace OclParser.typeChecker
{
    public class OCLSemanticAnalyzer {
        protected	OCLSemanticAnalyzerVisitor oclSemanticAnalyzerVisitor;
        protected  ConstraintSourceTracker constraintSourceTracker;

        public OCLSemanticAnalyzer() {
        }

        public OCLSemanticAnalyzer(ConstraintSourceTracker constraintSourceTracker) {
            this.constraintSourceTracker = constraintSourceTracker;
        }

        public void analyze(Environment context, CSTNode rootNode) {
    	
            oclSemanticAnalyzerVisitor = getSemanticAnalyzerVisitor(context);
            rootNode.accept(oclSemanticAnalyzerVisitor);
        }

        public void analyzeFeatureDefinitions(Environment context, CSTNode rootNode) {
            oclSemanticAnalyzerVisitor = new OCLDefinedFeaturesVisitor(context, constraintSourceTracker);
            rootNode.accept(oclSemanticAnalyzerVisitor);
        }
    
        public void analyze(Environment context, List<object> nodes) {
            // oclSemanticAnalyzerVisitor = getSemanticAnalyzerVisitor(context);
        
            foreach (CSTNode node in nodes)
                analyze(context, node);
        
            // node.accept(oclSemanticAnalyzerVisitor);
        }
    
        public void analyzeFeatureDefinitions(Environment context, List<object> nodes) {
            oclSemanticAnalyzerVisitor = new OCLDefinedFeaturesVisitor(context, constraintSourceTracker);
            foreach (CSTNode node in nodes)
                node.accept(oclSemanticAnalyzerVisitor);
        }
    
        protected OCLSemanticAnalyzerVisitor getSemanticAnalyzerVisitor(Environment context) {
            return	new OCLSemanticAnalyzerVisitor(context, constraintSourceTracker);
        }
    }
}
