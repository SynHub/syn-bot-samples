using System;
using System.IO;
using Office = Microsoft.Office.Core;

namespace OscovaExcelBot
{
    public partial class ThisAddIn
    {
        private static bool _isVisible;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
#if DEBUG
            //PLEASE CHANGE THE FOLLOWING TO THE APPROPRIATE LOCATION.
            const string excelFilePath = @"H:\GitHub\oscova-excel-bot\OscovaExcelBot\bin\Debug\employees.xlsx";
            if(File.Exists(excelFilePath)) Application.Workbooks.Open(excelFilePath);
#endif
        }

        public void DisplayBotControl()
        {
            if (_isVisible) return;

            try
            {
                var botControl = new BotControl();
                var hostControl = new HostControl { ElementHost = { Child = botControl } };
                var customPane = CustomTaskPanes.Add(hostControl, "Excel Bot");
                customPane.Width = hostControl.Width;
                customPane.Visible = true;
                _isVisible = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
           
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;

            Application.WorkbookOpen += wb =>
            {
                DisplayBotControl();
            };
        }

        #endregion
    }
}
