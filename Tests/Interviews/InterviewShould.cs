using Common.CustomClasses;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace Tests.Interviews;
public class InterviewShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformInterviewsMethods()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();
        var schedule = await SchedulesDefault();
        var courses = schedule.Data.List.Select(x => x.Campus.Courses).ToArray();

        // Arrange: Create Interview
        var interviewRequest = new InterviewRequest
        {
            ApplicantId = applicant.Id,
            CourseId = courses[0][0].Id,
            InterviewReading = 80,
            InterviewCommunication = 85,
            InterviewAnalytical = 85,
            InterviewDate = new DateTime(2024, 7, 4),
            Interviewer = "Mac Fly",
            RecordedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };
        var interviewResult = await Connect.Interview.Create(interviewRequest);
        Assert.True(interviewResult.IsSuccessful);
        var interviewId = interviewResult.Data;

        // Act & Assert: Verify interview was created
        var interviewModel = await Connect.Interview.Get(interviewId);
        Assert.NotNull(interviewModel);
        Assert.Equal(interviewId, interviewModel.Data.Id);

        // Act & Assert: Verify applicant's interview result was created
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(interviewId, applicantModel.Data.Interviews.Select(x => x.Id));

        // Arrange: Update interview set to active
        var updateActiveModel = new InterviewActiveUpdate()
        {
            Id = interviewId,
            IsUse = true,
            UpdatedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };
        var updatedActiveModel = await Connect.Interview.Activate(updateActiveModel);
        Assert.True(updatedActiveModel.IsSuccessful);

        // Act & Assert: Verify applicant's selected active result 
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(true, applicantModel.Data.Interviews.Select(x => x.IsUse));

        // Arrange: Update interview set to inactive
        updateActiveModel = new InterviewActiveUpdate()
        {
            Id = interviewId,
            IsUse = false,
            UpdatedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };
        updatedActiveModel = await Connect.Interview.Activate(updateActiveModel);
        Assert.True(updatedActiveModel.IsSuccessful);

        // Act & Assert: Verify applicant's selected active result 
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(false, applicantModel.Data.Interviews.Select(x => x.IsUse));


        // Arrange: Update interview rating
        var updateRatingModel = new InterviewRatingUpdate()
        {
            Id = interviewId,
            InterviewReading = 90,
            InterviewCommunication = 80,
            InterviewAnalytical = 90,
            Interviewer = "Mac Fly",
            UpdatedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };
        var updatedRatingModel = await Connect.Interview.UpdateRating(updateRatingModel);
        Assert.True(updatedRatingModel.IsSuccessful);

        // Arrange: Create another Interview with the same course within the same campus, expect failure
        var existingInterview = new InterviewRequest
        {
            ApplicantId = applicant.Id,
            CourseId = courses[0][0].Id,
            InterviewReading = 85,
            InterviewCommunication = 80,
            InterviewAnalytical = 80,
            InterviewDate = new DateTime(2024, 7, 4),
            Interviewer = "Mac Fly",
            RecordedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };
       var  exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
                await Connect.Interview.Create(existingInterview));

        // Assert: Check the exception message
        Assert.Contains("Applicant has already been interviewed in this program.", exceptionCreate.Message);

        // Arrange: Create another Interview 
        var interviewRequest2 = new InterviewRequest
        {
            ApplicantId = applicant.Id,
            CourseId = courses[0][1].Id,
            InterviewReading = 90,
            InterviewCommunication = 95,
            InterviewAnalytical = 85,
            InterviewDate = new DateTime(2024, 7, 4),
            Interviewer = "Mac Fly",
            RecordedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };

        var interviewResult2 = await Connect.Interview.Create(interviewRequest2);
        Assert.True(interviewResult2.IsSuccessful);
        var interviewId2 = interviewResult2.Data;

        // Act & Assert: Verify another interview was created
        var interviewModel2 = await Connect.Interview.Get(interviewId2);
        Assert.NotNull(interviewModel2);
        Assert.Equal(interviewId2, interviewModel2.Data.Id);

        // Act & Assert: Verify applicant's interview result was created
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Interviews);
        Assert.Contains(interviewId, applicantModel.Data.Interviews.Select(x => x.Id));
        Assert.Contains(interviewId2, applicantModel.Data.Interviews.Select(x => x.Id));
    }
}
