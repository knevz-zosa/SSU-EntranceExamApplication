using Common.CustomClasses;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace Tests.Departments;

public class DepartmentShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformDepartmentsMethod()
    {
        // Arrange: Authenticate first
        var userData = await LoginDefault();

        // Arrange: Create Campus
        var campusRequest = new CampusRequest
        {
            Name = "Campus123",
            HasDepartment = false,
            Address = "Current Address",
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"

        };

        var campusResult = await Connect.Campus.Create(campusRequest);
        Assert.True(campusResult.IsSuccessful);
        var campusId = campusResult.Data;

        // Act & Assert: Verify campus was created
        var campusModel = await Connect.Campus.Get(campusId);
        Assert.NotNull(campusModel.Data);
        Assert.Equal("Campus123", campusModel.Data.Name);

        // Arrange: Create Department
        var departmentRequest = new DepartmentRequest
        {
            Name = "Department123",
            CampusId = campusId,
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"

        };

        var departmentResult = await Connect.Department.Create(departmentRequest);
        Assert.True(departmentResult.IsSuccessful);
        var departmentId = departmentResult.Data;

        // Act & Assert: Verify department was created
        var departmentModel = await Connect.Department.Get(departmentId);
        Assert.NotNull(departmentModel.Data);
        Assert.Equal("Department123", departmentModel.Data.Name);

        // GET List
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 10,
            SortField = nameof(Department.Name),
            SortDir = DataGridQuerySortDirection.Ascending
        };

        var departmentModels = await Connect.Department.List(listQuery);
        Assert.NotNull(departmentModels);
        Assert.True(departmentModels.IsSuccessful);
        Assert.True(departmentModels.Data.List.Any());
        Assert.Contains(departmentModels.Data.List, x => x.Id == departmentModel.Data.Id);

        // Validation: Try creating another department with the same name, expect failure
        var duplicateRequest = new DepartmentRequest
        {
            Name = "Department123",
            CampusId = campusId,
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the duplicate
        var exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
            await Connect.Department.Create(duplicateRequest));

        // Assert: Check the exception message
        Assert.Contains("Department with this name already exists.", exceptionCreate.Message);

        // Arrange: Create Different Department
        var newDepartmentRequest = new DepartmentRequest
        {
            Name = "Department1234",
            CampusId = campusId,
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        var newDepartmentResult = await Connect.Department.Create(newDepartmentRequest);
        Assert.True(newDepartmentResult.IsSuccessful);
        var newDepId = newDepartmentResult.Data;

        // Act & Assert: Verify department was created
        var newDepartmentModel = await Connect.Department.Get(newDepId);
        Assert.NotNull(newDepartmentModel.Data);
        Assert.Equal("Department1234", newDepartmentModel.Data.Name);

        //Validation: Try updating another department to the name "Department123", expect failure
        var updateModel = new DepartmentUpdate
        {
            Id = newDepId,
            CampusId = campusId,
            Name = "Department123",
            UpdatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the update
        var exceptionUpdate = await Assert.ThrowsAsync<ResponseException>(async () =>
            await Connect.Department.Update(updateModel));

        // Assert: Check the exception message
        Assert.Contains("Department name already exists.", exceptionUpdate.Message);

        // Arrange: Update department
        updateModel = new DepartmentUpdate
        {
            Id = departmentId,
            Name = "Department12345",
            CampusId = campusId,            
            UpdatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        var updatedModel = await Connect.Department.Update(updateModel);
        Assert.True(updatedModel.IsSuccessful);

        // Act & Assert: Verify department was updated
        var updatedModelResult = await Connect.Department.Get(departmentId);
        Assert.NotNull(updatedModelResult.Data);
        Assert.Equal("Department12345", updatedModelResult.Data.Name);

        // Arrange: Delete department
        var deleteModel = await Connect.Department.Delete(departmentId);
        Assert.True(deleteModel.IsSuccessful);

        // Act & Assert: Verify department was deleted
        departmentModels = await Connect.Department.List(listQuery);
        Assert.NotNull(departmentModels);
        Assert.DoesNotContain(departmentModels.Data.List, x => x.Id == departmentModel.Data.Id);
    }
}
