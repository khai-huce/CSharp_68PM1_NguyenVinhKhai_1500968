using System;
using System.Drawing;
using System.Windows.Forms;

namespace QUANLYSV
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();


        }

        private void Main_Load(object sender, EventArgs e)
        {
            
            OpenUserControl(new QLSinhVien());
        }

        private void OpenUserControl(UserControl uc)
        {

            panelMain.Controls.Clear();


            uc.Dock = DockStyle.Fill;


            panelMain.Controls.Add(uc);


            uc.BringToFront();
        }

        private void quảnLíSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUserControl(new QLSinhVien());
        }

        private void quảnLíLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUserControl(new QLLopHoc());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                login login = new login();
                login.Show();

                this.Close();
            }
        }

    }
}