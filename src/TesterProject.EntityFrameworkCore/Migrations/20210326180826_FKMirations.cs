using Microsoft.EntityFrameworkCore.Migrations;

namespace TesterProject.Migrations
{
    public partial class FKMirations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressAndContacts_Addresses_AddressID",
                table: "AddressAndContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressAndContacts_Contacts_ContactID",
                table: "AddressAndContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Contacts_ContactId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                table: "Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressAndContacts",
                table: "AddressAndContacts");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AddressAndContacts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressAndContacts",
                table: "AddressAndContacts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AddressAndContacts_AddressID",
                table: "AddressAndContacts",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAndContacts_Addresses_AddressID",
                table: "AddressAndContacts",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAndContacts_Contacts_ContactID",
                table: "AddressAndContacts",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Contacts_ContactId",
                table: "Cars",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                table: "Phones",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressAndContacts_Addresses_AddressID",
                table: "AddressAndContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressAndContacts_Contacts_ContactID",
                table: "AddressAndContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Contacts_ContactId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                table: "Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressAndContacts",
                table: "AddressAndContacts");

            migrationBuilder.DropIndex(
                name: "IX_AddressAndContacts_AddressID",
                table: "AddressAndContacts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AddressAndContacts");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Phones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressAndContacts",
                table: "AddressAndContacts",
                columns: new[] { "AddressID", "ContactID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAndContacts_Addresses_AddressID",
                table: "AddressAndContacts",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressAndContacts_Contacts_ContactID",
                table: "AddressAndContacts",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Contacts_ContactId",
                table: "Cars",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                table: "Phones",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
