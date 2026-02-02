using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_service.Migrations
{
    /// <inheritdoc />
    public partial class MakeColumnIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Columns_ColumnId",
                table: "ProjectTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ColumnId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Columns_ColumnId",
                table: "ProjectTasks",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Columns_ColumnId",
                table: "ProjectTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "ColumnId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Columns_ColumnId",
                table: "ProjectTasks",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id");
        }
    }
}
