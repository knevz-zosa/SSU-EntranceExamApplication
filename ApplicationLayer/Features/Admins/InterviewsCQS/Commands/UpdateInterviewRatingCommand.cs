using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace ApplicationLayer.Features.Admins.InterviewsCQS.Commands;
public class UpdateInterviewRatingCommand : BaseUpdateCommand<InterviewRatingUpdate>
{
    public UpdateInterviewRatingCommand(InterviewRatingUpdate update)
    {
        Update = update;
    }
}
public class UpdateInterviewRatingCommandHandler : BaseUpdateCommandHandler<UpdateInterviewRatingCommand, InterviewRatingUpdate>
{
    public UpdateInterviewRatingCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork){}
    public override async Task<ResponseWrapper<int>> Handle(UpdateInterviewRatingCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Interview>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Interview does not exists.");

        var result = resultInDb.UpdateRating(command.Update.InterviewReading, command.Update.InterviewCommunication,
            command.Update.InterviewAnalytical, command.Update.UpdatedBy, command.Update.Interviewer);
        await _unitOfWork.WriteRepositoryFor<Interview>().UpdateAsync(result);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Interview rating updated successfuly.");
    }
}


