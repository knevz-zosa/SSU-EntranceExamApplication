using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateSoloParentCommand : BaseCreateCommand<SoloParentRequest>
{
    public CreateSoloParentCommand(SoloParentRequest request)
    {
        Request = request;
    }
}
public class CreateSoloParentCommandHandler : BaseCreateCommandHandler<CreateSoloParentCommand, SoloParentRequest>
{
    public CreateSoloParentCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateSoloParentCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<SoloParent>();
        await _unitOfWork.WriteRepositoryFor<SoloParent>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
