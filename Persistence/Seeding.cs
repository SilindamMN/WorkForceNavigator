namespace Persistence
{
  using Domain.Enties.Leaves;
  using Domain.Enties.TimeSheets;
  using Domain.Enties;
  using Domain.Entities;
  using Domain.Entities.TimeSheets;
  using Domain.Enums;
  using Microsoft.EntityFrameworkCore;
  using System;

  public static class ModelBuilderExtensions
  {
    public static void SeedData(this ModelBuilder modelBuilder)
    {
      // South African Company Names
      string[] saCompanies = {
                "Eskom Holdings SOC Ltd",
                "Sasol Limited",
                "MTN Group Limited",
                "Naspers Limited",
                "Standard Bank Group Limited",
                "Shoprite Holdings Limited",
                "Absa Group Limited",
                "Sibanye Stillwater Limited",
                "Anglo American Platinum Limited",
                "Vodacom Group Limited"
            };

      // South African Usernames
      string[] saUsernames = {
                "John",
                "Jane",
                "Mike",
                "Sarah",
                "David",
                "Emily",
                "Ryan",
                "Lisa",
                "Chris",
                "Jessica"
            };

      // Clients
      for (int i = 0; i < saCompanies.Length; i++)
      {
        modelBuilder.Entity<Client>().HasData(
            new Client
            {
              Id = i + 1,
              ClientName = saCompanies[i],
              Phone = $"+27 11 123 456{i}",
              Fax = $"+27 11 123 456{i}",
              Email = $"info@{saCompanies[i].Replace(" ", "")}.com",
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              IsActive = true,
              IsDeleted = false
            }
        );
      }

      // Departments
      for (int i = 0; i < 10; i++)
      {
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
              Id = i + 1,
              DepartmentName = $"Department {i + 1}",
              Description = $"Description for Department {i + 1}",
              CreatedAt = DateTime.Now,
              IsActive = true,
              IsDeleted = false,
              UpdatedAt = DateTime.Now,
            }
        );
      }

      // Job Titles
      for (int i = 0; i < 10; i++)
      {
        modelBuilder.Entity<JobTitle>().HasData(
            new JobTitle
            {
              Id = i + 1,
              Title = $"Job Title {i + 1}",
              Description = $"Description for Job Title {i + 1}",
              Seniority = (Seniority)(i % 4), // Using the enum directly
              DepartmentId = (i % 10) + 1, // Assuming each job title is tied to a department with corresponding id
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              IsActive = true,
              IsDeleted = false
            }
        );
      }

      // Leave Types
      modelBuilder.Entity<LeaveType>().HasData(
          new LeaveType
          {
            Id = 1,
            Name = "Sick",
            DefaultDays = 10,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsActive = true,
            IsDeleted = false
          },
          new LeaveType
          {
            Id = 2,
            Name = "Annual",
            DefaultDays = 20,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsActive = true,
            IsDeleted = false
          },
          new LeaveType
          {
            Id = 3,
            Name = "Family",
            DefaultDays = 5,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsActive = true,
            IsDeleted = false
          }
      );

      // Teams
      for (int i = 0; i < 10; i++)
      {
        modelBuilder.Entity<Team>().HasData(
            new Team
            {
              Id = i + 1,
              TeamName = $"Team {i + 1}",
              TeamLeader = saUsernames[i], // Assigning team leaders from the usernames
              Description = $"Description for Team {i + 1}",
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              IsActive = true,
              IsDeleted = false
            }
        );
      }

      // Projects
      for (int i = 0; i < 10; i++)
      {
        modelBuilder.Entity<Project>().HasData(
            new Project
            {
              Id = i + 1,
              ClientId = (i % 10) + 1, // Assuming it's tied to a South African Client with corresponding id
              TeamId = (i % 10) + 1, // Assuming it's tied to a local team with corresponding id
              ProjectName = $"Project {i + 1}",
              Description = $"Description of Project {i + 1}",
              StartDate = DateTime.Today,
              EndDate = DateTime.Today.AddDays(30),
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              IsActive = true,
              IsDeleted = false
            }
        );
      }

      // Leave Allocations
      for (int i = 0; i < 10; i++)
      {
        modelBuilder.Entity<LeaveAllocation>().HasData(
            new LeaveAllocation
            {
              Id = i + 1,
              NumberOfDays = 20,
              LeaveTypeId = (i % 3) + 1, // Assigning leave types cyclically
              Username = saUsernames[i],
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              IsActive = true,
              IsDeleted = false
            }
        );
      }
    }
  }
}
