-- ============================================================
-- Dong bo database PostgreSQL da ton tai voi code hien tai.
-- Dung file nay khi database da co bang roi, khong muon tao lai tu dau.
-- Ket noi vao database "quanlyhanghoa" roi chay toan bo file.
-- ============================================================

-- Bang taikhoan: code dang nhap can ten_taikhoan, mat_khau, vai_tro, trang_thai.
ALTER TABLE taikhoan ADD COLUMN IF NOT EXISTS trang_thai boolean NOT NULL DEFAULT true;
ALTER TABLE taikhoan ADD COLUMN IF NOT EXISTS vai_tro varchar(50) NOT NULL DEFAULT 'NhanVien';
ALTER TABLE taikhoan ALTER COLUMN mat_khau TYPE varchar(255);

-- Bang hanghoa: code quan ly hang hoa va nhap/xuat kho can cac cot nay.
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS gia_nhap numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS gia_ban numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS so_luong_ton integer NOT NULL DEFAULT 0;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS don_vi_tinh varchar(50);
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS ghi_chu text;
ALTER TABLE hanghoa ADD COLUMN IF NOT EXISTS is_deleted boolean NOT NULL DEFAULT false;

UPDATE hanghoa SET gia_nhap = 0 WHERE gia_nhap IS NULL OR gia_nhap < 0;
UPDATE hanghoa SET gia_ban = 0 WHERE gia_ban IS NULL OR gia_ban < 0;
UPDATE hanghoa SET so_luong_ton = 0 WHERE so_luong_ton IS NULL OR so_luong_ton < 0;
UPDATE hanghoa SET don_vi_tinh = '' WHERE don_vi_tinh IS NULL;
UPDATE hanghoa SET is_deleted = false WHERE is_deleted IS NULL;

ALTER TABLE hanghoa ALTER COLUMN gia_nhap SET DEFAULT 0;
ALTER TABLE hanghoa ALTER COLUMN gia_nhap SET NOT NULL;
ALTER TABLE hanghoa ALTER COLUMN gia_ban SET DEFAULT 0;
ALTER TABLE hanghoa ALTER COLUMN gia_ban SET NOT NULL;
ALTER TABLE hanghoa ALTER COLUMN so_luong_ton SET DEFAULT 0;
ALTER TABLE hanghoa ALTER COLUMN so_luong_ton SET NOT NULL;
ALTER TABLE hanghoa ALTER COLUMN don_vi_tinh SET DEFAULT '';
ALTER TABLE hanghoa ALTER COLUMN don_vi_tinh SET NOT NULL;
ALTER TABLE hanghoa ALTER COLUMN is_deleted SET DEFAULT false;
ALTER TABLE hanghoa ALTER COLUMN is_deleted SET NOT NULL;

-- Bang loaihang.
ALTER TABLE loaihang ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang nhacungcap.
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS dia_chi_ncc varchar(500);
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS so_dien_thoai varchar(20);
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS email varchar(100);
ALTER TABLE nhacungcap ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang khachhang.
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS dia_chi_kh varchar(500);
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS so_dien_thoai varchar(20);
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS email varchar(100);
ALTER TABLE khachhang ADD COLUMN IF NOT EXISTS ghi_chu text;

-- Bang nhanvien.
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS dia_chi_nv varchar(500);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS so_dien_thoai varchar(20);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS email varchar(100);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS ngay_sinh date;
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS chuc_vu varchar(100);
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS ghi_chu text;
ALTER TABLE nhanvien ADD COLUMN IF NOT EXISTS is_deleted boolean NOT NULL DEFAULT false;
UPDATE nhanvien SET is_deleted = false WHERE is_deleted IS NULL;
ALTER TABLE nhanvien ALTER COLUMN is_deleted SET DEFAULT false;
ALTER TABLE nhanvien ALTER COLUMN is_deleted SET NOT NULL;

-- Bang phieunhap.
ALTER TABLE phieunhap ADD COLUMN IF NOT EXISTS ngay_nhap date NOT NULL DEFAULT CURRENT_DATE;
ALTER TABLE phieunhap ADD COLUMN IF NOT EXISTS tong_tien numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE phieunhap ADD COLUMN IF NOT EXISTS ghi_chu text;
UPDATE phieunhap SET tong_tien = 0 WHERE tong_tien IS NULL OR tong_tien < 0;
ALTER TABLE phieunhap ALTER COLUMN tong_tien SET DEFAULT 0;
ALTER TABLE phieunhap ALTER COLUMN tong_tien SET NOT NULL;

-- Bang phieuxuat.
ALTER TABLE phieuxuat ADD COLUMN IF NOT EXISTS ngay_xuat date NOT NULL DEFAULT CURRENT_DATE;
ALTER TABLE phieuxuat ADD COLUMN IF NOT EXISTS tong_tien numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE phieuxuat ADD COLUMN IF NOT EXISTS ghi_chu text;
UPDATE phieuxuat SET tong_tien = 0 WHERE tong_tien IS NULL OR tong_tien < 0;
ALTER TABLE phieuxuat ALTER COLUMN tong_tien SET DEFAULT 0;
ALTER TABLE phieuxuat ALTER COLUMN tong_tien SET NOT NULL;

-- Bang chitietphieunhap: code hien tai dung so_luong, don_gia_nhap, thanh_tien.
ALTER TABLE chitietphieunhap ADD COLUMN IF NOT EXISTS so_luong integer NOT NULL DEFAULT 1;
ALTER TABLE chitietphieunhap ADD COLUMN IF NOT EXISTS don_gia_nhap numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE chitietphieunhap ADD COLUMN IF NOT EXISTS thanh_tien numeric(18, 2) NOT NULL DEFAULT 0;
UPDATE chitietphieunhap SET so_luong = 1 WHERE so_luong IS NULL OR so_luong <= 0;
UPDATE chitietphieunhap SET don_gia_nhap = 0 WHERE don_gia_nhap IS NULL OR don_gia_nhap < 0;
UPDATE chitietphieunhap SET thanh_tien = so_luong * don_gia_nhap WHERE thanh_tien IS NULL OR thanh_tien < 0;
ALTER TABLE chitietphieunhap ALTER COLUMN so_luong SET DEFAULT 1;
ALTER TABLE chitietphieunhap ALTER COLUMN so_luong SET NOT NULL;
ALTER TABLE chitietphieunhap ALTER COLUMN don_gia_nhap SET DEFAULT 0;
ALTER TABLE chitietphieunhap ALTER COLUMN don_gia_nhap SET NOT NULL;
ALTER TABLE chitietphieunhap ALTER COLUMN thanh_tien SET DEFAULT 0;
ALTER TABLE chitietphieunhap ALTER COLUMN thanh_tien SET NOT NULL;

-- Bang chitietphieuxuat: code hien tai dung so_luong, don_gia_xuat, thanh_tien.
ALTER TABLE chitietphieuxuat ADD COLUMN IF NOT EXISTS so_luong integer NOT NULL DEFAULT 1;
ALTER TABLE chitietphieuxuat ADD COLUMN IF NOT EXISTS don_gia_xuat numeric(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE chitietphieuxuat ADD COLUMN IF NOT EXISTS thanh_tien numeric(18, 2) NOT NULL DEFAULT 0;
UPDATE chitietphieuxuat SET so_luong = 1 WHERE so_luong IS NULL OR so_luong <= 0;
UPDATE chitietphieuxuat SET don_gia_xuat = 0 WHERE don_gia_xuat IS NULL OR don_gia_xuat < 0;
UPDATE chitietphieuxuat SET thanh_tien = so_luong * don_gia_xuat WHERE thanh_tien IS NULL OR thanh_tien < 0;
ALTER TABLE chitietphieuxuat ALTER COLUMN so_luong SET DEFAULT 1;
ALTER TABLE chitietphieuxuat ALTER COLUMN so_luong SET NOT NULL;
ALTER TABLE chitietphieuxuat ALTER COLUMN don_gia_xuat SET DEFAULT 0;
ALTER TABLE chitietphieuxuat ALTER COLUMN don_gia_xuat SET NOT NULL;
ALTER TABLE chitietphieuxuat ALTER COLUMN thanh_tien SET DEFAULT 0;
ALTER TABLE chitietphieuxuat ALTER COLUMN thanh_tien SET NOT NULL;

-- Bo sung CHECK constraint neu database cu chua co.
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_taikhoan_mat_khau_hash') THEN
        ALTER TABLE taikhoan ADD CONSTRAINT ck_taikhoan_mat_khau_hash CHECK (mat_khau LIKE 'pbkdf2$%');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_hanghoa_gia_nhap_khong_am') THEN
        ALTER TABLE hanghoa ADD CONSTRAINT ck_hanghoa_gia_nhap_khong_am CHECK (gia_nhap >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_hanghoa_gia_ban_khong_am') THEN
        ALTER TABLE hanghoa ADD CONSTRAINT ck_hanghoa_gia_ban_khong_am CHECK (gia_ban >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_hanghoa_ton_khong_am') THEN
        ALTER TABLE hanghoa ADD CONSTRAINT ck_hanghoa_ton_khong_am CHECK (so_luong_ton >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_phieunhap_tong_tien_khong_am') THEN
        ALTER TABLE phieunhap ADD CONSTRAINT ck_phieunhap_tong_tien_khong_am CHECK (tong_tien >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_phieuxuat_tong_tien_khong_am') THEN
        ALTER TABLE phieuxuat ADD CONSTRAINT ck_phieuxuat_tong_tien_khong_am CHECK (tong_tien >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_ctpn_so_luong_duong') THEN
        ALTER TABLE chitietphieunhap ADD CONSTRAINT ck_ctpn_so_luong_duong CHECK (so_luong > 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_ctpn_don_gia_khong_am') THEN
        ALTER TABLE chitietphieunhap ADD CONSTRAINT ck_ctpn_don_gia_khong_am CHECK (don_gia_nhap >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_ctpn_thanh_tien_khong_am') THEN
        ALTER TABLE chitietphieunhap ADD CONSTRAINT ck_ctpn_thanh_tien_khong_am CHECK (thanh_tien >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_ctpx_so_luong_duong') THEN
        ALTER TABLE chitietphieuxuat ADD CONSTRAINT ck_ctpx_so_luong_duong CHECK (so_luong > 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_ctpx_don_gia_khong_am') THEN
        ALTER TABLE chitietphieuxuat ADD CONSTRAINT ck_ctpx_don_gia_khong_am CHECK (don_gia_xuat >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'ck_ctpx_thanh_tien_khong_am') THEN
        ALTER TABLE chitietphieuxuat ADD CONSTRAINT ck_ctpx_thanh_tien_khong_am CHECK (thanh_tien >= 0);
    END IF;
END $$;

-- Bang audit log cho cac thao tac them/sua/xoa.
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

-- Bo sung FK quan trong neu database cu thieu.
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_hanghoa_loaihang') THEN
        ALTER TABLE hanghoa ADD CONSTRAINT fk_hanghoa_loaihang FOREIGN KEY (ma_loaihang) REFERENCES loaihang(ma_loaihang);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_hanghoa_nhacungcap') THEN
        ALTER TABLE hanghoa ADD CONSTRAINT fk_hanghoa_nhacungcap FOREIGN KEY (ma_nhacungcap) REFERENCES nhacungcap(ma_nhacungcap);
    END IF;
END $$;

-- Tao index neu chua co.
CREATE INDEX IF NOT EXISTS idx_hanghoa_ten ON hanghoa(ten_hanghoa);
CREATE INDEX IF NOT EXISTS idx_hanghoa_loai ON hanghoa(ma_loaihang);
CREATE INDEX IF NOT EXISTS idx_hanghoa_nhacungcap ON hanghoa(ma_nhacungcap);
CREATE UNIQUE INDEX IF NOT EXISTS uq_hanghoa_ten_ncc_active
    ON hanghoa (lower(trim(ten_hanghoa)), ma_nhacungcap)
    WHERE is_deleted = false;
CREATE INDEX IF NOT EXISTS idx_nhanvien_active ON nhanvien(is_deleted);
CREATE INDEX IF NOT EXISTS idx_phieunhap_ngay ON phieunhap(ngay_nhap);
CREATE INDEX IF NOT EXISTS idx_phieuxuat_ngay ON phieuxuat(ngay_xuat);
CREATE INDEX IF NOT EXISTS idx_auditlog_thoi_gian ON auditlog(thoi_gian DESC);
CREATE INDEX IF NOT EXISTS idx_auditlog_bang_ban_ghi ON auditlog(bang_du_lieu, ma_ban_ghi);
