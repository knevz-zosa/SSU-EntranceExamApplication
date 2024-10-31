using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Security;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.UsersCQS.Commands;

//User Profile
public class UpdateUserProfileCommand : BaseUpdateCommand<UserUpdateProfile>
{
    public UpdateUserProfileCommand(UserUpdateProfile update)
    {
        Update = update;
    }
}

public class UpdateUserProfileCommandHandler : BaseUpdateCommandHandler<UpdateUserProfileCommand, UserUpdateProfile>
{
    public UpdateUserProfileCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var trimmedFirstName= command.Update.FirstName.Trim().ToLower();
        var trimmedLastName= command.Update.LastName.Trim().ToLower();

        var resultExist = await _unitOfWork.ReadRepositoryFor<User>()
           .Entities.FirstOrDefaultAsync(x => x.Id != command.Update.Id && x.FirstName.Trim().ToLower() == trimmedFirstName
           && x.LastName.Trim().ToLower() == trimmedLastName);

        if (resultExist != null)
        {
            return new ResponseWrapper<int>().Failed("User with the same information already exist.");
        }

        var resultInDb = await _unitOfWork.ReadRepositoryFor<User>().GetAsync(command.Update.Id);
        if (resultInDb == null)
        {
            return new ResponseWrapper<int>().Failed("User does not exists.");
        }
        resultInDb.UpdateProfile(command.Update.FirstName.Trim(), command.Update.LastName.Trim());
        await _unitOfWork.WriteRepositoryFor<User>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "User Profile updated successfuly.");       
    }
}

//User Access and Role
public class UpdateUserAccessRoleCommand : BaseUpdateCommand<UserUpdateAccessRole>
{
    public UpdateUserAccessRoleCommand(UserUpdateAccessRole update)
    {
        Update = update;
    }
}

public class UpdateUserAccessRoleCommandHandler : BaseUpdateCommandHandler<UpdateUserAccessRoleCommand, UserUpdateAccessRole>
{
    public UpdateUserAccessRoleCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork){}

    public override async Task<ResponseWrapper<int>> Handle(UpdateUserAccessRoleCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<User>().GetAsync(command.Update.Id);
        if(resultInDb == null)
        {
            return new ResponseWrapper<int>().Failed("User does not exists.");
        }
        resultInDb.UpdateAccessRole(command.Update.Role, command.Update.Access);
        await _unitOfWork.WriteRepositoryFor<User>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "User Access/Role updated successfuly.");       
    }
}

public class UpdateUserCredentialCommand : BaseUpdateCommand<UserUpdateCredential>
{
    public UpdateUserCredentialCommand(UserUpdateCredential update)
    {
        Update = update;
    }
}

//User Credential
public class UpdateUserCredentialCommandHandler : BaseUpdateCommandHandler<UpdateUserCredentialCommand, UserUpdateCredential>
{
    private readonly IPasswordHasher _passwordHasher;
    public UpdateUserCredentialCommandHandler(IUnitOfWork<int> unitOfWork, IPasswordHasher passwordHasher) : base(unitOfWork)
    {
        _passwordHasher = passwordHasher;
    }
    public override async Task<ResponseWrapper<int>> Handle(UpdateUserCredentialCommand command, CancellationToken cancellationToken)
    {
        var trimmedUsername = command.Update.Username.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(trimmedUsername) || trimmedUsername.Contains(" "))
        {
            return new ResponseWrapper<int>().Failed(message: "Username must not contain spaces.");
        }

        var resultExist = await _unitOfWork.ReadRepositoryFor<User>()
           .Entities.FirstOrDefaultAsync(x => x.Id != command.Update.Id && x.Username.Trim().ToLower() == trimmedUsername);

        if (resultExist != null)
        {
            return new ResponseWrapper<int>().Failed("Username not available.");
        }

        var resultInDb = await _unitOfWork.ReadRepositoryFor<User>().GetAsync(command.Update.Id);
        if (resultInDb == null)
        {
            return new ResponseWrapper<int>().Failed("User does not exists.");
        }
        resultInDb.UpdateCredentials(command.Update.Username.Trim(),
                         _passwordHasher.Hash(command.Update.Password.Trim()));

        await _unitOfWork.WriteRepositoryFor<User>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);       

        return new ResponseWrapper<int>().Success(resultInDb.Id, "User Profile updated successfuly.");
    }
}
