using Microsoft.EntityFrameworkCore.Migrations;

namespace Materials_storage_subsystem.Migrations
{
    public partial class AddMaterialRemainings_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWarehouse");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaterialRemainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRemainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialRemainings_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialRemainings_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_WarehouseId",
                table: "Users",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRemainings_MaterialId",
                table: "MaterialRemainings",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRemainings_WarehouseId",
                table: "MaterialRemainings",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Warehouses_WarehouseId",
                table: "Users",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Warehouses_WarehouseId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "MaterialRemainings");

            migrationBuilder.DropIndex(
                name: "IX_Users_WarehouseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserWarehouse",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    WarehousesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWarehouse", x => new { x.UsersId, x.WarehousesId });
                    table.ForeignKey(
                        name: "FK_UserWarehouse_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWarehouse_Warehouses_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWarehouse_WarehousesId",
                table: "UserWarehouse",
                column: "WarehousesId");
        }
    }
}
