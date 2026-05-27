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
    public partial class UCQLSV : UserControl
    {
        public UCQLSV()
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

            // Labels inside groupBox – normal weight
            var labelFont = new Font("Segoe UI", 9f, FontStyle.Regular);
            foreach (Control c in groupBox1.Controls)
                if (c is Label lbl) { lbl.ForeColor = Color.FromArgb(60, 80, 100); lbl.Font = labelFont; }

            // TextBoxes inside groupBox
            foreach (Control c in groupBox1.Controls)
                if (c is TextBox tb) { tb.BorderStyle = BorderStyle.FixedSingle; tb.BackColor = Color.FromArgb(252, 253, 255); }

            // Buttons – Thêm / Sửa → primary blue; Xóa → red; Làm mới → gray
            StyleButton(btn_add, Form_main.PrimaryBtn);
            StyleButton(button3, Form_main.PrimaryBtn);
            StyleButton(button4, Form_main.DangerBtn);
            StyleButton(button5, Form_main.NeutralBtn);
            StyleButton(button11, Form_main.PrimaryBtn);

            // Search button
            StyleButton(button1, Form_main.PrimaryBtn);
            textBox6.BorderStyle = BorderStyle.FixedSingle;
            textBox6.BackColor = Color.White;
            label6.ForeColor = Form_main.NavyColor;

            // Pagination buttons
            Color pageBtnColor = Color.FromArgb(189, 195, 199);
            Color pageBtnFg   = Color.FromArgb(44, 62, 80);
            StyleButton(button6, pageBtnColor, pageBtnFg);
            StyleButton(button7, pageBtnColor, pageBtnFg);
            StyleButton(button8, pageBtnColor, pageBtnFg);
            StyleButton(button9, pageBtnColor, pageBtnFg);
            label7.ForeColor = Color.FromArgb(80, 100, 120);

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
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadData()
        {
            try
            {
                using (DatabaseDataContext db = new DatabaseDataContext())
                {
                    dataGridView2.DataSource = db.SinhViens.ToList();

                    // Format columns to make them beautiful and professional
                    if (dataGridView2.Columns["id"] != null)
                        dataGridView2.Columns["id"].Visible = false;

                    if (dataGridView2.Columns["mssv"] != null)
                    {
                        dataGridView2.Columns["mssv"].HeaderText = "Mã Số Sinh Viên";
                        dataGridView2.Columns["mssv"].FillWeight = 90;
                    }
                    if (dataGridView2.Columns["hoten"] != null)
                    {
                        dataGridView2.Columns["hoten"].HeaderText = "Họ và Tên";
                        dataGridView2.Columns["hoten"].FillWeight = 160;
                    }
                    if (dataGridView2.Columns["ngaysinh"] != null)
                    {
                        dataGridView2.Columns["ngaysinh"].HeaderText = "Ngày Sinh";
                        dataGridView2.Columns["ngaysinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                        dataGridView2.Columns["ngaysinh"].FillWeight = 90;
                    }
                    if (dataGridView2.Columns["gioitinh"] != null)
                    {
                        dataGridView2.Columns["gioitinh"].HeaderText = "Giới Tính";
                        dataGridView2.Columns["gioitinh"].FillWeight = 70;
                    }
                    if (dataGridView2.Columns["lop"] != null)
                    {
                        dataGridView2.Columns["lop"].HeaderText = "Lớp";
                        dataGridView2.Columns["lop"].FillWeight = 90;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UCQLSV_Load(object sender, EventArgs e)
        {
            LoadData();

            // Populate cb_lop
            cb_lop.Items.Clear();
            cb_lop.Items.AddRange(new object[] { "68PM1", "68PM2", "68PM3" });
            if (cb_lop.Items.Count > 0) cb_lop.SelectedIndex = 0;

            // Populate cb_gioitinh standard default
            if (cb_gioitinh.Items.Count > 0) cb_gioitinh.SelectedIndex = 0;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseDataContext db = new DatabaseDataContext())
                {
                    string mssvVal = tb_mssv.Text.Trim();
                    string hotenVal = tb_hoten.Text.Trim();

                    if (string.IsNullOrEmpty(mssvVal) || string.IsNullOrEmpty(hotenVal))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ MSSV và Họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check duplicate MSSV
                    if (db.SinhViens.Any(s => s.mssv == mssvVal))
                    {
                        MessageBox.Show("Mã số sinh viên đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SinhVien sv = new SinhVien
                    {
                        mssv = mssvVal,
                        hoten = hotenVal,
                        ngaysinh = date.Value,
                        gioitinh = cb_gioitinh.SelectedItem?.ToString() ?? cb_gioitinh.Text,
                        lop = cb_lop.SelectedItem?.ToString() ?? cb_lop.Text
                    };

                    db.SinhViens.InsertOnSubmit(sv);
                    db.SubmitChanges();

                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear inputs
                    tb_mssv.Text = "";
                    tb_hoten.Text = "";
                    tb_mssv.Focus();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
