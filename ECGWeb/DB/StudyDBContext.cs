using Microsoft.EntityFrameworkCore;
using ShareCode;

namespace ECGWeb.DB
{
    public class StudyDBContext : DbContext
    {
        public StudyDBContext(DbContextOptions<StudyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Study> studies { get; set; }
    }
}
