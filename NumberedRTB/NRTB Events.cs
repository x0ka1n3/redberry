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
        private void NRTB_press_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                upper_index_button.Checked = false;
                lower_index_button.Checked = false;
            }
        }
    }
}