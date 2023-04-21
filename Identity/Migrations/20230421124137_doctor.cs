using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class doctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1d49ce76-8525-4b7f-af9c-d306a6e2e4ee"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e6f108a-7bb0-4219-b602-26373b5c4017"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("917d1cfb-8987-4b70-845a-09cf0acde1cf"), new Guid("69511471-12b4-43e5-b55e-49c63a07f9cf") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("69511471-12b4-43e5-b55e-49c63a07f9cf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("917d1cfb-8987-4b70-845a-09cf0acde1cf"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2e69405d-dbac-433f-b1b1-1e85c83cf5de"), "88c3e1a2-6ca2-4c85-b044-2c46dc09252d", "Admin", "ADMIN" },
                    { new Guid("d73abf0e-0090-41fd-bd77-e9c175e0762b"), "a5c16956-fd65-4827-b24f-25a9a7787038", "Doctor", "DOCTOR" },
                    { new Guid("4ef58c95-4b5f-4899-ba7d-1c4a31c2de42"), "b9f146cc-5409-4a7e-99c6-dcf27be700fa", "Unconfirmed", "UNCONFIRMED" },
                    { new Guid("4f103bb8-24b2-4f60-935b-ff101cc9a0a9"), "759ec2c7-082d-4dd5-be46-51f30fe415aa", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0ea177cb-b9c0-4f77-83dd-e810fddf2aeb"), 0, "a83a89e7-281a-4e76-9e1e-d368adc46c6b", "test@pl.pl", true, "Wojciech", "Nytko", false, null, "TEST@PL.PL", "WAR014828", "AQAAAAEAACcQAAAAEMAbcG3Sc0tYBRpqRGnxMc1QBaBIvZqePPX6Oispr93i9qu06zZ1zHgFQKDXEwMuCQ==", null, true, "1938b5bc-01e5-44a7-a42f-7d2dd0ed4619", false, "WAR014828" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("0ea177cb-b9c0-4f77-83dd-e810fddf2aeb"), new Guid("4f103bb8-24b2-4f60-935b-ff101cc9a0a9") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2e69405d-dbac-433f-b1b1-1e85c83cf5de"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4ef58c95-4b5f-4899-ba7d-1c4a31c2de42"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d73abf0e-0090-41fd-bd77-e9c175e0762b"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("0ea177cb-b9c0-4f77-83dd-e810fddf2aeb"), new Guid("4f103bb8-24b2-4f60-935b-ff101cc9a0a9") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f103bb8-24b2-4f60-935b-ff101cc9a0a9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0ea177cb-b9c0-4f77-83dd-e810fddf2aeb"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1d49ce76-8525-4b7f-af9c-d306a6e2e4ee"), "2a37a1cb-b9de-43b6-9b96-c414744f1617", "Admin", "ADMIN" },
                    { new Guid("7e6f108a-7bb0-4219-b602-26373b5c4017"), "2b951e33-4c14-45d5-91e7-59301a294887", "Unconfirmed", "UNCONFIRMED" },
                    { new Guid("69511471-12b4-43e5-b55e-49c63a07f9cf"), "be05336b-5484-4e8b-b0c8-0b805b02d49e", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("917d1cfb-8987-4b70-845a-09cf0acde1cf"), 0, "fbb09156-5e8e-401c-a0fb-cf67d8f5052d", "test@pl.pl", true, "Wojciech", "Nytko", false, null, "TEST@PL.PL", "WAR014131", "AQAAAAEAACcQAAAAEFs3tvMu1io+NuOqiJU6YYzHxJjtV7W2NPmQHwIKvzepE5/00lLkex5ba+Vv9PlM6Q==", null, true, "524e2008-c2bb-4bc4-83a3-f2769c88271e", false, "WAR014131" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("917d1cfb-8987-4b70-845a-09cf0acde1cf"), new Guid("69511471-12b4-43e5-b55e-49c63a07f9cf") });
        }
    }
}
