using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_68PM2_BuiDaiPhu_0021468_Lab1
{
    public partial class Form_main : Form
    {
        // ── Bảng màu chủ đề ───────────────────────────
        internal static readonly Color BgColor     = Color.FromArgb(245, 247, 250);
        internal static readonly Color NavyColor   = Color.FromArgb(41,  57,  73);
        internal static readonly Color PrimaryBtn  = Color.FromArgb(52, 152, 219);
        internal static readonly Color DangerBtn   = Color.FromArgb(192, 57,  43);
        internal static readonly Color NeutralBtn  = Color.FromArgb(127, 140, 141);
        internal static readonly Color HeaderColor = Color.FromArgb(44,  62,  80);
        // ──────────────────────────────────────────────

        public Form_main()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            // Form
            this.BackColor = BgColor;

            // MenuStrip
            menuStrip1.BackColor = NavyColor;
            menuStrip1.ForeColor = Color.White;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new NavyMenuColorTable());

            // Menu items
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.ForeColor = Color.White;
                item.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            }

            // Panel
            panel_main.BackColor = BgColor;
        }

        private void panel_main_Paint(object sender, PaintEventArgs e) { }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UCQLSV uCQLSV = new UCQLSV();
            uCQLSV.Dock = DockStyle.Fill;
            panel_main.Controls.Clear();
            panel_main.Controls.Add(uCQLSV);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UCQLLH uCQLLH = new UCQLLH();
            uCQLLH.Dock = DockStyle.Fill;
            panel_main.Controls.Clear();
            panel_main.Controls.Add(uCQLLH);
        }
    }

    // Custom color table for MenuStrip
    internal class NavyMenuColorTable : ProfessionalColorTable
    {
        private readonly Color _navy = Color.FromArgb(41, 57, 73);
        private readonly Color _hover = Color.FromArgb(52, 73, 94);
        private readonly Color _pressed = Color.FromArgb(30, 42, 54);

        public override Color MenuStripGradientBegin => _navy;
        public override Color MenuStripGradientEnd   => _navy;
        public override Color MenuItemSelectedGradientBegin => _hover;
        public override Color MenuItemSelectedGradientEnd   => _hover;
        public override Color MenuItemPressedGradientBegin  => _pressed;
        public override Color MenuItemPressedGradientEnd    => _pressed;
        public override Color MenuItemSelected => _hover;
        public override Color MenuItemBorder   => Color.FromArgb(52, 73, 94);
        public override Color MenuBorder       => Color.FromArgb(52, 73, 94);
        public override Color ToolStripDropDownBackground => Color.White;
        public override Color ImageMarginGradientBegin => Color.White;
        public override Color ImageMarginGradientMiddle => Color.White;
        public override Color ImageMarginGradientEnd => Color.White;
    }
}
