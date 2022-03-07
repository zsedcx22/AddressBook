using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesterProject.Migrations
{
    public partial class NamesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LName",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FName",
                table: "Contacts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "Contacts",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Contacts",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Contacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Contacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Contacts",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Contacts",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contacts",
                newName: "FName");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Contacts",
                newName: "EntryDate");
        }
    }
}
