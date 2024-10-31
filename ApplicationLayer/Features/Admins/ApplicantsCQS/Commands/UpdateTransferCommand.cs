using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
public class UpdateTransferCommand : BaseUpdateCommand<ApplicantTransfer>
{
    public UpdateTransferCommand(ApplicantTransfer update)
    {
        Update = update;
    }
}
public class UpdateTransferCommandHaandler : BaseUpdateCommandHandler<UpdateTransferCommand, ApplicantTransfer>
{
    public UpdateTransferCommandHaandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(UpdateTransferCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Applicant does not exists.");

        var schedule = await _unitOfWork.ReadRepositoryFor<Schedule>().GetAsync(command.Update.ScheduleId);
        var course = await _unitOfWork.ReadRepositoryFor<Course>().GetAsync(command.Update.CourseId);
        resultInDb.Transfer(command.Update.CourseId, command.Update.ScheduleId, schedule, course);

        await _unitOfWork.WriteRepositoryFor<Applicant>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Transfer successful.");
    }
}
