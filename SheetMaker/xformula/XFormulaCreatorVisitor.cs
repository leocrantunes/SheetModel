using Ocl20.library.iface.expressions;
using Ocl20.library.impl;
using Ocl20.library.impl.expressions;

namespace SheetMaker.xformula
{
    public class XFormulaCreatorVisitor : IASTOclVisitor
    {
        private string formula = "=";
        private string source = "";

        public XFormulaCreatorVisitor()
        {}

        public string getFormula()
        {
            return formula;
        }

        public void visitIntegerLiteralExp(IntegerLiteralExp exp)
        {
            formula += exp.getIntegerSymbol();
        }

        public void visitRealLiteralExp(RealLiteralExp exp)
        {
            formula += exp.getRealSymbol();
        }

        public void visitBooleanLiteralExp(BooleanLiteralExp exp)
        {
            formula += exp.isBooleanSymbol();
        }

        public void visitStringLiteralExp(StringLiteralExp exp)
        {
            formula += exp.getStringSymbol();
        }

        public void visitNullLiteralExp(NullLiteralExp exp)
        {}

        public void visitInvalidLiteralExp(InvalidLiteralExp exp)
        {}

        public void visitEnumLiteralExp(EnumLiteralExp exp)
        {}

        public void visitTupleLiteralExp(TupleLiteralExp exp)
        {}

        public void visitCollectionLiteralExp(CollectionLiteralExp exp)
        {}

        public void visitOclTypeLiteralExp(OclTypeLiteralExp exp)
        {}

        public void visitVariableDeclaration(VariableDeclaration varDecl)
        {}

        public void visitVariableExp(VariableExp exp)
        {}

        public void visitIfExp(IfExp exp)
        {}

        public void beginVisitLetExp(LetExp exp)
        {}

        public void endVisitLetExp(LetExp exp)
        {}

        public void visitAttributeCallExp(AttributeCallExp exp)
        {
            var attribute = exp.getReferredAttribute();
            var name = attribute.getName();

            var expsource = (VariableExp) exp.getSource();
            var variable = expsource.getReferredVariable();
            var type = variable.getType();
            var typeName = type.getName();

            formula += string.Format("{0}[{1}]", typeName, name);
        }

        public void visitOperationCalllExpBeginBegin(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation != null)
            {
                string operationName = operation.getName();
                bool isBasicOperator = ((OperationCallExpImpl) exp).isBasicOperator(operationName);
                if (((OperationCallExpImpl)exp).isBooleanOperator(operationName) || !isBasicOperator)
                {
                    string xoperationname = operationName;
                    if (operationName.Equals("size")) xoperationname = "count";

                    formula += xoperationname.ToUpper() + "(";
                }
                else
                {
                    formula += "(";
                }
            }
        }

        public void visitOperationCalllExpBegin(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation != null)
            {
                string operationName = operation.getName();

                if (((OperationCallExpImpl) exp).isBooleanOperator(operationName))
                {
                    formula += ",";
                }
                else if (((OperationCallExpImpl) exp).isBasicOperator(operationName))
                {
                    formula += string.Format(" {0} ", operationName);
                }
            }
        }

        public void visitOperationArgumentExpEnd(OperationCallExp exp)
        {}

        public void visitOperationCalllExpEnd(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation != null)
            {
                formula += ")";
            }
        }

        public void visitAssociationEndCallExp(AssociationEndCallExp exp)
        {}

        public void visitIteratorExp(IteratorExp exp)
        {}

        public void visitIterateExp(IterateExp exp)
        {}
    }
}
