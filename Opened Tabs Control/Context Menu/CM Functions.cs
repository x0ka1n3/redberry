using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace redberry
{
    public partial class Redberry : Form
    {
        #region variables and controls

        ContextMenuStrip tab_context_menu = new ContextMenuStrip();
        TabPage clicked_tab;

        #endregion

        private void load_tab_context_menu()
        {
            tab_context_menu.Items.Add("Запустить", null, run_code_click);
            tab_context_menu.Items.Add("Сохранить", null, (sender, e) => save_file_click(sender, e, true));
            tab_context_menu.Items.Add("Сохранить как", null, (sender, e) => save_file_click(sender, e, false));
            tab_context_menu.Items.Add("Закрыть", null, close_tab_click);
        }
    }
}