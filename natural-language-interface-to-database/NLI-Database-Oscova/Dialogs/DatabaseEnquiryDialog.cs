using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace NLI_Database_Oscova.Dialogs
{
    internal class DatabaseEnquiryDialog: Dialog
    {
        [Expression("What is the @property of @name")]
        [Expression("@property of @name")]
        public void PropertyEnquiry(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            var name = result.Entities.OfType("name").Value;
            var property = result.Entities.OfType("property").Value;

            utility.PropertyByName(name, property);
            result.SendResponse($"{property} of Employee \"{name}\".");
        }

        [Expression("What is the @property of @job")]
        [Expression("@property of @job")]
        public void PropertyByJob(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            var job = result.Entities.OfType("job").Value;
            var property = result.Entities.OfType("property").Value;

            utility.PropertyByJob(job, property);
            result.SendResponse($"{property} of \"{job}\".");
        }

        [Expression("Find employee with the name @name")]
        [Expression("Who is @name?")]
        public void EmployeeName(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            var name = result.Entities.OfType("name").Value;
            utility.EmployeeByName(name);
            result.SendResponse($"Employee(s) with the name {name}.");
        }

        [Expression("Find employee whose job is @job")]
        [Expression("Who is the @job?")]
        public void EmployeeJob(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            var jobName = result.Entities.OfType("job").Value;
            utility.EmployeeByJob(jobName);
            result.SendResponse($"Employee(s) with job role \"{jobName}\".");
        }   
    }
}