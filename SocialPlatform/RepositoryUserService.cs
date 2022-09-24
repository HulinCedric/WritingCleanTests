namespace SocialPlatform;

public class RepositoryUserService
{
    private readonly IPasswordEncoder passwordEncoder;
    private readonly IUserRepository repository;

    public RepositoryUserService(
        IPasswordEncoder passwordEncoder,
        IUserRepository repository)
    {
        this.passwordEncoder = passwordEncoder;
        this.repository = repository;
    }

    public User RegisterNewUserAccount(RegistrationForm userAccountData)
    {
        if (EmailExist(userAccountData.Email))
            throw new DuplicateEmailException("The email address: " + userAccountData.Email + " is already in use.");

        var encodedPassword = EncodePassword(userAccountData);

        var registered = User.GetBuilder()
            .WithEmail(userAccountData.Email)
            .WithFirstName(userAccountData.FirstName)
            .WithLastName(userAccountData.LastName)
            .WithPassword(encodedPassword)
            .WithSignInProvider(userAccountData.SignInProvider)
            .Build();

        return repository.Save(registered);
    }

    private bool EmailExist(string email)
    {
        var user = repository.FindByEmail(email);

        if (user != null)
            return true;

        return false;
    }

    private string? EncodePassword(RegistrationForm dto)
    {
        string? encodedPassword = null;

        if (dto.IsNormalRegistration())
            encodedPassword = passwordEncoder.Encode(dto.Password);

        return encodedPassword;
    }
}