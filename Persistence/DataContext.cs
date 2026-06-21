namespace Persistence
{
    using Domain.Account;
    using Domain.Enties;
    using Domain.Enties.Leaves;
    using Domain.Enties.TimeSheets;
    using Domain.Entities;
    using Domain.Entities.TimeSheets;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Department = Domain.Enties.Department;

    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<TimesheetEntry> TimesheetEntries { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // ---------------- Managers ----------------
            builder.Entity<Manager>(entity =>
            {
                entity.HasKey(m => m.Id);

                // IMPORTANT: Map ManagerId to ApplicationUser FK
                entity.Property(m => m.ManagerId)
                    .HasColumnName("ManagerId")
                    .IsRequired();

                entity.HasOne(m => m.ManagerUser)
                    .WithMany()
                    .HasForeignKey(m => m.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Team)
                    .WithMany(t => t.Managers)
                    .HasForeignKey(m => m.TeamId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            // ---------------- Identity Mapping ----------------
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityRole<string>>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            // ---------------- Department → JobTitle ----------------
            builder.Entity<JobTitle>()
                .HasOne(x => x.Department)
                .WithMany(c => c.JobTitles)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);


            // ---------------- UserTeam (Many-to-Many) ----------------
            builder.Entity<UserTeam>()
                .HasKey(ut => new { ut.UserId, ut.TeamId });

            builder.Entity<UserTeam>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.NoAction); // IMPORTANT FIX

            builder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(ut => ut.TeamId)
                .OnDelete(DeleteBehavior.NoAction); // IMPORTANT FIX

            // ---------------- Project Relations ----------------
            builder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Project>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Projects)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------- Timesheet ----------------
            builder.Entity<TimesheetEntry>()
                .HasOne(te => te.Project)
                .WithMany(p => p.TimesheetEntries)
                .HasForeignKey(te => te.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---------------- Decimal Fix ----------------
            builder.Entity<ApplicationUser>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}