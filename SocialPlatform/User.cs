namespace SocialPlatform;

public class User
{
    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? Password { get; private set; }

    public Role Role { get; private set; }

    public SocialMediaService? SignInProvider { get; set; }

    public static UserBuilder GetBuilder()
        => new();

    public sealed class UserBuilder
    {
        private readonly User user;

        public UserBuilder()
            => user = new User
            {
                Role = Role.RoleUser
            };

        public UserBuilder WithEmail(string email)
        {
            user.Email = email;

            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            user.FirstName = firstName;

            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            user.LastName = lastName;

            return this;
        }

        public UserBuilder WithPassword(string? password)
        {
            user.Password = password;

            return this;
        }

        public UserBuilder WithSignInProvider(SocialMediaService? signInProvider)
        {
            user.SignInProvider = signInProvider;

            return this;
        }

        public User Build()
            => user;
    }
}