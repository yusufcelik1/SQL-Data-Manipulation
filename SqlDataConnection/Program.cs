using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace FileCopying
{
    class ExcelWriter
    {
        public void ConvertTextToExcel()
        {
            #region Initialization
            string[] InputNamesLine = File.ReadAllLines(@""); // Replace with your text file location
            excel.Application oXL;
            excel._Workbook oWB;
            excel._Worksheet oSheet;
            excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            #endregion
            try
            {
                // Start Excel and create the application object
                oXL = new excel.Application() { Visible = true };

                // Create a new workbook
                oWB = (excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (excel._Worksheet)oWB.ActiveSheet;

                // Add table headers in the first row
                oSheet.Cells[1, 1] = "Parameter Names";
                oSheet.Cells[1, 2] = "Values";

                // Format the headers
                oSheet.get_Range("A1", "B1").Font.Bold = true;
                oSheet.get_Range("A1", "B1").VerticalAlignment = excel.XlVAlign.xlVAlignCenter;

                int rowIndex = 2; // Start adding data from the second row
                foreach (string line in InputNamesLine)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] columns = line.Split(' ', '\r');
                        string column1 = columns[0];
                        string column2 = columns[1];

                        oSheet.Cells[rowIndex, 1] = column1;
                        oSheet.Cells[rowIndex, 2] = column2;
                        
                        rowIndex++;
                    }
                }

                Thread.Sleep(5000); // Delay for visualization

                // Save the Excel workbook
                oRng = oSheet.get_Range("A1", "B1");
                oXL.DisplayAlerts = false; // Suppress display alerts
                oWB.SaveAs(@"LocationWriteHere.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Close the workbook and Excel application
                oWB.Close();
                oXL.Quit();
            }
            catch (Exception e)
            {
                // Display an error message in case of an exception
                System.Windows.Forms.MessageBox.Show("Exception: " + e);
            }
        }
    }
}
