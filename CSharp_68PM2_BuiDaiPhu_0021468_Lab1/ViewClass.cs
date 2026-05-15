using System;
using System.Windows.Forms;

namespace CSharp_68PM2_BuiDaiPhu_0021468_Lab1
{
    public partial class ViewClass : Form
    {
        public ViewClass()
        {
            InitializeComponent();
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // Populate DataGridView
            dgvLopHoc.Rows.Add("1", "68PM1", "Lớp 68PM1", "abc");
            dgvLopHoc.Rows.Add("2", "68PM2", "Lớp 68PM2", "xyz");

            // Select the second row like in the image
            if (dgvLopHoc.Rows.Count > 1)
            {
                dgvLopHoc.Rows[1].Selected = true;
            }
            
            // Set values to match image input fields
            txtMaID.Text = "2";
            txtMaLop.Text = "68PM2";
            txtTenLop.Text = "Lớp 68PM2";
            txtGhiChu.Text = "xyz";
        }
    }
}
