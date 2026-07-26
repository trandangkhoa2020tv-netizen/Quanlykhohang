using Npgsql;

namespace QuanLyKhoHang.Data
{
    /// <summary>
    /// Các tác vụ bảo trì database cần chạy khi API khởi động.
    /// </summary>
    public static class DatabaseMaintenance
    {
        /// <summary>
        /// Tao hoac cap nhat cac cot va bang bat buoc khi API khoi dong.
        /// </summary>
        public static void EnsureRuntimeSchema(bool seedDemoData = false)
        {
            using NpgsqlConnection connection = DbConnection.GetConnection();
            connection.Open();

            ExecuteNonQuery(connection, RuntimeSchemaSql);
            if (seedDemoData)
            {
                SeedInitialDataIfDatabaseIsEmpty(connection);
            }

            ExecuteNonQuery(connection, @"
                ALTER TABLE IF EXISTS hanghoa ADD COLUMN IF NOT EXISTS is_deleted boolean DEFAULT false;
                UPDATE hanghoa SET is_deleted = false WHERE is_deleted IS NULL;
                ALTER TABLE IF EXISTS hanghoa ALTER COLUMN is_deleted SET DEFAULT false;
                ALTER TABLE IF EXISTS hanghoa ALTER COLUMN is_deleted SET NOT NULL;");

            ExecuteNonQuery(connection, @"
                ALTER TABLE IF EXISTS nhanvien ADD COLUMN IF NOT EXISTS is_deleted boolean DEFAULT false;
                UPDATE nhanvien SET is_deleted = false WHERE is_deleted IS NULL;
                ALTER TABLE IF EXISTS nhanvien ALTER COLUMN is_deleted SET DEFAULT false;
                ALTER TABLE IF EXISTS nhanvien ALTER COLUMN is_deleted SET NOT NULL;");

            ExecuteNonQuery(connection, @"
                CREATE TABLE IF NOT EXISTS auditlog
                (
                    ma_audit serial PRIMARY KEY,
                    ten_taikhoan varchar(100) NOT NULL,
                    vai_tro varchar(50) NOT NULL,
                    hanh_dong varchar(50) NOT NULL,
                    bang_du_lieu varchar(100) NOT NULL,
                    ma_ban_ghi integer NULL,
                    noi_dung text NULL,
                    ip_address varchar(100) NULL,
                    thoi_gian timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP
                );");
        }

        private const string RuntimeSchemaSql = @"
            CREATE TABLE IF NOT EXISTS loaihang
            (
                ma_loaihang serial PRIMARY KEY,
                ten_loaihang varchar(100) NOT NULL,
                ghi_chu text
            );

            CREATE TABLE IF NOT EXISTS nhacungcap
            (
                ma_nhacungcap serial PRIMARY KEY,
                ten_nhacungcap varchar(255) NOT NULL,
                dia_chi_ncc varchar(500),
                so_dien_thoai varchar(20),
                email varchar(100),
                ghi_chu text
            );

            CREATE TABLE IF NOT EXISTS khachhang
            (
                ma_khachhang serial PRIMARY KEY,
                ten_khachhang varchar(255) NOT NULL,
                dia_chi_kh varchar(500),
                so_dien_thoai varchar(20),
                email varchar(100),
                ghi_chu text
            );

            CREATE TABLE IF NOT EXISTS nhanvien
            (
                ma_nhanvien serial PRIMARY KEY,
                ten_nhanvien varchar(255) NOT NULL,
                dia_chi_nv varchar(500),
                so_dien_thoai varchar(20),
                email varchar(100),
                ngay_sinh date,
                chuc_vu varchar(100),
                ghi_chu text,
                is_deleted boolean NOT NULL DEFAULT false
            );

            CREATE TABLE IF NOT EXISTS hanghoa
            (
                ma_hanghoa serial PRIMARY KEY,
                ten_hanghoa varchar(255) NOT NULL,
                ma_loaihang integer NOT NULL,
                ma_nhacungcap integer NOT NULL,
                gia_nhap numeric(18, 2) NOT NULL DEFAULT 0,
                gia_ban numeric(18, 2) NOT NULL DEFAULT 0,
                so_luong_ton integer NOT NULL DEFAULT 0,
                don_vi_tinh varchar(50) NOT NULL DEFAULT '',
                ghi_chu text,
                is_deleted boolean NOT NULL DEFAULT false,
                CONSTRAINT fk_hanghoa_loaihang FOREIGN KEY (ma_loaihang) REFERENCES loaihang(ma_loaihang),
                CONSTRAINT fk_hanghoa_nhacungcap FOREIGN KEY (ma_nhacungcap) REFERENCES nhacungcap(ma_nhacungcap),
                CONSTRAINT ck_hanghoa_gia_nhap_khong_am CHECK (gia_nhap >= 0),
                CONSTRAINT ck_hanghoa_gia_ban_khong_am CHECK (gia_ban >= 0),
                CONSTRAINT ck_hanghoa_ton_khong_am CHECK (so_luong_ton >= 0)
            );

            CREATE TABLE IF NOT EXISTS taikhoan
            (
                ma_taikhoan serial PRIMARY KEY,
                ma_nhanvien integer NOT NULL,
                ten_taikhoan varchar(50) NOT NULL UNIQUE,
                mat_khau varchar(255) NOT NULL,
                vai_tro varchar(50) NOT NULL DEFAULT 'NhanVien',
                trang_thai boolean NOT NULL DEFAULT true,
                CONSTRAINT fk_taikhoan_nhanvien FOREIGN KEY (ma_nhanvien) REFERENCES nhanvien(ma_nhanvien),
                CONSTRAINT ck_taikhoan_mat_khau_hash CHECK (mat_khau LIKE 'pbkdf2$%')
            );

            CREATE TABLE IF NOT EXISTS phieunhap
            (
                ma_phieunhap serial PRIMARY KEY,
                ma_nhacungcap integer NOT NULL,
                ma_nhanvien integer NOT NULL,
                ngay_nhap date NOT NULL DEFAULT CURRENT_DATE,
                tong_tien numeric(18, 2) NOT NULL DEFAULT 0,
                ghi_chu text,
                CONSTRAINT fk_phieunhap_nhacungcap FOREIGN KEY (ma_nhacungcap) REFERENCES nhacungcap(ma_nhacungcap),
                CONSTRAINT fk_phieunhap_nhanvien FOREIGN KEY (ma_nhanvien) REFERENCES nhanvien(ma_nhanvien),
                CONSTRAINT ck_phieunhap_tong_tien_khong_am CHECK (tong_tien >= 0)
            );

            CREATE TABLE IF NOT EXISTS chitietphieunhap
            (
                ma_chitiet serial PRIMARY KEY,
                ma_phieunhap integer NOT NULL,
                ma_hanghoa integer NOT NULL,
                so_luong integer NOT NULL,
                don_gia_nhap numeric(18, 2) NOT NULL DEFAULT 0,
                thanh_tien numeric(18, 2) NOT NULL DEFAULT 0,
                CONSTRAINT fk_ctpn_phieunhap FOREIGN KEY (ma_phieunhap) REFERENCES phieunhap(ma_phieunhap) ON DELETE CASCADE,
                CONSTRAINT fk_ctpn_hanghoa FOREIGN KEY (ma_hanghoa) REFERENCES hanghoa(ma_hanghoa),
                CONSTRAINT ck_ctpn_so_luong_duong CHECK (so_luong > 0),
                CONSTRAINT ck_ctpn_don_gia_khong_am CHECK (don_gia_nhap >= 0),
                CONSTRAINT ck_ctpn_thanh_tien_khong_am CHECK (thanh_tien >= 0)
            );

            CREATE TABLE IF NOT EXISTS phieuxuat
            (
                ma_phieuxuat serial PRIMARY KEY,
                ma_khachhang integer NOT NULL,
                ma_nhanvien integer NOT NULL,
                ngay_xuat date NOT NULL DEFAULT CURRENT_DATE,
                tong_tien numeric(18, 2) NOT NULL DEFAULT 0,
                ghi_chu text,
                CONSTRAINT fk_phieuxuat_khachhang FOREIGN KEY (ma_khachhang) REFERENCES khachhang(ma_khachhang),
                CONSTRAINT fk_phieuxuat_nhanvien FOREIGN KEY (ma_nhanvien) REFERENCES nhanvien(ma_nhanvien),
                CONSTRAINT ck_phieuxuat_tong_tien_khong_am CHECK (tong_tien >= 0)
            );

            CREATE TABLE IF NOT EXISTS chitietphieuxuat
            (
                ma_chitiet serial PRIMARY KEY,
                ma_phieuxuat integer NOT NULL,
                ma_hanghoa integer NOT NULL,
                so_luong integer NOT NULL,
                don_gia_xuat numeric(18, 2) NOT NULL DEFAULT 0,
                thanh_tien numeric(18, 2) NOT NULL DEFAULT 0,
                CONSTRAINT fk_ctpx_phieuxuat FOREIGN KEY (ma_phieuxuat) REFERENCES phieuxuat(ma_phieuxuat) ON DELETE CASCADE,
                CONSTRAINT fk_ctpx_hanghoa FOREIGN KEY (ma_hanghoa) REFERENCES hanghoa(ma_hanghoa),
                CONSTRAINT ck_ctpx_so_luong_duong CHECK (so_luong > 0),
                CONSTRAINT ck_ctpx_don_gia_khong_am CHECK (don_gia_xuat >= 0),
                CONSTRAINT ck_ctpx_thanh_tien_khong_am CHECK (thanh_tien >= 0)
            );

            CREATE TABLE IF NOT EXISTS auditlog
            (
                ma_auditlog serial PRIMARY KEY,
                ten_taikhoan varchar(50),
                vai_tro varchar(50),
                hanh_dong varchar(50) NOT NULL,
                bang_du_lieu varchar(100) NOT NULL,
                ma_ban_ghi integer,
                noi_dung text,
                ip_address varchar(64),
                thoi_gian timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
            );

            ALTER TABLE taikhoan ADD COLUMN IF NOT EXISTS trang_thai boolean NOT NULL DEFAULT true;
            ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS is_deleted boolean NOT NULL DEFAULT false;
            ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS is_deleted boolean NOT NULL DEFAULT false;

            CREATE INDEX IF NOT EXISTS idx_hanghoa_ten ON hanghoa(ten_hanghoa);
            CREATE INDEX IF NOT EXISTS idx_hanghoa_loai ON hanghoa(ma_loaihang);
            CREATE INDEX IF NOT EXISTS idx_hanghoa_nhacungcap ON hanghoa(ma_nhacungcap);
            CREATE INDEX IF NOT EXISTS idx_nhanvien_active ON nhanvien(is_deleted);
            CREATE INDEX IF NOT EXISTS idx_phieunhap_ngay ON phieunhap(ngay_nhap);
            CREATE INDEX IF NOT EXISTS idx_phieuxuat_ngay ON phieuxuat(ngay_xuat);
            CREATE INDEX IF NOT EXISTS idx_auditlog_thoi_gian ON auditlog(thoi_gian DESC);
            CREATE INDEX IF NOT EXISTS idx_auditlog_bang_ban_ghi ON auditlog(bang_du_lieu, ma_ban_ghi);";

        /// <summary>
        /// Them du lieu nghiep vu mau khi duoc bat ro rang trong moi truong Development.
        /// </summary>
        private static void SeedInitialDataIfDatabaseIsEmpty(NpgsqlConnection connection)
        {
            if (HasAnyRows(connection, "taikhoan")
                || HasAnyRows(connection, "hanghoa")
                || HasAnyRows(connection, "nhanvien")
                || HasAnyRows(connection, "loaihang")
                || HasAnyRows(connection, "nhacungcap")
                || HasAnyRows(connection, "khachhang"))
            {
                return;
            }

            ExecuteNonQuery(connection, InitialDataSql);
        }

        private const string InitialDataSql = @"
            INSERT INTO loaihang (ma_loaihang, ten_loaihang, ghi_chu) VALUES
                (1, 'Dien tu', 'Hang dien tu va thiet bi cong nghe'),
                (2, 'Quan ao', 'Hang thoi trang'),
                (3, 'Thuc pham', 'Hang tieu dung thuc pham');

            INSERT INTO nhacungcap (ma_nhacungcap, ten_nhacungcap, dia_chi_ncc, so_dien_thoai, email, ghi_chu) VALUES
                (1, 'Cong ty A', '123 Duong ABC', '0123456789', 'a@company.com', 'Nha cung cap dien tu'),
                (2, 'Cong ty B', '456 Duong DEF', '0987654321', 'b@company.com', 'Nha cung cap thoi trang');

            INSERT INTO khachhang (ma_khachhang, ten_khachhang, dia_chi_kh, so_dien_thoai, email, ghi_chu) VALUES
                (1, 'Nguyen Van A', '789 Duong GHI', '0111111111', 'nguyenvana@example.com', 'Khach hang le'),
                (2, 'Tran Thi B', '101 Duong JKL', '0222222222', 'tranthib@example.com', 'Khach hang than thiet');

            INSERT INTO nhanvien (ma_nhanvien, ten_nhanvien, dia_chi_nv, so_dien_thoai, email, ngay_sinh, chuc_vu, ghi_chu) VALUES
                (1, 'Quan tri vien', 'Van phong', '0900000001', 'admin@example.com', DATE '1990-01-01', 'Admin', 'Tai khoan quan tri'),
                (2, 'Nhan vien kho', 'Kho hang', '0900000002', 'kho@example.com', DATE '1995-05-20', 'Thu kho', 'Phu trach nhap kho'),
                (3, 'Nhan vien ban hang', 'Quay ban hang', '0900000003', 'banhang@example.com', DATE '1996-06-15', 'Ban hang', 'Phu trach xuat kho');

            INSERT INTO hanghoa (ma_hanghoa, ten_hanghoa, ma_loaihang, ma_nhacungcap, gia_nhap, gia_ban, so_luong_ton, don_vi_tinh, ghi_chu) VALUES
                (1, 'Laptop Dell', 1, 1, 5000000, 6500000, 10, 'Cai', 'Laptop van phong'),
                (2, 'Ao thun nam', 2, 2, 50000, 80000, 50, 'Cai', 'Ao thun co ban'),
                (3, 'Mi goi', 3, 1, 3000, 5000, 200, 'Goi', 'Thuc pham nhanh');";

        /// <summary>
        /// Đồng bộ các sequence tự tăng với dữ liệu hiện có để tránh trùng khóa sau khi import dữ liệu mẫu.
        /// </summary>
        public static void EnsureSerialSequences()
        {
            using NpgsqlConnection connection = DbConnection.GetConnection();
            connection.Open();

            foreach ((string TableName, string ColumnName) column in GetSerialColumns(connection))
            {
                try
                {
                    using NpgsqlCommand command = new NpgsqlCommand(BuildSetValSql(column.TableName, column.ColumnName), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Khong the dong bo sequence {column.TableName}.{column.ColumnName}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Thuc thi mot lenh SQL khong tra du lieu tren ket noi dang mo.
        /// </summary>
        private static void ExecuteNonQuery(NpgsqlConnection connection, string sql)
        {
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Kiem tra bang da co du lieu hay chua.
        /// </summary>
        private static bool HasAnyRows(NpgsqlConnection connection, string tableName)
        {
            using NpgsqlCommand command = new NpgsqlCommand(
                $"SELECT EXISTS (SELECT 1 FROM {QuoteIdentifier(tableName)} LIMIT 1)",
                connection);
            return Convert.ToBoolean(command.ExecuteScalar());
        }

        /// <summary>
        /// Lấy danh sách các cột dùng sequence nextval trong schema public.
        /// </summary>
        private static List<(string TableName, string ColumnName)> GetSerialColumns(NpgsqlConnection connection)
        {
            const string sql = @"SELECT table_name, column_name
                                 FROM information_schema.columns
                                 WHERE table_schema = 'public'
                                   AND column_default LIKE 'nextval(%'
                                 ORDER BY table_name, ordinal_position";

            List<(string TableName, string ColumnName)> columns = new List<(string TableName, string ColumnName)>();
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                columns.Add((reader.GetString(0), reader.GetString(1)));
            }

            return columns;
        }

        /// <summary>
        /// Tạo câu SQL setval cho một sequence dựa trên giá trị lớn nhất hiện có của cột khóa.
        /// </summary>
        private static string BuildSetValSql(string tableName, string columnName)
        {
            return $@"SELECT setval(
                        pg_get_serial_sequence('public.{EscapeLiteral(tableName)}', '{EscapeLiteral(columnName)}'),
                        GREATEST(COALESCE((SELECT MAX({QuoteIdentifier(columnName)}) FROM {QuoteIdentifier(tableName)}), 0) + 1, 1),
                        false
                      );";
        }

        /// <summary>
        /// Bao tên bảng/cột bằng dấu nháy kép để dùng an toàn trong SQL identifier.
        /// </summary>
        private static string QuoteIdentifier(string value)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        /// <summary>
        /// Escape dấu nháy đơn trong chuỗi literal SQL.
        /// </summary>
        private static string EscapeLiteral(string value)
        {
            return value.Replace("'", "''");
        }
    }
}
