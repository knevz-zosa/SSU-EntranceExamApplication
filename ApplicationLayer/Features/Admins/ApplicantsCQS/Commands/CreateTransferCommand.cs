using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
public class CreateTransferCommand : BaseCreateCommand<TransferRequest>
{
    public CreateTransferCommand(TransferRequest request)
    {
        Request = request;
    }
}
public class CreateTransferCommandHandler : BaseCreateCommandHandler<CreateTransferCommand, TransferRequest>
{
    public CreateTransferCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<Transfer>();
        await _unitOfWork.WriteRepositoryFor<Transfer>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
