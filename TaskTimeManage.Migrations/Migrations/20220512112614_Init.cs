﻿using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTimeManage.Migrations.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

		_ = migrationBuilder.CreateTable(
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
				constraints: table => table.PrimaryKey("PK_User", x => x.Id));

		_ = migrationBuilder.CreateTable(
				name: "WorkItem",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
								.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					PublicId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
					Name = table.Column<string>(type: "text", nullable: false),
					UserId = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table => {
					_ = table.PrimaryKey("PK_WorkItem", x => x.Id);
					_ = table.ForeignKey(
										name: "FK_WorkItem_User_UserId",
										column: x => x.UserId,
										principalTable: "User",
										principalColumn: "Id",
										onDelete: ReferentialAction.Cascade);
				});

		_ = migrationBuilder.CreateTable(
				name: "WorkTime",
				columns: table => new {
					Id = table.Column<int>(type: "integer", nullable: false)
								.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					PublicId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
					Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					WorkItemId = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table => {
					_ = table.PrimaryKey("PK_WorkTime", x => x.Id);
					_ = table.ForeignKey(
										name: "FK_WorkTime_WorkItem_WorkItemId",
										column: x => x.WorkItemId,
										principalTable: "WorkItem",
										principalColumn: "Id",
										onDelete: ReferentialAction.Cascade);
				});

		_ = migrationBuilder.CreateIndex(
				name: "IX_User_PublicId",
				table: "User",
				column: "PublicId",
				unique: true);

		_ = migrationBuilder.CreateIndex(
				name: "IX_User_UserName",
				table: "User",
				column: "UserName",
				unique: true);

		_ = migrationBuilder.CreateIndex(
				name: "IX_WorkItem_PublicId",
				table: "WorkItem",
				column: "PublicId",
				unique: true);

		_ = migrationBuilder.CreateIndex(
				name: "IX_WorkItem_UserId",
				table: "WorkItem",
				column: "UserId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_WorkTime_PublicId",
				table: "WorkTime",
				column: "PublicId",
				unique: true);

		_ = migrationBuilder.CreateIndex(
				name: "IX_WorkTime_WorkItemId",
				table: "WorkTime",
				column: "WorkItemId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.DropTable(
				name: "WorkTime");

		_ = migrationBuilder.DropTable(
				name: "WorkItem");

		_ = migrationBuilder.DropTable(
				name: "User");
	}
}