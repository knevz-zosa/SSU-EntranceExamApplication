using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.CampusesCQS.Commands;
public class DeleteCampusCommand : BaseDeleteCommand { }
public class DeleteCampusCommandHandler : BaseDeleteCommandHandler<DeleteCampusCommand>
{
    public DeleteCampusCommandHandler(IUnitOfWork<int> unitOfWork)
        : base(unitOfWork) { }    
    public override async Task<ResponseWrapper<int>> Handle(DeleteCampusCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<Campus>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("Campus does not exist.");
        }

        await _unitOfWork.WriteRepositoryFor<Campus>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Campus deleted successfully.");
    }
}
