using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booky_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "ApplicationUsers");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 10, 2, 24, 211, DateTimeKind.Local).AddTicks(6950));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 10, 2, 24, 212, DateTimeKind.Local).AddTicks(6444));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 10, 2, 24, 212, DateTimeKind.Local).AddTicks(6459));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 10, 2, 24, 212, DateTimeKind.Local).AddTicks(6461));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 10, 2, 24, 212, DateTimeKind.Local).AddTicks(6463));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 10, 2, 24, 212, DateTimeKind.Local).AddTicks(6465));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 9, 55, 14, 261, DateTimeKind.Local).AddTicks(182));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 9, 55, 14, 262, DateTimeKind.Local).AddTicks(5192));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 9, 55, 14, 262, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 9, 55, 14, 262, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 9, 55, 14, 262, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 12, 9, 55, 14, 262, DateTimeKind.Local).AddTicks(5209));
        }
    }
}
