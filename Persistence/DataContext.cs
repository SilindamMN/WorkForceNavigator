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
    using System.Reflection.Emit;
    using Department = Domain.Enties.Department;

  public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Message> Messages { get; set; }
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

      builder.SeedData();
      base.OnModelCreating(builder);

      builder.Entity<JobTitle>()
        .HasOne(x => x.Department)
        .WithMany(c => c.JobTitles)
        .HasForeignKey(x => x.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<LeaveRequest>()
       .HasKey(lr => lr.Id); // Assuming 'Id' is the primary key property

      builder.Entity<ApplicationUser>(e =>
      {
        e.ToTable("Users");
      });
      builder.Entity<IdentityUserClaim<string>>(e =>
      {
        e.ToTable("UserClaims");
      });
      builder.Entity<IdentityUserLogin<string>>(e =>
      {
        e.ToTable("UserLogins");
      });
      builder.Entity<IdentityUserToken<string>>(e =>
      {
        e.ToTable("UserToken");
      });

      builder.Entity<IdentityRole<string>>(e =>
      {
        e.ToTable("Roles");
      });

      builder.Entity<IdentityRoleClaim<string>>(e =>
      {
        e.ToTable("RoleClaims");
      });

      builder.Entity<UserTeam>()
            .Property(ut => ut.Id)
            .ValueGeneratedOnAdd();

      builder.Entity<TimesheetEntry>()
          .HasOne(te => te.Project)
          .WithMany(p => p.TimesheetEntries)
          .HasForeignKey(te => te.ProjectId);
      builder.Entity<UserTeam>()

    .HasKey(ut => new { ut.UserId, ut.TeamId });

      builder.Entity<UserTeam>()
          .HasOne(ut => ut.User)
          .WithMany(u => u.UserTeams)
          .HasForeignKey(ut => ut.UserId);

      builder.Entity<UserTeam>()
          .HasOne(ut => ut.Team)
          .WithMany(t => t.UserTeams)
          .HasForeignKey(ut => ut.TeamId);

      builder.Entity<ApplicationUser>(entity =>
      {
        entity.Property(e => e.Salary)
            .HasColumnType("decimal(18, 2)");
      });

      //entity.HasOne(d => d.Client)
      //    .WithMany(p => p.ProjectDto)
      //    .HasForeignKey(d => d.ClientId)
      //    .HasConstraintName("FK_ProjectDto_Client");
    }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}