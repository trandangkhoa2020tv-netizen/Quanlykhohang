using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhoHang.ApiClients;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form đăng nhập hệ thống.
    /// Form này kiểm tra tài khoản/mật khẩu, lưu session và mở FrmMain khi đăng nhập thành công.
    /// </summary>
    public partial class FrmDangNhap : Form
    {
        private readonly AuthApiClient _authApiClient;

        /// <summary>
        /// Khởi tạo form đăng nhập, áp dụng giao diện riêng và chuẩn bị client xác thực.
        /// </summary>
        public FrmDangNhap()
        {
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            ApplyLoginTheme();
            _authApiClient = new AuthApiClient();
        }

        /// <summary>
        /// Áp dụng màu sắc, font chữ và kiểu nút cho màn hình đăng nhập.
        /// </summary>
        private void ApplyLoginTheme()
        {
            BackColor = Color.FromArgb(248, 250, 252);
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            pnlCard.BackColor = Color.White;
            pnlCard.BorderStyle = BorderStyle.FixedSingle;

            lblTitle.ForeColor = Color.FromArgb(37, 99, 235);
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            StyleTextBox(txtUsername);
            StyleTextBox(txtPassword);

            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.BackColor = Color.FromArgb(229, 231, 235);
            btnExit.ForeColor = Color.FromArgb(51, 65, 85);
            btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExit.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Định dạng ô nhập tài khoản/mật khẩu theo giao diện đăng nhập.
        /// </summary>
        private static void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.FromArgb(15, 23, 42);
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        /// <summary>
        /// Xử lý nút đăng nhập.
        /// Luồng xử lý: đọc input, validate rỗng, kiểm tra database, lưu UserSession, mở màn hình chính.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            // Mat khau la du lieu nguyen ven; khong cat khoang trang dau/cuoi.
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                string vaiTro = _authApiClient.CheckLogin(username, password);

                if (!string.IsNullOrEmpty(vaiTro))
                {
                    // Lưu thông tin phiên để FrmMain biết ai đang đăng nhập và có quyền gì.
                    UserSession.VaiTro = vaiTro;
                    UserSession.TenTaiKhoan = username;

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FrmMain frmMain = new FrmMain();
                    Hide();
                    frmMain.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối API: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thoát toàn bộ ứng dụng khi người dùng xác nhận.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
