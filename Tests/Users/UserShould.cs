using Common.CustomClasses;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
namespace Tests.Users;

public class UserShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformUsersMethods()
    {
        // Arrange: Authenticate first
        var userData = await LoginDefault();

        //Validation: Try creating with username contain white space, expect failure
        var createRequest = new UserRequest
        {

            FirstName = "John",
            LastName = "Doe",
            Username = "newu ser",
            Password = "password123",
            ConfirmPassword = "password123",
            Role = "Admin",
            Access = "Main"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the update
        var exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
           await Connect.User.Create(createRequest));

        // Assert: Check the exception message
        Assert.Contains("Username must not contain spaces.", exceptionCreate.Message);

        // Arrange: Create user
        createRequest = new UserRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "newuser",
            Password = PasswordHasher.Hash("password123"),
            ConfirmPassword = PasswordHasher.Hash("password123"),
            Role = "Admin",
            Access = "Main"
        };

        var createResult = await Connect.User.Create(createRequest);
        Assert.True(createResult.IsSuccessful);
        var userId = createResult.Data;

        // Act & Assert: Verify user was created
        var model = await Connect.User.Get(userId);
        Assert.NotNull(model);
        Assert.Equal("John", model.Data.FirstName);
        Assert.Equal("Doe", model.Data.LastName);
        Assert.Equal("newuser", model.Data.Username);

        // Validation: Try creating another user with existing profile, expect failure
        var duplicateProfileRequest = new UserRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "newuser2",
            Password = PasswordHasher.Hash("password1234"),
            ConfirmPassword = PasswordHasher.Hash("password1234"),
            Role = "Admin",
            Access = "Main"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the duplicate
         exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
            await Connect.User.Create(duplicateProfileRequest));

        // Assert: Check the exception message
        Assert.Contains("User with the same information already exists.", exceptionCreate.Message);

        // Validation: Try creating another user with existing username, expect failure
        var duplicateUsernameRequest = new UserRequest
        {
            FirstName = "Jane",
            LastName = "Doe",
            Username = "newuser",
            Password = "password1234",
            ConfirmPassword = "password1234",
            Role = "Admin",
            Access = "Main"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the duplicate
        exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
        await Connect.User.Create(duplicateUsernameRequest));

        // Assert: Check the exception message
        Assert.Contains("Username not available.", exceptionCreate.Message);


        // Arrange: Create Different user
        var newUserRequest = new UserRequest
        {
            FirstName = "Jane",
            LastName = "Doe",
            Username = "newuser2",
            Password = "password1234",
            ConfirmPassword = "password1234",
            Role = "Admin",
            Access = "Main"
        };

        var newUserResult = await Connect.User.Create(newUserRequest);
        Assert.True(newUserResult.IsSuccessful);
        var newUserId = newUserResult.Data;

        // Act & Assert: Verify user was created
        var newUserModel = await Connect.User.Get(newUserId);
        Assert.NotNull(newUserModel.Data);
        Assert.Equal("Jane", newUserModel.Data.FirstName);
        Assert.Equal("Doe", newUserModel.Data.LastName);
        Assert.Equal("newuser2", newUserModel.Data.Username);


        // GET List
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 10,
            SortField = nameof(User.LastName),
            SortDir = DataGridQuerySortDirection.Ascending
        };

        var models = await Connect.User.List(listQuery);
        Assert.NotNull(models);
        Assert.True(models.IsSuccessful);
        Assert.True(models.Data.List.Any());

        //Validation: Try updating with existing profile, expect failure
        var updateProfileModel = new UserUpdateProfile
        {
            Id = newUserId,
            FirstName = "John",
            LastName = "Doe"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the update
        var exceptionUpdate = await Assert.ThrowsAsync<ResponseException>(async () =>
            await Connect.User.UpdateProfile(updateProfileModel));

        // Assert: Check the exception message
        Assert.Contains("User with the same information already exist.", exceptionUpdate.Message);


        //Validation: Try updating with username, expect failure
        var updateUserCredentialModel = new UserUpdateCredential
        {
            Id = newUserId,
            Username = "newuser"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the update
        exceptionUpdate = await Assert.ThrowsAsync<ResponseException>(async () =>
           await Connect.User.UpdateCredential(updateUserCredentialModel));

        // Assert: Check the exception message
        Assert.Contains("Username not available.", exceptionUpdate.Message);


        //Validation: Try updating with username contain white space, expect failure
        updateUserCredentialModel = new UserUpdateCredential
        {
            Id = newUserId,
            Username = "new user"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the update
        exceptionUpdate = await Assert.ThrowsAsync<ResponseException>(async () =>
           await Connect.User.UpdateCredential(updateUserCredentialModel));

        // Assert: Check the exception message
        Assert.Contains("Username must not contain spaces.", exceptionUpdate.Message);


        // Arrange: Update user profile
        var updateProfile = new UserUpdateProfile
        {
            Id = userId,
            FirstName = "John Updated",
            LastName = "Doe Updated"
        };

        var updatedProfile = await Connect.User.UpdateProfile(updateProfile);
        Assert.True(updatedProfile.IsSuccessful);


        // Act & Assert: Verify user profile was updated
        var updatedProfileResult = await Connect.User.Get(userId);
        Assert.NotNull(updatedProfileResult);
        Assert.Equal("John Updated", updatedProfileResult.Data.FirstName);
        Assert.Equal("Doe Updated", updatedProfileResult.Data.LastName);

        // Arrange: Update user access role
        var updateAccessRole = new UserUpdateAccessRole
        {
            Id = userId,
            Role = "Registrar",
            Access = "Basey"
        };

        var updatedAccessRole = await Connect.User.UpdateAccess(updateAccessRole);
        Assert.True(updatedAccessRole.IsSuccessful);

        // Act & Assert: Verify user access role was updated
        var updatedAccessRoleResult = await Connect.User.Get(userId);
        Assert.NotNull(updatedAccessRoleResult);
        Assert.Equal("Registrar", updatedAccessRoleResult.Data.Role);
        Assert.Equal("Basey", updatedAccessRoleResult.Data.Access);

        // Arrange: Update user credential
        var updateCredential = new UserUpdateCredential
        {
            Id = userId,
            Username = "newuser123",
            Password = "newpassword123"
        };

        var updatedCredential = await Connect.User.UpdateCredential(updateCredential);
        Assert.True(updatedCredential.IsSuccessful);

        // Act & Assert: Verify user credential was updated
        var newLoginRequest = new LoginRequest { Username = "newuser123", Password = "newpassword123" };
        var newLoginResult = await Connect.Authentication.Login(newLoginRequest);
        Assert.NotNull(newLoginResult);
        Assert.True(newLoginResult.IsSuccessful);

        // Arrange: Delete user
        var deleteResult = await Connect.User.Delete(userId);
        Assert.True(deleteResult.IsSuccessful);

        // Act & Assert: Verify user was deleted
        models = await Connect.User.List(listQuery);
        Assert.NotNull(models);
        Assert.DoesNotContain(models.Data.List, x => x.Id == model.Data.Id);
    }

}
