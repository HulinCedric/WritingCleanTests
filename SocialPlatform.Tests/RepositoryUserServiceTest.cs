namespace SocialPlatform.Tests;

public class RepositoryUserServiceTest
{
    private readonly IPasswordEncoder passwordEncoderMock;

    private readonly IUserRepository repositoryMock;

    private readonly RepositoryUserService service;

    public RepositoryUserServiceTest()
    {
        repositoryMock = Substitute.For<IUserRepository>();
        passwordEncoderMock = Substitute.For<IPasswordEncoder>();

        service = new RepositoryUserService(passwordEncoderMock, repositoryMock);
    }

    [Fact]
    public void RegisterNewUserAccountByUsingSocialSignIn()
    {
        var form = new RegistrationForm
        {
            Email = "john.smith@gmail.com",
            FirstName = "John",
            LastName = "Smith",
            SignInProvider = SocialMediaService.Twitter
        };

        repositoryMock.FindByEmail("john.smith@gmail.com").ReturnsNull();
        repositoryMock.Save(Arg.Any<User>()).Returns(x => (User)x[0]);

        var modelObject = service.RegisterNewUserAccount(form);

        Assert.Equal("john.smith@gmail.com", modelObject.Email);
        Assert.Equal("John", modelObject.FirstName);
        Assert.Equal("Smith", modelObject.LastName);
        Assert.Equal(SocialMediaService.Twitter, modelObject.SignInProvider);
        Assert.Equal(Role.RoleUser, modelObject.Role);
        Assert.Null(modelObject.Password);

        repositoryMock.Received(1).FindByEmail("john.smith@gmail.com");
        repositoryMock.Received(1).Save(modelObject);
        Assert.Equal(2, repositoryMock.ReceivedCalls().Count());

        Assert.Empty(passwordEncoderMock.ReceivedCalls());
    }
}