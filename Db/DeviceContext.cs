using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

public class DeviceContext : DbContext
{
    public DbSet<DeviceMetadataModel> Devices{set; get;}
    public DeviceContext(DbContextOptions options, bool track) : base(options)
    {
        this.ChangeTracker.AutoDetectChangesEnabled = track;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

          modelBuilder.Entity<DeviceMetadataModel>()
                .Property(x => x.Id)
                .UseIdentityColumn()
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

    }
}


[Table("DeviceMetadata")]
public class DeviceMetadataModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {set; get;}

    public string Value {set; get;}
}
