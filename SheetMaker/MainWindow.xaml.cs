using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Ocl20.library.iface.common;
using Ocl20.library.iface.constraints;
using Ocl20.library.impl.common;
using Ocl20.library.impl.expressions;
using Ocl20.modelreader;
using Ocl20.parser.cst.context;
using Ocl20.parser.semantics.types;
using Ocl20.parser.typeChecker;
using SheetMaker.sheetmodel;
using SheetMaker.xformula;
using CoreAssociationEnd = Ocl20.library.iface.common.CoreAssociationEnd;
using Environment = Ocl20.library.iface.environment.Environment;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheetMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConstraintSourceTracker tracker = new ConstraintSourceTrackerImpl();
        private Environment environment;

        public enum AssociationType
        {
            OneToOne,
            OneToMany,
            ManyToMany
        }

        public MainWindow()
        {
            InitializeComponent();

            TextBoxPath.Text =
                @"C:\Users\Leo\Documents\Visual Studio 2010\Projects\SheetModel_20121206\SheetModel\ModelMaker\Company.classdiagram";
            TextBoxExpression.Text = "context Empregado::Operation1() : Boolean body: salario->sum() < 1000.0";
        }

        /// <summary>
        /// Evento de click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateClick(object sender, RoutedEventArgs e)
        {
            const string filePath = "C:\\spreadsheet\\Teste.xlsx";

            if (File.Exists(filePath))
                File.Delete(filePath);

            Excel.Application eApp = new Excel.Application();

            try
            {
                XWorkbook xWorkbook = createXWorkbook(TextBoxPath.Text);

                PSWOclCompiler oclCompiler = new PSWOclCompiler(environment, tracker);
                
                List<object> nodes = oclCompiler.compileOclStream(TextBoxExpression.Text, "", 
                    new StreamWriter(Console.OpenStandardOutput()), typeof(CSTContextDeclarationCS));
                CSTOperationContextCS operationContextCS = ((CSTOperationContextCS) nodes[0]);
                var constraints = operationContextCS.getConstraintsNodesCS();
                CSTOperationConstraintCS operationConstraint = (CSTOperationConstraintCS) constraints[0];
                ExpressionInOcl expressionInOcl = operationConstraint.getExpressionInOCL();
                OclExpressionImpl bodyExpression = (OclExpressionImpl) expressionInOcl.getBodyExpression();
                
                XFormulaCreatorVisitor visitor = new XFormulaCreatorVisitor();
                bodyExpression.accept(visitor);
                string formula = visitor.getFormula();

                CoreClassifier classifier = (CoreClassifier) expressionInOcl.getContextualElement();
                XDataTable targetTable = getTargetTable(xWorkbook, classifier);

                var operation = operationContextCS.getOperationNodeCS();
                var name = operation.getOperationName();
                XDataTableColumn targetColumn =
                    targetTable.getDataTableColumns().FirstOrDefault(c => c.getName().Equals(name));
                
                if (targetColumn == null) 
                    throw new Exception("Coluna não encontrada!");
                
                XTextExp xtext = new XTextExp();
                xtext.setTextSymbol(formula);
                targetColumn.setDataContent(xtext);
                
                MessageBox.Show(formula);

                Excel.Workbook eBook = createWorkbook(eApp, xWorkbook);
                eBook = createValidation(eBook, xWorkbook);
                //eBook = createFormulas(eBook, xWorkbook);

                eBook.SaveAs(filePath);
                eBook.Close();

                Marshal.ReleaseComObject(eBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                eApp.Quit();
                Marshal.ReleaseComObject(eApp);
            }
        }

        /// <summary>
        /// Retorna a tabela correspondente a determinado classifier
        /// </summary>
        /// <param name="xWorkbook">workbook onde se encontra tabela</param>
        /// <param name="classifier">classifier correspondente a tabela</param>
        /// <returns>Se encontrar, retorna a referência para a tabela, caso contrário, retorna null</returns>
        private XDataTable getTargetTable(XWorkbook xWorkbook, CoreClassifier classifier)
        {
            foreach (XWorksheet xWorksheet in xWorkbook.getWorksheets())
            {
                foreach (XDataTable xDataTable in xWorksheet.getDataTables())
                {
                    if (xDataTable.getName().Equals(classifier.getName()))
                        return xDataTable;
                }
            }
            return null;
        }

        /// <summary>
        /// Cria a workbook e todos os seus objetos no formato do Excel (COM)
        /// </summary>
        /// <param name="eApp"></param>
        /// <param name="xWorkbook"></param>
        private Excel.Workbook createWorkbook(Excel.Application eApp, XWorkbook xWorkbook)
        {
            Excel.Workbook eBook = eApp.Workbooks.Add();
            eBook.Title = xWorkbook.getName();

            int defaultNumWorksheets = eBook.Worksheets.Count;
            int numWorksheets = 1;
            foreach (XWorksheet xWorksheet in xWorkbook.getWorksheets())
            {
                //Excel.Worksheet eWorksheet = numWorksheets < defaultNumWorksheets
                //                                 ? eBook.Worksheets[numWorksheets]
                //                                 : eBook.Worksheets.Add(Missing.Value,
                //                                                        eBook.Worksheets[numWorksheets - 1],
                //                                                        Missing.Value, Missing.Value);

                Excel.Worksheet eWorksheet = eBook.Worksheets.Add();
                eWorksheet.Name = xWorksheet.getName();

                int i = 1;
                foreach (XDataTable xDataTable in xWorksheet.getDataTables())
                {
                    eWorksheet.Cells[i, 1] = xDataTable.getName();

                    Excel.ListObjects eListObjects = eWorksheet.ListObjects;
                    Excel.Range bBegin = (Excel.Range) eWorksheet.Cells[++i, 1];
                    Excel.ListObject eListObject = eListObjects.Add(Excel.XlListObjectSourceType.xlSrcRange, bBegin, Missing.Value, Excel.XlYesNoGuess.xlNo, Missing.Value);
                    eListObject.Name = xDataTable.getName() ?? "";
                    eListObject.ShowTotals = true;
                    
                    eListObject.ListRows.Add();
                    eListObject.ListRows.Add();

                    int columns = 1;
                    foreach (XDataTableColumn sheetColumn in xDataTable.getDataTableColumns())
                    {
                        Excel.ListColumns eListColumns = eListObject.ListColumns;
                        Excel.ListColumn eListColumn = columns == 1 ? eListColumns[1] : eListColumns.Add();
                        eListColumn.Name = sheetColumn.getName();
                        eListColumn.TotalsCalculation = Excel.XlTotalsCalculation.xlTotalsCalculationSum;
                        eListColumn.Range.EntireColumn.ColumnWidth = 14;
                        
                        columns++;

                        Marshal.ReleaseComObject(eListColumn);
                        Marshal.ReleaseComObject(eListColumns);
                    }

                    Marshal.ReleaseComObject(eListObject);
                    Marshal.ReleaseComObject(bBegin);
                    Marshal.ReleaseComObject(eListObjects);
                }

                numWorksheets++;
                Marshal.ReleaseComObject(eWorksheet);
            }

            return eBook;
        }


        /// <summary>
        /// Cria as fórmulas do workbook passado como parâmetro
        /// </summary>
        /// <param name="eBook"></param>
        /// <param name="xWorkbook"></param>
        private Excel.Workbook createFormulas(Excel.Workbook eBook, XWorkbook xWorkbook)
        {
            foreach (XWorksheet xWorksheet in xWorkbook.getWorksheets())
            {
                Excel.Worksheet eWorksheet = eBook.Sheets[xWorksheet.getName()];
                
                foreach (XDataTable xDataTable in xWorksheet.getDataTables())
                {
                    Excel.ListObjects eListObjects = eWorksheet.ListObjects;
                    Excel.ListObject eListObject = eListObjects[xDataTable.getName()];

                    foreach (XDataTableColumn sheetColumn in xDataTable.getDataTableColumns())
                    {
                        Excel.ListColumns eListColumns = eListObject.ListColumns;
                        Excel.ListColumn eListColumn = eListColumns[sheetColumn.getName()];
                        
                        if (sheetColumn.getDataContent() != null)
                        {
                            Excel.Range rng = eListColumn.DataBodyRange;
                            XTextExp formula = (XTextExp)sheetColumn.getDataContent();
                            rng.Formula = string.Format("{0}", formula.getTextSymbol());

                            Marshal.ReleaseComObject(rng);
                        }

                        Marshal.ReleaseComObject(eListColumn);
                        Marshal.ReleaseComObject(eListColumns);
                    }

                    Marshal.ReleaseComObject(eListObject);
                    Marshal.ReleaseComObject(eListObjects);
                }
                
                Marshal.ReleaseComObject(eWorksheet);
            }

            return eBook;
        }

        /// <summary>
        /// </summary>
        /// <param name="eBook"></param>
        /// <param name="xWorkbook"></param>
        private Excel.Workbook createValidation(Excel.Workbook eBook, XWorkbook xWorkbook)
        {
            foreach (XWorksheet xWorksheet in xWorkbook.getWorksheets())
            {
                Excel.Worksheet eWorksheet = eBook.Sheets[xWorksheet.getName()];

                foreach (XDataTable xDataTable in xWorksheet.getDataTables())
                {
                    Excel.ListObjects eListObjects = eWorksheet.ListObjects;
                    Excel.ListObject eListObject = eListObjects[xDataTable.getName()];

                    foreach (XDataTableColumn sheetColumn in xDataTable.getDataTableColumns())
                    {
                        Excel.ListColumns eListColumns = eListObject.ListColumns;
                        Excel.ListColumn eListColumn = eListColumns[sheetColumn.getName()];

                        string xreference = sheetColumn.getXReference();
                        if (xreference != null)
                        {
                            var targettable = new XDataTable();
                            var targetsheet = GetXReference(xWorkbook, xreference, ref targettable);
                            if (targettable != null)
                            {
                                var index = targettable.getKeyIndex();
                                eWorksheet = eBook.Sheets[targetsheet.getName()];
                                eListObjects = eWorksheet.ListObjects;
                                eListObject = eListObjects[targettable.getName()];
                                eListColumns = eListObject.ListColumns;
                                eListColumn = eListColumns[index];

                                string rangename = targettable.getName() + index.ToString(CultureInfo.InvariantCulture);
                                eBook.Names.Add(rangename, eListColumn.DataBodyRange);

                                Excel.Name targetName = eBook.Names.Item(rangename, Type.Missing, Type.Missing);

                                eWorksheet = eBook.Sheets[xWorksheet.getName()];
                                eListObjects = eWorksheet.ListObjects;
                                eListObject = eListObjects[xDataTable.getName()];
                                eListColumns = eListObject.ListColumns;
                                eListColumn = eListColumns[sheetColumn.getName()];
                                eListColumn.DataBodyRange.Validation.Add(Excel.XlDVType.xlValidateList, 
                                    Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlBetween, 
                                    targetName, Type.Missing);
        
                            }

                            Marshal.ReleaseComObject(eListColumn);
                        }

                        Marshal.ReleaseComObject(eListColumn);
                        Marshal.ReleaseComObject(eListColumns);
                    }

                    Marshal.ReleaseComObject(eListObject);
                    Marshal.ReleaseComObject(eListObjects);
                }

                Marshal.ReleaseComObject(eWorksheet);
            }

            return eBook;
        }

        private XWorksheet GetXReference(XWorkbook xWorkbook, string xreference, ref XDataTable targettable)
        {
            foreach (XWorksheet xWorksheet in xWorkbook.getWorksheets())
            {
                targettable = xWorksheet.getDataTables().FirstOrDefault(d => d.getName().Equals(xreference));
                if (targettable != null) return xWorksheet;
            }
            targettable = null;
            return null;
        }


        /// <summary>
        /// Evento de click do botão de abrir arquivo .classdiagram do modelo 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void openFileClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result == true)
                TextBoxPath.Text = openFileDialog.FileName;
        }

        /// <summary>
        /// Retorna a representação de uma planilha em memória que satisfaz o modelo passado como parâmetro
        /// </summary>
        /// <param name="classDiagramPath">caminho para o arquivo .classdiagram que contém o modelo</param>
        /// <returns>represetação da planilha em memória</returns>
        public XWorkbook createXWorkbook(string classDiagramPath)
        {
            ModelReader reader = new VscdReader(classDiagramPath);
            CoreModelImpl model = (CoreModelImpl) reader.getModel();
            OclTypesFactory.setEnvironment(model);

            XWorkbook xWorkbook = new XWorkbook();
            xWorkbook.setName(model.getName());

            environment = model.getEnvironmentWithoutParents();
            IEnumerable<CoreClassifier> modelClasses = getClasses(environment, model);

            foreach (CoreClassifier coreClassifier in modelClasses)
            {
                XWorksheet xWorksheet = new XWorksheet();
                xWorksheet.setName(coreClassifier.getName());
                xWorksheet.setWorkbook(xWorkbook);
                updateWorkbook(xWorkbook, xWorksheet);
                
                XDataTable xDataTable = new XDataTable();
                xDataTable.setName(coreClassifier.getName());
                xDataTable.setWorksheet(xWorksheet);
                updateWorksheet(xWorksheet, xDataTable);

                createFeatureColumns(coreClassifier, xDataTable);
                createAssociationColumn(model, coreClassifier, xDataTable);
            }

            return xWorkbook;
        }

        /// <summary>
        /// Cria colunas na tabela em questão para cada feature do classifier
        /// </summary>
        /// <param name="coreClassifier">classifier</param>
        /// <param name="xDataTable">tabela</param>
        private void createFeatureColumns(CoreClassifier coreClassifier, XDataTable xDataTable)
        {
            List<object> features = new List<object>();
            features.AddRange(coreClassifier.getClassifierFeatures().Where(f => f.GetType() == typeof(CoreAttributeImpl)));
            features.AddRange(coreClassifier.getClassifierFeatures().Where(f => f.GetType() == typeof(CoreOperationImpl)));
            
            foreach (CoreFeature feature in features)
            {
                XDataTableColumn xDataTableColumn = new XDataTableColumn();
                xDataTableColumn.setName(feature.getName());
                xDataTableColumn.setDataTable(xDataTable);
                updateDataTable(xDataTable, xDataTableColumn);
            }
        }

        /// <summary>
        /// Cria colunas na tabela em questão para cada association end com multiplicidade 1 do classifier
        /// </summary>
        /// <param name="model">modelo onde se encontra o classifier</param>
        /// <param name="coreClassifier">classifier</param>
        /// <param name="xDataTable">tabela</param>
        private void createAssociationColumn(CoreModel model, CoreClassifier coreClassifier, XDataTable xDataTable)
        {
            foreach (CoreAssociationEnd associationEnd in model.getAssociationEndsForClassifier(coreClassifier))
            {
                if (associationEnd.isOneMultiplicity())
                {
                    XDataTableColumn xDataTableColumn = new XDataTableColumn();
                    xDataTableColumn.setXReference(associationEnd.getType().getName());
                    xDataTableColumn.setName(associationEnd.getName());
                    xDataTableColumn.setDataTable(xDataTable);
                    updateDataTable(xDataTable, xDataTableColumn);
                }
            }
        }

        /// <summary>
        /// Adiciona uma nova worksheet a uma workbook existente
        /// </summary>
        /// <param name="xWorkbook">workbook</param>
        /// <param name="xWorksheet">nova worksheet</param>
        private void updateWorkbook(XWorkbook xWorkbook, XWorksheet xWorksheet)
        {
            List<XWorksheet> xWorksheets = xWorkbook.getWorksheets();
            xWorksheets.Add(xWorksheet);
            xWorkbook.setWorksheets(xWorksheets);
        }

        /// <summary>
        /// Adiciona uma nova datatable a uma worksheet existente
        /// </summary>
        /// <param name="xWorksheet">worksheet</param>
        /// <param name="xDataTable">nova datatable</param>
        private void updateWorksheet(XWorksheet xWorksheet, XDataTable xDataTable)
        {
            List<XDataTable> xDataTables = xWorksheet.getDataTables();
            xDataTables.Add(xDataTable);
            xWorksheet.setDataTables(xDataTables);
        }

        /// <summary>
        /// Adiciona uma nova coluna a uma tabela existente
        /// </summary>
        /// <param name="xDataTable">datatable</param>
        /// <param name="xDataTableColumn">nova coluna</param>
        private void updateDataTable(XDataTable xDataTable, XDataTableColumn xDataTableColumn)
        {
            List<XDataTableColumn> xDataTableColumns = xDataTable.getDataTableColumns();
            xDataTableColumns.Add(xDataTableColumn);
            xDataTable.setDataTableColumns(xDataTableColumns);
        }

        /// <summary>
        /// Retorna todas as classes existente em um determinado modelo
        /// </summary>
        /// <param name="environment">environment</param>
        /// <param name="model">modelo</param>
        /// <returns></returns>
        private IEnumerable<CoreClassifier> getClasses(Environment environment, CoreModelImpl model)
        {
            var classes = environment.getAllOfType(typeof(CoreClassifierImpl));
            List<CoreClassifier> realClasses = new List<CoreClassifier>();
            foreach (CoreClassifierImpl classifier in classes)
            {
                if (!model.isPrimitiveType(classifier))
                    realClasses.Add(classifier);
            }

            return realClasses;
        }
    }
}
