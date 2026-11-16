using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Models
{
    /// <summary>Dashboard'daki tüm istatistikleri ve son kayıtları tek modelle View'a taşır.</summary>
    public class DashboardViewModel
    {
        public string AdminName { get; set; } = "Admin";

        // İstatistik kartları
        public int TotalProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int TotalMessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public int ReadMessageCount => TotalMessageCount - UnreadMessageCount;

        // Son kayıtlar
        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();

        // Kategori bazında proje sayıları (GroupBy)
        public List<CategoryProjectCount> CategorySummary { get; set; } = new();
    }

    public class CategoryProjectCount
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = "";
        public int ProjectCount { get; set; }
    }
}
