using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Áp dụng giao diện dùng chung cho các form WinForms trong hệ thống.
    /// </summary>
    internal static class UiTheme
    {
        private const float EmphasisFontSize = 10F;

        /// <summary>
        /// Thiết lập màu nền, font chữ và duyệt toàn bộ control trên form để áp dụng style.
        /// </summary>
        public static void Apply(Form form)
        {
            form.BackColor = SystemColors.Control;
            // Cac form nay duoc nhung vao pnlContent; padding o day lam cat mat
            // cac control co bo cuc co dinh khi cua so nho hon 1200px.
            form.Padding = new Padding(0);

            foreach (Control control in form.Controls)
            {
                ApplyControl(control);
            }
        }

        /// <summary>
        /// Them nut tim kiem hien thi ngay ben phai o nhap tu khoa.
        /// </summary>
        public static void AddSearchButton(TextBox searchTextBox, Action searchAction)
        {
            if (searchTextBox?.Parent == null || searchAction == null)
            {
                return;
            }

            if (searchTextBox.Parent.Controls["btnTimKiem"] != null)
            {
                return;
            }

            Button searchButton = new Button
            {
                Name = "btnTimKiem",
                Text = "Tìm kiếm",
                Location = new Point(searchTextBox.Right + 10, Math.Max(0, searchTextBox.Top - 1)),
                Size = new Size(90, 35),
                Anchor = searchTextBox.Anchor
            };

            ApplyButton(searchButton);
            searchButton.Click += (_, _) =>
            {
                searchAction();
                searchTextBox.Focus();
            };

            searchTextBox.Parent.Controls.Add(searchButton);
            searchButton.BringToFront();
        }

        /// <summary>
        /// Đặt cụm ô nhập và nút tìm kiếm cạnh nút thêm hàng trên các form lập phiếu.
        /// Nếu vùng hiển thị quá hẹp, cụm tìm kiếm được giữ ở hàng trên để không bị cắt.
        /// </summary>
        public static void PlaceSearchControlsBesideAddButton(Panel parent, TextBox searchTextBox, Button addButton)
        {
            if (parent == null || searchTextBox == null || addButton?.Parent == null ||
                parent.Controls["btnTimKiem"] is not Button searchButton)
            {
                return;
            }

            Point fallbackTextLocation = searchTextBox.Location;

            void PlaceControls()
            {
                Point addLocation = parent.PointToClient(addButton.Parent.PointToScreen(addButton.Location));
                int preferredTextX = addLocation.X + addButton.Width + 10;
                int preferredTextY = addLocation.Y + Math.Max(0, (addButton.Height - searchTextBox.Height) / 2);
                int preferredButtonX = preferredTextX + searchTextBox.Width + 10;
                int preferredButtonY = addLocation.Y + Math.Max(0, (addButton.Height - searchButton.Height) / 2);
                int maxX = Math.Max(0, parent.ClientSize.Width - searchButton.Width - 10);
                int fallbackTextMaxX = Math.Max(0, parent.ClientSize.Width - searchTextBox.Width - 10);
                int fallbackTextX = Math.Min(Math.Max(0, fallbackTextLocation.X), fallbackTextMaxX);
                int fallbackButtonX = Math.Min(Math.Max(0, fallbackTextX + searchTextBox.Width + 10), maxX);

                if (preferredButtonX + searchButton.Width <= parent.ClientSize.Width - 10)
                {
                    searchTextBox.Location = new Point(preferredTextX, preferredTextY);
                    searchButton.Location = new Point(preferredButtonX, preferredButtonY);
                }
                else
                {
                    searchTextBox.Location = new Point(fallbackTextX, Math.Max(0, fallbackTextLocation.Y));
                    searchButton.Location = new Point(fallbackButtonX, Math.Max(0, searchTextBox.Top - 1));
                }

                searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                searchTextBox.BringToFront();
                searchButton.BringToFront();
            }

            PlaceControls();
            parent.Resize += (_, _) => PlaceControls();
        }

        /// <summary>
        /// Nhận diện loại control hiện tại và gọi hàm style phù hợp, sau đó xử lý tiếp các control con.
        /// </summary>
        private static void ApplyControl(Control control)
        {
            switch (control)
            {
                case Panel panel:
                    ApplyPanel(panel);
                    break;

                case GroupBox groupBox:
                    ApplyGroupBox(groupBox);
                    break;

                case Label label:
                    ApplyLabel(label);
                    break;

                case TextBox textBox:
                    ApplyTextBox(textBox);
                    break;

                case ComboBox comboBox:
                    ApplyComboBox(comboBox);
                    break;

                case Button button:
                    ApplyButton(button);
                    break;

                case DataGridView grid:
                    ApplyGrid(grid);
                    break;
            }

            foreach (Control child in control.Controls)
            {
                ApplyControl(child);
            }
        }

        /// <summary>
        /// Định dạng panel làm vùng chứa chính, thêm viền và padding cho các panel nhập liệu/điều khiển.
        /// </summary>
        private static void ApplyPanel(Panel panel)
        {
            if (panel.Name == "pnlBottom")
            {
                return;
            }

            panel.BackColor = SystemColors.Control;

            if (panel.Name == "pnlTopControls" || panel.Name == "pnlTop")
            {
                panel.BorderStyle = BorderStyle.None;
                panel.Padding = Padding.Empty;
            }
        }

        /// <summary>
        /// Định dạng GroupBox dùng cho các khối nhập thông tin.
        /// </summary>
        private static void ApplyGroupBox(GroupBox groupBox)
        {
            groupBox.ForeColor = SystemColors.ControlText;
            groupBox.BackColor = SystemColors.Control;
            groupBox.Font = SystemFonts.MessageBoxFont;
        }

        /// <summary>
        /// Định dạng Label; các label tiêu đề, tìm kiếm và tổng tiền có màu/font riêng.
        /// </summary>
        private static void ApplyLabel(Label label)
        {
            // Title labels keep the font, color, and sizing configured in each form's Designer.
            if (string.Equals(label.Tag?.ToString(), "Title", StringComparison.OrdinalIgnoreCase) ||
                label.Name.StartsWith("lblTitle", StringComparison.OrdinalIgnoreCase) ||
                label.Name.StartsWith("lblTieuDe", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            label.ForeColor = SystemColors.ControlText;
            label.Font = SystemFonts.MessageBoxFont;

            if (label.Name == "lblTimKiem")
            {
                label.Visible = false;
            }
            else if (label.Name == "lblLichSuTitle")
            {
                label.BackColor = SystemColors.Control;
                label.Font = new Font(SystemFonts.MessageBoxFont, FontStyle.Bold);
                label.Padding = Padding.Empty;
            }
            else if (label.Name == "lblTongTien")
            {
                label.Font = new Font(SystemFonts.MessageBoxFont.FontFamily, EmphasisFontSize, FontStyle.Bold);
            }
        }

        /// <summary>
        /// Định dạng TextBox theo kiểu ô nhập liệu đồng nhất trên toàn ứng dụng.
        /// </summary>
        private static void ApplyTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.BackColor = SystemColors.Window;
            textBox.ForeColor = SystemColors.WindowText;
            textBox.Font = SystemFonts.MessageBoxFont;

            if (string.Equals(textBox.Name, "txtTimKiem", StringComparison.OrdinalIgnoreCase))
            {
                textBox.PlaceholderText = "Nhập từ khóa tìm kiếm...";
            }

            if (textBox.Name is "txtGiaNhap" or "txtGiaBan" or "txtSoLuong" or "txtDonGia")
            {
                textBox.TextAlign = HorizontalAlignment.Right;
            }
        }

        /// <summary>
        /// Định dạng ComboBox dùng để chọn danh mục như hàng hóa, nhân viên, khách hàng.
        /// </summary>
        private static void ApplyComboBox(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Standard;
            comboBox.BackColor = SystemColors.Window;
            comboBox.ForeColor = SystemColors.WindowText;
            comboBox.Font = SystemFonts.MessageBoxFont;
            comboBox.IntegralHeight = false;
            comboBox.DropDownHeight = 320;
        }

        /// <summary>
        /// Định dạng Button theo tên chức năng như thêm, sửa, xóa, lưu phiếu, xuất Excel/PDF.
        /// </summary>
        private static void ApplyButton(Button button)
        {
            if (button.Name is "btnLuuPhieu" or "btnExcel" or "btnPdf")
            {
                ApplyDocumentActionButton(button);
                return;
            }

            button.FlatStyle = FlatStyle.Standard;
            button.UseVisualStyleBackColor = true;
            button.BackColor = SystemColors.Control;
            button.ForeColor = SystemColors.ControlText;
            button.Cursor = Cursors.Default;
            button.Font = SystemFonts.MessageBoxFont;

            string action = GetButtonAction(button.Name);
            switch (action)
            {
                case "add":
                    button.Text = "+ Thêm mới";
                    SetSolidButton(button, Color.FromArgb(37, 99, 235));
                    break;

                case "edit":
                    SetSolidButton(button, Color.FromArgb(245, 158, 11));
                    break;

                case "delete":
                    SetSolidButton(button, Color.FromArgb(220, 38, 38));
                    break;

                case "refresh":
                    button.Text = "Làm mới";
                    button.BackColor = Color.White;
                    button.ForeColor = Color.FromArgb(51, 65, 85);
                    button.FlatAppearance.BorderSize = 1;
                    break;

                case "search":
                    button.Text = "Tìm kiếm";
                    SetSolidButton(button, Color.FromArgb(37, 99, 235));
                    break;

                case "add-item":
                    button.Text = "+ Thêm hàng";
                    SetSolidButton(button, Color.FromArgb(37, 99, 235));
                    break;

                case "save":
                    SetSolidButton(button, Color.FromArgb(22, 163, 74));
                    break;

                case "excel":
                    SetSolidButton(button, Color.FromArgb(21, 128, 61));
                    break;

                case "pdf":
                    SetSolidButton(button, Color.FromArgb(185, 28, 28));
                    break;

                default:
                    if (button.BackColor == SystemColors.Control || button.BackColor == Color.Empty)
                    {
                        button.BackColor = SystemColors.Control;
                        button.ForeColor = SystemColors.ControlText;
                        button.FlatAppearance.BorderSize = 1;
                    }
                    break;
            }
        }

        private static void ApplyDocumentActionButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.UseVisualStyleBackColor = false;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Padding = new Padding(10, 0, 10, 0);
            button.FlatAppearance.BorderSize = 0;

            if (button.Name == "btnLuuPhieu")
            {
                SetSolidButton(button, Color.FromArgb(37, 99, 235));
            }
            else if (button.Name == "btnExcel")
            {
                SetSolidButton(button, Color.FromArgb(21, 128, 61));
            }
            else
            {
                SetSolidButton(button, Color.FromArgb(185, 28, 28));
            }
        }

        /// <summary>
        /// Chuẩn hóa tên nút thành nhóm thao tác để các màn hình dùng tên control khác nhau
        /// vẫn nhận cùng một kiểu hiển thị.
        /// </summary>
        private static string GetButtonAction(string buttonName)
        {
            string name = string.Empty;

            if (name.Equals("btnThemMon", StringComparison.OrdinalIgnoreCase))
            {
                return "add-item";
            }

            if (name.Equals("btnTimKiem", StringComparison.OrdinalIgnoreCase))
            {
                return "search";
            }

            if (name.Equals("btnLuuPhieu", StringComparison.OrdinalIgnoreCase))
            {
                return "save";
            }

            if (name.Equals("btnExcel", StringComparison.OrdinalIgnoreCase))
            {
                return "excel";
            }

            if (name.Equals("btnPdf", StringComparison.OrdinalIgnoreCase))
            {
                return "pdf";
            }

            if (name.Equals("btnThem", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnThem", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnDanhMucThem", StringComparison.OrdinalIgnoreCase))
            {
                return "add";
            }

            if (name.Equals("btnSua", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnSua", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnDanhMucSua", StringComparison.OrdinalIgnoreCase))
            {
                return "edit";
            }

            if (name.Equals("btnXoa", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnXoa", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnDanhMucXoa", StringComparison.OrdinalIgnoreCase))
            {
                return "delete";
            }

            if (name.Equals("btnLamMoi", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnLamMoi", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("btnDanhMucLamMoi", StringComparison.OrdinalIgnoreCase))
            {
                return "refresh";
            }

            return string.Empty;
        }

        /// <summary>
        /// Áp dụng kiểu nút màu đặc với chữ trắng cho các thao tác chính.
        /// </summary>
        private static void SetSolidButton(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.08F);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.08F);
        }

        /// <summary>
        /// Định dạng DataGridView để hiển thị danh sách dữ liệu rõ ràng và chỉ đọc.
        /// </summary>
        private static void ApplyGrid(DataGridView grid)
        {
            grid.BackgroundColor = SystemColors.Window;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            grid.GridColor = SystemColors.ControlLight;
            grid.RowHeadersVisible = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            grid.EnableHeadersVisualStyles = true;
            grid.RowTemplate.Height = 28;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            grid.ScrollBars = ScrollBars.Both;
            grid.DefaultCellStyle.NullValue = string.Empty;

            grid.ColumnHeadersDefaultCellStyle.Font = new Font(SystemFonts.MessageBoxFont, FontStyle.Bold);
            grid.ColumnHeadersHeight = 28;

            grid.DefaultCellStyle.BackColor = SystemColors.Window;
            grid.DefaultCellStyle.ForeColor = SystemColors.WindowText;
            grid.DefaultCellStyle.Font = SystemFonts.MessageBoxFont;
            grid.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            grid.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            grid.DefaultCellStyle.Padding = Padding.Empty;

            grid.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.Window;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.WindowText;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            grid.AlternatingRowsDefaultCellStyle.Padding = Padding.Empty;
        }

        /// <summary>
        /// Đặt tiêu đề tiếng Việt cho các cột tự sinh từ DataTable.
        /// </summary>
        public static void SetGridHeaders(DataGridView grid, params string[] headers)
        {
            if (grid == null || headers == null)
            {
                return;
            }

            int count = Math.Min(grid.Columns.Count, headers.Length);
            for (int index = 0; index < count; index++)
            {
                grid.Columns[index].HeaderText = headers[index];
            }
        }

        /// <summary>
        /// Escape gia tri nguoi dung nhap truoc khi dua vao DataView.RowFilter.
        /// Neu khong escape, cac ky tu nhu [, ], % hoac * co the lam loi tim kiem.
        /// </summary>
        public static string EscapeRowFilterValue(string value)
        {
            StringBuilder escaped = new StringBuilder();
            foreach (char character in value ?? string.Empty)
            {
                switch (character)
                {
                    case '\'':
                        escaped.Append("''");
                        break;
                    case '[':
                        escaped.Append("[[]");
                        break;
                    case ']':
                        escaped.Append("[]]");
                        break;
                    case '%':
                        escaped.Append("[%]");
                        break;
                    case '*':
                        escaped.Append("[*]");
                        break;
                    default:
                        escaped.Append(character);
                        break;
                }
            }

            return escaped.ToString();
        }

        /// <summary>
        /// Gan DataTable vao ComboBox an toan, ke ca khi API tra ve bang rong/khong co cot.
        /// </summary>
        public static bool BindComboBox(
            ComboBox comboBox,
            DataTable dataSource,
            int displayColumnIndex,
            int valueColumnIndex)
        {
            comboBox.DataSource = null;
            comboBox.DisplayMember = string.Empty;
            comboBox.ValueMember = string.Empty;
            comboBox.Enabled = false;

            if (dataSource == null
                || dataSource.Columns.Count <= Math.Max(displayColumnIndex, valueColumnIndex))
            {
                return false;
            }

            comboBox.DataSource = dataSource;
            comboBox.DisplayMember = dataSource.Columns[displayColumnIndex].ColumnName;
            comboBox.ValueMember = dataSource.Columns[valueColumnIndex].ColumnName;
            comboBox.Enabled = dataSource.Rows.Count > 0;
            return comboBox.Enabled;
        }
    }
}
