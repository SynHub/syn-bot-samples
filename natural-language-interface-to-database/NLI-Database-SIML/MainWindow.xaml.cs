using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using NLI_Database_SIML.Adapter;
using NLI_Database_SIML.Sets;
using Syn.Bot.Siml;

namespace NLI_Database_SIML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public DatabaseUtility DatabaseUtility { get; }
        public SimlBot Bot { get; }

        public MainWindow()
        {
            InitializeComponent();
            Bot = new SimlBot();
            DatabaseUtility = new DatabaseUtility();
            DatabaseUtility.Initialize();
            UpdateDataGrid("SELECT * From Employees");
            Bot.Sets.Add(new NameSet(DatabaseUtility));
            Bot.Sets.Add(new JobSet(DatabaseUtility));
            Bot.Sets.Add(new SalarySet(DatabaseUtility));
            Bot.Sets.Add(new AgeSet(DatabaseUtility));
            Bot.Sets.Add(new IdSet(DatabaseUtility));
            Bot.Adapters.Add(new SqlAdapter(this));
            var simlFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "SIML"), "*.siml", SearchOption.AllDirectories);
            foreach (var simlDocument in simlFiles.Select(XDocument.Load))
            {
                Bot.AddSiml(simlDocument);
            }
            ExamplesBox.Text = Properties.Resources.Examples;
        }

        public void UpdateDataGrid(string sql)
        {
            var dataSet = new DataSet();
            var dataAdapter = new SQLiteDataAdapter(sql, DatabaseUtility.Connection);
            dataAdapter.Fill(dataSet);
            EmployeeGrid.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void ExecuteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = Bot.Chat(string.IsNullOrEmpty(InputBox.Text) ? "clear" : InputBox.Text);
            ResponseLabel.Content = result.BotMessage;
            InputBox.Clear();
        }

        private void InputBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ExecuteButton_OnClick(null, null);
            }
        }
    }
}