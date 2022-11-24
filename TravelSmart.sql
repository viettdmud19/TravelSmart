Create database TravelSmart
go
use TravelSmart
go

create table DIADIEM
(
	Ma_DD int  IDENTITY(1,1),
	Ten_DD NVARCHAR(50) NOT NULL,
	MoTa_DD NTEXT NOT NULL,
	ANH_DD VARCHAR(50),
	Luot_YeuThich int DEFAULT 0,
	CONSTRAINT PR_MADD PRIMARY KEY(Ma_DD) 
	)
	

Create Table LIENKET(
	Ma_LK INT  IDENTITY(1,1),
	Ten_LK NVARCHAR(100) NOT NULL,
	DienThoai nvarchar(15),
	Anh_LK VARCHAR(45),
	Email varchar(50) not null,
	DiaChi NVARCHAR(100) NOT NULL,
	CONSTRAINT PR_MALK PRIMARY KEY (Ma_LK)
	)
/****MADD , MA LK   ****/
create table TOUR(
	Ma_TOUR INT IDENTITY (1,1),
	Ten_TOUR NVARCHAR(100)NOT NULL,
	Anh_MAIN VARCHAR(45) NOT NULL,
	Anh_TOUR VARCHAR(45) NOT NULL,
	Lich_Trinh  NVARCHAR(MAX),
	GiaTien_TOUR MONEY CHECK(GiaTien_TOUR >=0),
	SoLuong int check (soluong >0),
	NgayTOUR SMALLDATETIME, 
	Ma_DD int ,
	Ma_LK INT ,
	
	CONSTRAINT PR_TOUR PRIMARY KEY (Ma_TOUR)
	)
  /*MA_DD Ma_DM    */
Create TABLE HOTEL(
	Ma_HOTEL INT IDENTITY(1,1),
	Ten_HOTEL NVARCHAR(100) NOT NULL,
	Anh_HOTEL VARCHAR(25) NOT NULL,
	Anh_Bia varchar(25) NOT NULL,
	DT_HOTEL VARCHAR(15) NOT NULL,
	DiaChi_HOTEL NVARCHAR(100) NOT NULL,
	Ma_DD INT ,
	
	CONSTRAINT PR_MaHOTEL PRIMARY KEY (Ma_HOTEL)
)


	/* MADD */
CREATE TABLE AMTHUC(
	Ma_AT int IDENTITY(1,1),
	Ma_HOTEL INT ,
	Ten_AT NVARCHAR(50) NOT NULL,
	Anh_AT  VARCHAR(25) NOT NULL,
	
	CONSTRAINT PR_MAAT PRIMARY KEY (Ma_AT)
	)
	
	/* luu tru*/
CREATE TABLE GIAITRI(
	Ma_GT int identity(1,1),
	Ma_HOTEL INT ,
	Ten_GT NVARCHAR(50) NOT NULL,
	ANH_GT VARCHAR(25) NOT NULL,
	
	CONSTRAINT PR_MAGT PRIMARY KEY (Ma_GT)
	)
	
	/* MALK*/


CREATE TABLE ADMIN
(
	MaAd INT IDENTITY(1,1),
	HoTen NVARCHAR(50),
	DienThoai VARCHAR(10),
	TenDN VARCHAR(15),
	MatKhau VARCHAR(50),
	Quyen Int Default 1,
	CONSTRAINT PK_AM PRIMARY KEY(MaAd)
)
CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1),
	HoTen NVARCHAR(50) NOT NULL,
	TaiKhoan VARCHAR(15) UNIQUE,
	MatKhau VARCHAR(50) NOT NULL,
	Email VARCHAR(50) UNIQUE,
	DiaChi NVARCHAR(50),
	DienThoai VARCHAR(10),
	NgaySinh SMALLDATETIME,
	CONSTRAINT PK_Kh PRIMARY KEY(MaKH)
)
CREATE TABLE DONDATHANG
(
	MaDonHang INT IDENTITY(1,1),
	DaThanhToan BIT DEFAULT(0),
	NgayDat SMALLDATETIME,
	MaKH INT,
	CONSTRAINT PK_DDH PRIMARY KEY(MaDonHang)
)
CREATE TABLE CHITIETDATHANG
(
	MaDonHang INT,
	MaTour INT,
	SoLuong INT CHECK(SoLuong>0),
	DonGia money CHECK(DonGia>=0),
	CONSTRAINT PK_CTDH PRIMARY KEY(MaDonHang,MaTour)
)
create table TINTUC(
	ma_TT int IDENTITY(1,1),
	TieuDe_TT NVARCHAR(100),
	MoTa_TT NTEXT,
	Anh_TT VARCHAR(25),
	Ma_KH int ,
	NgayDang_TT SMALLDATETIME,
	CONSTRAINT PR_TT PRIMARY KEY (ma_TT)
)

/* khóa ngoai  */
ALTER TABLE TOUR ADD CONSTRAINT FK_MADD FOREIGN KEY (Ma_DD) REFERENCES DIADIEM(Ma_DD)
ALTER TABLE TOUR ADD CONSTRAINT FK_MALK FOREIGN KEY (Ma_LK) REFERENCES LIENKET(Ma_LK)


ALTER TABLE HOTEL ADD CONSTRAINT FK_MALD FOREIGN KEY (Ma_DD) REFERENCES DIADIEM(Ma_DD)



alter TABLE AMTHUC ADD CONSTRAINT FK_MAHL FOREIGN KEY (Ma_HOTEL) REFERENCES HOTEL(Ma_HOTEL)

ALTER TABLE GIAITRI ADD CONSTRAINT FK_MAGH FOREIGN KEY (Ma_HOTEL) REFERENCES HOTEL(Ma_HOTEL)

ALTER TABLE DONDATHANG ADD CONSTRAINT FK_MAKH FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)

ALTER TABLE CHITIETDATHANG ADD CONSTRAINT FK_MADH FOREIGN KEY (MaDonHang) REFERENCES DONDATHANG(MaDonHang)
ALTER TABLE CHITIETDATHANG ADD CONSTRAINT FK_MAtour FOREIGN KEY (MaTour) REFERENCES TOUR(Ma_TOUR)

ALTER TABLE TINTUC  ADD CONSTRAINT FR_MAKT FOREIGN KEY (Ma_KH) REFERENCES KHACHHANG(MaKH)

	/*bảng danh mục*/

/* bảng địa điểm */
INSERT INTO DIADIEM VALUES(N'TP Hồ Chí Minh',N'Giữ vai trò quan trọng trong nền kinh tế Việt Nam,
Thành phố Hồ Chí Minh chiếm 21,3% tổng sản phẩm (GDP) và 29,38% tổng thu ngân sách của cả nước.
Nhờ điều kiện tự nhiên thuận lợi, Thành phố Hồ Chí Minh trở thành một đầu mối giao thông quan trọng của Việt Nam và Đông Nam Á, bao gồm cả đường bộ, đường sắt, đường thủy và đường không.
Vào năm 2007, thành phố đón khoảng 3 triệu khách du lịch quốc tế, tức 70% lượng khách vào Việt Nam.
Các lĩnh vực giáo dục, truyền thông, thể thao, giải trí, Thành phố Hồ Chí Minh đều giữ vai trò quan trọng bậc nhất.',
N'hcm.jpg',5)
INSERT INTO DIADIEM VALUES(N'Hà Nội',N'Hà Nội, thủ đô của Việt Nam, nổi tiếng với kiến trúc trăm tuổi và nền văn hóa phong phú với sự ảnh hưởng của khu vực Đông Nam Á, Trung Quốc và Pháp.
Trung tâm thành phố là Khu phố cổ nhộn nhịp, nơi các con phố hẹp được mang tên "hàng". 
Có rất nhiều ngôi đền nhỏ, bao gồm Bạch Mã, tôn vinh một con ngựa huyền thoại, cùng với chợ Đồng Xuân, bán hàng gia dụng và thức ăn đường phố.',
N'hanoi.jpg',6)
INSERT INTO DIADIEM VALUES(N'Cần Thơ',N'Cần Thơ gạo trắng nước trong.
Ai đi đến đó thì không muốn về" Câu ca dao lưu truyền từ bao đời đã làm lay động lòng người mỗi khi có dịp dừng chân ghé thăm vùng đất Tây Đô. Bến Ninh Kiều, chợ nổi Cái Răng, vườn cỏ Bằng Lăng hay vườn trái cây trĩu quả chắc hẳn sẽ níu chân du khách khi đặt chân đến Cần Thơ.
Cuộc sống nơi đây mang đậm nét của một vùng quê sông nước.
Với lợi thế này, Cần Thơ đang là một điểm du lịch hấp dẫn của miền Tây.',
N'cantho.jpg',4)
INSERT INTO DIADIEM VALUES(N'Đà Nẵng',N'Đà Nẵng là một thành phố trực thuộc trung ương, nằm trong vùng Nam Trung Bộ, Việt Nam, là trung tâm kinh tế, tài chính, chính trị, văn hóa, du lịch, giáo dục - đào tạo, khoa học và công nghệ, y tế chuyên sâu của khu vực miền Trung - Tây Nguyên và cả nước. 
Đà Nẵng là thành phố quan trọng nhất miền Trung, đồng thời cũng là một trong 5 thành phố trực thuộc Trung ương ở Việt Nam, đô thị loại 1 trung tâm cấp quốc gia, cùng với Hải Phòng và Cần Thơ.
Năm 2018, Đà Nẵng được chọn đại diện cho Việt Nam lọt vào top 10 địa điểm tốt nhất để sống ở nước ngoài do Tạp chí du lịch Live and Invest Overseas (LIO) bình chọn.',
N'tp_danang.jpg',4)
INSERT INTO DIADIEM VALUES(N'Cà Mau',N'Cà Mau là tỉnh ven biển ở cực nam của Việt Nam, nằm trong khu vực Đồng bằng sông Cửu Long.
Cà Mau là một vùng đất trẻ, mới được khai phá khoảng trên 300 năm.
Vùng đất Cà Mau ngày xưa được Mạc Cửu dẫn người Hoa đến khai phá. 
Sau khi Mạc Cửu dâng toàn đất này thần phục Nhà Nguyễn, Mạc Thiên Tứ con của Mạc Cửu đã vâng lệnh triều đình Chúa Nguyễn lập ra đạo Long Xuyên.
Qua nhiều lần thay đổi về hành chính, mãi đến ngày 1 tháng 1 năm 1997, tỉnh Cà Mau được tái lập theo Nghị quyết của Quốc hội khóa IX, kỳ họp thứ 10.
Ngày 6 tháng 11 năm 1996 về việc điều chỉnh địa giới hành chính, chia tách tỉnh Minh Hải thành tỉnh Cà Mau và tỉnh Bạc Liêu.',
N'camau.jpg',3)
INSERT INTO DIADIEM VALUES(N'Hà Giang',N'Hà Giang là một tỉnh thuộc vùng núi phía Bắc Việt Nam. 
Tỉnh Hà Giang phía đông giáp tỉnh Cao Bằng, phía tây giáp tỉnh Yên Bái và Lào Cai, phía nam giáp tỉnh Tuyên Quang.
Hà Giang ở cực Bắc, cuốn hút lòng người với núi đá vôi hùng vĩ,
cao nguyên Đồng Văn huyền thoại bên bờ sông Lô, những cung đường uốn lượn, những ruộng bậc thang đẹp như tranh, nhà Vương trầm mặc, phố cổ Đồng Văn, và chợ tình Khâu Vai cho những mối tơ duyên dở dang.',
N'hagiang.jpg',4)

/*bảng liên kết */
INSERT INTO LIENKET VALUES(N'Công Ty Cổ Phần Đầu Tư – Vận Tải – DL Hoàng Việt ',N'0918 618 565','LK1.png','yenphu@hoangviettravel.vn',N'62 Trần Quốc Toản, phường Trần Hưng Đạo, quận Hoàn Kiếm, Hà Nội, Việt Nam')
INSERT INTO LIENKET VALUES(N'Công ty Du Lịch Hello Việt Nam Travel ',N'028 39 205 813','LK2.png','support@helloviettravel.com',N'28/3 Bùi Viện, P Phạm Ngũ Lão, Quận 1, HCM')
INSERT INTO LIENKET VALUES(N'VIETTOURIST - Tour DL Việt ',N' 0909886688','LK3.jpg','viettourist@icloud.com',N'91-93 Lê Quốc Hưng, P.12, Q.4, TP.HCM')
INSERT INTO LIENKET VALUES(N'Công Ty Du Lịch Bình Minh Phương Nam ',N'0934 624 954','LK4.jpg','binhminhphuongnamtk.travel@gmail.com',N'180-190 Cống Quỳnh – Phường Nguyễn Cư Trinh, Quận 1 – Tp. Hồ Chí Minh.')
INSERT INTO LIENKET VALUES(N'Công Ty TNHH DV Du Lịch TOP TEN',N'1900 633 678','LK5.jpg','info@toptentravel.com.vn',N'02 Giải Phóng, Phường 04, Quận Tân Bình,Hồ Chí Minh')
INSERT INTO LIENKET VALUES(N'Công Ty TNHH DL Dịch Vụ Tiên Phong ',N'024 6273 2611','LK6.png',N'info@tptravel.com.vn',N'58 Kim Mã, Ba Đình, Hà Nội, Việt Nam')


/* bảng tour */
INSERT INTO TOUR VALUES(N'Tour Thành Phố Hồ Chí Minh- Sông nước Miền tây 4 ngày 3 đêm','M1.jpg','T1.jpg',
N'Ngày 1:TP Hồ Chí Minh - Cần Thơ
  Ngày 2:Cần Thơ - Sóc Trăng - Bạc Liêu -Cần Thơ 
  Ngày 3:Cà Mau - Bạc Liêu
  Ngày 4:Cà Mau','3868000',23,'09/18/2021',1,1)
INSERT INTO TOUR VALUES(N'Tour Hồ Chí Minh -Đà Nẵng -Huế -Hạ Long -Hà Nội 10 ngày','M2.jpg','T2.jpg',
N'Ngày 1:TP Hồ Chí Minh - Tây Ninh - Củ Chi
  Ngày 3:TP Hồ Chí Minh - Đà Nẵng - Hội An
  Ngày 5:Hội An - Huế
  Ngày 7:Huế - Hà Nội - Hạ Long
  Ngày 9-10:Hạ Long - Hà Nội','36300000 ',15,'11/09/2021',1,1)

INSERT INTO TOUR VALUES(N'Tour Hà Nội -Tam Cốc -Cúc Phương 2 ngày 1 đêm','M3.jpg','T3.jpg',
N'Ngày 1:Hà Nôi - Hoa Lư - Tam Côc
  Ngày 2:Tam Cốc - Vườn QG Cúc Phương - Hà Nội','3868000',23,'09/18/2021',2,2)
INSERT INTO TOUR VALUES(N'Tour Hà Nội -Hạ Long -Tuần Châu 2 ngày 1 đêm','M4.jpg','T4.jpg',
N'Ngày 1:Hà Nội - Hạ Long - Tuần Châu
  Ngày 2:Tuần Châu - Hà Nội','1800000',15,'11/09/2021',2,2)

INSERT INTO TOUR VALUES(N'Tour Chợ nổi Cái Bè Vĩnh Long -Cần Thơ -Chợ nổi Cái Răng 2 ngày','M5.jpg','T5.jpg',
N'Ngày 1:Sài Gòn - Cái Bè - Vĩnh Long - Cần Thơ
  Ngày 2:Cần Thơ - Cái Răng - Sài Gòn','1050000',23,'09/27/2021',3,3)
INSERT INTO TOUR VALUES(N'Tour Sài Gòn - Xuôi dòng Mekong 4 ngày','M6.jpg','T6.jpg',
N'Ngày 1:TP Hồ Chí Minh - Cái Bè
  Ngày 3:Trà Ôn - Cần Thơ
  Ngày 4:Cần Thơ - TP Hồ Chí Minh','8300000',18,'10/09/2021',3,3)

INSERT INTO TOUR VALUES(N'Tour Hội An -Bà Nà -Đà Nẵng 2 ngày 1 đêm','M7.jpg','T7.jpg',
N'Ngày 1:Hội An - Bà Nà
  Ngày 2:Bà Nà(Đà Nẵng) - Hội An','4000000',18,'10/20/2021',4,4)
INSERT INTO TOUR VALUES(N'Tour Hội An -Đà Nẵng -Huế 2 ngày 1 đêm','M2.jpg','T3.jpg',
N'Ngày 1:Hội An - Đà Nẵng - Huế
  Ngày 2:Huế - Hội An','3500000',20,'09/10/2021',4,4)

INSERT INTO TOUR VALUES(N'Tour Thành Phố Hồ Chí Minh -Sông nước Miền tây 4 ngày','M1.jpg','T1.jpg',
N'Ngày 1:TP Hồ Chí Minh - Cần Thơ
  Ngày 2:Cần Thơ - Sóc Trăng - Bạc Liêu -Cần Thơ 
  Ngày 3:Cà Mau - Bạc Liêu
  Ngày 4:Cà Mau','3868000',18,'10/27/2021',5,5)
INSERT INTO TOUR VALUES(N'Tour Chợ nổi Cái Bè Vĩnh Long -Cần Thơ -Chợ nổi Cái Răng 2 ngày','M5.jpg','T5.jpg',
N'Ngày 1:Sài Gòn - Cái Bè - Vĩnh Long - Cần Thơ
  Ngày 2:Cần Thơ - Cái Răng - Sài Gòn','1050000',20,'11/15/2021',5,5)

INSERT INTO TOUR VALUES(N'Tour Hà Nội - Hà Giang 3 ngày 2 đêm','M8.jpg','T8.jpg',
N'Ngày 1:Hà Nội - Hà Giang - Quản Bạ
  Ngày 2:Quản Bạ - Đồng Văn - Mèo Vạc
  Ngày 3:Mèo Vạc - Yên Minh - Hà Nội','4000000',25,'10/09/2021',6,6)
INSERT INTO TOUR VALUES(N'Tour Hà Giang -Cao nguyên đá Đồng Văn 3 ngày 2 đêm','M3.jpg','T9.jpg',
N'Ngày 1:Hà Nội - Hà Giang
  Ngày 2:Hà Giang - Yên Minh - Đồng Văn
  Ngày 3:Đồng Văn - Hà Giang - Hà Nội ','3500000',18,'09/10/2021',6,6)


/* HOTEL */
INSERT INTO HOTEL VALUES (N'The Alcove LIBRARY HOTEL','H1.jpg','t1.jpeg','028 6256 9966',N'133A Nguyễn Đình Chính, Quận Phú Nhuận, TP HCM',1)
INSERT INTO HOTEL VALUES (N'HaLais Hotel','H2.jpg','t2.jpg','024 3371 1111',N'48 Trần Nhân Tông, phường Nguyễn Du, quận Hai Bà Trưng, Hanoi',2)
INSERT INTO HOTEL VALUES (N'Danang Capsule Hotel','H4.jpg','t2.jpg','091 332 14 41',N'63 Lê Minh Trung, An Hải Bắc, Sơn Trà, Đà Nẵng',4)

/* ẩm thực */
INSERT INTO AMTHUC VALUES(1,N'ALE Bar & Restaurant','A1.jpg')
INSERT INTO AMTHUC VALUES(2,N'Nhà hàng Đông Tây','A2.jpg')
INSERT INTO AMTHUC VALUES(3,N'Nhà hàng Prince','A3.jpg')

/* giải trí */
INSERT INTO GIAITRI VALUES(1,N'LaCochinchine Spa','G1.jpg')
INSERT INTO GIAITRI VALUES(2,N'Wellness Spa','G2.jpg')
INSERT INTO GIAITRI VALUES(3,N'IN BALANCE SPA','G3.jpg')



/* admin */
INSERT into ADMIN VALUES ( N'Đặng Hải Việt', N'0328763738', N'viet123', N'123456789', 1)

/* Tin tứ*/

/* trigger */
GO
create trigger trg_CHITIETDATHANG ON CHITIETDATHANG AFTER INSERT 
AS BEGIN
	UPDATE TOUR 
	SET TOUR.SoLuong = TOUR.SoLuong -( 
		SELECT CHITIETDATHANG.SoLuong
		FROM CHITIETDATHANG
		WHERE CHITIETDATHANG.MaTour = TOUR.Ma_TOUR
		)
	FROM TOUR
	JOIN inserted on TOUR.Ma_TOUR = inserted.MaTour
	end