using System.Collections.Generic;
using System.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl;
using Ocl20.library.impl.common;
using Ocl20.library.impl.expressions;
using SheetMaker.sheetmodel;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace SheetMaker.xformula
{
    public class XFormulaCreatorVisitor : IASTOclVisitor
    {
        private XWorkbook xWorkbook;
        private string formula = "=";
        private CoreClassifier currentClassifier;
        private List<CoreAssociationEnd> associationEnds;
        private int navigationLevel = 0;

        public XFormulaCreatorVisitor(XWorkbook xWorkbook)
        {
            this.xWorkbook = xWorkbook;
            associationEnds = new List<CoreAssociationEnd>();
        }

        public string getFormula()
        {
            return formula;
        }

        public void visitIntegerLiteralExp(IntegerLiteralExp exp)
        {
            // get integer symbol and add to formula
            formula += exp.getIntegerSymbol();
        }

        public void visitRealLiteralExp(RealLiteralExp exp)
        {
            // get real symbol and add to formula
            formula += exp.getRealSymbol();
        }

        public void visitBooleanLiteralExp(BooleanLiteralExp exp)
        {
            // get boolean symbol and add to formula
            formula += exp.isBooleanSymbol();
        }

        public void visitStringLiteralExp(StringLiteralExp exp)
        {
            // get string symbol and add to formula with quotation marks
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
            string typeName, name;
            getTableReference(exp, out name, out typeName);
            
            // format table_name[column_name] (same of classifier.attribute)
            formula += string.Format("{0}[{1}]", typeName, name);
        }

        public void visitOperationCalllExpBeforeBegin(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation == null) return;
            
            // get type of operation
            string operationName = operation.getName();
            OperationCallExpImpl operationCall = (OperationCallExpImpl) exp;
            bool isBasicOperator = operationCall.isBasicOperator(operationName);
            bool isBooleanOperator = operationCall.isBooleanOperator(operationName);

            // boolean operators or not basic operators (because boolean operator is a basic operator)
            if (isBooleanOperator || !isBasicOperator)
            {
                // set formula name based on operation name
                string xoperationname = operationName;
                var source = operationCall.getSource();

                // map operation name to formula name based on source expression
                if (source is AssociationEndCallExpImpl || source is IteratorExpImpl)
                {
                    if (operationName.Equals("size")) xoperationname = "COUNTIFS";
                    else if (operationName.Equals("sum")) xoperationname = "SUMIFS";
                }
                else
                {
                    if (operationName.Equals("size")) xoperationname = "COUNTA";
                    else if (operationName.Equals("sum")) xoperationname = "SUM";
                    else if (operationName.Equals("toDate")) xoperationname = ""; // suppress toDate operation
                }

                formula += xoperationname.ToUpper();
            }

            formula += "(";
        }

        public void visitOperationCalllExpBegin(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation == null) return;

            // get type of operation
            string operationName = operation.getName();
            OperationCallExpImpl operationCall = (OperationCallExpImpl)exp;
            bool isBasicOperator = operationCall.isBasicOperator(operationName);
            bool isBooleanOperator = operationCall.isBooleanOperator(operationName);

            if (isBooleanOperator)
                formula += ",";
            else if (isBasicOperator)
                formula += string.Format(" {0} ", operationName);
        }

        public void visitOperationArgumentExpEnd(OperationCallExp exp)
        {}

        public void visitOperationCalllExpEnd(OperationCallExp exp)
        {
            // put parenthesis in the end of operation formula
            var operation = exp.getReferredOperation();
            if (operation != null)
                formula += ")";
        }

        public void visitAssociationEndCallExp(AssociationEndCallExp exp)
        {
            var associationEnd = exp.getReferredAssociationEnd();
            bool isOneMultiplicity = associationEnd.isOneMultiplicity();
            if (!isOneMultiplicity)
            {
                var otherType = associationEnd.getType();
                var otherTypeName = otherType.getName();

                var otherKeyAttribute = (CoreAttributeImpl)otherType.getAllAttributes().FirstOrDefault(); // primeiro atributo é o atributo-chave
                var otherKeyName = otherKeyAttribute != null ? otherKeyAttribute.getName() : "";

                var participant = associationEnd.getTheParticipant();
                var association = associationEnd.getAssociation();
                var otherAssociationEnd =
                    (CoreAssociationEndImpl) association.getTheAssociationEnds(participant).FirstOrDefault();
                var otherName = otherAssociationEnd != null ? otherAssociationEnd.getName() : "";

                var expsource = (VariableExp)exp.getSource();
                var variable = expsource.getReferredVariable();
                var type = variable.getType();
                var typeName = type.getName();

                var keyAttribute = (CoreAttributeImpl)type.getAllAttributes().FirstOrDefault(); // primeiro atributo é o atributo-chave
                var name = keyAttribute != null ? keyAttribute.getName() : "";

                string otherFormula = string.Format("=INDEX({0},MATCH([area],{0}[{1}],0),COL({0}[{2}]))", otherTypeName, otherKeyName, otherName);

                associationEnds.Add(associationEnd);
                
                formula += string.Format("{0}[{1}],{2}[{3}]", otherTypeName, otherName, typeName, name);
            }

            navigationLevel++;
        }

        public void visitIteratorExpBegin(IteratorExp exp)
        {
            if (exp.getName() == "collect")
            {
                var body = exp.getBody();
                if (body is AttributeCallExpImpl)
                {
                    var bodyImpl = (AttributeCallExpImpl) body;
                    currentClassifier = (CoreClassifier) bodyImpl.getReferredAttribute().getElemOwner();
                    bodyImpl.accept(this);
                    formula += ",";
                }
                else if (body is AssociationEndCallExpImpl)
                {
                    var bodyImpl = (AssociationEndCallExpImpl) body;
                    bodyImpl.accept(this);
                    formula += ",";
                }
            }
            else if (exp.getName() == "select")
            {
                var body = exp.getBody();
                var bodyImpl = body as OperationCallExpImpl;
                if (bodyImpl != null)
                {
                    var operation = bodyImpl.getReferredOperation();
                    if (operation != null)
                    {
                        string operationName = operation.getName();
                        if (bodyImpl.isBasicOperator(operationName) && !bodyImpl.isBooleanOperator(operationName))
                        {
                            var bodySource = bodyImpl.getSource();
                            var bodySourceImpl = bodySource as AttributeCallExpImpl;
                            if (bodySourceImpl != null)
                            {
                                if (navigationLevel == 0)
                                {
                                    bodySourceImpl.accept(this);
                                }
                                else
                                {
                                    string typeName, name;
                                    getTableReference(bodySourceImpl, out name, out typeName);

                                    var columnName = string.Format("{0}_{1}", typeName.ToLower(), name.ToLower());
                                    formula += string.Format("{0}[{1}]", currentClassifier.getName(), columnName); 
                                }
                                
                                formula += ",";
                                var bodySourceOperation = bodyImpl.getReferredOperation();
                                if (bodySourceOperation.getName() != "=")
                                    formula += string.Format("\"{0}\"&", operationName);
                            }

                            var argument = (OclExpressionImpl) bodyImpl.getArguments().FirstOrDefault();
                            if (argument != null)
                                argument.accept(this);
                        }

                        formula += ",";
                    }
                }
            }
        }

        private void getTableReference(AttributeCallExp exp, out string name, out string typeName)
        {
            // get referred attribute name
            var attribute = exp.getReferredAttribute();
            name = attribute.getName();

            // get attribute source (always VariableExp) to get type (classifier) name
            var expsource = (VariableExp)exp.getSource();
            var variable = expsource.getReferredVariable();
            var type = variable.getType();
            typeName = type.getName();
        }

        public void visitIteratorExp(IteratorExp exp)
        {}

        public void visitIterateExp(IterateExp exp)
        {}
    }
}
