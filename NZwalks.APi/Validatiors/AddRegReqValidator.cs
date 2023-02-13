using FluentValidation;
using NZwalks.APi.Models.DTO;

namespace NZwalks.APi.Validatiors
{
    public class AddRegReqValidator : AbstractValidator<Models.DTO.AddRegReq>
    {
        public AddRegReqValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
