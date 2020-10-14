using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace AndrewWithInterface
{
    public partial class MainForm : Form
    {
        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private int filesCount;
        public MainForm()
        {
            InitializeComponent();
            this.Show();
            refresh_Click(null, null);
            checkBoxIsOOO_CheckedChanged(null, null);
            savePathShow();
            dateTimePicker.Value = DateTime.Today;
        }
        private void textBoxCheck(TextBox obj, ref bool resultFlag)
        {
            if (obj.TextLength == 0)
            {
                obj.BackColor = Color.FromArgb(255, 128, 128);
                resultFlag = false;
            }
        }
        private bool DataCheck()
        {
            bool resultFlag = true;
            if (checkBoxNoLimits.Checked)
                return true;
            // Return to all textBoxes their default colors
            #region
            textBoxNumber.BackColor = Color.White;
            textBoxName.BackColor = Color.White;
            textBoxFioIm.BackColor = Color.White;
            textBoxFioRod.BackColor = Color.White;
            textBoxAdressUr.BackColor = Color.White;
            textBoxAdressFact.BackColor = Color.White;
            textBoxAdressMail.BackColor = Color.White;
            textBoxINN.BackColor = Color.White;
            textBoxORGN.BackColor = Color.White;
            textBoxBank.BackColor = Color.White;
            textBoxBIK.BackColor = Color.White;
            textBoxAccountRash.BackColor = Color.White;
            textBoxAccountCorr.BackColor = Color.White;
            textBoxPhone.BackColor = Color.White;
            textBoxEmail.BackColor = Color.White;
            textBoxAdressConnection.BackColor = Color.White;
            textBoxSpeed.BackColor = Color.White;
            textBoxCost.BackColor = Color.White;
            textBoxIP.BackColor = Color.White;
            textBoxMask.BackColor = Color.White;
            textBoxGateway.BackColor = Color.White;
            textBoxLineParams.BackColor = Color.White;
            #endregion
            // Just a bunch of checks
            #region
            textBoxCheck(textBoxNumber, ref resultFlag);
            if (checkBoxIsOOO.Checked) textBoxCheck(textBoxName, ref resultFlag);
            textBoxCheck(textBoxNumber, ref resultFlag);
            textBoxCheck(textBoxFioIm, ref resultFlag);
            textBoxCheck(textBoxFioRod, ref resultFlag);
            textBoxCheck(textBoxAdressUr, ref resultFlag);
            textBoxCheck(textBoxAdressFact, ref resultFlag);
            textBoxCheck(textBoxAdressMail, ref resultFlag);
            textBoxCheck(textBoxINN, ref resultFlag);
            textBoxCheck(textBoxORGN, ref resultFlag);
            textBoxCheck(textBoxBank, ref resultFlag);
            textBoxCheck(textBoxBIK, ref resultFlag);
            textBoxCheck(textBoxAccountRash, ref resultFlag);
            textBoxCheck(textBoxAccountCorr, ref resultFlag);
            textBoxCheck(textBoxPhone, ref resultFlag);
            textBoxCheck(textBoxEmail, ref resultFlag);
            textBoxCheck(textBoxAdressConnection, ref resultFlag);
            textBoxCheck(textBoxSpeed, ref resultFlag);
            textBoxCheck(textBoxCost, ref resultFlag);
            textBoxCheck(textBoxIP, ref resultFlag);
            textBoxCheck(textBoxMask, ref resultFlag);
            textBoxCheck(textBoxGateway, ref resultFlag);
            textBoxCheck(textBoxLineParams, ref resultFlag);
            #endregion
            return resultFlag;
        }
        private void buttonContinue_Click(object sender, EventArgs e)
        {
            refresh_Click(null, null);
            if (filesCount < 4)
                return;

            if (DataCheck())
                FileCreation();
            else
                MessageBox.Show("Заполните все выделенные красным поля для продолжения работы");
        }
        private void FileCreation()
        {
            if (textBoxSpeed.TextLength == 0)
                textBoxSpeed.Text = "0";

            string createDirectoryPath = "";
            string dogovorName = "";
            string prilojName = "";
            string vvodName = "";
            string kartochkaName = "";

            try
            {
                if (checkBoxIsOOO.Checked) // Правильность названий
                {
                    // ООО
                    createDirectoryPath = $"\\ООО {textBoxName.Text} К-{textBoxNumber.Text}";
                    dogovorName = $"Договор ООО {textBoxName.Text}  К-{textBoxNumber.Text}.doc";
                    prilojName = $"Приложение 1 ООО {textBoxName.Text} К-{textBoxNumber.Text}.doc";
                    vvodName = $"Акт ввода ООО {textBoxName.Text} К-{textBoxNumber.Text}.doc";
                    kartochkaName = $"Карточка ООО {textBoxName.Text} К-{textBoxNumber.Text}.xls";
                }
                else
                {
                    // ИП
                    string tempSurname = textBoxFioIm.Text.Split(' ')[0];
                    createDirectoryPath = $"\\ИП {tempSurname} К-{textBoxNumber.Text}";
                    dogovorName = $"Договор ИП {tempSurname} К-{textBoxNumber.Text}.doc";
                    prilojName = $"Приложение 1 ИП {tempSurname} К-{textBoxNumber.Text}.doc";
                    vvodName = $"Акт ввода ИП {tempSurname} К-{textBoxNumber.Text}.doc";
                    kartochkaName = $"Карточка ИП {tempSurname} К-{textBoxNumber.Text}.xls";
                }
                if (Directory.Exists(folderPath + createDirectoryPath))
                {
                    DialogResult result = MessageBox.Show("В указанной директории уже существует папка с тем же пакетом документом. Заменить?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        MessageBox.Show("Operation aborted");
                        return;
                    }
                }
                toolStripStatusLabelProgressBar.ProgressBar.Value = 0;
                Directory.CreateDirectory(folderPath + createDirectoryPath);

                string day, month = "", year;
                day = dateTimePicker.Value.ToString("dd");
                switch (Convert.ToInt32(dateTimePicker.Value.ToString("MM")))
                {
                    case 1:
                        {
                            month = "Января";
                            break;
                        }
                    case 2:
                        {
                            month = "Февраля";
                            break;
                        }
                    case 3:
                        {
                            month = "Марта";
                            break;
                        }
                    case 4:
                        {
                            month = "Апреля";
                            break;
                        }
                    case 5:
                        {
                            month = "Мая";
                            break;
                        }
                    case 6:
                        {
                            month = "Июня";
                            break;
                        }
                    case 7:
                        {
                            month = "Июля";
                            break;
                        }
                    case 8:
                        {
                            month = "Августа";
                            break;
                        }
                    case 9:
                        {
                            month = "Сентября";
                            break;
                        }
                    case 10:
                        {
                            month = "Октября";
                            break;
                        }
                    case 11:
                        {
                            month = "Ноября";
                            break;
                        }
                    case 12:
                        {
                            month = "Декабря";
                            break;
                        }
                }
                year = dateTimePicker.Value.ToString("yyyy");

                using (MWord doc = new MWord("Dogovor.doc", folderPath + createDirectoryPath + $"\\{dogovorName}")) // договор
                {
                    doc.FindAndReplace("<Number>", textBoxNumber.Text);
                    doc.FindAndReplace("<Day>", day);
                    doc.FindAndReplace("<Month>", month);
                    doc.FindAndReplace("<Year>", year);
                    if (checkBoxIsOOO.Checked)
                    {
                        doc.FindAndReplace("<OrganizationTypeFull>", "Общество с ограниченной ответственностью");
                        doc.FindAndReplace("<isOOOName>", textBoxName.Text);
                        doc.FindAndReplace("<isOOO>", $", в лице директора {textBoxFioRod.Text}");
                        doc.FindAndReplace("<isOOOUstav>", "Устава");
                        doc.FindAndReplace("<OrganizationType>", "ООО");
                    }
                    else
                    {
                        doc.FindAndReplace("<OrganizationTypeFull>", "ИП");
                        doc.FindAndReplace("<isOOOName>", textBoxFioIm.Text);
                        doc.FindAndReplace("<isOOOUstav>", "свидетельства");
                        doc.FindAndReplace("<isOOO>", $"");
                        doc.FindAndReplace("<OrganizationType>", "ИП");
                    }
                    doc.FindAndReplace("<AdressUr>", textBoxAdressUr.Text);
                    doc.FindAndReplace("<AdressMail>", textBoxAdressMail.Text); ;
                    doc.FindAndReplace("<INN>", textBoxINN.Text);
                    doc.FindAndReplace("<ORGN>", textBoxORGN.Text);
                    doc.FindAndReplace("<Bank>", textBoxBank.Text);
                    doc.FindAndReplace("<BIK>", textBoxBIK.Text);
                    doc.FindAndReplace("<AccountRash>", textBoxAccountRash.Text);
                    doc.FindAndReplace("<AccountCorr>", textBoxAccountCorr.Text);
                    doc.FindAndReplace("<Phone>", textBoxPhone.Text);
                    doc.FindAndReplace("<Fax>", textBoxFax.Text);

                    doc.Save();
                }
                toolStripStatusLabelProgressBar.ProgressBar.Value = 25;
                using (MWord doc = new MWord("Priloj.doc", folderPath + createDirectoryPath + $"\\{ prilojName}")) // Приложение
                {
                    doc.FindAndReplace("<Number>", textBoxNumber.Text);
                    doc.FindAndReplace("<Day>", day);
                    doc.FindAndReplace("<Month>", month);
                    doc.FindAndReplace("<Year>", year);
                    if (checkBoxIsOOO.Checked)
                    {
                        doc.FindAndReplace("<OrganizationType>", "ООО");
                        doc.FindAndReplace("<isOOOName>", textBoxName.Text);
                    }
                    else
                    {
                        doc.FindAndReplace("<OrganizationType>", "ИП");
                        doc.FindAndReplace("<isOOOName>", textBoxFioIm.Text);
                    }
                    doc.FindAndReplace("<Phone>", textBoxPhone.Text);
                    doc.FindAndReplace("<AdressConnection>", textBoxAdressConnection.Text);

                    doc.FindAndReplace("<LineParams>", textBoxLineParams.Text);
                    doc.FindAndReplace("<IP>", textBoxIP.Text);
                    doc.FindAndReplace("<Mask>", textBoxMask.Text);
                    doc.FindAndReplace("<Gateway>", textBoxGateway.Text);
                    doc.FindAndReplace("<IP>", textBoxIP.Text);

                    doc.FindAndReplace("<Speed>", Convert.ToString(Convert.ToInt32(textBoxSpeed.Text) * 1024));
                    doc.FindAndReplace("<Cost>", textBoxCost.Text);

                    doc.Save();
                }
                toolStripStatusLabelProgressBar.ProgressBar.Value = 50;
                using (MWord doc = new MWord("Vvod.doc", folderPath + createDirectoryPath + $"\\{vvodName}")) // Акт ввода в экспл
                {
                    doc.FindAndReplace("<Number>", textBoxNumber.Text);
                    doc.FindAndReplace("<Day>", day);
                    doc.FindAndReplace("<Month>", month);
                    doc.FindAndReplace("<Year>", year);
                    if (checkBoxIsOOO.Checked)
                    {
                        doc.FindAndReplace("<OrganizationType>", "ООО");
                        doc.FindAndReplace("<isOOOName>", textBoxName.Text);
                        doc.FindAndReplace("<isOOO>", $", в лице директора {textBoxFioRod.Text}");
                    }
                    else
                    {
                        doc.FindAndReplace("<OrganizationType>", "ИП");
                        doc.FindAndReplace("<isOOOName>", textBoxFioIm.Text);
                        doc.FindAndReplace("<isOOO>", $"");
                    }
                    doc.FindAndReplace("<AdressConnection>", textBoxAdressConnection.Text);
                    doc.FindAndReplace("<Speed>", Convert.ToString(Convert.ToInt32(textBoxSpeed.Text) * 1024));

                    doc.Save();
                }
                toolStripStatusLabelProgressBar.ProgressBar.Value = 75;
                using (MExcel doc = new MExcel("Kartochka.xls", folderPath + createDirectoryPath + $"\\{kartochkaName}")) // карточка
                {
                    doc.FindAndReplace("<Speed>", Convert.ToString(Convert.ToInt32(textBoxSpeed.Text) * 1024));
                    doc.FindAndReplace("<Cost>", textBoxCost.Text);
                    if (checkBoxIsOOO.Checked)
                    {
                        doc.FindAndReplace("<OrganizationType>", "ООО");
                        doc.FindAndReplace("<isOOOName>", textBoxName.Text);
                        doc.FindAndReplace("<isOOODirector>", "Директор");
                        doc.FindAndReplace("<FIO>", textBoxFioIm.Text);
                    }
                    else
                    {
                        doc.FindAndReplace("<isOOODirector>", "");
                        doc.FindAndReplace("<FIO>", "");
                        doc.Merge("C2:C4");
                        doc.FindAndReplace("<OrganizationType>", "ИП");
                        doc.FindAndReplace("<isOOOName>", textBoxFioIm.Text);
                    }
                    doc.FindAndReplace("<Phone>", textBoxPhone.Text);
                    doc.FindAndReplace("<Fax>", textBoxFax.Text);
                    doc.FindAndReplace("<Email>", textBoxEmail.Text);

                    doc.FindAndReplace("<AdressUr>", textBoxAdressUr.Text);
                    doc.FindAndReplace("<AdressFact>", textBoxAdressFact.Text);
                    doc.FindAndReplace("<AdressMail>", textBoxAdressMail.Text);
                    doc.FindAndReplace("<AdressConnection>", textBoxAdressConnection.Text);

                    doc.FindAndReplace("<INN>", textBoxINN.Text);
                    doc.FindAndReplace("<ORGN>", textBoxORGN.Text);
                    doc.FindAndReplace("<Bank>", textBoxBank.Text);
                    doc.FindAndReplace("<BIK>", textBoxBIK.Text);
                    doc.FindAndReplace("<AccountRash>", textBoxAccountRash.Text);
                    doc.FindAndReplace("<AccountCorr>", textBoxAccountCorr.Text);

                    doc.FindAndReplace("<Number>", textBoxNumber.Text);
                    doc.FindAndReplace("<Day>", day);
                    doc.FindAndReplace("<Month>", month);
                    doc.FindAndReplace("<Year>", year);

                    doc.FindAndReplace("<Mask>", textBoxMask.Text);
                    doc.FindAndReplace("<IP>", textBoxIP.Text);

                    doc.Save();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошиб Очка");
            }
            toolStripStatusLabelProgressBar.ProgressBar.Value = 100;
            MessageBox.Show("Operation Completed");
        }
        private void refresh_Click(object sender, EventArgs e)
        {
            string errorList = "";
            filesCount = 4;
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Dogovor.doc"))
            {
                --filesCount;
                errorList += "\n - Dogovor.doc";
            }
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Priloj.doc"))
            {
                --filesCount;
                errorList += "\n - Priloj.doc";
            }
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Vvod.doc"))
            {
                --filesCount;
                errorList += "\n - Vvod.doc";
            }
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Kartochka.xls"))
            {
                --filesCount;
                errorList += "\n - Kartochka.xls";
            }
            toolStripStatusLabelAllGreen.Text = $"Шаблонные файлы:{filesCount}/4";
            if (filesCount < 4)
            {
                toolStripStatusLabelAllGreen.BackColor = Color.Red;
                MessageBox.Show("Программе не удалось найти указанные шаблонные файлы, необходимые для работы. Добавьте следующие файлы в одну директорию с .exe файлом программы:" + errorList);
            }
            else
                toolStripStatusLabelAllGreen.BackColor = Color.Green;
        }
        private void checkBoxIsOOO_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsOOO.Checked)
                textBoxName.Enabled = true;
            else
            {
                textBoxName.Text = "";
                textBoxName.Enabled = false;
            }
        }
        /*ToolStrip/StatusStrip*/
        #region
        private void savePathShow()
        {
            toolStripStatusLabelSavePath.Text = "Save path: " + folderPath;
        }
        private void toolStripButtonFolderChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            DialogResult result = FBD.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderPath = FBD.SelectedPath;
                savePathShow();
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Author tempForm = new Author();
            tempForm.Show();
        }
        #endregion

        /*KeyPress*/
        #region
        private void DigitsOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void DigitsWithDots(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!=46)
                e.Handled = true;
        }
        private void DigitsWithSlash(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 47)
                e.Handled = true;
        }
        private void textBoxINN_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsWithSlash(sender, e);
        }
        private void textBoxORGN_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxBIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxAccountRash_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxAccountCorr_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsWithDots(sender, e);
        }
        private void textBoxMask_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsWithDots(sender, e);
        }
        private void textBoxGateway_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        private void textBoxLineParams_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitsOnly(sender, e);
        }
        #endregion
    }
}
