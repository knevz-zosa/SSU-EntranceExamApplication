using ApplicationLayer.Features.Login;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;

    public AuthService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ResponseWrapper<UserResponse>> Login(LoginRequest request)
    {
        var query = new LoginCommand
        {
            Username = request.Username,
            Password = request.Password,
        };

        var result = await _mediator.Send(query);

        if (!result.IsSuccessful)
        {
            return new ResponseWrapper<UserResponse>
            {
                IsSuccessful = false,
                Messages = result.Messages
            };
        }
        return result;
    }
}
