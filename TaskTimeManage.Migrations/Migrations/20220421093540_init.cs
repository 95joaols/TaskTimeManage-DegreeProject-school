using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

using TaskTimeManage.Domain.Entity;

#nullable disable

namespace TaskTimeManage.Migrations.Migrations
{
	public partial class init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
					.Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

			migrationBuilder.CreateTable(
					name: "User",
					columns: table => new {
						Id = table.Column<int>(type: "integer", nullable: false)
									.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
						PublicId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
						UserName = table.Column<string>(type: "text", nullable: false),
						Password = table.Column<string>(type: "text", nullable: false),
						Salt = table.Column<string>(type: "text", nullable: false),
						CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_User", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Task",
					columns: table => new {
						Id = table.Column<int>(type: "integer", nullable: false)
									.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
						PublicId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
						Name = table.Column<string>(type: "text", nullable: false),
						UserId = table.Column<int>(type: "integer", nullable: false),
						WorkTimes = table.Column<List<WorkTime>>(type: "jsonb", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Task", x => x.Id);
						table.ForeignKey(
											name: "FK_Task_User_UserId",
											column: x => x.UserId,
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_Task_UserId",
					table: "Task",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_User_UserName",
					table: "User",
					column: "UserName",
					unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "Task");

			migrationBuilder.DropTable(
					name: "User");
		}
	}
}
