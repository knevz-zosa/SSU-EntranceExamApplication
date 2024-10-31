using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateAcademicBackgroundCommand : BaseCreateCommand<AcademicBackgroundRequest>
{
    public CreateAcademicBackgroundCommand(AcademicBackgroundRequest request)
    {
        Request = request;
    }
}

public class CreateAcademicBackgroundCommandHandler : BaseCreateCommandHandler<CreateAcademicBackgroundCommand, AcademicBackgroundRequest>
{
    public CreateAcademicBackgroundCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(CreateAcademicBackgroundCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;
        
        var isRegistered = await _unitOfWork.ReadRepositoryFor<Registered>().Entities
            .Include(r => r.Applicant) 
            .ThenInclude(a => a.AcademicBackground) 
            .AsNoTracking()
            .AnyAsync(r => r.Applicant.AcademicBackground.LRN == result.LRN, cancellationToken);

        if (isRegistered)
            return new ResponseWrapper<int>().Failed(message: "Applicant with the same LRN already exists.");

        var model = result.Adapt<AcademicBackground>();
        await _unitOfWork.WriteRepositoryFor<AcademicBackground>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

