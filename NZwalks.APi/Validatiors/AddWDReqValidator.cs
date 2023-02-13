using FluentValidation;

namespace NZwalks.APi.Validatiors
{
    public class AddWDReqValidator :AbstractValidator<Models.DTO.AddWDReq>
    {
        public AddWDReqValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
