using System.Drawing;
using System.Windows.Forms;

namespace redberry
{
    public partial class Redberry : Form
    {
        #region variables and controls

        MenuStrip menu_strip = new MenuStrip();
        ToolStripMenuItem file_strip = new ToolStripMenuItem("Файл");
        ToolStripMenuItem view_strip = new ToolStripMenuItem("Вид");
        ToolStripMenuItem new_tab_button = new ToolStripMenuItem("Создать файл");
        ToolStripMenuItem open_file_strip = new ToolStripMenuItem("Открыть файл");
        ToolStripMenuItem open_code_button = new ToolStripMenuItem("Программа");
        ToolStripMenuItem open_result_button = new ToolStripMenuItem("Результат");
        ToolStripMenuItem open_greek_alphabet_button = new ToolStripMenuItem("Греческий алфавит") { Checked = true, CheckOnClick = true };

        Panel menu_strip_line = new Panel();

        #endregion

        private void load_menu_strip()
        {
            menu_strip.Renderer = new menu_strip_renderer();

            file_strip.DropDownItems.Add(new_tab_button);
            file_strip.DropDownItems.Add(open_file_strip);
            open_file_strip.DropDownItems.Add(open_code_button);
            open_file_strip.DropDownItems.Add(open_result_button);

            view_strip.DropDownItems.Add(open_greek_alphabet_button);
            menu_strip.Items.Add(file_strip);
            menu_strip.Items.Add(view_strip);
            menu_strip.Location = new Point(0, 0);
            menu_strip.BackColor = Color.FromArgb(242, 242, 242);

            new_tab_button.Click += new_tab_button_click;
            open_code_button.Click += (sender, e) => open_button_click(sender, e, false);
            open_result_button.Click += (sender, e) => open_button_click(sender, e, true);
            open_greek_alphabet_button.Click += open_greek_alphabet_button_click;

            this.Controls.Add(menu_strip);

            menu_strip_line.Size = new Size(this.Width, menu_strip.Height / 5);
            menu_strip_line.Location = new Point(0, 6 * menu_strip.Height / 5 - 1);
            menu_strip_line.BackColor = Color.FromArgb(214, 214, 214);
            menu_strip_line.Visible = true;

            this.Controls.Add(menu_strip_line);
        }
    }
}
