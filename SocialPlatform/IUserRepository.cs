namespace SocialPlatform;

public interface IUserRepository
{
    User? FindByEmail(string email);
    User Save(User user);
}