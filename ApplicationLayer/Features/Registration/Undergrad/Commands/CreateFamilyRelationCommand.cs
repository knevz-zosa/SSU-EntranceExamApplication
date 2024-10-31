using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateFamilyRelationCommand : BaseCreateCommand<FamilyRelationRequest>
{
    public CreateFamilyRelationCommand(FamilyRelationRequest request)
    {
        Request = request;
    }
}
public class CreateFamilyRelationCommandHandler : BaseCreateCommandHandler<CreateFamilyRelationCommand, FamilyRelationRequest>
{
    public CreateFamilyRelationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateFamilyRelationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<FamilyRelation>();
        await _unitOfWork.WriteRepositoryFor<FamilyRelation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
