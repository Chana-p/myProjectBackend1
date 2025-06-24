using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustId = table.Column<int>(type: "integer", nullable: false),
                    CustNum = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CustEmail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustPhone = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__049E3AA9C8D5AF3A", x => x.CustId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "integer", nullable: false),
                    EmpNum = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EGmail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EPhone = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__AF2DBB99A44967FF", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSum",
                columns: table => new
                {
                    prodId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PSum = table.Column<int>(type: "integer", nullable: false),
                    PImporter = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Pcompany = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PDescription = table.Column<string>(type: "text", nullable: true),
                    PPicture = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__319F67F1894DDDE3", x => x.prodId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CustId = table.Column<int>(type: "integer", nullable: false),
                    EmpId = table.Column<int>(type: "integer", nullable: true),
                    paymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    sent = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    pcc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BCFA3EC6A01", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK__Orders__CustId__2EDAF651",
                        column: x => x.CustId,
                        principalTable: "Customers",
                        principalColumn: "CustId");
                    table.ForeignKey(
                        name: "FK__Orders__EmpId__2FCF1A8A",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProdId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__3214EC0753BEA1DE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderDeta__ProdI__208CD6FA",
                        column: x => x.ProdId,
                        principalTable: "ProductsSum",
                        principalColumn: "prodId");
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProdId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table__3214EC074619E7C4", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Table__ProdId__1CBC4616",
                        column: x => x.ProdId,
                        principalTable: "ProductsSum",
                        principalColumn: "prodId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProdId",
                table: "OrderDetail",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustId",
                table: "Orders",
                column: "CustId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmpId",
                table: "Orders",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_ProdId",
                table: "Table",
                column: "ProdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ProductsSum");
        }
    }
}
