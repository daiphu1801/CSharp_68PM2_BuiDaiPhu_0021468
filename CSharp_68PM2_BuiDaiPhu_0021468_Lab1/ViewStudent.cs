using System;
using System.Windows.Forms;

namespace CSharp_68PM2_BuiDaiPhu_0021468_Lab1
{
    public partial class ViewStudent : Form
    {
        public ViewStudent()
        {
            InitializeComponent();
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // Populate ComboBox Lớp
            cmbLop.Items.Add("68PM1 - Lớp 68PM1");
            cmbLop.Items.Add("68PM2 - Lớp 68PM2");
            cmbLop.SelectedIndex = 0;

            // Set default ComboBox Giới tính
            cmbGioiTinh.SelectedIndex = 0;

            // Populate DataGridView
            dgvSinhVien.Rows.Add("1", "hieu", "Nam", "11/03/2026", "68PM1");
            dgvSinhVien.Rows.Add("2", "Nguyễn Văn B", "Nam", "11/03/2026", "68PM2");
            dgvSinhVien.Rows.Add("3", "Trần Văn C", "Nam", "21/03/2026", "68PM2");
        }
    }
}
