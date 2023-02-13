using FluentValidation;

namespace NZwalks.APi.Validatiors
{
    public class Updatewalkreqvalidator : AbstractValidator<Models.DTO.Updatewalkreq>

    {
        public Updatewalkreqvalidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
