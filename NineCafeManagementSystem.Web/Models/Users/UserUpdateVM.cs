public class UserUpdateVM
{
    public string Id { get; set; } = string.Empty;

    [Required, Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required, Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Phone, Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    // No Role field 
    [DataType(DataType.Password)]
    [Display(Name = "New Password (leave blank to keep current)")]
    public string? Password { get; set; } 
}