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
    public partial class DSSV : Form
    {
        public DSSV(string maLop)
        {
            InitializeComponent();
            ApplyTheme();
            loadData(maLop);
            txt_maLop.Text = maLop;
        }

        private void ApplyTheme()
        {
            this.BackColor = Form_main.BgColor;
            
            // Labels
            label1.ForeColor = Form_main.NavyColor;
            label1.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            
            txt_maLop.ForeColor = Form_main.PrimaryBtn;
            txt_maLop.Font = new Font("Segoe UI", 11f, FontStyle.Bold);

            // DataGridView Theme
            ThemeDataGridView(dataGridView1);
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

        public void loadData(string maLop)
        {
            try
            {
                using (DatabaseDataContext db = new DatabaseDataContext())
                {
                    var results = db.SinhViens
                        .Where(sv => sv.lop == maLop)
                        .Select(sv => new
                        {
                            MSSV = sv.mssv,
                            HoTen = sv.hoten,
                            NgaySinh = sv.ngaysinh,
                            GioiTinh = sv.gioitinh,
                            Lop = sv.lop
                        })
                        .ToList();

                    dataGridView1.DataSource = results;

                    // Rename headers and set weights
                    if (dataGridView1.Columns["MSSV"] != null)
                    {
                        dataGridView1.Columns["MSSV"].HeaderText = "Mã Số Sinh Viên";
                        dataGridView1.Columns["MSSV"].FillWeight = 100;
                    }
                    if (dataGridView1.Columns["HoTen"] != null)
                    {
                        dataGridView1.Columns["HoTen"].HeaderText = "Họ và Tên";
                        dataGridView1.Columns["HoTen"].FillWeight = 160;
                    }
                    if (dataGridView1.Columns["NgaySinh"] != null)
                    {
                        dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                        dataGridView1.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                        dataGridView1.Columns["NgaySinh"].FillWeight = 100;
                    }
                    if (dataGridView1.Columns["GioiTinh"] != null)
                    {
                        dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
                        dataGridView1.Columns["GioiTinh"].FillWeight = 80;
                    }
                    if (dataGridView1.Columns["Lop"] != null)
                    {
                        dataGridView1.Columns["Lop"].HeaderText = "Lớp";
                        dataGridView1.Columns["Lop"].FillWeight = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
