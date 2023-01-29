using Microsoft.EntityFrameworkCore.Migrations;

namespace Materials_storage_subsystem.Migrations
{
    public partial class alpha_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseSheets_ExpenseSheetId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Materials_MaterialId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "MaterialMovements");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_MaterialId",
                table: "MaterialMovements",
                newName: "IX_MaterialMovements_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_ExpenseSheetId",
                table: "MaterialMovements",
                newName: "IX_MaterialMovements_ExpenseSheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialMovements",
                table: "MaterialMovements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialMovements_ExpenseSheets_ExpenseSheetId",
                table: "MaterialMovements",
                column: "ExpenseSheetId",
                principalTable: "ExpenseSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialMovements_Materials_MaterialId",
                table: "MaterialMovements",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialMovements_ExpenseSheets_ExpenseSheetId",
                table: "MaterialMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialMovements_Materials_MaterialId",
                table: "MaterialMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialMovements",
                table: "MaterialMovements");

            migrationBuilder.RenameTable(
                name: "MaterialMovements",
                newName: "Expenses");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialMovements_MaterialId",
                table: "Expenses",
                newName: "IX_Expenses_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialMovements_ExpenseSheetId",
                table: "Expenses",
                newName: "IX_Expenses_ExpenseSheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseSheets_ExpenseSheetId",
                table: "Expenses",
                column: "ExpenseSheetId",
                principalTable: "ExpenseSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Materials_MaterialId",
                table: "Expenses",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
