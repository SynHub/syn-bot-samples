using System.Xml.Linq;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Interfaces;

namespace NLI_Database_SIML.Adapter
{
    public class SqlAdapter : IAdapter
    {
        private readonly MainWindow _window;
        public SqlAdapter(MainWindow window)
        {
            _window = window;
        }
        public bool IsRecursive => true;
        public XName TagName => SimlSpecification.Namespace.X + "Sql";

        public string Evaluate(Context parameter)
        {
            _window.UpdateDataGrid(parameter.Element.Value);
            return string.Empty;
        }
    }
}