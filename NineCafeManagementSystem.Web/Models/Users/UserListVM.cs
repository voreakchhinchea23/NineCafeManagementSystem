namespace NineCafeManagementSystem.Web.Models.Users
{
    public class UserListVM
    {
        public string Id { get; set; } = string.Empty;
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "Role")]
        public string Role { get; set; } = "None";

    }
}
