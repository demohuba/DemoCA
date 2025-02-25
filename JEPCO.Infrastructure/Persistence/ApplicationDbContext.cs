using JEPCO.Domain.Common;
using JEPCO.Domain.Entities;
using JEPCO.Infrastructure.Enums;
using JEPCO.Infrastructure.Extensions;
using JEPCO.Infrastructure.Models;
using JEPCO.Infrastructure.Persistence.DbContextInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Geometries;

namespace JEPCO.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IDbContextSchema
    {
        public string Schema { get; }
        internal SequenceManager SequenceManager { get; private set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
           : base(options)
        {
            // keep these configurations
            // Ensures that setting and geting datetimes are done in utc
            // Ensures taht no conversion for min and max datetimes are done in the db
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);


            SequenceManager = new SequenceManager(this);
            Schema = configuration.GetValue<string>("DBSchema")!;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // define schema
            builder.HasDefaultSchema(Schema);

            // keep this at top
            base.OnModelCreating(builder);

            // add gis extension
            builder.HasPostgresExtension("postgis");

            // set IsDeleted flag != true  
            builder.HandleSoftDeleteGlobally();

            // seed data
            builder.Seed();

            // Apply cutom enities configurations if any

            // builder.ApplyConfiguration(new UserConfiguration());

            // Add sequences if needed
            // builder.HasSequence(SequenceNameConstants.GovernmentSequence, (e) => { e.StartsAt(1000); });
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



        #region private functions
        private void BeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Fill audit proeprties
                FillAuditProperties(entry);

                // AuditLogEntity Logging 
                if (entry.Entity is not IAuditLoggableEntity || entry.Entity is AuditLogEntity || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                // implement audit logging
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    object? replacedCurrentValue = null;
                    object? replacedOriginalValue = null;
                    if (property.CurrentValue is not null && (property.CurrentValue is Point))
                    {
                        var pointVal = (Point)property.CurrentValue;
                        replacedCurrentValue = $"Long: {pointVal.X.ToString()} - Lat: {pointVal.Y.ToString()}";
                    }
                    if (property.OriginalValue is not null && property.OriginalValue is Point)
                    {
                        var pointVal = (Point)property.OriginalValue;
                        replacedOriginalValue = $"Long: {pointVal.X.ToString()} - Lat: {pointVal.Y.ToString()}";
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = replacedCurrentValue ?? property.CurrentValue;
                            auditEntry.UserId = entry.Property("CreatedBy").CurrentValue != null ? entry.Property("CreatedBy").CurrentValue.ToString() : "Null";
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = replacedOriginalValue ?? property.OriginalValue;
                            auditEntry.UserId = entry.Property("LastModifiedBy").CurrentValue != null ? entry.Property("LastModifiedBy").CurrentValue.ToString() : "Null";
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                var isDeletableEntity = entry.Entity is IDeletableEntity;
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = isDeletableEntity && (bool)entry.Property("IsDeleted").CurrentValue == true ? AuditType.SoftDelete : AuditType.Update;
                                auditEntry.OldValues[propertyName] = replacedOriginalValue ?? property.OriginalValue;
                                auditEntry.NewValues[propertyName] = replacedCurrentValue ?? property.CurrentValue;
                                auditEntry.UserId = entry.Property("LastModifiedBy").CurrentValue != null ? entry.Property("LastModifiedBy").CurrentValue.ToString() : "Null";
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
        private void FillAuditProperties(EntityEntry entry)
        {
            var now = DateTime.UtcNow;

            // Fill AuditLogEntity Properties
            if (entry.Entity is IAuditableEntity ae)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ae.CreatedAt = now;
                        ae.LastModifiedAt = now;
                        break;
                    case EntityState.Modified:
                        Entry(ae).Property(e => e.CreatedAt).IsModified = false;
                        ae.LastModifiedAt = now;
                        break;
                }
            }
        }

        #endregion


        #region Entities


        #region Lookups
        public DbSet<LK_GenderEntity> LK_Genders { get; set; }
        public DbSet<LK_CustomerMeterRelationTypeEntity> LK_CustomerMeterRelationTypes { get; set; }
        public DbSet<LK_LanguageEntity> LK_Languages { get; set; }
        public DbSet<LK_MobilePlatformTypeEntity> LK_MobilePlatformTypes { get; set; }
        public DbSet<LK_NotificationTypeEntity> LK_NotificationTypes { get; set; }
        public DbSet<LK_SelfMeterReadSapStatusEntity> LK_SelfMeterReadSapStatus { get; set; }
        public DbSet<LK_UserTypeEntity> LK_UserTypes { get; set; }
        #endregion



        public DbSet<AuditLogEntity> AuditLogs { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<CustomerMeterEntity> CustomerMeters { get; set; }
        public DbSet<SelfMetersReadEntity> SelfMetersReads { get; set; }
        public DbSet<UserCloudMessagingTokenEntity> UserCloudMessagingTokens { get; set; }
        public DbSet<UserNotificationPreferencesEntity> UserNotificationPreferences { get; set; }

        #endregion

    }
}
