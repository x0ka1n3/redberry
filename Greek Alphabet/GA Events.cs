using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redberry
{
    public partial class Redberry : Form
    {
        private void greek_letter_click(object sender, EventArgs e)
        {
            if (opened_tabs_control.TabCount > 0)
            {
                NumberedRTB NRTB = get_active_NRTB();

                NRTB.RichTextBox.SelectedText = (sender as Button).Text;
            }
        }

        private void greek_alphabet_caps_changed(object sender, EventArgs e)
        {
            if (greek_alphabet_caps.Text.Equals("Строчные буквы"))
            {
                greek_alphabet_caps.Text = "Заглавные буквы";
                hide_greek_alphabet();
                visualize_greek_alphabet();
            }
            else
            {
                greek_alphabet_caps.Text = "Строчные буквы";
                hide_GREEK_alphabet();
                visualize_greek_alphabet();
            }
        }
    }
}
