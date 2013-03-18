using System.Collections.Generic;
using System.Globalization;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl;
using Ocl20.library.impl.expressions;
using Ocl20.parser.cst;
using Ocl20.parser.cst.context;
using Ocl20.parser.cst.expression;
using Ocl20.parser.cst.literalExp;
using Ocl20.parser.cst.type;
using SheetMaker.sheetmodel;

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
        {}

        public void visitOperationCalllExpBeginBegin(OperationCallExp exp)
        {
            string operationName = exp.getReferredOperation().getName();
            if (((OperationCallExpImpl)exp).isBooleanOperator(operationName))
            {
                formula += operationName + "(";
            }
            else if (((OperationCallExpImpl) exp).isBasicOperator(operationName))
            {
                formula += "(";
            }
        }

        public void visitOperationCalllExpBegin(OperationCallExp exp)
        {
            string operationName = exp.getReferredOperation().getName();

            if (((OperationCallExpImpl)exp).isBooleanOperator(operationName))
            {
                foreach (var oclExpression in exp.getArguments())
                {}
            }

            formula += string.Format(" {0} ", operationName);
        }

        public void visitOperationArgumentExpEnd(OperationCallExp exp)
        {}

        public void visitOperationCalllExpEnd(OperationCallExp exp)
        {
            formula += ")";
        }

        public void visitAssociationEndCallExp(AssociationEndCallExp exp)
        {}

        public void visitIteratorExp(IteratorExp exp)
        {}

        public void visitIterateExp(IterateExp exp)
        {}
    }
}
