using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace OscovaExcelBot.BotData.Dialogs
{
    internal class DatabaseGenericDialog: Dialog
    {
        [Expression]
        public void Default(Context context, Result result)
        {
            result.SendResponse("Please rephrase your query.");
        }

        [Expression("{clear}")]
        [Expression("{reset}")]
        [Entity("clear-command")]
        public void ResetTable(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            result.SendResponse("Cleared.");
            utility.Employees.Clear();
        }
    }
}
