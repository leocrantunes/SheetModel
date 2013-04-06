using System.Linq;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl;
using Ocl20.library.impl.common;
using Ocl20.library.impl.expressions;
using SheetMaker.sheetmodel;

namespace SheetMaker.xformula
{
    public class XFormulaCreatorVisitor : IASTOclVisitor
    {
        private XWorkbook xWorkbook;
        private string formula = "=";

        public XFormulaCreatorVisitor(XWorkbook xWorkbook)
        {
            this.xWorkbook = xWorkbook;
        }

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
            formula += "\"" + exp.getStringSymbol() + "\"";
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
                OperationCallExpImpl operationCall = (OperationCallExpImpl) exp; 
                bool isBasicOperator = operationCall.isBasicOperator(operationName);
                if ((operationCall.isBooleanOperator(operationName) || !isBasicOperator))
                {
                    string xoperationname = operationName;
                    if (operationName.Equals("size"))
                    {
                        if (operationCall.getSource() is AssociationEndCallExpImpl)
                            xoperationname = "countif";
                        else if (operationCall.getSource() is IteratorExpImpl)
                            xoperationname = "countifs";
                        else
                            xoperationname = "counta";
                    }
                    if (operationName.Equals("sum"))
                    {
                        if (operationCall.getSource() is AssociationEndCallExpImpl ||
                            operationCall.getSource() is IteratorExpImpl)
                        {
                            if (operationCall.getSource() is IteratorExpImpl &&
                                ((IteratorExpImpl) operationCall.getSource()).getSource() is IteratorExpImpl)
                            {
                                formula = "SUMIFS(";
                                var body = ((IteratorExpImpl)operationCall.getSource()).getBody();
                                if (body is AttributeCallExpImpl)
                                {
                                    var attribute = ((AttributeCallExpImpl)body).getReferredAttribute();
                                    var name = attribute.getName();
                                    var classifier = (CoreClassifierImpl)attribute.getElemOwner();
                                    var classifierName = classifier.getName();

                                    formula += string.Format("{0}[{1}],{0}[{2}],{0}[{2}]", classifierName, name, "numero_depto");

                                    return;
                                }
                            }
                            else xoperationname = "sumif";
                        }
                        else if (operationCall.getSource() is IteratorExpImpl &&
                                 ((IteratorExpImpl) operationCall.getSource()).getName().Equals("select"))
                            xoperationname = "sumifs";
                    }

                    formula += xoperationname.ToUpper();
                }
                
                formula += "(";
            }
        }

        public void visitOperationCalllExpBegin(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation != null)
            {
                string operationName = operation.getName();
                if (((OperationCallExpImpl) exp).isBooleanOperator(operationName))
                    formula += ",";
                
                else if (((OperationCallExpImpl) exp).isBasicOperator(operationName))
                    formula += string.Format(" {0} ", operationName);
            }
        }

        public void visitOperationArgumentExpEnd(OperationCallExp exp)
        {}

        public void visitOperationCalllExpEnd(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation != null)
                formula += ")";
        }

        public void visitAssociationEndCallExp(AssociationEndCallExp exp)
        {
            var associationEnd = exp.getReferredAssociationEnd();
            var otherType = associationEnd.getType();
            var otherTypeName = otherType.getName();

            var otherKeyAttribute = (CoreAttributeImpl) otherType.getAllAttributes().FirstOrDefault(); // primeiro atributo á o atributo-chave
            var otherKeyName = otherKeyAttribute != null ? otherKeyAttribute.getName() : "";

            var participant = associationEnd.getTheParticipant();
            var association = associationEnd.getAssociation();
            var otherAssociationEnd =
                (CoreAssociationEndImpl) association.getTheAssociationEnds(participant).FirstOrDefault();
            var otherName = otherAssociationEnd != null ? otherAssociationEnd.getName() : "";
            
            var expsource = (VariableExp) exp.getSource();
            var variable = expsource.getReferredVariable();
            var type = variable.getType();
            var typeName = type.getName();

            var keyAttribute = (CoreAttributeImpl) type.getAllAttributes().FirstOrDefault(); // primeiro atributo á o atributo-chave
            var name = keyAttribute != null ? keyAttribute.getName() : "";

            string otherFormula = string.Format("=INDEX({0},MATCH([area];{0}[{1}];0),COL({0}[{2}]))", otherTypeName, otherKeyName, otherName, name, name);

            if (!(exp.getAppliedProperty() is IterateExpImpl))
                formula += string.Format("{0}[{1}],{2}[{3}]", otherTypeName, otherName, typeName, name);
        }

        public void visitIteratorExp(IteratorExp exp)
        {
            string name = "";
            string classifierName = "";
            var body =  exp.getBody();
            if (body is AttributeCallExpImpl && !(exp.getSource() is IteratorExpImpl))
            {
                var attribute = ((AttributeCallExpImpl)body).getReferredAttribute();
                name = attribute.getName();
                var classifier = (CoreClassifierImpl)attribute.getElemOwner();
                classifierName = classifier.getName();
                formula += string.Format(",{0}[{1}]", classifierName, name);
            }
            else if (body is OperationCallExpImpl)
            {
                var operation = ((OperationCallExpImpl)body).getReferredOperation();
                name = operation.getName();
                var source = ((OperationCallExpImpl) body).getSource();
                var attribute = ((AttributeCallExpImpl)source).getReferredAttribute();
                var attributeName = attribute.getName();
                var classifier = (CoreClassifierImpl)attribute.getElemOwner();
                classifierName = classifier.getName();
                if (operation.getName().Equals("=")) formula += string.Format(",{0}[{1}],", classifierName, attributeName);
                else formula += string.Format(",{0}[{1}],\"{2}\"&", classifierName, attributeName, name);

                var argument = (OclExpressionImpl)((OperationCallExpImpl)body).getArguments().FirstOrDefault();
                if (argument != null)
                    argument.accept(this);
            }
        }

        public void visitIterateExp(IterateExp exp)
        {}
    }
}
