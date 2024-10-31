using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.FamilyRelations;
public interface IFamilyRelationService
{
    Task<ResponseWrapper<int>> Create(FamilyRelationRequest request);
}
