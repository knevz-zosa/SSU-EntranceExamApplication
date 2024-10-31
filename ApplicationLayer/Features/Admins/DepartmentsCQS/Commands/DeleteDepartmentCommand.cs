using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.DepartmentsCQS.Commands;
public class DeleteDepartmentCommand : BaseDeleteCommand { }
public class DeleteDepartmentCommandHandler : BaseDeleteCommandHandler<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandHandler(IUnitOfWork<int> unitOfWork)
        : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<Department>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("Department does not exist.");
        }

        await _unitOfWork.WriteRepositoryFor<Department>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Department deleted successfully.");
    }
}
