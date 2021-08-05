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
    interface tabTagInterface{}

    public class tabTag : tabTagInterface
    {
        public bool changed = false;
        public string path = null;
        public bool isResult = false;
    }

    public partial class Form1 : Form
    {
        #region variables and controls

        int font_size = 20;

        MenuStrip menu_strip = new MenuStrip();
        ToolStripMenuItem file_strip = new ToolStripMenuItem("Файл");
        ToolStripMenuItem new_tab_button = new ToolStripMenuItem("Создать файл");
        ToolStripMenuItem open_file_strip = new ToolStripMenuItem("Открыть файл");
        ToolStripMenuItem open_code_button = new ToolStripMenuItem("Программа");
        ToolStripMenuItem open_result_button = new ToolStripMenuItem("Результат");
        ToolStripMenuItem run_code_button = new ToolStripMenuItem("Запустить код");
        ToolStripMenuItem open_greek_alphabet_button = new ToolStripMenuItem("Греческий алфавит") { Checked = false, CheckOnClick = true };

        #region greek alphabet

        Button alpha_button = new Button() { Text = "α", TextAlign = ContentAlignment.MiddleCenter };
        Button beta_button = new Button() { Text = "β", TextAlign = ContentAlignment.MiddleCenter };
        Button gamma_button = new Button() { Text = "γ", TextAlign = ContentAlignment.MiddleCenter };
        Button delta_button = new Button() { Text = "δ", TextAlign = ContentAlignment.MiddleCenter };
        Button epsilon_button = new Button() { Text = "ε", TextAlign = ContentAlignment.MiddleCenter };
        Button zeta_button = new Button() { Text = "ζ", TextAlign = ContentAlignment.MiddleCenter };
        Button eta_button = new Button() { Text = "η", TextAlign = ContentAlignment.MiddleCenter };
        Button theta_button = new Button() { Text = "θ", TextAlign = ContentAlignment.MiddleCenter };
        Button iota_button = new Button() { Text = "ι", TextAlign = ContentAlignment.MiddleCenter };
        Button kappa_button = new Button() { Text = "κ", TextAlign = ContentAlignment.MiddleCenter };
        Button lambda_button = new Button() { Text = "λ", TextAlign = ContentAlignment.MiddleCenter };
        Button mu_button = new Button() { Text = "μ", TextAlign = ContentAlignment.MiddleCenter };
        Button nu_button = new Button() { Text = "ν", TextAlign = ContentAlignment.MiddleCenter };
        Button xi_button = new Button() { Text = "ξ", TextAlign = ContentAlignment.MiddleCenter };
        Button omicron_button = new Button() { Text = "ο", TextAlign = ContentAlignment.MiddleCenter };
        Button pi_button = new Button() { Text = "π", TextAlign = ContentAlignment.MiddleCenter };
        Button rho_button = new Button() { Text = "ρ", TextAlign = ContentAlignment.MiddleCenter };
        Button sigma_button = new Button() { Text = "σ", TextAlign = ContentAlignment.MiddleCenter };
        Button tau_button = new Button() { Text = "τ", TextAlign = ContentAlignment.MiddleCenter };
        Button upsilon_button = new Button() { Text = "υ", TextAlign = ContentAlignment.MiddleCenter };
        Button phi_button = new Button() { Text = "φ", TextAlign = ContentAlignment.MiddleCenter };
        Button chi_button = new Button() { Text = "χ", TextAlign = ContentAlignment.MiddleCenter };
        Button psi_button = new Button() { Text = "ψ", TextAlign = ContentAlignment.MiddleCenter };
        Button omega_button = new Button() { Text = "ω", TextAlign = ContentAlignment.MiddleCenter };

        Button ALPHA_button = new Button() { Text = "Α", TextAlign = ContentAlignment.MiddleCenter };
        Button BETA_button = new Button() { Text = "Β", TextAlign = ContentAlignment.MiddleCenter };
        Button GAMMA_button = new Button() { Text = "Γ", TextAlign = ContentAlignment.MiddleCenter };
        Button DELTA_button = new Button() { Text = "Δ", TextAlign = ContentAlignment.MiddleCenter };
        Button EPSILON_button = new Button() { Text = "Ε", TextAlign = ContentAlignment.MiddleCenter };
        Button ZETA_button = new Button() { Text = "Ζ", TextAlign = ContentAlignment.MiddleCenter };
        Button ETA_button = new Button() { Text = "Η", TextAlign = ContentAlignment.MiddleCenter };
        Button THETA_button = new Button() { Text = "Θ", TextAlign = ContentAlignment.MiddleCenter };
        Button IOTA_button = new Button() { Text = "Ι", TextAlign = ContentAlignment.MiddleCenter };
        Button KAPPA_button = new Button() { Text = "Κ", TextAlign = ContentAlignment.MiddleCenter };
        Button LAMBDA_button = new Button() { Text = "Λ", TextAlign = ContentAlignment.MiddleCenter };
        Button MU_button = new Button() { Text = "Μ", TextAlign = ContentAlignment.MiddleCenter };
        Button NU_button = new Button() { Text = "Ν", TextAlign = ContentAlignment.MiddleCenter };
        Button XI_button = new Button() { Text = "Ξ", TextAlign = ContentAlignment.MiddleCenter };
        Button OMICRON_button = new Button() { Text = "Ο", TextAlign = ContentAlignment.MiddleCenter };
        Button PI_button = new Button() { Text = "Π", TextAlign = ContentAlignment.MiddleCenter };
        Button RHO_button = new Button() { Text = "Ρ", TextAlign = ContentAlignment.MiddleCenter };
        Button SIGMA_button = new Button() { Text = "Σ", TextAlign = ContentAlignment.MiddleCenter };
        Button TAU_button = new Button() { Text = "Τ", TextAlign = ContentAlignment.MiddleCenter };
        Button UPSILON_button = new Button() { Text = "Υ", TextAlign = ContentAlignment.MiddleCenter };
        Button PHI_button = new Button() { Text = "Φ", TextAlign = ContentAlignment.MiddleCenter };
        Button CHI_button = new Button() { Text = "Χ", TextAlign = ContentAlignment.MiddleCenter };
        Button PSI_button = new Button() { Text = "Ψ", TextAlign = ContentAlignment.MiddleCenter };
        Button OMEGA_button = new Button() { Text = "Ω", TextAlign = ContentAlignment.MiddleCenter };

        #endregion

        Panel menu_strip_line = new Panel();

        CheckBox greek_alphabet_caps = new CheckBox();
        Panel greek_alphabet_background = new Panel();
        CheckBox upper_index_button = new CheckBox();
        CheckBox lower_index_button = new CheckBox();

        TabControl opened_tabs_control = new TabControl();
        ContextMenuStrip tab_context_menu = new ContextMenuStrip();
        TabPage clickedTab;

        #endregion

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(242, 242, 242);
            this.SizeChanged += Form1_size_changed;

            load_menu_strip();
            load_index_buttons();

            load_greek_alphabet();
            reposition_greek_alphabet();

            load_opened_tabs_control();
            reposition_opened_tabs_control();

            tab_context_menu.Items.Add("Сохранить", null, (sender, e) => save_file(sender, e, true));
            tab_context_menu.Items.Add("Сохранить как", null, (sender, e) => save_file(sender, e, false));
            tab_context_menu.Items.Add("test", null, test);
            tab_context_menu.Items.Add("Закрыть", null, close_tab);
        }

        private NumberedRTB getNRTB()
        {
            NumberedRTB new_NRTB = new NumberedRTB();
            foreach (Control control in opened_tabs_control.SelectedTab.Controls)
                if (control.GetType() == typeof(NumberedRTB))
                {
                    new_NRTB = (NumberedRTB)control;
                    return new_NRTB;
                }
            return null;
        }

        private void load_menu_strip()
        {
            menu_strip.Renderer = new menu_strip_renderer();

            file_strip.DropDownItems.Add(new_tab_button);
            file_strip.DropDownItems.Add(open_file_strip);
            open_file_strip.DropDownItems.Add(open_code_button);
            open_file_strip.DropDownItems.Add(open_result_button);

            menu_strip.Items.Add(file_strip);
            menu_strip.Items.Add(run_code_button);
            menu_strip.Items.Add(open_greek_alphabet_button);
            menu_strip.Location = new Point(0, 0);
            menu_strip.BackColor = Color.FromArgb(242, 242, 242);

            new_tab_button.Click += new_tab_button_click;
            run_code_button.Click += run_script;
            open_code_button.Click += (sender, e) => open_button_click(sender, e, false);
            open_result_button.Click += (sender, e) => open_button_click(sender, e, true);
            open_greek_alphabet_button.Click += open_greek_alphabet_button_click;

            this.Controls.Add(menu_strip);
        }

        private void load_index_buttons()
        {
            upper_index_button.Size = new Size(2 * font_size, 2 * font_size);
            lower_index_button.Size = new Size(2 * font_size, 2 * font_size);
            upper_index_button.Location = new Point(0, 4 * menu_strip.Height / 3);
            lower_index_button.Location = new Point(0, 4 * menu_strip.Height / 3 + 2 * font_size);
            upper_index_button.BackColor = Color.White;
            lower_index_button.BackColor = Color.White;
            upper_index_button.Visible = true;
            lower_index_button.Visible = true;
            upper_index_button.Appearance = Appearance.Button;
            lower_index_button.Appearance = Appearance.Button;
            upper_index_button.CheckState = CheckState.Unchecked;
            lower_index_button.CheckState = CheckState.Unchecked;
            upper_index_button.TextAlign = ContentAlignment.MiddleCenter;
            lower_index_button.TextAlign = ContentAlignment.MiddleCenter;
            upper_index_button.Text = "^";
            lower_index_button.Text = "_";

            upper_index_button.CheckedChanged += upper_index_button_CheckedChanged;
            lower_index_button.CheckedChanged += lower_index_button_CheckedChanged;

            this.Controls.Add(upper_index_button);
            this.Controls.Add(lower_index_button);

            menu_strip_line.Size = new Size(this.Width, upper_index_button.Height / 12);
            menu_strip_line.Location = new Point(0, menu_strip.Height + (upper_index_button.Location.Y - menu_strip.Height) / 2 - menu_strip_line.Height / 2);
            menu_strip_line.BackColor = Color.FromArgb(214, 214, 214);
            menu_strip_line.Visible = true;

            this.Controls.Add(menu_strip_line);
        }

        private void load_greek_alphabet()
        {
            greek_alphabet_caps.Size = new Size(12 * font_size, 2 * font_size);
            greek_alphabet_caps.Visible = false;
            greek_alphabet_caps.Appearance = Appearance.Button;
            greek_alphabet_caps.CheckState = CheckState.Unchecked;
            greek_alphabet_caps.TextAlign = ContentAlignment.MiddleCenter;
            greek_alphabet_caps.BackColor = Color.White;
            greek_alphabet_caps.Text = "Строчные буквы";
            greek_alphabet_caps.CheckedChanged += greek_alphabet_caps_changed;
            this.Controls.Add(greek_alphabet_caps);

            #region greek alphabet

            Font a_font = new Font("Times New Roman", font_size, FontStyle.Italic);
            Size a_size = new Size(3 * font_size, 3 * font_size);

            alpha_button.Font = a_font;
            alpha_button.Size = a_size;
            alpha_button.Visible = false;
            alpha_button.BackColor = Color.White;
            alpha_button.Click += greek_letter_click;
            this.Controls.Add(alpha_button);

            beta_button.Font = a_font;
            beta_button.Size = a_size;
            beta_button.Visible = false;
            beta_button.BackColor = Color.White;
            beta_button.Click += greek_letter_click;
            this.Controls.Add(beta_button);

            gamma_button.Font = a_font;
            gamma_button.Size = a_size;
            gamma_button.Visible = false;
            gamma_button.BackColor = Color.White;
            gamma_button.Click += greek_letter_click;
            this.Controls.Add(gamma_button);

            delta_button.Font = a_font;
            delta_button.Size = a_size;
            delta_button.Visible = false;
            delta_button.BackColor = Color.White;
            delta_button.Click += greek_letter_click;
            this.Controls.Add(delta_button);

            epsilon_button.Font = a_font;
            epsilon_button.Size = a_size;
            epsilon_button.Visible = false;
            epsilon_button.BackColor = Color.White;
            epsilon_button.Click += greek_letter_click;
            this.Controls.Add(epsilon_button);

            zeta_button.Font = a_font;
            zeta_button.Size = a_size;
            zeta_button.Visible = false;
            zeta_button.BackColor = Color.White;
            zeta_button.Click += greek_letter_click;
            this.Controls.Add(zeta_button);

            eta_button.Font = a_font;
            eta_button.Size = a_size;
            eta_button.Visible = false;
            eta_button.BackColor = Color.White;
            eta_button.Click += greek_letter_click;
            this.Controls.Add(eta_button);

            theta_button.Font = a_font;
            theta_button.Size = a_size;
            theta_button.Visible = false;
            theta_button.BackColor = Color.White;
            theta_button.Click += greek_letter_click;
            this.Controls.Add(theta_button);

            iota_button.Font = a_font;
            iota_button.Size = a_size;
            iota_button.Visible = false;
            iota_button.BackColor = Color.White;
            iota_button.Click += greek_letter_click;
            this.Controls.Add(iota_button);

            kappa_button.Font = a_font;
            kappa_button.Size = a_size;
            kappa_button.Visible = false;
            kappa_button.BackColor = Color.White;
            kappa_button.Click += greek_letter_click;
            this.Controls.Add(kappa_button);

            lambda_button.Font = a_font;
            lambda_button.Size = a_size;
            lambda_button.Visible = false;
            lambda_button.BackColor = Color.White;
            lambda_button.Click += greek_letter_click;
            this.Controls.Add(lambda_button);

            mu_button.Font = a_font;
            mu_button.Size = a_size;
            mu_button.Visible = false;
            mu_button.BackColor = Color.White;
            mu_button.Click += greek_letter_click;
            this.Controls.Add(mu_button);

            nu_button.Font = a_font;
            nu_button.Size = a_size;
            nu_button.Visible = false;
            nu_button.BackColor = Color.White;
            nu_button.Click += greek_letter_click;
            this.Controls.Add(nu_button);

            xi_button.Font = a_font;
            xi_button.Size = a_size;
            xi_button.Visible = false;
            xi_button.BackColor = Color.White;
            xi_button.Click += greek_letter_click;
            this.Controls.Add(xi_button);

            omicron_button.Font = a_font;
            omicron_button.Size = a_size;
            omicron_button.Visible = false;
            omicron_button.BackColor = Color.White;
            omicron_button.Click += greek_letter_click;
            this.Controls.Add(omicron_button);

            pi_button.Font = a_font;
            pi_button.Size = a_size;
            pi_button.Visible = false;
            pi_button.BackColor = Color.White;
            pi_button.Click += greek_letter_click;
            this.Controls.Add(pi_button);

            rho_button.Font = a_font;
            rho_button.Size = a_size;
            rho_button.Visible = false;
            rho_button.BackColor = Color.White;
            rho_button.Click += greek_letter_click;
            this.Controls.Add(rho_button);

            sigma_button.Font = a_font;
            sigma_button.Size = a_size;
            sigma_button.Visible = false;
            sigma_button.BackColor = Color.White;
            sigma_button.Click += greek_letter_click;
            this.Controls.Add(sigma_button);

            tau_button.Font = a_font;
            tau_button.Size = a_size;
            tau_button.Visible = false;
            tau_button.BackColor = Color.White;
            tau_button.Click += greek_letter_click;
            this.Controls.Add(tau_button);

            upsilon_button.Font = a_font;
            upsilon_button.Size = a_size;
            upsilon_button.Visible = false;
            upsilon_button.BackColor = Color.White;
            upsilon_button.Click += greek_letter_click;
            this.Controls.Add(upsilon_button);

            phi_button.Font = a_font;
            phi_button.Size = a_size;
            phi_button.Visible = false;
            phi_button.BackColor = Color.White;
            phi_button.Click += greek_letter_click;
            this.Controls.Add(phi_button);

            chi_button.Font = a_font;
            chi_button.Size = a_size;
            chi_button.Visible = false;
            chi_button.BackColor = Color.White;
            chi_button.Click += greek_letter_click;
            this.Controls.Add(chi_button);

            psi_button.Font = a_font;
            psi_button.Size = a_size;
            psi_button.Visible = false;
            psi_button.BackColor = Color.White;
            psi_button.Click += greek_letter_click;
            this.Controls.Add(psi_button);

            omega_button.Font = a_font;
            omega_button.Size = a_size;
            omega_button.Visible = false;
            omega_button.BackColor = Color.White;
            omega_button.Click += greek_letter_click;
            this.Controls.Add(omega_button);

            ALPHA_button.Font = a_font;
            ALPHA_button.Size = a_size;
            ALPHA_button.Visible = false;
            ALPHA_button.BackColor = Color.White;
            ALPHA_button.Click += greek_letter_click;
            this.Controls.Add(ALPHA_button);

            BETA_button.Font = a_font;
            BETA_button.Size = a_size;
            BETA_button.Visible = false;
            BETA_button.BackColor = Color.White;
            BETA_button.Click += greek_letter_click;
            this.Controls.Add(BETA_button);

            GAMMA_button.Font = a_font;
            GAMMA_button.Size = a_size;
            GAMMA_button.Visible = false;
            GAMMA_button.BackColor = Color.White;
            GAMMA_button.Click += greek_letter_click;
            this.Controls.Add(GAMMA_button);

            DELTA_button.Font = a_font;
            DELTA_button.Size = a_size;
            DELTA_button.Visible = false;
            DELTA_button.BackColor = Color.White;
            DELTA_button.Click += greek_letter_click;
            this.Controls.Add(DELTA_button);

            EPSILON_button.Font = a_font;
            EPSILON_button.Size = a_size;
            EPSILON_button.Visible = false;
            EPSILON_button.BackColor = Color.White;
            EPSILON_button.Click += greek_letter_click;
            this.Controls.Add(EPSILON_button);

            ZETA_button.Font = a_font;
            ZETA_button.Size = a_size;
            ZETA_button.Visible = false;
            ZETA_button.BackColor = Color.White;
            ZETA_button.Click += greek_letter_click;
            this.Controls.Add(ZETA_button);

            ETA_button.Font = a_font;
            ETA_button.Size = a_size;
            ETA_button.Visible = false;
            ETA_button.BackColor = Color.White;
            ETA_button.Click += greek_letter_click;
            this.Controls.Add(ETA_button);

            THETA_button.Font = a_font;
            THETA_button.Size = a_size;
            THETA_button.Visible = false;
            THETA_button.BackColor = Color.White;
            THETA_button.Click += greek_letter_click;
            this.Controls.Add(THETA_button);

            IOTA_button.Font = a_font;
            IOTA_button.Size = a_size;
            IOTA_button.Visible = false;
            IOTA_button.BackColor = Color.White;
            IOTA_button.Click += greek_letter_click;
            this.Controls.Add(IOTA_button);

            KAPPA_button.Font = a_font;
            KAPPA_button.Size = a_size;
            KAPPA_button.Visible = false;
            KAPPA_button.BackColor = Color.White;
            KAPPA_button.Click += greek_letter_click;
            this.Controls.Add(KAPPA_button);

            LAMBDA_button.Font = a_font;
            LAMBDA_button.Size = a_size;
            LAMBDA_button.Visible = false;
            LAMBDA_button.BackColor = Color.White;
            LAMBDA_button.Click += greek_letter_click;
            this.Controls.Add(LAMBDA_button);

            MU_button.Font = a_font;
            MU_button.Size = a_size;
            MU_button.Visible = false;
            MU_button.BackColor = Color.White;
            MU_button.Click += greek_letter_click;
            this.Controls.Add(MU_button);

            NU_button.Font = a_font;
            NU_button.Size = a_size;
            NU_button.Visible = false;
            NU_button.BackColor = Color.White;
            NU_button.Click += greek_letter_click;
            this.Controls.Add(NU_button);

            XI_button.Font = a_font;
            XI_button.Size = a_size;
            XI_button.Visible = false;
            XI_button.BackColor = Color.White;
            XI_button.Click += greek_letter_click;
            this.Controls.Add(XI_button);

            OMICRON_button.Font = a_font;
            OMICRON_button.Size = a_size;
            OMICRON_button.Visible = false;
            OMICRON_button.BackColor = Color.White;
            OMICRON_button.Click += greek_letter_click;
            this.Controls.Add(OMICRON_button);

            PI_button.Font = a_font;
            PI_button.Size = a_size;
            PI_button.Visible = false;
            PI_button.BackColor = Color.White;
            PI_button.Click += greek_letter_click;
            this.Controls.Add(PI_button);

            RHO_button.Font = a_font;
            RHO_button.Size = a_size;
            RHO_button.Visible = false;
            RHO_button.BackColor = Color.White;
            RHO_button.Click += greek_letter_click;
            this.Controls.Add(RHO_button);

            SIGMA_button.Font = a_font;
            SIGMA_button.Size = a_size;
            SIGMA_button.Visible = false;
            SIGMA_button.BackColor = Color.White;
            SIGMA_button.Click += greek_letter_click;
            this.Controls.Add(SIGMA_button);

            TAU_button.Font = a_font;
            TAU_button.Size = a_size;
            TAU_button.Visible = false;
            TAU_button.BackColor = Color.White;
            TAU_button.Click += greek_letter_click;
            this.Controls.Add(TAU_button);

            UPSILON_button.Font = a_font;
            UPSILON_button.Size = a_size;
            UPSILON_button.Visible = false;
            UPSILON_button.BackColor = Color.White;
            UPSILON_button.Click += greek_letter_click;
            this.Controls.Add(UPSILON_button);

            PHI_button.Font = a_font;
            PHI_button.Size = a_size;
            PHI_button.Visible = false;
            PHI_button.BackColor = Color.White;
            PHI_button.Click += greek_letter_click;
            this.Controls.Add(PHI_button);

            CHI_button.Font = a_font;
            CHI_button.Size = a_size;
            CHI_button.Visible = false;
            CHI_button.BackColor = Color.White;
            CHI_button.Click += greek_letter_click;
            this.Controls.Add(CHI_button);

            PSI_button.Font = a_font;
            PSI_button.Size = a_size;
            PSI_button.Visible = false;
            PSI_button.BackColor = Color.White;
            PSI_button.Click += greek_letter_click;
            this.Controls.Add(PSI_button);

            OMEGA_button.Font = a_font;
            OMEGA_button.Size = a_size;
            OMEGA_button.Visible = false;
            OMEGA_button.BackColor = Color.White;
            OMEGA_button.Click += greek_letter_click;
            this.Controls.Add(OMEGA_button);

            #endregion

            greek_alphabet_background.Size = new Size(greek_alphabet_caps.Size.Width + alpha_button.Width / 5, greek_alphabet_caps.Size.Height + 6 * alpha_button.Size.Height + alpha_button.Size.Height / 5);
            greek_alphabet_background.Visible = false;
            greek_alphabet_background.BackColor = Color.White;
            this.Controls.Add(greek_alphabet_background);

            this.MinimumSize = greek_alphabet_background.Size;
        }

        private void reposition_greek_alphabet()
        {
            greek_alphabet_caps.Location = new Point(ClientSize.Width - 12 * font_size - alpha_button.Width / 20, 4 * menu_strip.Height / 3);
            greek_alphabet_background.Location = new Point(ClientSize.Width - 12 * font_size - alpha_button.Height / 10, 4 * menu_strip.Height / 3 - alpha_button.Height / 10);

            #region greek alphabet

            alpha_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            ALPHA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            beta_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            BETA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            gamma_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            GAMMA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            delta_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            DELTA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height);
            epsilon_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            EPSILON_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            zeta_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            ZETA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            eta_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            ETA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            theta_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            THETA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + alpha_button.Height);
            iota_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            IOTA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            kappa_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            KAPPA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            lambda_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            LAMBDA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            mu_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            MU_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 2 * alpha_button.Height);
            nu_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            NU_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            xi_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            XI_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            omicron_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            OMICRON_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            pi_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            PI_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 3 * alpha_button.Height);
            rho_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            RHO_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            sigma_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            SIGMA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            tau_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            TAU_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            upsilon_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            UPSILON_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 4 * alpha_button.Height);
            phi_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            PHI_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            chi_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            CHI_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            psi_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            PSI_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 2 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            omega_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);
            OMEGA_button.Location = new Point(ClientSize.Width - greek_alphabet_caps.Width + 3 * alpha_button.Width - alpha_button.Width / 20, 4 * menu_strip.Height / 3 + greek_alphabet_caps.Height + 5 * alpha_button.Height);

            #endregion
        }

        private void visualize_greek_alphabet()
        {
            greek_alphabet_caps.Visible = true;
            greek_alphabet_background.Visible = true;
            greek_alphabet_background.SendToBack();

            #region greek alphabet

            if (greek_alphabet_caps.Checked == true)
            {
                ALPHA_button.Visible = true;
                BETA_button.Visible = true;
                GAMMA_button.Visible = true;
                DELTA_button.Visible = true;
                EPSILON_button.Visible = true;
                ZETA_button.Visible = true;
                ETA_button.Visible = true;
                THETA_button.Visible = true;
                IOTA_button.Visible = true;
                KAPPA_button.Visible = true;
                LAMBDA_button.Visible = true;
                MU_button.Visible = true;
                NU_button.Visible = true;
                XI_button.Visible = true;
                OMICRON_button.Visible = true;
                PI_button.Visible = true;
                RHO_button.Visible = true;
                SIGMA_button.Visible = true;
                TAU_button.Visible = true;
                UPSILON_button.Visible = true;
                PHI_button.Visible = true;
                CHI_button.Visible = true;
                PSI_button.Visible = true;
                OMEGA_button.Visible = true;
            }
            else
            {
                alpha_button.Visible = true;
                beta_button.Visible = true;
                gamma_button.Visible = true;
                delta_button.Visible = true;
                epsilon_button.Visible = true;
                zeta_button.Visible = true;
                eta_button.Visible = true;
                theta_button.Visible = true;
                iota_button.Visible = true;
                kappa_button.Visible = true;
                lambda_button.Visible = true;
                mu_button.Visible = true;
                nu_button.Visible = true;
                xi_button.Visible = true;
                omicron_button.Visible = true;
                pi_button.Visible = true;
                rho_button.Visible = true;
                sigma_button.Visible = true;
                tau_button.Visible = true;
                upsilon_button.Visible = true;
                phi_button.Visible = true;
                chi_button.Visible = true;
                psi_button.Visible = true;
                omega_button.Visible = true;
            }

            #endregion
        }

        private void hide_greek_alphabet()
        {
            alpha_button.Visible = false;
            beta_button.Visible = false;
            gamma_button.Visible = false;
            delta_button.Visible = false;
            epsilon_button.Visible = false;
            zeta_button.Visible = false;
            eta_button.Visible = false;
            theta_button.Visible = false;
            iota_button.Visible = false;
            kappa_button.Visible = false;
            lambda_button.Visible = false;
            mu_button.Visible = false;
            nu_button.Visible = false;
            xi_button.Visible = false;
            omicron_button.Visible = false;
            pi_button.Visible = false;
            rho_button.Visible = false;
            sigma_button.Visible = false;
            tau_button.Visible = false;
            upsilon_button.Visible = false;
            phi_button.Visible = false;
            chi_button.Visible = false;
            psi_button.Visible = false;
            omega_button.Visible = false;
        }

        private void hide_GREEK_alphabet()
        {
            ALPHA_button.Visible = false;
            BETA_button.Visible = false;
            GAMMA_button.Visible = false;
            DELTA_button.Visible = false;
            EPSILON_button.Visible = false;
            ZETA_button.Visible = false;
            ETA_button.Visible = false;
            THETA_button.Visible = false;
            IOTA_button.Visible = false;
            KAPPA_button.Visible = false;
            LAMBDA_button.Visible = false;
            MU_button.Visible = false;
            NU_button.Visible = false;
            XI_button.Visible = false;
            OMICRON_button.Visible = false;
            PI_button.Visible = false;
            RHO_button.Visible = false;
            SIGMA_button.Visible = false;
            TAU_button.Visible = false;
            UPSILON_button.Visible = false;
            PHI_button.Visible = false;
            CHI_button.Visible = false;
            PSI_button.Visible = false;
            OMEGA_button.Visible = false;
        }

        private void load_opened_tabs_control()
        {
            opened_tabs_control.Visible = false;
            opened_tabs_control.Location = new Point(2 * font_size, 4 * menu_strip.Height / 3 + 1);
            opened_tabs_control.ItemSize = new Size(opened_tabs_control.ItemSize.Width, 2 * font_size - 2);
            opened_tabs_control.Selecting += opened_tabs_control_selecting;
            opened_tabs_control.MouseClick += open_tab_context_menu;

            this.Controls.Add(opened_tabs_control);
        }

        private void reposition_opened_tabs_control()
        {
            if (alpha_button.Visible == true) opened_tabs_control.Size = new Size(greek_alphabet_background.Location.X - upper_index_button.Width, ClientSize.Height - opened_tabs_control.Location.Y);
            else opened_tabs_control.Size = new Size(ClientSize.Width - upper_index_button.Width, ClientSize.Height - opened_tabs_control.Location.Y);
            this.Refresh();

            foreach (TabPage tab in opened_tabs_control.TabPages)
                foreach (Control control in tab.Controls)
                    if (control.GetType() == typeof(NumberedRTB))
                        control.Size = new Size(opened_tabs_control.SelectedTab.Size.Width, opened_tabs_control.SelectedTab.Size.Height);
        }
    }
}
