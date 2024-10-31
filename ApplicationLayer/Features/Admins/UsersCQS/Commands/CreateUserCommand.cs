using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Responses;
using Common.Security;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.UsersCQS.Commands;
public class CreateUserCommand : BaseCreateCommand<UserRequest>
{
    public CreateUserCommand(UserRequest request)
    {
        Request = request;
    }
}

public class CreateUserCommandHandler : BaseCreateCommandHandler<CreateUserCommand, UserRequest>
{
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUnitOfWork<int> unitOfWork, IPasswordHasher passwordHasher) : base(unitOfWork)
    {
        _passwordHasher = passwordHasher;
    }

    public override async Task<ResponseWrapper<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {       

        var trimmedFirstName = command.Request.FirstName.Trim().ToLower();
        var trimmedLastName = command.Request.LastName.Trim().ToLower();
        var trimmedUserName = command.Request.Username.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(trimmedUserName) || trimmedUserName.Contains(" "))
        {
            return new ResponseWrapper<int>().Failed(message: "Username must not contain spaces.");
        }

        var existingUser = await _unitOfWork.ReadRepositoryFor<User>()
         .Entities.FirstOrDefaultAsync(x =>
            x.FirstName.Trim().ToLower() == trimmedFirstName &&
            x.LastName.Trim().ToLower() == trimmedLastName, cancellationToken);

        if (existingUser != null)
            return new ResponseWrapper<int>().Failed(message: "User with the same information already exists.");

        var existingUserName = await _unitOfWork.ReadRepositoryFor<User>()
           .Entities.FirstOrDefaultAsync(x => x.Username.Trim().ToLower() == trimmedUserName, cancellationToken);

        if (existingUserName != null)
            return new ResponseWrapper<int>().Failed(message: "Username not available.");

        var model = command.Request.Adapt<User>();

        var passwordHash = _passwordHasher.Hash(command.Request.Password.Trim());

        model.FirstName = model.FirstName.Trim();
        model.LastName = model.LastName.Trim();
        model.Username = model.Username.Trim();

        model.Password = passwordHash;
        await _unitOfWork.WriteRepositoryFor<User>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "User created successfully.");
    }
}
    //public async Task<ResponseWrapper<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    //{
    //    var result = request.Request;

    //    var trimmedFirstName = result.FirstName.Trim().ToLower();
    //    var trimmedLastName = result.LastName.Trim().ToLower();
    //    var trimmedUserName = result.Username.Trim().ToLower();


    //    if (string.IsNullOrWhiteSpace(trimmedUserName) || trimmedUserName.Contains(" "))
    //    {
    //        return new ResponseWrapper<int>().Failed(message: "Username must not contain spaces.");
    //    }

    //    var existingUser = await _unitOfWork.ReadRepositoryFor<User>()
    //     .Entities.FirstOrDefaultAsync(x => 
    //        x.FirstName.Trim().ToLower() == trimmedFirstName &&
    //        x.LastName.Trim().ToLower() == trimmedLastName, cancellationToken);

    //    if (existingUser != null)
    //        return new ResponseWrapper<int>().Failed(message: "User with the same information already exists.");

    //    var existingUserName = await _unitOfWork.ReadRepositoryFor<User>()
    //       .Entities.FirstOrDefaultAsync(x => x.Username.Trim().ToLower() == trimmedUserName, cancellationToken);

    //    if (existingUserName != null)
    //        return new ResponseWrapper<int>().Failed(message: "Username not available.");

    //    var model = result.Adapt<User>();

    //    var passwordHash = _passwordHasher.Hash(result.Password.Trim());

    //    model.FirstName = model.FirstName.Trim();
    //    model.LastName = model.LastName.Trim();
    //    model.Username = model.Username.Trim();

    //    model.Password = passwordHash;
    //    await _unitOfWork.WriteRepositoryFor<User>().CreateAsync(model);
    //    await _unitOfWork.CommitAsync(cancellationToken);

    //    return new ResponseWrapper<int>().Success(model.Id, "User created successfully.");

    //}
//}

