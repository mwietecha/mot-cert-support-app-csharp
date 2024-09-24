namespace Timesheet.Test.Api;

public class Login(string email, string password)
{
    public string email { get; set; } = email;
    public string password { get; set; } = password;
}