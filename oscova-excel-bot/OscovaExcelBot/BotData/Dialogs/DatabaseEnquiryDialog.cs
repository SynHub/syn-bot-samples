using System.Linq;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace OscovaExcelBot.BotData.Dialogs
{
    internal class DatabaseEnquiryDialog: Dialog
    {
        [Expression("What is the @property of @name")]
        [Expression("@property of @name")]
        public void PropertyEnquiry(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            var name = result.Entities.OfType("name").ToString();
            var property = result.Entities.OfType("property").ToString();

            var empByName = utility.EmployeesBy(ColumnName.Name, name);
            utility.Display(empByName);

            if (empByName.Any())
            {
                var propValue = empByName[0].GetValue(property);
                result.SendResponse($"{property} of Employee \"{name}\" is {propValue}.");
            }
        }

        [Expression("What is the @property of @role")]
        [Expression("@property of @role")]
        public void PropertyByJob(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            var role = result.Entities.OfType("role").ToString(); 
            var property = result.Entities.OfType("property").ToString();

            var empByRole = utility.EmployeesBy(ColumnName.Role, role);
            utility.Display(empByRole);

            if (empByRole.Any())
            {
                var propValue = empByRole[0].GetValue(property);
                result.SendResponse($"{property} of \"{role}\" is {propValue}.");
            }
        }

        [Expression("Find employee with the name @name")]
        [Expression("Who is @name?")]
        public void EmployeeName(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            var name = result.Entities.OfType("name").ToString();

            var empByName = utility.EmployeesBy(ColumnName.Name, name);
            utility.Display(empByName);

            result.SendResponse($"Employee(s) with the name {name}.");
        }

        [Expression("Find employee whose role is @role")]
        [Expression("Who is the @role?")]
        public void EmployeeRole(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();
            var role = result.Entities.OfType("role").ToString();

            var empByName = utility.EmployeesBy(ColumnName.Role, role);
            utility.Display(empByName);

            result.SendResponse($"Employee(s) with job role \"{role}\".");
        }   
    }
}