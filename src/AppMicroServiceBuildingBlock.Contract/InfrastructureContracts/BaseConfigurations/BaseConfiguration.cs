namespace AppMicroServiceBuildingBlock.Contract.InfrastructureContracts.BaseConfigurations;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureWithSchema(builder, Schemas.BaseSchema);
    }

    public virtual void ConfigureWithSchema(EntityTypeBuilder<TEntity> builder, string schema = Schemas.BaseSchema,
        string tableNamePrefix = Schemas.TableNamePrefix)
    {
        builder.ToTable($"{tableNamePrefix}{typeof(TEntity).Name}", schema);
    }
}
