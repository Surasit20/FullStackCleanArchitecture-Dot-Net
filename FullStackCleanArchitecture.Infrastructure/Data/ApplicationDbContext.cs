using System.Reflection;
using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Application.Employees.Queries.GetEmployees;
using FullStackCleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullStackCleanArchitecture.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<Employee> Employees => Set<Employee>();

}
