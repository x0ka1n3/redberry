using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace redberry
{
    public partial class Form1 : Form
    {
        private void Form1_size_changed(object sender, EventArgs e)
        {
            reposition_greek_alphabet();
            reposition_opened_tabs_control();
            menu_strip_line.Size = new Size(this.Width, upper_index_button.Height / 12);
        }

        void open_button_click(object sender, EventArgs e, bool isResult)
        {
            OpenFileDialog new_file = new OpenFileDialog();
            string filePath = "";
            string fileContent = "";

            if (isResult)   new_file.Filter = "txt files (*.txt)|*.txt|groovy files (*.groovy)|*.groovy|All files (*.*)|*.*";
            else            new_file.Filter = "groovy files (*.groovy)|*.groovy|txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (new_file.ShowDialog() == DialogResult.OK)
            {
                filePath = new_file.SafeFileName;

                var fileStream = new_file.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream)) fileContent = reader.ReadToEnd();

                fileContent = fileContent.Replace("    ", "\t");

                if (isResult)   fileContent = change_slashed_to_greek(fileContent);
                else            fileContent = change_double_slashed_to_greek(fileContent);

                new_tab_button_click(sender, e);
                ((tabTag)opened_tabs_control.SelectedTab.Tag).path = new_file.FileName;

                NumberedRTB new_NRTB = getNRTB();

                new_NRTB.Visible = false;
                new_NRTB.RichTextBox.Text = fileContent;
                print_indices(new_NRTB.RichTextBox);
                new_NRTB.Visible = true;

                opened_tabs_control.SelectedTab.Text = filePath;
                opened_tabs_control.SelectedTab.Name = filePath;

                ((tabTag)opened_tabs_control.SelectedTab.Tag).isResult = isResult;
            }
        }

        private void save_file(object sender, EventArgs e, bool flag)
        {
            NumberedRTB new_NRTB = new NumberedRTB();
            foreach (Control control in clickedTab.Controls)
                if (control.GetType() == typeof(NumberedRTB))
                    new_NRTB = (NumberedRTB)control;

            if (flag && (((tabTag)clickedTab.Tag).path != null))
            {
                using (StreamWriter writer = new StreamWriter(((tabTag)clickedTab.Tag).path))
                {
                    writer.Write(change_greek_to_double_slashed(create_code(new_NRTB.RichTextBox)));

                    clickedTab.Text = clickedTab.Name;
                }
            }

            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "groovy files (*.groovy)|*.groovy|txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    Stream fileStream;
                    if ((fileStream = saveFile.OpenFile()) != null)
                    {
                        string fileName = saveFile.FileName;
                        using (StreamWriter writer = new StreamWriter(fileStream))
                        {
                            writer.Write(change_greek_to_double_slashed(create_code(new_NRTB.RichTextBox)));
                        }
                        clickedTab.Text = new System.IO.FileInfo(saveFile.FileName).Name;
                        clickedTab.Name = clickedTab.Text;
                        fileStream.Close();

                        ((tabTag)clickedTab.Tag).changed = true;
                        ((tabTag)clickedTab.Tag).path = fileName;
                    }
                }
            }

        }

        private void run_script(object sender, EventArgs e)
        {
            if ((opened_tabs_control.TabPages.Count > 0) && ((tabTag)opened_tabs_control.SelectedTab.Tag).path != "");
            {
                string filePath = ((tabTag)opened_tabs_control.SelectedTab.Tag).path;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                var proc = new Process();
                var startInfo = new ProcessStartInfo("cmd.exe", $"/C groovy \"{filePath}\"");
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                proc.StartInfo = startInfo;

                proc.Start();
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                output = output.Replace("    ", "\t");
                new_tab_button_click(null, null);

                NumberedRTB new_NRTB = getNRTB();
                new_NRTB.RichTextBox.Text = change_slashed_to_greek(output);

                opened_tabs_control.SelectedTab.Name = $"result of {fileName}";
                opened_tabs_control.SelectedTab.Text = opened_tabs_control.SelectedTab.Name;

                ((tabTag)opened_tabs_control.SelectedTab.Tag).changed = false;
                ((tabTag)opened_tabs_control.SelectedTab.Tag).isResult = true;
            }
        }

        private void new_tab_button_click(object sender, EventArgs e)
        {
            TabPage new_tab = new TabPage("Новая вкладка");
            NumberedRTB new_NRTB = new NumberedRTB();

            new_NRTB.RichTextBox.Font = new Font("Times New Roman", font_size, FontStyle.Italic);
            new_NRTB.RichTextBox.AcceptsTab = true;
            new_NRTB.RichTextBox.WordWrap = false;
            new_NRTB.RichTextBox.KeyDown += NRTB_press_enter;
            new_NRTB.RichTextBox.DetectUrls = false;
            new_tab.Controls.Add(new_NRTB);

            new_tab.Tag = new tabTag();
            new_tab.Name = new_tab.Text;
            opened_tabs_control.TabPages.Add(new_tab);
            opened_tabs_control.SelectedTab = new_tab;
            opened_tabs_control.Visible = true;
            reposition_opened_tabs_control();
            new_NRTB.Focus();
        }
        private void close_tab(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            if (((tabTag)clickedTab.Tag).changed == false)
            {
                result = MessageBox.Show("Файл не был сохранен. Сохранить изменения?", "Несохраненные изменения", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    save_file(null, null, true);
                    opened_tabs_control.TabPages.Remove(clickedTab);
                    if (opened_tabs_control.TabCount == 0) opened_tabs_control.Visible = false;
                }
                else if (result == DialogResult.No)
                {
                    opened_tabs_control.TabPages.Remove(clickedTab);
                    if (opened_tabs_control.TabCount == 0) opened_tabs_control.Visible = false;
                }
            }
            else
            {
                opened_tabs_control.TabPages.Remove(clickedTab);
                if (opened_tabs_control.TabCount == 0) opened_tabs_control.Visible = false;
            }
        }
        private void open_tab_context_menu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < opened_tabs_control.TabCount; i++)
                    if (opened_tabs_control.GetTabRect(i).Contains(e.Location))
                        clickedTab = opened_tabs_control.TabPages[i];
                tab_context_menu.Show(opened_tabs_control, e.Location);
            }
        }

        private void opened_tabs_control_selecting(object sender, EventArgs e)
        {
            upper_index_button.Checked = false;
            lower_index_button.Checked = false;
        }

        private void NRTB_press_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                upper_index_button.Checked = false;
                lower_index_button.Checked = false;
            }
        }

        private void open_greek_alphabet_button_click(object sender, EventArgs e)
        {
            if (open_greek_alphabet_button.Checked == true) { visualize_greek_alphabet(); reposition_opened_tabs_control(); }
            else { greek_alphabet_caps.Visible = false; greek_alphabet_background.Visible = false; hide_greek_alphabet(); hide_GREEK_alphabet(); reposition_opened_tabs_control(); }
        }

        private void greek_alphabet_caps_changed(object sender, EventArgs e)
        {
            string res = greek_alphabet_caps.Checked ? "Заглавные" : "Строчные";
            greek_alphabet_caps.Text = $"{res} буквы";
            hide_greek_alphabet();
            hide_GREEK_alphabet();
            visualize_greek_alphabet();
        }

        private void greek_letter_click(object sender, EventArgs e)
        {
            if (opened_tabs_control.TabCount > 0)
                getNRTB().RichTextBox.SelectedText = (sender as Button).Text;
        }

        private void upper_index_button_CheckedChanged(object sender, EventArgs e)
        {
            if (opened_tabs_control.TabCount != 0)
            {
                NumberedRTB NRTB = getNRTB();

                if (upper_index_button.Checked == true)
                {
                    lower_index_button.Checked = false;
                    NRTB.RichTextBox.SelectionCharOffset = 3 * font_size / 4;
                    NRTB.RichTextBox.SelectionFont = new Font("Times New Roman", 4 * font_size / 5, FontStyle.Italic);
                }
                else
                {
                    NRTB.RichTextBox.SelectionCharOffset = 0;
                    NRTB.RichTextBox.SelectionFont = new Font("Times New Roman", font_size, FontStyle.Italic);
                }

                NRTB.Focus();
            }
            else upper_index_button.Checked = false;
        }

        private void lower_index_button_CheckedChanged(object sender, EventArgs e)
        {
            if (opened_tabs_control.TabCount != 0)
            {
                NumberedRTB NRTB = getNRTB();

                if (lower_index_button.Checked == true)
                {
                    upper_index_button.Checked = false;
                    NRTB.RichTextBox.SelectionCharOffset = -font_size / 5;
                    NRTB.RichTextBox.SelectionFont = new Font("Times New Roman", 4 * font_size / 5, FontStyle.Italic);
                }
                else
                {
                    NRTB.RichTextBox.SelectionCharOffset = 0;
                    NRTB.RichTextBox.SelectionFont = new Font("Times New Roman", font_size, FontStyle.Italic);
                }

                NRTB.Focus();
            }
            else lower_index_button.Checked = false;
        }
    }
}