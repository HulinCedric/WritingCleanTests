namespace SocialPlatform;

public interface IPasswordEncoder
{
    string? Encode(string? password);
}