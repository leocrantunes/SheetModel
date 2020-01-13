using System.Collections.Generic;
using System.Linq;
using Ocl20.library.iface.common;
using Ocl20.library.iface.expressions;
using Ocl20.library.impl;
using Ocl20.library.impl.common;
using Ocl20.library.impl.expressions;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;

namespace SheetMaker.xformula
{
    public class XFormulaCreatorVisitor : IASTOclVisitor
    {
        private string formula = "=";
        private string otherFormula;
        private string firstNavigationName;
        private CoreClassifier currentClassifier;
        private int navigationLevel = 0;
        private readonly Dictionary<string, string> columnToFormula;

        public XFormulaCreatorVisitor()
        {
            columnToFormula = new Dictionary<string, string>();
        }

        public string getFormula()
        {
            return formula;
        }

        public Dictionary<string, string> getExtraColumns()
        {
            return columnToFormula;
        }

        public CoreClassifier getCurrentClassifier()
        {
            return currentClassifier;
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
            if (exp.getStringSymbol() == "today")
            {
                formula += "TODAY()";
            }
            else
            {
                // get string symbol and add to formula with quotation marks
                formula += "\"" + exp.getStringSymbol() + "\"";
            }
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

        public void visitIfExpBegin(IfExp exp)
        {
            formula += "IF(";
        }

        public void visitIfExpThenBegin(IfExp exp)
        {
            formula += ",";
        }

        public void visitIfExpElseBegin(IfExp exp)
        {
            formula += ",";
        }

        public void visitIfExp(IfExp exp)
        {
            formula += ")";
        }

        public void beginVisitLetExp(LetExp exp)
        {}

        public void endVisitLetExp(LetExp exp)
        {}

        public void visitAttributeCallExp(AttributeCallExp exp)
        {
            if (exp.getSource() is AssociationEndCallExpImpl)
            {
                string typeName, name;
                getTableReference(exp, out name, out typeName);

                var columnName = string.Format("{0}_{1}", typeName.ToLower(), name.ToLower());
                formula += string.Format("{0}[{1}]", currentClassifier.getName(), columnName);

                otherFormula += string.Format(",COL({0}[{1}]))", typeName, name);
                columnToFormula.Add(columnName, otherFormula);
            }
            else
            {
                string typeName, name;
                getTableReference(exp, out name, out typeName);

                // format table_name[column_name] (same of classifier.attribute)
                formula += string.Format("{0}[{1}]", typeName, name);

                if (currentClassifier == null)
                    currentClassifier = (CoreClassifier)exp.getReferredAttribute().getElemOwner();    
            }
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
            var source = operationCall.getSource();

            // boolean operators or not basic operators (because boolean operator is a basic operator)
            if (isBooleanOperator || !isBasicOperator)
            {
                // set formula name based on operation name
                string xoperationname = operationName;
                
                // map operation name to formula name based on source expression
                if (source is AssociationEndCallExpImpl || source is IteratorExpImpl)
                {
                    if (operationName.Equals("size")) xoperationname = "COUNTIFS";
                    else if (operationName.Equals("sum")) xoperationname = "SUMIFS";
                }
                else if (source is VariableExpImpl)
                {
                    formula += string.Format("[{0}]", operationName);
                }
                else
                {
                    if (operationName.Equals("size")) xoperationname = "COUNTA";
                    else if (operationName.Equals("sum")) xoperationname = "SUM";
                    else if (operationName.Equals("toDate")) xoperationname = ""; // suppress toDate operation
                }

                if (!(source is VariableExpImpl))
                    formula += xoperationname.ToUpper();
            }

            if (!(source is VariableExpImpl))
                formula += "(";
        }

        public void visitOperationCalllExpBegin(OperationCallExp exp)
        {
            var operation = exp.getReferredOperation();
            if (operation == null) return;

            // get type of operation
            string operationName = operation.getName();
            OperationCallExpImpl operationCall = (OperationCallExpImpl) exp;
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
            if (operation != null && !(exp.getSource() is VariableExpImpl))
            {
                formula += ")";
                navigationLevel = 0;
            }
        }

        public void visitAssociationEndCallExp(AssociationEndCallExp exp)
        {
            var associationEnd = exp.getReferredAssociationEnd();
            bool isOneMultiplicity = associationEnd.isOneMultiplicity();
            string otherTypeName, otherKeyName, typeName, name;

            if (isOneMultiplicity)
            {
                getOneMultiplicityAssociationReference(exp, associationEnd, 
                    out otherTypeName, out otherKeyName, out typeName, out name);
                otherFormula = string.Format("=INDEX({0},MATCH([{1}],{0}[{2}],0)", otherTypeName, name, otherKeyName);
            }
            else
            {
                string otherName;
                getTargetAssociationReference(exp, associationEnd, 
                    out otherTypeName, out otherKeyName, out otherName, out typeName, out name);

                if (navigationLevel == 0)
                {
                    // add reference to key column of associated table
                    formula += string.Format("{0}[{1}],{2}[{3}]", otherTypeName, otherName, typeName, name);
                }
                else
                {
                    // create new column on target table (where expression is based on)
                    var columnName = string.Format("{0}_{1}", typeName.ToLower(), name.ToLower());
                    formula += string.Format("{0}[{1}],{2}[{3}]", currentClassifier.getName(), columnName, typeName, name);
                    otherFormula = string.Format("=INDEX({4},MATCH(INDEX({0},MATCH([{1}],{0}[{2}],0),COL({0}[{3}])),{4}[{5}],0),COL({4}[{5}]))",
                            otherTypeName, firstNavigationName, otherKeyName, otherName, typeName, name);
                    columnToFormula.Add(columnName, otherFormula);
                }    
            }
            
            navigationLevel++;
        }

        private void getTargetAssociationReference(AssociationEndCallExp exp, CoreAssociationEnd associationEnd, out string otherTypeName, out string otherKeyName, out string otherName, out string typeName, out string name)
        {
            var otherType = associationEnd.getType();
            otherTypeName = otherType.getName();

            var otherKeyAttribute =
                (CoreAttributeImpl) otherType.getAllAttributes().FirstOrDefault(a => ((CoreAttributeImpl) a).hasStereotype("Id")) ??
                (CoreAttributeImpl) otherType.getAllAttributes().FirstOrDefault();
            otherKeyName = otherKeyAttribute != null ? otherKeyAttribute.getName() : "";

            var participant = associationEnd.getTheParticipant();
            var association = associationEnd.getAssociation();
            var otherAssociationEnd = (CoreAssociationEndImpl) association.getTheAssociationEnds(participant).FirstOrDefault();
            otherName = otherAssociationEnd != null ? otherAssociationEnd.getName() : "";

            var expsource = (VariableExp)exp.getSource();
            var variable = expsource.getReferredVariable();
            var type = variable.getType();
            typeName = type.getName();

            var keyAttribute = 
                (CoreAttributeImpl) type.getAllAttributes().FirstOrDefault(a => ((CoreAttributeImpl) a).hasStereotype("Id")) ??
                (CoreAttributeImpl) type.getAllAttributes().FirstOrDefault();
            name = keyAttribute != null ? keyAttribute.getName() : "";
        }

        private void getOneMultiplicityAssociationReference(AssociationEndCallExp exp, CoreAssociationEnd associationEnd, out string otherTypeName, out string otherKeyName, out string typeName, out string name)
        {
            var otherType = associationEnd.getType();
            otherTypeName = otherType.getName();

            var otherKeyAttribute =
                (CoreAttributeImpl)otherType.getAllAttributes().FirstOrDefault(a => ((CoreAttributeImpl)a).hasStereotype("Id")) ??
                (CoreAttributeImpl)otherType.getAllAttributes().FirstOrDefault();
            otherKeyName = otherKeyAttribute != null ? otherKeyAttribute.getName() : "";

            var expsource = (VariableExp)exp.getSource();
            var variable = expsource.getReferredVariable();
            var type = variable.getType();
            typeName = type.getName();

            name = associationEnd.getName();
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
                    
                    var associationEnd = bodyImpl.getReferredAssociationEnd();
                    bool isOneMultiplicity = associationEnd.isOneMultiplicity();
                    if (!isOneMultiplicity)
                    {
                        string otherTypeName, otherKeyName, otherName, typeName, name;
                        getTargetAssociationReference(bodyImpl, associationEnd, out otherTypeName, out otherKeyName,
                                                      out otherName, out typeName, out name);
                        otherFormula = string.Format("=INDEX({0},MATCH([{1}],{0}[{2}],0)",typeName, otherName, name);
                        firstNavigationName = otherName;
                    }

                    navigationLevel++;
                }
                else if (body is OperationCallExpImpl)
                {
                    var bodyImpl = (OperationCallExpImpl) body;

                    // get referred operation name
                    var operation = bodyImpl.getReferredOperation();
                    var name = operation.getName();

                    var expsource = (VariableExp) bodyImpl.getSource();
                    var variable = expsource.getReferredVariable();
                    var type = variable.getType();
                    var typeName = type.getName();

                    formula += string.Format("{0}[{1}]", typeName, name);
                    formula += ",";

                    if (currentClassifier == null)
                        currentClassifier = (CoreClassifier) operation.getElemOwner();
                }
            }
            else if (exp.getName() == "select")
            {
                processSelectExpression(exp);
            }
        }

        private void processSelectExpression(IteratorExp exp)
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

                                otherFormula += string.Format(",COL({0}[{1}]))", typeName, name);
                                columnToFormula.Add(columnName, otherFormula);
                            }

                            formula += ",";
                            var bodySourceOperation = bodyImpl.getReferredOperation();
                            if (bodySourceOperation.getName() != "=")
                                formula += string.Format("\"{0}\"&", operationName);
                        }
                        else
                        {
                            var obodySourceImpl = bodySource as OperationCallExpImpl;
                            if (obodySourceImpl != null && navigationLevel == 0)
                            {
                                // get referred operation name
                                var bodySourceOperation = obodySourceImpl.getReferredOperation();
                                var name = bodySourceOperation.getName();

                                var expsource = (VariableExp)obodySourceImpl.getSource();
                                var variable = expsource.getReferredVariable();
                                var type = variable.getType();
                                var typeName = type.getName();

                                formula += string.Format("{0}[{1}]", typeName, name);
                                formula += ",";
                            }
                        }

                        var argument = (OclExpressionImpl)bodyImpl.getArguments().FirstOrDefault();
                        if (argument != null)
                            argument.accept(this);
                    }

                    formula += ",";
                }
            }
        }

        private void getTableReference(AttributeCallExp exp, out string name, out string typeName)
        {
            // get referred attribute name
            var attribute = exp.getReferredAttribute();
            name = attribute.getName();
            typeName = null;

            // get attribute source to get type (classifier) name
            if (exp.getSource() is VariableExpImpl)
            {
                var expsource = (VariableExp) exp.getSource();
                var variable = expsource.getReferredVariable();
                var type = variable.getType();
                typeName = type.getName();
            }
            else if (exp.getSource() is AssociationEndCallExpImpl)
            {
                var expsource = (AssociationEndCallExp)exp.getSource();
                var associationEnd = expsource.getReferredAssociationEnd();
                var type = associationEnd.getType();
                typeName = type.getName();
            }
        }

        public void visitIteratorExp(IteratorExp exp)
        {}

        public void visitIterateExp(IterateExp exp)
        {}
    }
}