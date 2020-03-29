using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airline.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    AcID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcModel = table.Column<string>(type: "varchar(30)", nullable: false),
                    AcType = table.Column<string>(type: "varchar(45)", nullable: false),
                    AcCapacity = table.Column<int>(type: "int", nullable: false),
                    AcRegisterDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AcModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.AcID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "varchar(40)", nullable: false),
                    EmpLastName = table.Column<string>(type: "varchar(40)", nullable: false),
                    EmpGender = table.Column<string>(type: "char(1)", nullable: false),
                    EmpHomeAdrs = table.Column<string>(type: "varchar(max)", nullable: false),
                    EmpPhoneNumber = table.Column<string>(type: "varchar(30)", nullable: false),
                    EmpEmail = table.Column<string>(type: "varchar(320)", nullable: false),
                    EmpBirthdate = table.Column<DateTime>(type: "Date", nullable: false),
                    EmpHireDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    EmpModifiedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    EmpCardID = table.Column<string>(type: "varchar(30)", nullable: false),
                    EmpSalary = table.Column<int>(type: "int", nullable: false),
                    EmpType = table.Column<string>(type: "nvarchar(55)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpID);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlgID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcID = table.Column<int>(type: "int", nullable: false),
                    FlgDeparture = table.Column<DateTime>(type: "datetime", nullable: false),
                    FlgArrival = table.Column<DateTime>(type: "datetime", nullable: false),
                    FlgFare = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlgID);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_AcID",
                        column: x => x.AcID,
                        principalTable: "Aircrafts",
                        principalColumn: "AcID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightAssignedEmps",
                columns: table => new
                {
                    FlgEmpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlgID = table.Column<int>(type: "int", nullable: false),
                    EmpID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightAssignedEmps", x => x.FlgEmpID);
                    table.ForeignKey(
                        name: "FK_FlightAssignedEmps_Employees_EmpID",
                        column: x => x.EmpID,
                        principalTable: "Employees",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightAssignedEmps_Flights_FlgID",
                        column: x => x.FlgID,
                        principalTable: "Flights",
                        principalColumn: "FlgID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightAssignedEmps_EmpID",
                table: "FlightAssignedEmps",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_FlightAssignedEmps_FlgID",
                table: "FlightAssignedEmps",
                column: "FlgID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AcID",
                table: "Flights",
                column: "AcID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightAssignedEmps");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Aircrafts");
        }
    }
}
