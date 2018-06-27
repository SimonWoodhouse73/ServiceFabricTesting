using Api.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.EntityFramework
{
    /// <summary>
    /// Defines the database context and table mappings
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// The logger factory
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <param name="loggerFactory">An instance of the <see cref="ILoggerFactory" /></param>
        public DatabaseContext(ILoggerFactory loggerFactory)
            : this(new DbContextOptions<DatabaseContext>(), loggerFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <param name="dbContextOptions">An instance of the <see cref="DbContextOptions" /></param>
        /// <param name="loggerFactory">An instance of the <see cref="ILoggerFactory" /></param>
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions, ILoggerFactory loggerFactory)
            : base(dbContextOptions)
        {
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Gets or sets a DBSet for querying.
        /// </summary>
        /// <value>The court entities.</value>
        public DbSet<CourtEntity> CourtEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="DisputeEntity" />.
        /// </summary>
        /// <value>The dispute entities.</value>
        public DbSet<DisputeEntity> DisputeEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="InsolvencyOrderEntity" />.
        /// </summary>
        /// <value>The insolvency order entities.</value>
        public DbSet<InsolvencyOrderEntity> InsolvencyOrderEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="InsolvencyOrderFlattenedEntity" />.
        /// </summary>
        /// <value>The insolvency order flattened entities.</value>
        public DbSet<InsolvencyOrderFlattenedEntity> InsolvencyOrderFlattenedEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="InsolvencyOrderAddressEntity" />.
        /// </summary>
        /// <value>The insolvency order address entities.</value>
        public DbSet<InsolvencyOrderAddressEntity> InsolvencyOrderAddressEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="InsolvencyOrderDisputeEntity" />.
        /// </summary>
        /// <value>The insolvency order dispute entities.</value>
        public DbSet<InsolvencyOrderDisputeEntity> InsolvencyOrderDisputeEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="InsolvencyOrderHistoryEntity" />.
        /// </summary>
        /// <value>The insolvency order history entities.</value>
        public DbSet<InsolvencyOrderHistoryEntity> InsolvencyOrderHistoryEntities { get; set; }

        /// <summary>
        /// Gets or sets a DBSet for querying  <see cref="InsolvencyOrderHistoryEntity" />.
        /// </summary>
        /// <value>The insolvency order person entities.</value>
        public DbSet<InsolvencyOrderPersonEntity> InsolvencyOrderPersonEntities { get; set; }

        /// <summary>
        /// Gets or sets the insolvency order status entities.
        /// </summary>
        /// <value>The insolvency order status entities.</value>
        public DbSet<InsolvencyOrderStatusEntity> InsolvencyOrderStatusEntities { get; set; }

        /// <summary>
        /// Gets or sets the insolvency order type entities.
        /// </summary>
        /// <value>The insolvency order type entities.</value>
        public DbSet<InsolvencyOrderTypeEntity> InsolvencyOrderTypeEntities { get; set; }

        /// <summary>
        /// Gets or sets the insolvency restrictions type entities.
        /// </summary>
        /// <value>The insolvency restrictions type entities.</value>
        public DbSet<InsolvencyRestrictionsTypeEntity> InsolvencyRestrictionsTypeEntities { get; set; }

        /// <summary>
        /// Gets or sets the insolvency trading details entities.
        /// </summary>
        /// <value>The insolvency trading details entities.</value>
        public DbSet<InsolvencyTradingDetailsEntity> InsolvencyTradingDetailsEntities { get; set; }

#if DEBUG

        /// <summary>
        /// Overrides the entity onConfiguring using fluent API
        /// </summary>
        /// <param name="optionsBuilder"> <see cref="DbContextOptionsBuilder"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseLoggerFactory(loggerFactory);
        }

#endif

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context.
        /// The resulting model may be cached and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.
        /// Databases (and other extensions) typically define extension methods on this object that allow
        /// you to configure aspects of the model that are specific to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context
        /// (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourtEntity>(entity =>
            {
                entity.HasKey(e => e.CourtId);
                entity.ToTable("vwCourt");
                entity.Property(e => e.CourtId);
                entity.Property(e => e.CourtName);
                entity.Property(e => e.CourtCode);
                entity.Property(e => e.District);
            });

            modelBuilder.Entity<DisputeEntity>(entity =>
            {
                entity.HasKey(e => e.DisputeId);
                entity.ToTable("vwDispute");
                entity.Property(e => e.DisputeId);
                entity.Property(e => e.DateRaised).HasColumnName("DisputeDate");
                entity.Property(e => e.RefNum);
                entity.HasMany(e => e.InsolvencyOrderDisputes);
            });

            modelBuilder.Entity<InsolvencyOrderAddressEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderAddressId);
                entity.ToTable("vwInsolvencyOrderAddress");
                entity.Property(e => e.InsolvencyOrderAddressId);
                entity.Property(e => e.LastKnownAddress);
                entity.Property(e => e.LastKnownPostCode);
            });

            modelBuilder.Entity<InsolvencyOrderDisputeEntity>(entity =>
            {
                entity.ToTable("vwInsolvencyOrderDispute");
                entity.HasKey(e => new { e.InsolvencyOrderId, e.DisputeId });
                entity
                    .HasOne(e => e.InsolvencyOrder)
                    .WithMany(e => e.InsolvencyOrderDisputes)
                    .HasForeignKey(e => e.InsolvencyOrderId);
                entity
                    .HasOne(e => e.Dispute)
                    .WithMany(e => e.InsolvencyOrderDisputes)
                    .HasForeignKey(e => e.DisputeId);
            });

            modelBuilder.Entity<InsolvencyOrderEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderId);
                entity.ToTable("vwInsolvencyOrder");
                entity.Property(e => e.InsolvencyOrderId);
                entity.Property(e => e.InsolvencyOrderTypeId);
                entity.Property(e => e.ResidenceId);
                entity.Property(e => e.OrderDate);
                entity.Property(e => e.RestrictionsTypeId);
                entity.Property(e => e.RestrictionsStartDate);
                entity.Property(e => e.RestrictionsEndDate);
                entity.Property(e => e.LineOfBusiness);
                entity.Property(e => e.ValueOfDebt);
                entity.Property(e => e.DischargeDate);
                entity.Property(e => e.OnlineSuppressed);
                entity
                    .HasOne(e => e.InsolvencyOrderType)
                    .WithMany(e => e.InsolvencyOrderEntities)
                    .HasForeignKey(e => e.InsolvencyOrderTypeId);
                entity
                    .HasOne(e => e.InsolvencyRestrictionsType)
                    .WithMany(e => e.InsolvencyOrderEntities)
                    .HasForeignKey(e => e.RestrictionsTypeId);
            });

            modelBuilder.Entity<InsolvencyOrderFlattenedEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderId);
                entity.ToTable("vwInsolvencyOrderFlattened");
            });

            modelBuilder.Entity<InsolvencyOrderHistoryEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderHistoryId);
                entity.ToTable("vwInsolvencyOrderHistory");
                entity.Property(e => e.InsolvencyOrderHistoryId);
                entity.Property(e => e.CaseReference);
                entity.Property(e => e.CaseYear);
            });

            modelBuilder.Entity<InsolvencyOrderPersonEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderPersonId);
                entity.ToTable("vwInsolvencyOrderPerson");
                entity.Property(e => e.InsolvencyOrderPersonId);
                entity.Property(e => e.DateOfBirth);
                entity.Property(e => e.Forename);
                entity.Property(e => e.Surname);
                entity.Property(e => e.Title);
            });

            modelBuilder.Entity<InsolvencyOrderStatusEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderStatusId);
                entity.ToTable("vwInsolvencyOrderStatus");
                entity.Property(e => e.InsolvencyOrderStatusId);
                entity.Property(e => e.Description);
                entity.Property(e => e.StatusAggregate);
            });

            modelBuilder.Entity<InsolvencyOrderTypeEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyOrderTypeId);
                entity.ToTable("vwInsolvencyOrderType");
                entity.Property(e => e.InsolvencyOrderTypeId);
                entity.Property(e => e.CallReportCode);
                entity.Property(e => e.Description);
                entity.HasMany(x => x.InsolvencyOrderEntities);
            });

            modelBuilder.Entity<InsolvencyRestrictionsTypeEntity>(entity =>
            {
                entity.HasKey(e => e.RestrictionsTypeId);
                entity.ToTable("vwInsolvencyRestrictionsType");
                entity.Property(e => e.RestrictionsTypeId);
                entity.Property(e => e.Code);
                entity.Property(e => e.Description);
            });

            modelBuilder.Entity<InsolvencyTradingDetailsEntity>(entity =>
            {
                entity.HasKey(e => e.InsolvencyTradingId);
                entity.ToTable("vwInsolvencyTradingDetails");
                entity.Property(e => e.InsolvencyTradingId);
                entity.Property(e => e.TradingAddress);
                entity.Property(e => e.TradingName);
            });
        }
    }
}
