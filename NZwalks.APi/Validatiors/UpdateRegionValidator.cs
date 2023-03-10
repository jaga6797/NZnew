using FluentValidation;

namespace NZwalks.APi.Validatiors
{
    public class UpdateRegionValidator :AbstractValidator<Models.DTO.UpdateRegReq>
    {
        public UpdateRegionValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
