using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace ApplicationLayer.Features.Admins.ExaminationsCQS.Commands;
public class UpdateExaminationCommand : BaseUpdateCommand<ExaminationUpdate>
{
    public UpdateExaminationCommand(ExaminationUpdate update)
    {
        Update = update;
    }
}
public class UpdateExaminationCommandHandler : BaseUpdateCommandHandler<UpdateExaminationCommand, ExaminationUpdate>
{
    public UpdateExaminationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork){}
    public override async Task<ResponseWrapper<int>> Handle(UpdateExaminationCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Examination>().GetAsync(command.Update.Id);
        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Result does not exists.");

        var result = resultInDb.Update(command.Update.ReadingRawScore, command.Update.MathRawScore,
            command.Update.ScienceRawScore, command.Update.IntelligenceRawScore, command.Update.UpdatedBy);
        await _unitOfWork.WriteRepositoryFor<Examination>().UpdateAsync(result);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Result updated successfuly.");
    }
}
