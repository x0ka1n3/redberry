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
        private void opened_tabs_control_selecting(object sender, EventArgs e)
        {
            upper_index_button.Checked = false;
            lower_index_button.Checked = false;
        }

        private void open_tab_context_menu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < opened_tabs_control.TabCount; i++)
                    if (opened_tabs_control.GetTabRect(i).Contains(e.Location))
                        clicked_tab = opened_tabs_control.TabPages[i];
                tab_context_menu.Show(opened_tabs_control, e.Location);
            }
        }
    }
}
