using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace QUANLYSV
{
    public partial class QLSinhVien : UserControl
    {
        private string selectedStudentId;

        public QLSinhVien()
        {
            InitializeComponent();
            ConfigureControls();
        }

        private void QLSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClassList4CBX();
                LoadStudentList();
                ClearStudentForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ConfigureControls()
        {
            dgvStdView.AutoGenerateColumns = false;
            dgvStdView.AllowUserToAddRows = false;
            dgvStdView.ReadOnly = true;
            dgvStdView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStdView.MultiSelect = false;
            dgvStdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStdView.RowHeadersVisible = false;

            StdID.DataPropertyName = "MaSV";
            Column1.DataPropertyName = "HoTen";
            Column2.DataPropertyName = "GioiTinh";
            Column3.DataPropertyName = "NgaySinh";
            Column4.DataPropertyName = "TenLop";

            StdID.FillWeight = 75;
            Column1.FillWeight = 150;
            Column2.FillWeight = 80;
            Column3.FillWeight = 90;
            Column4.FillWeight = 120;

            cbGender.DisplayMember = string.Empty;
            cbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cbClass.DropDownStyle = ComboBoxStyle.DropDownList;
            btnEditStd.Enabled = false;
        }

        private void LoadStudentList()
        {
            using (DatabaseDataContext db = CreateDataContext())
            {
                var students = (from sv in db.SinhVien
                                join lop in db.LopHoc on sv.MaLop equals lop.MaLop
                                orderby sv.MaSV
                                select new
                                {
                                    sv.MaSV,
                                    sv.HoTen,
                                    sv.GioiTinh,
                                    sv.NgaySinh,
                                    lop.TenLop
                                })
                    .ToList()
                    .Select(sv => new
                    {
                        sv.MaSV,
                        sv.HoTen,
                        sv.GioiTinh,
                        NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                        sv.TenLop
                    })
                    .ToList();

                dgvStdView.DataSource = students;
                label7.Text = string.Format("Trang 1/1 | {0} bản ghi", students.Count);
            }
        }

        private void LoadClassList4CBX()
        {
            using (DatabaseDataContext db = CreateDataContext())
            {
                var classes = db.LopHoc
                    .OrderBy(lop => lop.MaLop)
                    .Select(lop => new
                    {
                        lop.MaLop,
                        lop.TenLop
                    })
                    .ToList();

                cbClass.DataSource = classes;
                cbClass.DisplayMember = "TenLop";
                cbClass.ValueMember = "MaLop";
                cbClass.SelectedIndex = classes.Count > 0 ? 0 : -1;
            }
        }

        private void btnAddStd_Click(object sender, EventArgs e)
        {
            string maSinhVien = txtStdId.Text.Trim();
            string hoTen = txtName.Text.Trim();
            string gioiTinh = cbGender.SelectedItem == null ? string.Empty : cbGender.SelectedItem.ToString();
            string maLop = cbClass.SelectedValue == null ? string.Empty : cbClass.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStdId.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(gioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbGender.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbClass.Focus();
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    bool isDuplicateStudentId = db.SinhVien.Any(sv => sv.MaSV == maSinhVien);

                    if (isDuplicateStudentId)
                    {
                        MessageBox.Show("Mã sinh viên đã tồn tại.", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtStdId.Focus();
                        return;
                    }

                    SinhVien sinhVien = new SinhVien
                    {
                        MaSV = maSinhVien,
                        HoTen = hoTen,
                        GioiTinh = gioiTinh,
                        NgaySinh = txtStdDate.Value.Date,
                        MaLop = maLop
                    };

                    db.SinhVien.InsertOnSubmit(sinhVien);
                    db.SubmitChanges();
                }

                LoadStudentList();
                ClearStudentForm();
                MessageBox.Show("Thêm sinh viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string studentId = Convert.ToString(dgvStdView.Rows[e.RowIndex].Cells["StdID"].Value);

            if (string.IsNullOrWhiteSpace(studentId))
            {
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    SinhVien student = db.SinhVien.FirstOrDefault(sv => sv.MaSV == studentId);

                    if (student == null)
                    {
                        MessageBox.Show("Không tìm thấy sinh viên đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadStudentList();
                        ClearStudentForm();
                        return;
                    }

                    selectedStudentId = student.MaSV;
                    txtStdId.Text = student.MaSV;
                    txtStdId.ReadOnly = true;
                    txtName.Text = student.HoTen;
                    txtStdDate.Value = student.NgaySinh;
                    cbGender.SelectedItem = student.GioiTinh;
                    cbClass.SelectedValue = student.MaLop;
                    btnAddStd.Enabled = false;
                    btnEditStd.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearStudentForm()
        {
            selectedStudentId = null;
            txtStdId.Clear();
            txtStdId.ReadOnly = false;
            txtName.Clear();
            txtStdDate.Value = DateTime.Today;
            cbGender.SelectedIndex = cbGender.Items.Count > 0 ? 0 : -1;
            cbClass.SelectedIndex = cbClass.Items.Count > 0 ? 0 : -1;
            dgvStdView.ClearSelection();
            btnAddStd.Enabled = true;
            btnEditStd.Enabled = false;
            txtStdId.Focus();
        }
    }
}
