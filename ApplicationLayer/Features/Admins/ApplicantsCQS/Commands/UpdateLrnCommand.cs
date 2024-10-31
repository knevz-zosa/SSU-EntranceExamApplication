using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
public class UpdateLrnCommand : BaseUpdateCommand<LrnUpdate>
{
    public UpdateLrnCommand(LrnUpdate update)
    {
        Update = update;
    }
}
public class UpdateLrnCommandHandler : BaseUpdateCommandHandler<UpdateLrnCommand, LrnUpdate>
{
    public UpdateLrnCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(UpdateLrnCommand command, CancellationToken cancellationToken)
    {
        //var isRegistered = await _unitOfWork.ReadRepositoryFor<Registered>().Entities
        //  .Include(r => r.Applicant)
        //  .ThenInclude(a => a.AcademicBackground)
        //  .AsNoTracking()
        //  .AnyAsync(r => r.Applicant.AcademicBackground.LRN == result.LRN, cancellationToken);
        var result = command.Update;


        var resultExist = await _unitOfWork.ReadRepositoryFor<Registered>().Entities
            .Include(x => x.Applicant)
                .ThenInclude(x => x.AcademicBackground)
            .AsNoTracking()
           .AnyAsync(x => x.Id != x.ApplicantId && x.Applicant.AcademicBackground.LRN == result.LRN, cancellationToken);


        if (resultExist)
            return new ResponseWrapper<int>().Failed(message: "Applicant with the same LRN already exists.");

        var resultInDb = await _unitOfWork.ReadRepositoryFor<AcademicBackground>().GetAsync(command.Update.Id);

        if (resultInDb == null)
            return new ResponseWrapper<int>().Failed("Applicant does not exists.");

        resultInDb.Update(result.LRN);

        await _unitOfWork.WriteRepositoryFor<AcademicBackground>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(result.Id, "LRN updated successfuly.");
    }
}
