using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QUANLYSV
{
    public partial class frmClassStudents : Form
    {
        private string classId;
        private string className;

        public frmClassStudents(string classId, string className)
        {
            InitializeComponent();
            this.classId = classId;
            this.className = className;
            this.Load += new EventHandler(this.frmClassStudents_Load);
        }

        private void frmClassStudents_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "DANH SÁCH SINH VIÊN - LỚP: " + className.ToUpper();
            LoadStudents();
        }

        private DatabaseDataContext CreateDataContext()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["QLSVConnectionString"];

            if (connectionString == null)
            {
                throw new InvalidOperationException("Không tìm thấy connection string QLSVConnectionString trong App.config.");
            }

            return new DatabaseDataContext(connectionString.ConnectionString);
        }

        private void LoadStudents()
        {
            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    var students = (from sv in db.SinhVien
                                    where sv.MaLop == classId
                                    orderby sv.MaSV
                                    select new
                                    {
                                        sv.MaSV,
                                        sv.HoTen,
                                        sv.GioiTinh,
                                        sv.NgaySinh
                                    })
                                    .ToList()
                                    .Select(sv => new
                                    {
                                        sv.MaSV,
                                        sv.HoTen,
                                        sv.GioiTinh,
                                        NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy")
                                    })
                                    .ToList();

                    dgvStudents.DataSource = students;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
