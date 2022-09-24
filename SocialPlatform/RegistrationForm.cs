namespace SocialPlatform;

public class RegistrationForm
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Password { get; set; }

    public string? PasswordVerification { get; set; }

    public SocialMediaService? SignInProvider { get; set; }

    public bool IsNormalRegistration()
        => SignInProvider == null;

    public bool IsSocialSignIn()
        => SignInProvider != null;
}