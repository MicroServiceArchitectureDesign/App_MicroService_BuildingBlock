namespace AppMicroServiceBuildingBlock.Contract.InfrastructureContracts.BaseConfigurations;

public abstract class NotificationConfiguration<TEntity> : BaseConfiguration<TEntity>
    where TEntity : class
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.ConfigureWithSchema(builder, Schemas.NotificationSchema);
    }
}
