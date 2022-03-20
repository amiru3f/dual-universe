using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}


[Table("DeviceMetadata")]
public class DeviceMetadataModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id {set; get;}

    public string Value {set; get;}
}
