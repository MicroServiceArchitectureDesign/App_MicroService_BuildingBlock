using AppMicroServiceBuildingBlock.Contract.DomainContracts.Enumerations.Base;

namespace AppMicroServiceBuildingBlock.Contract.DomainContracts.Enumerations;

public record UnitType : BaseEnumeration<UnitType>
{
    public UnitType(int id, string name, string enName)
        : base(id, name, enName)
    {

    }

    public static UnitType M = new(1, "متر", nameof(M));
    public static UnitType Cm = new(2, "سانتی متر", nameof(Cm));
    public static UnitType Mm = new(3, "میلی متر", nameof(Mm));
    public static UnitType G = new(4, "گِرَم", nameof(G));
    public static UnitType Kg = new(5, "کیلوگرم", nameof(Kg));
    public static UnitType Pound = new(6, "پوند", nameof(Pound));
}
