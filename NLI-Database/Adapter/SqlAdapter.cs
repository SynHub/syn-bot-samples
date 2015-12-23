using System.Xml.Linq;
using Syn.Bot;
using Syn.Bot.Interfaces;

namespace NLI_Database.Adapter
{
    public class SqlAdapter : IAdapter
    {
        private readonly MainWindow _window;
        public SqlAdapter(MainWindow window)
        {
            _window = window;
        }
        public bool IsRecursive { get { return true; } }
        public XName TagName { get { return Specification.Namespace.X + "Sql"; } }
        public string Evaluate(Context parameter)
        {
            _window.UpdateDataGrid(parameter.Element.Value);
            return string.Empty;
        }
    }
}
