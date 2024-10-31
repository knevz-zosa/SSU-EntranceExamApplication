using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateParentGuardianInformationCommand : BaseCreateCommand<ParentGuardianInformationRequest>
{
    public CreateParentGuardianInformationCommand(ParentGuardianInformationRequest request)
    {
        Request = request;
    }
}
public class CreateParentGuardianInformationCommandHandler : BaseCreateCommandHandler<CreateParentGuardianInformationCommand, ParentGuardianInformationRequest>
{
    public CreateParentGuardianInformationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateParentGuardianInformationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<ParentGuardianInformation>();
        await _unitOfWork.WriteRepositoryFor<ParentGuardianInformation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

