using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace ApplicationLayer.Features.Admins.UsersCQS.Commands;

public class DeleteUserCommand : BaseDeleteCommand { }

public class DeleteUserCommandHandler : BaseDeleteCommandHandler<DeleteUserCommand>
{
    public DeleteUserCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<User>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("User does not exist.");
        }

        await _unitOfWork.WriteRepositoryFor<User>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "User deleted successfully.");
    }
}
