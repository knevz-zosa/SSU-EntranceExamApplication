using Common.Requests;
using Domain.Entities;

namespace Tests.Transfers;
public class TransferShould : TestBaseIntegration
{
    [Fact]
    public async Task PerformTransfersMethods()
    {
        var user = await LoginDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();
        var schedules = await SchedulesDefault();
        var courses = schedules.Data.List.Select(x => x.Campus.Courses).ToArray();

        //Get applicant
        var applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Schedule);

        //Get applicant old schedule id
        var oldScheduleId = applicantModel.Data.Schedule.Id;
        Assert.Equal(oldScheduleId, applicantModel.Data.ScheduleId);

        // Arrange: transfer applicant to another schedule
        var applicantNewSchedule = new ApplicantTransfer()
        {
            Id = applicantModel.Data.Id,
            ScheduleId = schedules.Data.List.ToArray()[1].Id,
            CourseId = applicantModel.Data.CourseId
        };
        var applicantNewScheduleModel = await Connect.Applicant.Transfer(applicantNewSchedule);
        Assert.True(applicantNewScheduleModel.IsSuccessful);

        // Assert: Verify applicant's schedule have changed
        applicantModel = await Connect.Applicant.Get(applicant.Id);
        Assert.NotNull(applicantModel);
        Assert.NotNull(applicantModel.Data.Schedule);
        Assert.NotEqual(oldScheduleId, applicantModel.Data.ScheduleId);
    }
}
