
using FluentValidation;

namespace NZwalks.APi.Validatiors
{
    public class AddWalkReqValidations : AbstractValidator<Models.DTO.AddWalkReq>
    {
        public AddWalkReqValidations()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
