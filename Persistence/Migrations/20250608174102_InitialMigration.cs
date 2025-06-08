using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 640, DateTimeKind.Local).AddTicks(5428), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(1835) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7776), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7779) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7896), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7896) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7912), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7912) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7927), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7927) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7950), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7951) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7965), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7965) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7980), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7981) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7995), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(7996) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(8078), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(8079) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9081), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9083) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9230), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9231) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9248), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9248) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9262), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9262) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9276), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9276) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9292), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9292) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9306), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9306) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9320), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9334), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9334) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9351), new DateTime(2025, 6, 8, 19, 41, 1, 641, DateTimeKind.Local).AddTicks(9351) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(746), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(748) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(902), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(904) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(921), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(921) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(935), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(935) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(948), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(949) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(965), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(965) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1037), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1037) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1054), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1054) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1068), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1068) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1083), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1083) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6602), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6604) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6744), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6744) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6758), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6759) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6772), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6773) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6785), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6786) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6801), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6802) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6814), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6815) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6828), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6828) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6841), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6841) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6856), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(6856) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1966), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1968) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1972), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1973) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1974), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5032), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5034) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5305), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5305) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5382), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5382) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5454), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5454) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5473), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5473) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5496), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5496) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5513), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5513) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5530), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5547), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5547) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5566), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(5566) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3044), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3045) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3184), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3185) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3203), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3204) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3219), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3219) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3234), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3234) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3251), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3251) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3265), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3266) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3280), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3281) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3295), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3295) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3312), new DateTime(2025, 6, 8, 19, 41, 1, 642, DateTimeKind.Local).AddTicks(3313) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 884, DateTimeKind.Local).AddTicks(5723), new DateTime(2024, 12, 3, 13, 50, 41, 886, DateTimeKind.Local).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5060), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5322), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5327) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5363), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5363) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5394), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5394) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5435), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5471), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5471) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5502), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5502) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5533), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5533) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5564), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(5564) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(7584), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(7594) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8011), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8011) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8052), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8052) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8083), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8083) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8108), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8108) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8144), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8144) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8170), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8170) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8201), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8206) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8232), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8232) });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8268), new DateTime(2024, 12, 3, 13, 50, 41, 887, DateTimeKind.Local).AddTicks(8268) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(895), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1208), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1208) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1244), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1244) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1275), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1275) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1306), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1306) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1337), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1337) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1363), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1368) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1393), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1393) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1419), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1419) });

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1532), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(1532) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3089), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3094) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3382), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3382) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3413), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3413) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3444), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3444) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3469), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3469) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3531), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3531) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3562), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3562) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3587), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3587) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3613), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "LeaveAllocations",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3644), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(3649) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(3367), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(3373) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(3383), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(3383) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(3383), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(3388) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(246), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(251) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(652), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(657) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(698), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(698) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(734), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(734) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(770), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(770) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(806), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(806) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(914), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(914) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(956), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(986), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(986) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(1027), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 3, 13, 50, 41, 889, DateTimeKind.Local).AddTicks(1027) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(5912), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(5917) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6303), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6308) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6344), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6344) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6375), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6375) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6405), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6411) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6441), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6441) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6472), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6477) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6503), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6508) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6534), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6534) });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6570), new DateTime(2024, 12, 3, 13, 50, 41, 888, DateTimeKind.Local).AddTicks(6570) });
        }
    }
}
