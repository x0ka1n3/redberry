using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace redberry
{
    public partial class Redberry : Form
    {
        #region variables and controls

        ContextMenuStrip code_context_menu = new ContextMenuStrip();
        ContextMenuStrip result_context_menu = new ContextMenuStrip();
        TabPage clicked_tab;

        #endregion

        private void load_code_context_menu()
        {
            code_context_menu.Items.Add("Запустить", null, run_code_click);
            code_context_menu.Items.Add("Сохранить", null, (sender, e) => save_file_click(sender, e, true));
            code_context_menu.Items.Add("Сохранить как...", null, (sender, e) => save_file_click(sender, e, false));
            code_context_menu.Items.Add("Закрыть", null, close_tab_click);
            code_context_menu.Items.Add("Программа", null, change_isResult);
            //code_context_menu.Items.Add("test", null, test);
        }

        private void load_result_context_menu()
        {
            result_context_menu.Items.Add("Сохранить", null, (sender, e) => save_file_click(sender, e, true));
            result_context_menu.Items.Add("Сохранить как...", null, (sender, e) => save_file_click(sender, e, false));
            result_context_menu.Items.Add("Закрыть", null, close_tab_click);
            result_context_menu.Items.Add("Результат", null, change_isResult);
        }

        void test(object sender, EventArgs e)
        {
            NumberedRTB nrtb = get_active_NRTB();

            //MatchCollection tensors = Regex.Matches(nrtb.RichTextBox.Text, "'(?:[^\"'\\\\]|\\\\.)*?'\\.t");
            MatchCollection tensors = Regex.Matches(nrtb.RichTextBox.Text, "use");
            
            foreach (Match tensor in tensors)
            {
                
            }
            File.WriteAllText("rtf.txt", nrtb.RichTextBox.Rtf);
        }
    }
}