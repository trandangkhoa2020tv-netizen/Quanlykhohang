-- ============================================================
-- Du lieu mau cho PostgreSQL.
-- Chay sau create_tables.sql.
-- File nay khong dung ON CONFLICT de tranh mot so extension SQL gạch đỏ sai dialect.
-- ============================================================

INSERT INTO loaihang (ma_loaihang, ten_loaihang, ghi_chu)
SELECT 1, 'Dien tu', 'Hang dien tu va thiet bi cong nghe'
WHERE NOT EXISTS (SELECT 1 FROM loaihang WHERE ma_loaihang = 1);

INSERT INTO loaihang (ma_loaihang, ten_loaihang, ghi_chu)
SELECT 2, 'Quan ao', 'Hang thoi trang'
WHERE NOT EXISTS (SELECT 1 FROM loaihang WHERE ma_loaihang = 2);

INSERT INTO loaihang (ma_loaihang, ten_loaihang, ghi_chu)
SELECT 3, 'Thuc pham', 'Hang tieu dung thuc pham'
WHERE NOT EXISTS (SELECT 1 FROM loaihang WHERE ma_loaihang = 3);

INSERT INTO nhacungcap (ma_nhacungcap, ten_nhacungcap, dia_chi_ncc, so_dien_thoai, email, ghi_chu)
SELECT 1, 'Cong ty A', '123 Duong ABC', '0123456789', 'a@company.com', 'Nha cung cap dien tu'
WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE ma_nhacungcap = 1);

INSERT INTO nhacungcap (ma_nhacungcap, ten_nhacungcap, dia_chi_ncc, so_dien_thoai, email, ghi_chu)
SELECT 2, 'Cong ty B', '456 Duong DEF', '0987654321', 'b@company.com', 'Nha cung cap thoi trang'
WHERE NOT EXISTS (SELECT 1 FROM nhacungcap WHERE ma_nhacungcap = 2);

INSERT INTO khachhang (ma_khachhang, ten_khachhang, dia_chi_kh, so_dien_thoai, email, ghi_chu)
SELECT 1, 'Nguyen Van A', '789 Duong GHI', '0111111111', 'nguyenvana@example.com', 'Khach hang le'
WHERE NOT EXISTS (SELECT 1 FROM khachhang WHERE ma_khachhang = 1);

INSERT INTO khachhang (ma_khachhang, ten_khachhang, dia_chi_kh, so_dien_thoai, email, ghi_chu)
SELECT 2, 'Tran Thi B', '101 Duong JKL', '0222222222', 'tranthib@example.com', 'Khach hang than thiet'
WHERE NOT EXISTS (SELECT 1 FROM khachhang WHERE ma_khachhang = 2);

INSERT INTO nhanvien (ma_nhanvien, ten_nhanvien, dia_chi_nv, so_dien_thoai, email, ngay_sinh, chuc_vu, ghi_chu)
SELECT 1, 'Quan tri vien', 'Van phong', '0900000001', 'admin@example.com', DATE '1990-01-01', 'Admin', 'Tai khoan quan tri'
WHERE NOT EXISTS (SELECT 1 FROM nhanvien WHERE ma_nhanvien = 1);

INSERT INTO nhanvien (ma_nhanvien, ten_nhanvien, dia_chi_nv, so_dien_thoai, email, ngay_sinh, chuc_vu, ghi_chu)
SELECT 2, 'Nhan vien kho', 'Kho hang', '0900000002', 'kho@example.com', DATE '1995-05-20', 'Thu kho', 'Phu trach nhap kho'
WHERE NOT EXISTS (SELECT 1 FROM nhanvien WHERE ma_nhanvien = 2);

INSERT INTO nhanvien (ma_nhanvien, ten_nhanvien, dia_chi_nv, so_dien_thoai, email, ngay_sinh, chuc_vu, ghi_chu)
SELECT 3, 'Nhan vien ban hang', 'Quay ban hang', '0900000003', 'banhang@example.com', DATE '1996-06-15', 'Ban hang', 'Phu trach xuat kho'
WHERE NOT EXISTS (SELECT 1 FROM nhanvien WHERE ma_nhanvien = 3);

INSERT INTO hanghoa (ma_hanghoa, ten_hanghoa, ma_loaihang, ma_nhacungcap, gia_nhap, gia_ban, so_luong_ton, don_vi_tinh, ghi_chu)
SELECT 1, 'Laptop Dell', 1, 1, 5000000, 6500000, 10, 'Cai', 'Laptop van phong'
WHERE NOT EXISTS (SELECT 1 FROM hanghoa WHERE ma_hanghoa = 1);

INSERT INTO hanghoa (ma_hanghoa, ten_hanghoa, ma_loaihang, ma_nhacungcap, gia_nhap, gia_ban, so_luong_ton, don_vi_tinh, ghi_chu)
SELECT 2, 'Ao thun nam', 2, 2, 50000, 80000, 50, 'Cai', 'Ao thun co ban'
WHERE NOT EXISTS (SELECT 1 FROM hanghoa WHERE ma_hanghoa = 2);

INSERT INTO hanghoa (ma_hanghoa, ten_hanghoa, ma_loaihang, ma_nhacungcap, gia_nhap, gia_ban, so_luong_ton, don_vi_tinh, ghi_chu)
SELECT 3, 'Mi goi', 3, 1, 3000, 5000, 200, 'Goi', 'Thuc pham nhanh'
WHERE NOT EXISTS (SELECT 1 FROM hanghoa WHERE ma_hanghoa = 3);

-- Dong bo lai sequence sau khi chen ma co dinh.
SELECT setval(pg_get_serial_sequence('loaihang', 'ma_loaihang'), COALESCE((SELECT MAX(ma_loaihang) FROM loaihang), 1), true);
SELECT setval(pg_get_serial_sequence('nhacungcap', 'ma_nhacungcap'), COALESCE((SELECT MAX(ma_nhacungcap) FROM nhacungcap), 1), true);
SELECT setval(pg_get_serial_sequence('khachhang', 'ma_khachhang'), COALESCE((SELECT MAX(ma_khachhang) FROM khachhang), 1), true);
SELECT setval(pg_get_serial_sequence('nhanvien', 'ma_nhanvien'), COALESCE((SELECT MAX(ma_nhanvien) FROM nhanvien), 1), true);
SELECT setval(pg_get_serial_sequence('hanghoa', 'ma_hanghoa'), COALESCE((SELECT MAX(ma_hanghoa) FROM hanghoa), 1), true);
SELECT setval(pg_get_serial_sequence('taikhoan', 'ma_taikhoan'), COALESCE((SELECT MAX(ma_taikhoan) FROM taikhoan), 1), true);
