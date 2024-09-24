namespace Timesheet.Test.Unit;
using Timesheet.DB;
using Timesheet.Models.Auth;

public class AuthDBTest
{
    private AuthDB _authDB = new();

    [SetUp]
    public void Setup()
    {
        PrepareDB prepareDB = new();
        prepareDB.SeedUsers();
    }

    [Test]
    public void CheckLoginReturnsPositiveResult() {
        LoginResult loginResult = _authDB.CheckLogin("admin@test.com", "password123");

        Assert.That(loginResult.isUser, Is.True);
    }
} 