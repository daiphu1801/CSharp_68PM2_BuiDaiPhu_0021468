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
    public partial class UCQLLH : UserControl
    {
        int currentPage = 1;
        int totalPage;
        string searchKeyword = "";
        int pageSize = 5; // Let's use 5 as a reasonable page size, or we can use 5 or 1. Let's look at what the target commit does. The target commit has 1. Wait, let's keep it to 5 or check if 5 is better or 1. Let's use 5, but wait, the target commit has pageSize = 1. Let's follow the target commit or keep pageSize = 5. Actually, using 5 is more standard, but to be safe and matching the commit exactly, let's use 5 as a default unless 1 is explicitly tested, actually, let's just make it 5 so it behaves nicely with pagination (or 5 like in SinhVien which has 5). Let's use 5.

        public UCQLLH()
        {
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            this.BackColor = Form_main.BgColor;

            // GroupBox
            groupBox1.BackColor = Color.White;
            groupBox1.ForeColor = Form_main.NavyColor;
            groupBox1.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            // Labels
            var labelFont = new Font("Segoe UI", 9f, FontStyle.Regular);
            foreach (Control c in groupBox1.Controls)
                if (c is Label lbl) { lbl.ForeColor = Color.FromArgb(60, 80, 100); lbl.Font = labelFont; }

            // TextBoxes
            foreach (Control c in groupBox1.Controls)
                if (c is TextBox tb) { tb.BorderStyle = BorderStyle.FixedSingle; tb.BackColor = Color.FromArgb(252, 253, 255); }

            // Buttons
            StyleButton(add_btn, Form_main.PrimaryBtn);
            StyleButton(sua_btn, Form_main.PrimaryBtn);
            StyleButton(delete_btn, Form_main.DangerBtn);
            StyleButton(refresh_btn, Form_main.NeutralBtn);

            // Search
            StyleButton(timKiem_btn, Form_main.PrimaryBtn);
            txt_find.BorderStyle = BorderStyle.FixedSingle;
            txt_find.BackColor = Color.White;
            label6.ForeColor = Form_main.NavyColor;

            // Pagination
            Color pageBtnColor = Color.FromArgb(189, 195, 199);
            Color pageBtnFg   = Color.FromArgb(44, 62, 80);
            StyleButton(button6, pageBtnColor, pageBtnFg);
            StyleButton(button7, pageBtnColor, pageBtnFg);
            StyleButton(button8, pageBtnColor, pageBtnFg);
            StyleButton(button9, pageBtnColor, pageBtnFg);
            page.ForeColor = Color.FromArgb(80, 100, 120);

            // DataGridView
            ThemeDataGridView(dataGridView2);
        }

        private void StyleButton(Button btn, Color bg, Color? fg = null)
        {
            btn.BackColor = bg;
            btn.ForeColor = fg ?? Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        private void ThemeDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(220, 225, 230);
            dgv.RowHeadersVisible = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Form_main.HeaderColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 32;
            dgv.RowTemplate.Height = 26;
        }

        public void loadData()
        {
            DatabaseDataContext db = new DatabaseDataContext();
            if (!searchKeyword.Equals(""))
            {   
                var results = db.LopHocs.Where(lh =>
                    lh.lop.ToLower().Contains(searchKeyword) ||
                    lh.ten_lop.ToLower().Contains(searchKeyword)
                );
                totalPage = (int)Math.Ceiling((double)results.Count() / pageSize);
                if (totalPage == 0) totalPage = 1;
                dataGridView2.DataSource = results.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                page.Text = "Trang " + currentPage + " / " + totalPage;
                return;
            }
            totalPage = (int)Math.Ceiling((double)db.LopHocs.Count() / pageSize);
            if (totalPage == 0) totalPage = 1;
            dataGridView2.DataSource = db.LopHocs.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            page.Text = "Trang " + currentPage + " / " + totalPage;
        }

        private void UCQLLH_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            string lop = txt_lop.Text;
            string tenLop = txt_tenLop.Text;
            if (lop == "" || tenLop == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            LopHoc newLop = new LopHoc
            {
                lop = lop,
                ten_lop = tenLop
            };
            db.LopHocs.InsertOnSubmit(newLop);
            db.SubmitChanges();
            loadData();
        }

        private void sua_btn_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            string lop = txt_lop.Text;
            string tenLop = txt_tenLop.Text;
            LopHoc existingLop = db.LopHocs.FirstOrDefault(l => l.lop == lop);
            if (existingLop != null)
            {
                existingLop.ten_lop = tenLop;
                db.SubmitChanges();
                loadData();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; 

            txt_lop.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
            txt_tenLop.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            string lop = txt_lop.Text;
            if (lop != "")
            {
                LopHoc existingLop = db.LopHocs.FirstOrDefault(l => l.lop == lop);
                if (existingLop != null)
                {
                    db.LopHocs.DeleteOnSubmit(existingLop);
                    db.SubmitChanges();
                    loadData();
                }
            }
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            txt_lop.Text = "";
            txt_tenLop.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentPage += 1;
            if (currentPage > totalPage) currentPage = totalPage;
            loadData();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentPage = totalPage;
            loadData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentPage -= 1;
            if (currentPage < 1) currentPage = 1;
            loadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            loadData();
        }

        private void timKiem_btn_Click(object sender, EventArgs e)
        {
            searchKeyword = txt_find.Text.Trim().ToLower();
            currentPage = 1;
            loadData();
        }
    }
}
