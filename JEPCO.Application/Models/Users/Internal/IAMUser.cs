namespace JEPCO.Application.Models.Users.Internal;

public class IAMUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public bool Enabled { get; set; }
}
