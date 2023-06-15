using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStoreWebAPI.Migrations
{
    public partial class ScoreUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Review__D52345D177B702A6",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Purchase__D52345D128A58DD1",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GamesPla__95ED08B03A7BF4D4",
                table: "GamesPlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GamesGen__DA80C7886504C626",
                table: "GamesGenres");

            migrationBuilder.RenameIndex(
                name: "UQ__Users__A9D105348F8B0ADD",
                table: "Users",
                newName: "UQ__Users__A9D10534F2941804");

            migrationBuilder.RenameIndex(
                name: "UQ__Users__5E55825BF96C7D68",
                table: "Users",
                newName: "UQ__Users__5E55825B25F41094");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "Review",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Games",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Review__D52345D18161759A",
                table: "Review",
                columns: new[] { "UserID", "GameID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Purchase__D52345D10AF4019E",
                table: "Purchases",
                columns: new[] { "UserID", "GameID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__GamesPla__95ED08B0F9E5EDE7",
                table: "GamesPlatforms",
                columns: new[] { "GameID", "PlatformID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__GamesGen__DA80C78846F82486",
                table: "GamesGenres",
                columns: new[] { "GameID", "GenreID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Review__D52345D18161759A",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Purchase__D52345D10AF4019E",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GamesPla__95ED08B0F9E5EDE7",
                table: "GamesPlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GamesGen__DA80C78846F82486",
                table: "GamesGenres");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.RenameIndex(
                name: "UQ__Users__A9D10534F2941804",
                table: "Users",
                newName: "UQ__Users__A9D105348F8B0ADD");

            migrationBuilder.RenameIndex(
                name: "UQ__Users__5E55825B25F41094",
                table: "Users",
                newName: "UQ__Users__5E55825BF96C7D68");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "Review",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Games",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Review__D52345D177B702A6",
                table: "Review",
                columns: new[] { "UserID", "GameID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Purchase__D52345D128A58DD1",
                table: "Purchases",
                columns: new[] { "UserID", "GameID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__GamesPla__95ED08B03A7BF4D4",
                table: "GamesPlatforms",
                columns: new[] { "GameID", "PlatformID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__GamesGen__DA80C7886504C626",
                table: "GamesGenres",
                columns: new[] { "GameID", "GenreID" });
        }
    }
}
