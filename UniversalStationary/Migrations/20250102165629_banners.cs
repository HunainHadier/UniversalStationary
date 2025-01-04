using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalStationary.Migrations
{
    /// <inheritdoc />
    public partial class banners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("252f5d49-4638-43cf-af9d-0947eeb7e9bd"));

            migrationBuilder.AddColumn<string>(
                name: "sitebanners",
                table: "logochanges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Address", "City", "Created", "Email", "Gender", "Password", "PhoneNumber", "PostalCode", "ProfilePicture", "Role", "Updated", "UserName" },
                values: new object[] { new Guid("589a5f94-fa13-4d88-b261-7acdd0305ebd"), "Malir Karachi", "Karachi", new DateTime(2025, 1, 2, 21, 56, 28, 761, DateTimeKind.Local).AddTicks(2908), "superadmin@gmail.com", "Male", "$2a$11$DmpAD2HB8yOn8J9UPZ5Ot.43fyaMnqWMgr.cHUJbW6/AxWSl2Q9i2", "1234567890", "12345", "AllImages/userimage.png", "Admin", new DateTime(2025, 1, 2, 21, 56, 28, 761, DateTimeKind.Local).AddTicks(2939), "SuperAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("589a5f94-fa13-4d88-b261-7acdd0305ebd"));

            migrationBuilder.DropColumn(
                name: "sitebanners",
                table: "logochanges");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Address", "City", "Created", "Email", "Gender", "Password", "PhoneNumber", "PostalCode", "ProfilePicture", "Role", "Updated", "UserName" },
                values: new object[] { new Guid("252f5d49-4638-43cf-af9d-0947eeb7e9bd"), "Malir Karachi", "Karachi", new DateTime(2025, 1, 1, 13, 2, 16, 10, DateTimeKind.Local).AddTicks(8407), "superadmin@gmail.com", "Male", "$2a$11$vdCpayq7IKX384FeMDh8ueRi.g.k8QfQxkDyqQcuP5LJe71oid/de", "1234567890", "12345", "AllImages/userimage.png", "Admin", new DateTime(2025, 1, 1, 13, 2, 16, 10, DateTimeKind.Local).AddTicks(8428), "SuperAdmin" });
        }
    }
}
