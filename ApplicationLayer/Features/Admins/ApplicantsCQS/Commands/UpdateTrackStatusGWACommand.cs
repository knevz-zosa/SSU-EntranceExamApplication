using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
public class UpdateTrackStatusGWACommand : BaseUpdateCommand<ApplicantUpdateGwaStatusTrack>
{
    public UpdateTrackStatusGWACommand(ApplicantUpdateGwaStatusTrack update)
    {
        Update = update;
    }
}
public class UpdateTrackStatusGWACommandHandler : BaseUpdateCommandHandler<UpdateTrackStatusGWACommand, ApplicantUpdateGwaStatusTrack>
{
    public UpdateTrackStatusGWACommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateTrackStatusGWACommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Applicant does not exists.");

        resultInDb.UpdateGwaStatusTrack(command.Update.GWA, command.Update.ApplicantStatus,
           command.Update.Track);

        await _unitOfWork.WriteRepositoryFor<Applicant>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Update successful.");
    }
}
