using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreatePersonalityProfileCommand : BaseCreateCommand<PersonalityProfileRequest>
{
    public CreatePersonalityProfileCommand(PersonalityProfileRequest request)
    {
        Request = request;
    }
}

public class CreatePersonalityProfileCommandHandler : BaseCreateCommandHandler<CreatePersonalityProfileCommand, PersonalityProfileRequest>
{
    public CreatePersonalityProfileCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePersonalityProfileCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<PersonalityProfile>();
        await _unitOfWork.WriteRepositoryFor<PersonalityProfile>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
