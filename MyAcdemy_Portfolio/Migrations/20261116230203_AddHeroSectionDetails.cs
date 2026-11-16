using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAcdemy_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddHeroSectionDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Card1SubTitle",
                table: "HeroSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Card1Title",
                table: "HeroSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Card2SubTitle",
                table: "HeroSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Card2Title",
                table: "HeroSections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypingTexts",
                table: "HeroSections",
                type: "nvarchar(max)",
                nullable: true);

            // Mevcut kayıtları sitede şu an görünen sabit yazılarla doldur (veri kaybı olmasın)
            migrationBuilder.Sql(@"
                UPDATE HeroSections SET
                    TypingTexts   = N'Modern Web Uygulamaları Geliştiriyorum.' + CHAR(10) +
                                    N'Yazılımla Problemlere Çözüm Üretiyorum.' + CHAR(10) +
                                    N'Kullanıcı Odaklı Deneyimler Tasarlıyorum.' + CHAR(10) +
                                    N'Yeni Teknolojilerle Geleceği İnşa Ediyorum.',
                    Card1Title    = N'Clean Code',
                    Card1SubTitle = N'C# & ASP.NET Core',
                    Card2Title    = N'Software Engineer',
                    Card2SubTitle = N'Full Stack Developer'
                WHERE TypingTexts IS NULL;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Card1SubTitle",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "Card1Title",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "Card2SubTitle",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "Card2Title",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "TypingTexts",
                table: "HeroSections");
        }
    }
}
