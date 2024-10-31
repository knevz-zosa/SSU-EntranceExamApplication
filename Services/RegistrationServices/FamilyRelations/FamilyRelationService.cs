using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.FamilyRelations;
public class FamilyRelationService : IFamilyRelationService
{
    private readonly IMediator _mediator;

    public FamilyRelationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(FamilyRelationRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateFamilyRelationCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
