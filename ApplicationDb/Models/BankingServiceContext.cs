using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels;

public class BankingServiceContext : DbContext
{
	public DbSet<Account> Accounts { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<Currency> Currencies { get; set; }
	public DbSet<Employee> Employees { get; set; }

	public BankingServiceContext()
	{
	}

	public BankingServiceContext(DbContextOptions<BankingServiceContext> options)
		: base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BankingService;Username=postgres;Password=sqlserver");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresExtension("uuid-ossp");
	}
}
