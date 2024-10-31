using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
public class DeleteApplicantCommand : BaseDeleteCommand { }
public class DeleteApplicantCommandHandler : BaseDeleteCommandHandler<DeleteApplicantCommand>
{
    public DeleteApplicantCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(DeleteApplicantCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<Applicant>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("Applicant does not exist.");
        }

        await _unitOfWork.WriteRepositoryFor<Applicant>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Applicant deleted successfully.");
    }
}
