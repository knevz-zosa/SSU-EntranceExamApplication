using Common.CustomClasses;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace Tests.Courses;
public class CourseShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformCoursesMethods()
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

        // Arrange: Create Course
        var courseRequest = new CourseRequest
        {
            Name = "Course123",
            CampusId = campusId,
            DepartmentId = departmentId,
            IsOpen = true,
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"

        };

        var courseResult = await Connect.Course.Create(courseRequest);
        Assert.True(courseResult.IsSuccessful);
        var courseId = courseResult.Data;

        // Act & Assert: Verify course was created
        var courseModel = await Connect.Course.Get(courseId);
        Assert.NotNull(courseModel.Data);
        Assert.Equal("Course123", courseModel.Data.Name);

        // GET List
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 10,
            SortField = nameof(Course.Name),
            SortDir = DataGridQuerySortDirection.Ascending
        };

        var courseModels = await Connect.Course.List(listQuery, "All");
        Assert.NotNull(courseModels);
        Assert.True(courseModels.IsSuccessful);
        Assert.True(courseModels.Data.List.Any());
        Assert.Contains(courseModels.Data.List, x => x.Id == courseModel.Data.Id);


        // Validation: Try creating another course with the same name in the same campus, expect failure
        var duplicateRequest = new CourseRequest
        {
            Name = "Course123",
            CampusId = campusId,
            DepartmentId = departmentId,
            IsOpen = true,
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the duplicate
        var exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
            await Connect.Course.Create(duplicateRequest));

        // Assert: Check the exception message
        Assert.Contains("Course with this name already exists.", exceptionCreate.Message);

        // Arrange: Create Different Course
        var newCourseRequest = new CourseRequest
        {
            Name = "Course1234",
            CampusId = campusId,
            DepartmentId= departmentId,
            IsOpen = true,
            CreatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        var newCourseResult = await Connect.Course.Create(newCourseRequest);
        Assert.True(newCourseResult.IsSuccessful);
        var newCourseId = newCourseResult.Data;

        // Act & Assert: Verify department was created
        var newCourseModel = await Connect.Course.Get(newCourseId);
        Assert.NotNull(newCourseModel.Data);
        Assert.Equal("Course1234", newCourseModel.Data.Name);

        //Validation: Try updating another course to the name "Course123", expect failure
        var updateModel = new CourseUpdate
        {
            Id = newCourseId,
            Name = "Course123",
            CampusId = campusId,
            DepartmentId = departmentId,
            IsOpen = true,
            UpdatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        // Act & Assert: Expect a ResponseException to be thrown for the update
        var exceptionUpdate = await Assert.ThrowsAsync<ResponseException>(async () =>
            await Connect.Course.Update(updateModel));

        // Assert: Check the exception message
        Assert.Contains("Course name already exists.", exceptionUpdate.Message);

        // Arrange: Update course
        updateModel = new CourseUpdate
        {
            Id = newCourseId,
            Name = "Course12345",
            CampusId = campusId,
            DepartmentId = departmentId,
            IsOpen = true,
            UpdatedBy = $"{userData.Data.FirstName} {userData.Data.LastName}"
        };

        var updatedModel = await Connect.Course.Update(updateModel);
        Assert.True(updatedModel.IsSuccessful);

        // Act & Assert: Verify course was updated
        var updatedModelResult = await Connect.Course.Get(newCourseId);
        Assert.NotNull(updatedModelResult.Data);
        Assert.Equal("Course12345", updatedModelResult.Data.Name);

        // Arrange: Delete course
        var deleteModel = await Connect.Course.Delete(newCourseId);
        Assert.True(deleteModel.IsSuccessful);

        // Act & Assert: Verify course was deleted
        courseModels = await Connect.Course.List(listQuery, string.Empty);
        Assert.NotNull(courseModels);
        Assert.DoesNotContain(courseModels.Data.List, x => x.Id == courseModel.Data.Id);
    }
}
