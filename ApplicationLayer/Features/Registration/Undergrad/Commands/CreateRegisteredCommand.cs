using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateRegisteredCommand : BaseCreateCommand<RegisteredRequest>
{
    public CreateRegisteredCommand(RegisteredRequest request)
    {
        Request = request;
    }
}
public class CreateRegisteredCommandHandler : BaseCreateCommandHandler<CreateRegisteredCommand, RegisteredRequest>
{
    public CreateRegisteredCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateRegisteredCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<Registered>();
        await _unitOfWork.WriteRepositoryFor<Registered>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
    