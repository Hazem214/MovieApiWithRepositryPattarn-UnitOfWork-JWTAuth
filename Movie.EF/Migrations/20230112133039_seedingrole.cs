using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.EF.Migrations
{
    public partial class seedingrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20951f9a-e79a-4736-941e-1db445528aeb", "775cdb82-0635-41b4-ab22-07a712a04be0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c047cae-639d-42b4-86ac-401f7fea0b17", "fa083e37-087f-40bc-97bd-7221a80c2285", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20951f9a-e79a-4736-941e-1db445528aeb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c047cae-639d-42b4-86ac-401f7fea0b17");
        }
    }
}
