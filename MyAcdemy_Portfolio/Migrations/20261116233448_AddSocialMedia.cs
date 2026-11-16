using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAcdemy_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddSocialMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    SocialMediaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.SocialMediaID);
                });

            // Mevcut veriyi taşı: İletişim tablosundaki GitHub / LinkedIn adresleri sosyal medya kaydı olur
            migrationBuilder.Sql(@"
                INSERT INTO SocialMedias (PlatformName, Url, IconClass, DisplayOrder, IsActive)
                SELECT TOP 1 N'GitHub', Github, N'bi-github', 1, 1 FROM Contacts WHERE Github IS NOT NULL AND Github <> '';
                INSERT INTO SocialMedias (PlatformName, Url, IconClass, DisplayOrder, IsActive)
                SELECT TOP 1 N'LinkedIn', Linkedin, N'bi-linkedin', 2, 1 FROM Contacts WHERE Linkedin IS NOT NULL AND Linkedin <> '';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMedias");
        }
    }
}
