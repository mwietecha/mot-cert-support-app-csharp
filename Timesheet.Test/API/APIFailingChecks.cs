namespace Timesheet.Test.Api;

using System.Net;
using Timesheet.Models.Auth;
using Timesheet.Models.User;

using static RestAssured.Dsl;

public class APIFailingChecks {

    private string token; // Private variable to store authentication token.

    [SetUp] // Annotation indicating that this method runs before each test.
    public void getToken() {
        // Retrieves credentials by sending a POST request to the login endpoint.
        // The credentials consist of an email and password provided in JSON format.
        Credentials credentials = (Credentials)Given()
                .Body("{\"email\":\"user@test.com\",\"password\":\"password123\"}")
                .ContentType("application/json")
                .Post("/v1/auth/login")
                .DeserializeTo(typeof (Credentials));

        // Stores the authentication token obtained from the credentials.
        token = credentials.Token;
    }

    [Test] // Annotation indicating that this method is a test case.
    public void testUserDetails() {
        // Sends a GET request to retrieve user details using the stored token for authorization.
        // Expects the returned User object's username to be "update@me.com".
        User user = (User)Given()
                .Header("Authorization", "Bearer " + token)
                .Get("/v1/user/1")
                .DeserializeTo(typeof (User));

        Assert.That(user.Username, Is.EqualTo("update@me.com")); // Asserts the retrieved username.
    }

    [Test] // Annotation indicating another test case.
    public void positiveResponseWhenDeletingUser() {
        // Creates a new user by sending a POST request with user details in JSON format.
        // Uses the stored token for authorization in the header.
        User user = (User)Given()
                .Body(new User("delete-me", "delete@me.com", "password123", null))
                .ContentType("application/json")
                .Header("Authorization", "Bearer " + token)
                .Post("/v1/user")
                .DeserializeTo(typeof (User));

        // Sends a DELETE request to delete the user using the stored user's ID in the URL.
        HttpResponseMessage response = Given()
                .Header("Authorization", "Bearer " + token)
                .Delete("/v1/user/" + user.Id)
                .Then()
                .Extract()
                .Response();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Accepted)); // Asserts the expected HTTP status code.
    }
}