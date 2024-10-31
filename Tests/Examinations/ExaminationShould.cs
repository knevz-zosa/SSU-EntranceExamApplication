using Common.Requests;

namespace Tests.Examinations;
public class ExaminationShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformExaminationsMethods()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        // Arrange: Create Examination
        var examRequest = new ExaminationRequest
        {
            ApplicantId = applicant.Id,
            IntelligenceRawScore = 80,
            MathRawScore = 33,
            ReadingRawScore = 35,
            ScienceRawScore = 34,
            RecordedBy = $"{user.Data.FirstName}, {user.Data.LastName}"
        };
        var examResult = await Connect.Examination.Create(examRequest);
        Assert.True(examResult.IsSuccessful);
        var examId = examResult.Data;

        // Act & Assert: Verify exam was created
        var examModel = await Connect.Examination.Get(examId);
        Assert.NotNull(examModel);
        Assert.Equal(1, examModel.Data.Id);

        // Act & Assert: Verify applicant's exam result was created
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.Equal(1, applicantModel.Data.Id);

        // Arrange: Update examination
        var updateModel = new ExaminationUpdate
        {
            Id = examId,
            IntelligenceRawScore = 99,
            MathRawScore = 35,
            ReadingRawScore = 25,
            ScienceRawScore = 30
        };
        var updatedModel = await Connect.Examination.Update(updateModel);
        Assert.True(updatedModel.IsSuccessful);

        // Act & Assert: Verify applicant's exam result was updated
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.Equal(1, applicantModel.Data.Id);
    }
}
