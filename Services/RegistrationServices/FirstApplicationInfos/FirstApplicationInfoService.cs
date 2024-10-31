using ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.FirstApplicationInfos;
public class FirstApplicationInfoService : IFirstApplicationInfoService
{
    private readonly IMediator _mediator;

    public FirstApplicationInfoService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public  async Task<ResponseWrapper<int>> Create(FirstApplicationInfoRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateFirstApplicationInfoCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<FirstApplicationInfoResponse>> Get(int id)
    {
        var query = new GetFirstApplicationInfoQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }
}
