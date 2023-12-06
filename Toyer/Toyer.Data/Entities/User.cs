namespace Toyer.Data.Entities;

public class User
{
    public Guid Guid { get; set; }
    public string Login { get; set; }
    public BinaryData Password { get; set; }
    public DateTime AccCreationDate { get; set; }
    public Guid PersonalInfoId { get; set; }
}
