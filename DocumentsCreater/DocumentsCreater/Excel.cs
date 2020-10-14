using System;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace AndrewWithInterface
{
    public class MExcel: IDisposable
    {
        private Excel.Application excelApp;
        private Excel.Workbook excelWorkbook;
        private Excel.Worksheet excelWorksheet;
        private Excel.Range excelRange;
        private string saveAs = "";
        public MExcel(string FileName, string SaveAs)
        {
            excelApp = new Excel.Application();
            excelWorkbook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "\\" + FileName);
            excelWorksheet = excelApp.ActiveSheet; 
            excelRange = excelWorksheet.UsedRange;
            saveAs = SaveAs;
        }
        public void Merge(string rangeString)
        {
            excelWorksheet.Range[excelWorksheet.Cells[2, 2], excelWorksheet.Cells[4, 2]].Merge();   
        }

        public void FindAndReplace(object ToFindText, object replaceWithText)
        {
            excelRange.Replace(ToFindText, replaceWithText);
        }

        public void Save()
        {
            excelApp.DisplayAlerts = false;
            excelWorkbook.SaveAs(saveAs);
            excelWorkbook.Close();
        }
        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(excelRange);
            Marshal.ReleaseComObject(excelWorksheet);
            Marshal.ReleaseComObject(excelWorkbook);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
        }
    }
}
