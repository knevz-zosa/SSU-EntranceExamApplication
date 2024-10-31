using ApplicationLayer.IRepositories;
using Common.Responses;
using Common.Security;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Login;
public class LoginCommand : IRequest<ResponseWrapper<UserResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseWrapper<UserResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IUnitOfWork<int> unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    public async Task<ResponseWrapper<UserResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<User>()
                .Entities.FirstOrDefaultAsync(x => x.Username == request.Username);

        if (resultInDb == null || !_passwordHasher.VerifyPassword(resultInDb.Password, request.Password))
        {
            return new ResponseWrapper<UserResponse>().Failed(message: "Invalid Account.");
        }

        return new ResponseWrapper<UserResponse>().Success(data: resultInDb.Adapt<UserResponse>());
    }
}
