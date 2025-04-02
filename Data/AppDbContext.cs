using ChatBot.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ChatbotQA> ChatbotQAs { get; set; }
    }
}
