using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
public class UpdateEmergencyContactCommand : BaseUpdateCommand<EmergencyContactUpdate>
{
    public UpdateEmergencyContactCommand(EmergencyContactUpdate update)
    {
        Update = update;
    }
}
public class UpdateEmergencyContactCommandHandler : BaseUpdateCommandHandler<UpdateEmergencyContactCommand, EmergencyContactUpdate>
{
    public UpdateEmergencyContactCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateEmergencyContactCommand command, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<EmergencyContact>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Emergency contact does not exists.");

        var result = resultInDb.Update(command.Update.Name.Trim(), command.Update.ContactNo,
            command.Update.Address, command.Update.Relationship);

        await _unitOfWork.WriteRepositoryFor<EmergencyContact>().UpdateAsync(result);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "Emergency contact updated successfuly.");

    }
}
