using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace QUANLYSV
{
    public partial class QLSinhVien : UserControl
    {
        private const int PageSize = 5;

        private string selectedStudentId;
        private string currentKeyword = string.Empty;
        private int currentPage = 1;
        private int totalPages = 1;

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
                LoadStudentList(resetPage: true);
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

            SetEditMode(false);
            UpdatePagingButtons(0);
        }

        private void LoadStudentList(string keyword = null, bool resetPage = false)
        {
            if (keyword != null)
            {
                currentKeyword = keyword.Trim();
            }

            if (resetPage)
            {
                currentPage = 1;
            }

            using (DatabaseDataContext db = CreateDataContext())
            {
                var query = from sv in db.SinhVien
                            join lop in db.LopHoc on sv.MaLop equals lop.MaLop
                            select new
                            {
                                sv.MaSV,
                                sv.HoTen,
                                sv.GioiTinh,
                                sv.NgaySinh,
                                sv.MaLop,
                                lop.TenLop
                            };

                if (!string.IsNullOrWhiteSpace(currentKeyword))
                {
                    query = query.Where(sv => sv.MaSV.Contains(currentKeyword)
                                           || sv.HoTen.Contains(currentKeyword)
                                           || sv.GioiTinh.Contains(currentKeyword)
                                           || sv.TenLop.Contains(currentKeyword));
                }

                query = query.OrderBy(sv => sv.MaSV);

                int totalRecords = query.Count();
                totalPages = Math.Max(1, (int)Math.Ceiling(totalRecords / (double)PageSize));
                currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

                var pageRecords = query
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                List<StudentGridItem> pageStudents = pageRecords
                    .Select(sv => new StudentGridItem
                    {
                        MaSV = sv.MaSV,
                        HoTen = sv.HoTen,
                        GioiTinh = sv.GioiTinh,
                        NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                        MaLop = sv.MaLop,
                        TenLop = sv.TenLop
                    })
                    .ToList();

                dgvStdView.DataSource = pageStudents;
                label7.Text = string.Format("Trang {0}/{1} | {2} bản ghi", currentPage, totalPages, totalRecords);
                UpdatePagingButtons(totalRecords);
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

        private bool ContainsSearchText(string source, string keyword)
        {
            return !string.IsNullOrEmpty(source)
                && source.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void btnAddStd_Click(object sender, EventArgs e)
        {
            string studentId = txtStdId.Text.Trim();

            if (string.IsNullOrWhiteSpace(studentId))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStdId.Focus();
                return;
            }

            string fullName;
            string gender;
            string classId;

            if (!TryGetStudentInput(out fullName, out gender, out classId))
            {
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    if (db.SinhVien.Any(sv => sv.MaSV == studentId))
                    {
                        MessageBox.Show("Mã sinh viên đã tồn tại.", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtStdId.Focus();
                        return;
                    }

                    SinhVien student = new SinhVien
                    {
                        MaSV = studentId,
                        HoTen = fullName,
                        GioiTinh = gender,
                        NgaySinh = txtStdDate.Value.Date,
                        MaLop = classId
                    };

                    db.SinhVien.InsertOnSubmit(student);
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
                    SetEditMode(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditStd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedStudentId))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa trong danh sách.", "Chưa chọn sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName;
            string gender;
            string classId;

            if (!TryGetStudentInput(out fullName, out gender, out classId))
            {
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    SinhVien student = db.SinhVien.FirstOrDefault(sv => sv.MaSV == selectedStudentId);

                    if (student == null)
                    {
                        MessageBox.Show("Sinh viên cần sửa không còn tồn tại.", "Không tìm thấy dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadStudentList();
                        ClearStudentForm();
                        return;
                    }

                    student.HoTen = fullName;
                    student.GioiTinh = gender;
                    student.NgaySinh = txtStdDate.Value.Date;
                    student.MaLop = classId;
                    db.SubmitChanges();
                }

                LoadStudentList();
                ClearStudentForm();
                MessageBox.Show("Cập nhật sinh viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể cập nhật sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteStd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedStudentId))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa trong danh sách.", "Chưa chọn sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên có mã " + selectedStudentId + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    SinhVien student = db.SinhVien.FirstOrDefault(sv => sv.MaSV == selectedStudentId);

                    if (student == null)
                    {
                        MessageBox.Show("Sinh viên cần xóa không còn tồn tại.", "Không tìm thấy dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadStudentList();
                        ClearStudentForm();
                        return;
                    }

                    db.SinhVien.DeleteOnSubmit(student);
                    db.SubmitChanges();
                }

                LoadStudentList();
                ClearStudentForm();
                MessageBox.Show("Xóa sinh viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadStudentList(txtFind.Text, resetPage: true);
            ClearStudentForm();
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnFind_Click(sender, EventArgs.Empty);
            }
        }

        private void btnFrist_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadStudentList();
            ClearStudentForm();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadStudentList();
                ClearStudentForm();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadStudentList();
                ClearStudentForm();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            LoadStudentList();
            ClearStudentForm();
        }

        private bool TryGetStudentInput(out string fullName, out string gender, out string classId)
        {
            fullName = txtName.Text.Trim();
            gender = cbGender.SelectedItem == null ? string.Empty : cbGender.SelectedItem.ToString();
            classId = cbClass.SelectedValue == null ? string.Empty : cbClass.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbGender.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(classId))
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbClass.Focus();
                return false;
            }

            return true;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            LoadStudentList(string.Empty, resetPage: true);
            ClearStudentForm();
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
            SetEditMode(false);
            txtStdId.Focus();
        }

        private void SetEditMode(bool isEditing)
        {
            btnAddStd.Enabled = !isEditing;
            btnEditStd.Enabled = isEditing;
            btnDeleteStd.Enabled = isEditing;
        }

        private void UpdatePagingButtons(int totalRecords)
        {
            bool hasRecords = totalRecords > 0;
            btnFrist.Enabled = hasRecords && currentPage > 1;
            btnPre.Enabled = hasRecords && currentPage > 1;
            btnNext.Enabled = hasRecords && currentPage < totalPages;
            button1.Enabled = hasRecords && currentPage < totalPages;
        }

        private class StudentGridItem
        {
            public string MaSV { get; set; }

            public string HoTen { get; set; }

            public string GioiTinh { get; set; }

            public string NgaySinh { get; set; }

            public string MaLop { get; set; }

            public string TenLop { get; set; }
        }
    }
}
