namespace JEPCO.Shared.Models
{
    public class CurrentUserInformation
    {
        public Guid UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
