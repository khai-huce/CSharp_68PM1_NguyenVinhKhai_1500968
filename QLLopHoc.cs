using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QUANLYSV
{
    public partial class QLLopHoc : UserControl
    {
        private const int PageSize = 5;

        private string selectedClassId;
        private string currentKeyword = string.Empty;
        private int currentPage = 1;
        private int totalPages = 1;

        public QLLopHoc()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.QLLopHoc_Load);
        }

        private void QLLopHoc_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClassList(resetPage: true);
                ClearForm();
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

        private void LoadClassList(string keyword = null, bool resetPage = false)
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
                var query = from lop in db.LopHoc
                            select new
                            {
                                lop.MaLop,
                                lop.TenLop,
                                lop.GhiChu
                            };

                if (!string.IsNullOrWhiteSpace(currentKeyword))
                {
                    query = query.Where(lop => lop.MaLop.Contains(currentKeyword)
                                            || lop.TenLop.Contains(currentKeyword));
                }

                query = query.OrderBy(lop => lop.MaLop);

                int totalRecords = query.Count();
                totalPages = Math.Max(1, (int)Math.Ceiling(totalRecords / (double)PageSize));
                currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

                var pageRecords = query
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                dgvStdView.DataSource = pageRecords;
                label7.Text = string.Format("Trang {0}/{1} | {2} bản ghi", currentPage, totalPages, totalRecords);
                UpdatePagingButtons(totalRecords);
            }
        }

        private void btnAddStd_Click(object sender, EventArgs e)
        {
            string classId = txtStdId.Text.Trim();
            string className = txtName.Text.Trim();
            string notes = txtGhiChu.Text.Trim();

            if (string.IsNullOrWhiteSpace(classId))
            {
                MessageBox.Show("Vui lòng nhập mã lớp học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStdId.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(className))
            {
                MessageBox.Show("Vui lòng nhập tên lớp học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    if (db.LopHoc.Any(lop => lop.MaLop == classId))
                    {
                        MessageBox.Show("Mã lớp học đã tồn tại.", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtStdId.Focus();
                        return;
                    }

                    LopHoc newClass = new LopHoc
                    {
                        MaLop = classId,
                        TenLop = className,
                        GhiChu = string.IsNullOrWhiteSpace(notes) ? null : notes
                    };

                    db.LopHoc.InsertOnSubmit(newClass);
                    db.SubmitChanges();
                }

                LoadClassList();
                ClearForm();
                MessageBox.Show("Thêm lớp học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditStd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedClassId))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần sửa trong danh sách.", "Chưa chọn lớp học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string className = txtName.Text.Trim();
            string notes = txtGhiChu.Text.Trim();

            if (string.IsNullOrWhiteSpace(className))
            {
                MessageBox.Show("Vui lòng nhập tên lớp học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    LopHoc classObj = db.LopHoc.FirstOrDefault(lop => lop.MaLop == selectedClassId);

                    if (classObj == null)
                    {
                        MessageBox.Show("Lớp học cần sửa không còn tồn tại.", "Không tìm thấy dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadClassList();
                        ClearForm();
                        return;
                    }

                    classObj.TenLop = className;
                    classObj.GhiChu = string.IsNullOrWhiteSpace(notes) ? null : notes;
                    db.SubmitChanges();
                }

                LoadClassList();
                ClearForm();
                MessageBox.Show("Cập nhật lớp học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể cập nhật lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteStd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedClassId))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần xóa trong danh sách.", "Chưa chọn lớp học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc muốn xóa lớp học có mã " + selectedClassId + " không?",
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
                    LopHoc classObj = db.LopHoc.FirstOrDefault(lop => lop.MaLop == selectedClassId);

                    if (classObj == null)
                    {
                        MessageBox.Show("Lớp học cần xóa không còn tồn tại.", "Không tìm thấy dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadClassList();
                        ClearForm();
                        return;
                    }

                    if (db.SinhVien.Any(sv => sv.MaLop == selectedClassId))
                    {
                        MessageBox.Show("Không thể xóa lớp học này vì đang có sinh viên thuộc lớp.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    db.LopHoc.DeleteOnSubmit(classObj);
                    db.SubmitChanges();
                }

                LoadClassList();
                ClearForm();
                MessageBox.Show("Xóa lớp học thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string classId = Convert.ToString(dgvStdView.Rows[e.RowIndex].Cells["StdID"].Value);

            if (string.IsNullOrWhiteSpace(classId))
            {
                return;
            }

            try
            {
                using (DatabaseDataContext db = CreateDataContext())
                {
                    LopHoc classObj = db.LopHoc.FirstOrDefault(lop => lop.MaLop == classId);

                    if (classObj == null)
                    {
                        MessageBox.Show("Không tìm thấy lớp học đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadClassList();
                        ClearForm();
                        return;
                    }

                    selectedClassId = classObj.MaLop;
                    txtStdId.Text = classObj.MaLop;
                    txtStdId.ReadOnly = true;
                    txtName.Text = classObj.TenLop;
                    txtGhiChu.Text = classObj.GhiChu;
                    SetEditMode(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadClassList(txtFind.Text, resetPage: true);
            ClearForm();
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnFind_Click(sender, EventArgs.Empty);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            LoadClassList(string.Empty, resetPage: true);
            ClearForm();
        }

        private void btnFrist_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadClassList();
            ClearForm();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadClassList();
                ClearForm();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadClassList();
                ClearForm();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            LoadClassList();
            ClearForm();
        }

        private void ClearForm()
        {
            selectedClassId = null;
            txtStdId.Clear();
            txtStdId.ReadOnly = false;
            txtName.Clear();
            txtGhiChu.Clear();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}