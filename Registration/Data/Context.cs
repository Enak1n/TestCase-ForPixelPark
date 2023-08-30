using Microsoft.EntityFrameworkCore;
using Registration.Entity;

namespace Registration.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) :
                base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<EmailModel> Emails { get; set; }
    }
}
