using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVSP.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diem_danh",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anh_minh_chung = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    thoi_gian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diem_danh", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ds_rubrics",
                columns: table => new
                {
                    id_rubric = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_rubric = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ds_rubrics", x => x.id_rubric);
                });

            migrationBuilder.CreateTable(
                name: "tai_khoan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_ca_nhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ho_ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mat_khau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    loai_tk = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tai_khoan", x => x.id);
                    table.UniqueConstraint("AK_tai_khoan_ma_ca_nhan", x => x.ma_ca_nhan);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_rubric",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tieu_chi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    diem_toi_da = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_rubric", x => new { x.id, x.tieu_chi });
                    table.ForeignKey(
                        name: "FK_chi_tiet_rubric_ds_rubrics_id",
                        column: x => x.id,
                        principalTable: "ds_rubrics",
                        principalColumn: "id_rubric");
                });

            migrationBuilder.CreateTable(
                name: "giang_vien",
                columns: table => new
                {
                    ma_giang_vien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    khoa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giang_vien", x => x.ma_giang_vien);
                    table.ForeignKey(
                        name: "FK_giang_vien_tai_khoan_ma_giang_vien",
                        column: x => x.ma_giang_vien,
                        principalTable: "tai_khoan",
                        principalColumn: "ma_ca_nhan");
                });

            migrationBuilder.CreateTable(
                name: "sinh_vien",
                columns: table => new
                {
                    ma_sinh_vien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nien_khoa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    lop = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nganh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    khoa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sinh_vien", x => x.ma_sinh_vien);
                    table.ForeignKey(
                        name: "FK_sinh_vien_tai_khoan_ma_sinh_vien",
                        column: x => x.ma_sinh_vien,
                        principalTable: "tai_khoan",
                        principalColumn: "ma_ca_nhan");
                });

            migrationBuilder.CreateTable(
                name: "ban_to_chuc",
                columns: table => new
                {
                    ma_giang_vien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bat_dau_nk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ket_thuc_nk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ban_to_chuc", x => x.ma_giang_vien);
                    table.ForeignKey(
                        name: "FK_ban_to_chuc_giang_vien_ma_giang_vien",
                        column: x => x.ma_giang_vien,
                        principalTable: "giang_vien",
                        principalColumn: "ma_giang_vien");
                });

            migrationBuilder.CreateTable(
                name: "bcn_khoa",
                columns: table => new
                {
                    ma_giang_vien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    khoa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    bat_dau_nk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ket_thuc_nk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bcn_khoa", x => x.ma_giang_vien);
                    table.ForeignKey(
                        name: "FK_bcn_khoa_giang_vien_ma_giang_vien",
                        column: x => x.ma_giang_vien,
                        principalTable: "giang_vien",
                        principalColumn: "ma_giang_vien");
                });

            migrationBuilder.CreateTable(
                name: "can_bo_lop",
                columns: table => new
                {
                    ma_sinh_vien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bat_dau_nk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ket_thuc_nk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_can_bo_lop", x => x.ma_sinh_vien);
                    table.ForeignKey(
                        name: "FK_can_bo_lop_sinh_vien_ma_sinh_vien",
                        column: x => x.ma_sinh_vien,
                        principalTable: "sinh_vien",
                        principalColumn: "ma_sinh_vien");
                });

            migrationBuilder.CreateTable(
                name: "diem_ren_luyen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_sv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    diem = table.Column<int>(type: "int", nullable: false),
                    xep_loai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ky_hoc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nam_hoc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diem_ren_luyen", x => x.id);
                    table.ForeignKey(
                        name: "FK_diem_ren_luyen_sinh_vien_ma_sv",
                        column: x => x.ma_sv,
                        principalTable: "sinh_vien",
                        principalColumn: "ma_sinh_vien");
                });

            migrationBuilder.CreateTable(
                name: "su_kien",
                columns: table => new
                {
                    id_sk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_sk = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    nam_hoc = table.Column<int>(type: "int", nullable: false),
                    ma_btc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trang_thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_su_kien", x => x.id_sk);
                    table.ForeignKey(
                        name: "FK_su_kien_ban_to_chuc_ma_btc",
                        column: x => x.ma_btc,
                        principalTable: "ban_to_chuc",
                        principalColumn: "ma_giang_vien");
                });

            migrationBuilder.CreateTable(
                name: "hoat_dong_thi",
                columns: table => new
                {
                    id_hd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_hd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_rubric = table.Column<int>(type: "int", nullable: false),
                    hinh_thuc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tg_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tg_ket_thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dia_diem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_sk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoat_dong_thi", x => x.id_hd);
                    table.ForeignKey(
                        name: "FK_hoat_dong_thi_ds_rubrics_id_rubric",
                        column: x => x.id_rubric,
                        principalTable: "ds_rubrics",
                        principalColumn: "id_rubric");
                    table.ForeignKey(
                        name: "FK_hoat_dong_thi_su_kien_id_sk",
                        column: x => x.id_sk,
                        principalTable: "su_kien",
                        principalColumn: "id_sk");
                });

            migrationBuilder.CreateTable(
                name: "ban_giam_khao",
                columns: table => new
                {
                    id_hd = table.Column<int>(type: "int", nullable: false),
                    ma_gv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ban_giam_khao", x => new { x.id_hd, x.ma_gv });
                    table.ForeignKey(
                        name: "FK_ban_giam_khao_giang_vien_ma_gv",
                        column: x => x.ma_gv,
                        principalTable: "giang_vien",
                        principalColumn: "ma_giang_vien");
                    table.ForeignKey(
                        name: "FK_ban_giam_khao_hoat_dong_thi_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                });

            migrationBuilder.CreateTable(
                name: "dang_ky_ca_nhan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_hd = table.Column<int>(type: "int", nullable: false),
                    ma_sv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trang_thai = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dang_ky_ca_nhan", x => x.id);
                    table.ForeignKey(
                        name: "FK_dang_ky_ca_nhan_hoat_dong_thi_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                    table.ForeignKey(
                        name: "FK_dang_ky_ca_nhan_sinh_vien_ma_sv",
                        column: x => x.ma_sv,
                        principalTable: "sinh_vien",
                        principalColumn: "ma_sinh_vien");
                });

            migrationBuilder.CreateTable(
                name: "dang_ky_nhom",
                columns: table => new
                {
                    id_nhom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_nhom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ma_tham_gia = table.Column<int>(type: "int", nullable: false),
                    id_hd = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dang_ky_nhom", x => x.id_nhom);
                    table.ForeignKey(
                        name: "FK_dang_ky_nhom_hoat_dong_thi_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                });

            migrationBuilder.CreateTable(
                name: "hoat_dong_ho_tro",
                columns: table => new
                {
                    id_hd_ho_tro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_hd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    loai_ho_tro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tg_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tg_ket_thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dia_diem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_hd = table.Column<int>(type: "int", nullable: false),
                    ma_gv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoat_dong_ho_tro", x => x.id_hd_ho_tro);
                    table.ForeignKey(
                        name: "FK_hoat_dong_ho_tro_giang_vien_ma_gv",
                        column: x => x.ma_gv,
                        principalTable: "giang_vien",
                        principalColumn: "ma_giang_vien");
                    table.ForeignKey(
                        name: "FK_hoat_dong_ho_tro_hoat_dong_thi_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                });

            migrationBuilder.CreateTable(
                name: "hoat_dong_tham_du",
                columns: table => new
                {
                    id_hd_tham_du = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_hd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_hd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoat_dong_tham_du", x => x.id_hd_tham_du);
                    table.ForeignKey(
                        name: "FK_hoat_dong_tham_du_hoat_dong_thi_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                });

            migrationBuilder.CreateTable(
                name: "cham_diem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_hd_thi = table.Column<int>(type: "int", nullable: false),
                    loai_bai_thi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_dang_ky = table.Column<int>(type: "int", nullable: false),
                    diem = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    nhan_xet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ma_bgk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DangKyCaNhanId = table.Column<int>(type: "int", nullable: true),
                    DangKyNhomIdNhom = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cham_diem", x => x.id);
                    table.ForeignKey(
                        name: "FK_cham_diem_dang_ky_ca_nhan_DangKyCaNhanId",
                        column: x => x.DangKyCaNhanId,
                        principalTable: "dang_ky_ca_nhan",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cham_diem_dang_ky_nhom_DangKyNhomIdNhom",
                        column: x => x.DangKyNhomIdNhom,
                        principalTable: "dang_ky_nhom",
                        principalColumn: "id_nhom");
                    table.ForeignKey(
                        name: "FK_cham_diem_hoat_dong_thi_id_hd_thi",
                        column: x => x.id_hd_thi,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                });

            migrationBuilder.CreateTable(
                name: "thanh_vien_nhom",
                columns: table => new
                {
                    id_nhom = table.Column<int>(type: "int", nullable: false),
                    ma_sv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanh_vien_nhom", x => new { x.id_nhom, x.ma_sv });
                    table.ForeignKey(
                        name: "FK_thanh_vien_nhom_dang_ky_nhom_id_nhom",
                        column: x => x.id_nhom,
                        principalTable: "dang_ky_nhom",
                        principalColumn: "id_nhom");
                    table.ForeignKey(
                        name: "FK_thanh_vien_nhom_sinh_vien_ma_sv",
                        column: x => x.ma_sv,
                        principalTable: "sinh_vien",
                        principalColumn: "ma_sinh_vien");
                });

            migrationBuilder.CreateTable(
                name: "dang_ky_tham_du",
                columns: table => new
                {
                    ma_sv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_hd = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dang_ky_tham_du", x => new { x.ma_sv, x.id_hd });
                    table.ForeignKey(
                        name: "FK_dang_ky_tham_du_hoat_dong_tham_du_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_tham_du",
                        principalColumn: "id_hd_tham_du");
                    table.ForeignKey(
                        name: "FK_dang_ky_tham_du_sinh_vien_ma_sv",
                        column: x => x.ma_sv,
                        principalTable: "sinh_vien",
                        principalColumn: "ma_sinh_vien");
                });

            migrationBuilder.CreateTable(
                name: "ket_qua",
                columns: table => new
                {
                    id_cham_diem = table.Column<int>(type: "int", nullable: false),
                    giai_thuong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trang_thai = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ket_qua", x => x.id_cham_diem);
                    table.ForeignKey(
                        name: "FK_ket_qua_cham_diem_id_cham_diem",
                        column: x => x.id_cham_diem,
                        principalTable: "cham_diem",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "thong_tin_gcn",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loai_chung_nhan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    doi_tuong = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    giai_thuong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_hd = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<bool>(type: "bit", nullable: false),
                    ma_btc = table.Column<int>(type: "int", nullable: false),
                    KetQuaIdChamDiem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thong_tin_gcn", x => x.id);
                    table.ForeignKey(
                        name: "FK_thong_tin_gcn_hoat_dong_thi_id_hd",
                        column: x => x.id_hd,
                        principalTable: "hoat_dong_thi",
                        principalColumn: "id_hd");
                    table.ForeignKey(
                        name: "FK_thong_tin_gcn_ket_qua_KetQuaIdChamDiem",
                        column: x => x.KetQuaIdChamDiem,
                        principalTable: "ket_qua",
                        principalColumn: "id_cham_diem");
                });

            migrationBuilder.CreateTable(
                name: "log_gcn",
                columns: table => new
                {
                    id_gcn = table.Column<int>(type: "int", nullable: false),
                    hanh_dong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ma_btc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_gcn", x => x.id_gcn);
                    table.ForeignKey(
                        name: "FK_log_gcn_thong_tin_gcn_id_gcn",
                        column: x => x.id_gcn,
                        principalTable: "thong_tin_gcn",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "log_tra_cuu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_gcn = table.Column<int>(type: "int", nullable: false),
                    tg_tra_cuu = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_tra_cuu", x => x.id);
                    table.ForeignKey(
                        name: "FK_log_tra_cuu_thong_tin_gcn_id_gcn",
                        column: x => x.id_gcn,
                        principalTable: "thong_tin_gcn",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ban_giam_khao_ma_gv",
                table: "ban_giam_khao",
                column: "ma_gv");

            migrationBuilder.CreateIndex(
                name: "IX_cham_diem_DangKyCaNhanId",
                table: "cham_diem",
                column: "DangKyCaNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_cham_diem_DangKyNhomIdNhom",
                table: "cham_diem",
                column: "DangKyNhomIdNhom");

            migrationBuilder.CreateIndex(
                name: "IX_cham_diem_id_hd_thi",
                table: "cham_diem",
                column: "id_hd_thi");

            migrationBuilder.CreateIndex(
                name: "IX_dang_ky_ca_nhan_id_hd",
                table: "dang_ky_ca_nhan",
                column: "id_hd");

            migrationBuilder.CreateIndex(
                name: "IX_dang_ky_ca_nhan_ma_sv",
                table: "dang_ky_ca_nhan",
                column: "ma_sv");

            migrationBuilder.CreateIndex(
                name: "IX_dang_ky_nhom_id_hd",
                table: "dang_ky_nhom",
                column: "id_hd");

            migrationBuilder.CreateIndex(
                name: "IX_dang_ky_nhom_ma_tham_gia",
                table: "dang_ky_nhom",
                column: "ma_tham_gia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dang_ky_tham_du_id_hd",
                table: "dang_ky_tham_du",
                column: "id_hd");

            migrationBuilder.CreateIndex(
                name: "IX_diem_ren_luyen_ma_sv",
                table: "diem_ren_luyen",
                column: "ma_sv");

            migrationBuilder.CreateIndex(
                name: "IX_hoat_dong_ho_tro_id_hd",
                table: "hoat_dong_ho_tro",
                column: "id_hd");

            migrationBuilder.CreateIndex(
                name: "IX_hoat_dong_ho_tro_ma_gv",
                table: "hoat_dong_ho_tro",
                column: "ma_gv");

            migrationBuilder.CreateIndex(
                name: "IX_hoat_dong_tham_du_id_hd",
                table: "hoat_dong_tham_du",
                column: "id_hd");

            migrationBuilder.CreateIndex(
                name: "IX_hoat_dong_thi_id_rubric",
                table: "hoat_dong_thi",
                column: "id_rubric");

            migrationBuilder.CreateIndex(
                name: "IX_hoat_dong_thi_id_sk",
                table: "hoat_dong_thi",
                column: "id_sk");

            migrationBuilder.CreateIndex(
                name: "IX_hoat_dong_thi_ten_hd_id_sk",
                table: "hoat_dong_thi",
                columns: new[] { "ten_hd", "id_sk" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_log_tra_cuu_id_gcn",
                table: "log_tra_cuu",
                column: "id_gcn");

            migrationBuilder.CreateIndex(
                name: "IX_su_kien_ma_btc",
                table: "su_kien",
                column: "ma_btc");

            migrationBuilder.CreateIndex(
                name: "IX_su_kien_ten_sk_nam_hoc",
                table: "su_kien",
                columns: new[] { "ten_sk", "nam_hoc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tai_khoan_ma_ca_nhan",
                table: "tai_khoan",
                column: "ma_ca_nhan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_thanh_vien_nhom_ma_sv",
                table: "thanh_vien_nhom",
                column: "ma_sv");

            migrationBuilder.CreateIndex(
                name: "IX_thong_tin_gcn_id_hd",
                table: "thong_tin_gcn",
                column: "id_hd");

            migrationBuilder.CreateIndex(
                name: "IX_thong_tin_gcn_KetQuaIdChamDiem",
                table: "thong_tin_gcn",
                column: "KetQuaIdChamDiem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ban_giam_khao");

            migrationBuilder.DropTable(
                name: "bcn_khoa");

            migrationBuilder.DropTable(
                name: "can_bo_lop");

            migrationBuilder.DropTable(
                name: "chi_tiet_rubric");

            migrationBuilder.DropTable(
                name: "dang_ky_tham_du");

            migrationBuilder.DropTable(
                name: "diem_danh");

            migrationBuilder.DropTable(
                name: "diem_ren_luyen");

            migrationBuilder.DropTable(
                name: "hoat_dong_ho_tro");

            migrationBuilder.DropTable(
                name: "log_gcn");

            migrationBuilder.DropTable(
                name: "log_tra_cuu");

            migrationBuilder.DropTable(
                name: "thanh_vien_nhom");

            migrationBuilder.DropTable(
                name: "hoat_dong_tham_du");

            migrationBuilder.DropTable(
                name: "thong_tin_gcn");

            migrationBuilder.DropTable(
                name: "ket_qua");

            migrationBuilder.DropTable(
                name: "cham_diem");

            migrationBuilder.DropTable(
                name: "dang_ky_ca_nhan");

            migrationBuilder.DropTable(
                name: "dang_ky_nhom");

            migrationBuilder.DropTable(
                name: "sinh_vien");

            migrationBuilder.DropTable(
                name: "hoat_dong_thi");

            migrationBuilder.DropTable(
                name: "ds_rubrics");

            migrationBuilder.DropTable(
                name: "su_kien");

            migrationBuilder.DropTable(
                name: "ban_to_chuc");

            migrationBuilder.DropTable(
                name: "giang_vien");

            migrationBuilder.DropTable(
                name: "tai_khoan");
        }
    }
}
