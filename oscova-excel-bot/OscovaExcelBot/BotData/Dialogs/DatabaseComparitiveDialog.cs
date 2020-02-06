using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace OscovaExcelBot.BotData.Dialogs
{
    internal class DatabaseComparitiveDialog: Dialog
    {
        [Expression("find employee with @property @gt @sys.number.integer")]
        [Expression("@property @gt @sys.number.integer")]
        public void EmployeeBySalaryMax(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            var propEntity = result.Entities.OfType("property").Value.ToLower();
            if (propEntity == "salary")
            {
                var com = result.Entities.OfType("gt").ToString();
                var salValue = result.Entities.OfType("sys.number.integer").Value;

                var empByName = utility.EmployeeCompare(salValue, "gt");
                utility.Display(empByName);

                result.SendResponse($"Employee(s) with \"{propEntity}\" \"{com}\" \"{salValue}\"");
            }
        }

        [Expression("find employee with @property @lt @sys.number.integer")]
        [Expression("@property @lt @sys.number.integer")]
        public void EmployeeBySalaryMin(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            var propEntity = result.Entities.OfType("property").Value.ToLower();
            if (propEntity == "salary")
            {
                var com = result.Entities.OfType("lt").ToString();
                var salValue = result.Entities.OfType("sys.number.integer").Value;

                var empByName = utility.EmployeeCompare(salValue, "lt");
                utility.Display(empByName);

                result.SendResponse($"Employee(s) with \"{propEntity}\" \"{com}\" \"{salValue}\"");
            }
        }
    }
}
