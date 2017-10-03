using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using OscovaExcelBot.BotData.Dialogs;
using Prism.Commands;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Collections;
using Syn.Bot.Oscova.Entities;
using Syn.Utilities.Components;
using Action = System.Action;
using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace OscovaExcelBot
{
    public class BotViewModel : ViewModelBase
    {
        private string _input;
        private Action _sendAction;
        private string _output;

        public BotViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            FullEmployeeList = new List<Employee>();
            Input = string.Empty;

            SendCommand = new DelegateCommand(() =>
            {
                _sendAction?.Invoke();
            });

            Initialize();
        }

        public void Initialize()
        {
            try
            {
                Employees.Clear();
                FullEmployeeList.Clear();
                Input = string.Empty;

                var sheet = Globals.ThisAddIn.Application.ActiveSheet as Worksheet;

                var columnCount = sheet?.UsedRange.Columns.Count;
                var rowCount = sheet?.UsedRange.Rows.Count;

                var columnNameIndexTable = new Dictionary<int, string>();

                //Add column names and index.
                for (var i = 1; i <= columnCount; i++)
                {
                    var cell = sheet.UsedRange.Cells[1, i] as Range;
                    if (cell != null) columnNameIndexTable.Add(i, cell.Value.ToString());
                }

                //Create employees list from row data.
                for (var r = 2; r <= rowCount; r++)
                {
                    var employee = new Employee();
                    for (var c = 1; c <= columnCount; c++)
                    {
                        var cell = sheet.UsedRange.Cells[r, c] as Range;
                        var columnName = columnNameIndexTable[c].ToLower();
                        var stringCellValue = cell?.Value.ToString();
                        if (stringCellValue == null) break;
                        if (columnName == "id") employee.Id = int.Parse(stringCellValue);
                        if (columnName == "salary") employee.Salary = int.Parse(stringCellValue);
                        if (columnName == "age") employee.Age = int.Parse(stringCellValue);
                        if (columnName == "name") employee.Name = stringCellValue;
                        if (columnName == "role") employee.Role = stringCellValue;
                    }
                    FullEmployeeList.Add(employee);
                }


                Bot = new OscovaBot();
                Bot.MainUser.Context.SharedData.Add(this);
                Bot.Dialogs.Add(new DatabaseGenericDialog());
                Bot.Dialogs.Add(new DatabaseEnquiryDialog());
                Bot.Dialogs.Add(new DatabaseSuperlativeDialog());

                Bot.MainUser.ResponseReceived += (sender, args) =>
                {
                    Output = args.Response.Text;
                };

                Task.Run(() =>
                {
                    Bot.CreateRecognizer("property", new[] { "ID", "Name", "Role", "Age", "Salary" });
                    Bot.CreateRecognizer("role", new[] { "CEO", "Manager", "Admin", "Engineer", "Tech", "Support" });
                    AddEmployeeNameRecognizer();
                    Bot.Trainer.StartTraining();
                });

                _sendAction = () =>
                {
                    Output = string.Empty;
                    var evaluationResult = Bot.Evaluate(Input);
                    evaluationResult.Invoke();
                    Input = string.Empty;
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Shows how to create an entity recognizer without loading entries.
        private void AddEmployeeNameRecognizer()
        {
            try
            {
                Bot.CreateRecognizer("Name", request =>
                {
                    var requestTest = request.NormalizedText;
                    var entities = new EntityCollection();

                    foreach (var emp in FullEmployeeList)
                    {
                        var name = emp.Name;
                        var wordIndex = requestTest.IndexOf(name, StringComparison.OrdinalIgnoreCase);
                        if (wordIndex != -1)
                        {
                            var entity = new Entity("Name")
                            {
                                Value = name,
                                Reference = name,
                                Index = wordIndex
                            };
                            entities.Add(entity);
                        }
                    }
                    return entities;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }  
        }

        public List<Employee> FullEmployeeList { get; }

        public OscovaBot Bot { get; private set; }

        public ObservableCollection<Employee> Employees { get; }

        public string Input
        {
            get { return _input; }
            set { _input = value; OnPropertyChanged(nameof(Input)); }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged(nameof(Output)); }
        }

        public DelegateCommand SendCommand { get; }

        #region Helper Methods

        public List<Employee> EmployeesBy(string propName, string propValue)
        {
            return EmployeesBy(propName, propValue, FullEmployeeList);
        }

        public List<Employee> EmployeesBy(string propertyName, string propertyValue, IEnumerable<Employee> employees)
        {
            propertyName = propertyName.ToLower();
            return employees.Where(emp => emp.GetValue(propertyName).Equals(propertyValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Display(IEnumerable<Employee> employees)
        {
            Employees.Clear();
            Employees.AddRange(employees);
        }

        #endregion
    }
}