namespace Toyer.Data.Entities;

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly CreationDate { get; set; } = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    public DateTime? LastRegistrationDate { get; set; }

    public string? UserFK { get; set; }

    public int DeviceTypeId { get; set; }
    public DeviceType DeviceType { get; set; }

}
