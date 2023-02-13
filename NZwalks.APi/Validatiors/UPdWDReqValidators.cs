
using FluentValidation;

namespace NZwalks.APi.Validatiors
{
    public class UPdWDReqValidators : AbstractValidator<Models.DTO.UPdWDReq>
    {
        public UPdWDReqValidators()
        {
            RuleFor(x => x.Code).NotEmpty();

        }
    }
}
