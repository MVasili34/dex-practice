using Microsoft.EntityFrameworkCore;

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

        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Amount).HasDefaultValueSql("0");

            entity.HasOne(d => d.CurrencyIsoNavigation).WithMany(p => p.Accounts)
                .HasConstraintName("currency_code");

            entity.HasOne(d => d.Owner).WithMany(p => p.Accounts)
                .HasConstraintName("owner_id");
        });
    }
}
