namespace ShareYourSword.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
