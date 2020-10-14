using System;
using System.IO;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;

namespace AndrewWithInterface
{
    public class MWord : IDisposable
    {
        private Word.Application wordApp;
        private Word.Document myWordDoc;
        private string saveAs = "";
        public MWord(string FileName, string SaveAs)
        {
            wordApp = new Word.Application();
            myWordDoc = wordApp.Documents.Open(AppDomain.CurrentDomain.BaseDirectory + "\\"+FileName);
            myWordDoc.Activate();
            saveAs = SaveAs;
        }
        public void FindAndReplace(/*Word.Application wordApp, */object ToFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format,
                ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl
                );
        }

        public void Save()
        {
            myWordDoc.SaveAs(saveAs);
            myWordDoc.Close();
        }

        public void Dispose()
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(myWordDoc);
            wordApp.Quit();
            Marshal.ReleaseComObject(wordApp);
        }
    }
}
