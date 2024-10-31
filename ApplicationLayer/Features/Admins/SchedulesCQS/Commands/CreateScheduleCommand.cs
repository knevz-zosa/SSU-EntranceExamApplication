using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.SchedulesCQS.Commands;
public class CreateScheduleCommand : BaseCreateCommand<ScheduleRequest>
{
    public CreateScheduleCommand(ScheduleRequest request)
    {
        Request = request;
    }
}

public class CreateScheduleCommandHandler : BaseCreateCommandHandler<CreateScheduleCommand, ScheduleRequest>
{
    public CreateScheduleCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(CreateScheduleCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;
        var existingResult = await _unitOfWork.ReadRepositoryFor<Schedule>()
            .Entities.FirstOrDefaultAsync(x => x.Campus.Id == result.CampusId && x.ScheduleDate == result.ScheduleDate
            && x.Time == result.Time);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Schedule with the same date and time in this campus already exists.");

        var model = result.Adapt<Schedule>();
        model.SchoolYear = $"{model.ScheduleDate.Year} - {(model.ScheduleDate.Year + 1)}";
        await _unitOfWork.WriteRepositoryFor<Schedule>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Schedule created successfully.");
    }
}
