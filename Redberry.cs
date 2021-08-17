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
        #region variables and controls

        ushort font_size = 20;
        ushort border_width = 2;
        ushort min_blank = 2;

        #endregion

        public Redberry()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(242, 242, 242);
            SizeChanged += form_size_changed;

            load_menu_strip();
            load_index_buttons();
            load_opened_tabs_control();
            load_tab_context_menu();
            load_greek_alphabet();

            reposition_greek_alphabet();
            if (open_greek_alphabet_button.Checked == true) visualize_greek_alphabet();
            reposition_opened_tabs_control();

            MinimumSize = new Size(960, 540);
        }

        private void form_size_changed(object sender, EventArgs e)
        {
            reposition_greek_alphabet();
            reposition_opened_tabs_control();
            menu_strip_line.Size = new Size(ClientSize.Width, menu_strip.Height / 5);
        }
    }
}
