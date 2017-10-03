using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;
using NLI_Database_Oscova.Dialogs;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Collections;
using Syn.Bot.Oscova.Entities;

namespace NLI_Database_Oscova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public OscovaBot Bot { get; }
        public DatabaseUtility DatabaseUtility { get; }

        public MainWindow()
        {
            InitializeComponent();

            Bot = new OscovaBot();
           

            Bot.MainUser.ResponseReceived += (sender, args) =>
            {
                ResponseLabel.Content = args.Response.Text;
            };

            DatabaseUtility = new DatabaseUtility(this);
            DatabaseUtility.Initialize();
            UpdateDataGrid("SELECT * From Employees");

            Bot.MainUser.Context.SharedData.Add(DatabaseUtility);

            CreateNameParser();
            CreateJobParser();

            Bot.CreateRecognizer("set", new[] { "change", "update", "set" });
            Bot.CreateRecognizer("property", new[] { "id", "name", "job", "age", "salary" });
            Bot.CreateRecognizer("equality", new[] { "greater than", "less than", "equal to", "not equal to", "greater than or equal to", "less than or equal to" });

            //Add all required dialogs.
            Bot.Dialogs.Add(new DatabaseGenericDialog());
            Bot.Dialogs.Add(new DatabaseUpdateByNameDialog());
            Bot.Dialogs.Add(new DatabaseUpdateByIdDialog());
            Bot.Dialogs.Add(new DatabaseEnquiryDialog());
            Bot.Dialogs.Add(new DatabaseSuperlativeDialog());

            Bot.Language.Filters.AddWordFilter("=", "equal to");
            Bot.Language.Filters.AddWordFilter( ">", "greater than");
            Bot.Language.Filters.AddWordFilter("<", "less than");
            Bot.Language.Filters.AddWordFilter( ">=", "greater than or equal to");
            Bot.Language.Filters.AddWordFilter( "<=", "less than or equal to");
            Bot.Language.Filters.AddWordFilter("!=", "not equal to");

            //Finally Train the Bot.
            //var data = Bot.Trainer.GetTrainingData();
            //data.Save(@"d:\training-data.xml");
            Bot.Trainer.StartTraining();
        }

        public void UpdateDataGrid(string sql)
        {
            var dataSet = new DataSet();
            var dataAdapter = new SQLiteDataAdapter(sql, DatabaseUtility.Connection);
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables.Count > 0)
            {
                EmployeeGrid.ItemsSource = dataSet.Tables[0].DefaultView;
            }
        }


        //Shows how to create an entity recognizer without loading entries.
        private void CreateNameParser()
        {
            Bot.CreateRecognizer("Name", request =>
            {
                var requestTest = request.NormalizedText;
                var entities = new EntityCollection();

                DatabaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
                var reader = DatabaseUtility.Command.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var wordIndex = requestTest.IndexOf(name, StringComparison.OrdinalIgnoreCase);
                    if (wordIndex != -1)
                    {
                        var entity = new Entity("Name")
                        {
                            Value = name,
                            Index = wordIndex
                        };
                        entities.Add(entity);
                    }
                }

                reader.Close();
                return entities;
            });
        }

        //Shows how to create a parser by loading all entries.
        private void CreateJobParser()
        {
            var parser = Bot.CreateRecognizer("Job");
            DatabaseUtility.Command.CommandText = "SELECT * FROM EMPLOYEES";
            var reader = DatabaseUtility.Command.ExecuteReader();
            while (reader.Read())
            {
                parser.Entries.Add(reader["Job"].ToString());
            }
            reader.Close();
        }

        private void InputBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                EvaluateButton_OnClick(null, null);
            }
        }

        private void EvaluateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var message = string.IsNullOrEmpty(InputBox.Text) ? "clear" : InputBox.Text;
            var result = Bot.Evaluate(message);
            ResultBox.Text = result.Serialize();
            result.Invoke();
            InputBox.Clear();
        }
    }
}