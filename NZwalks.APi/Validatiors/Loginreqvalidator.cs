using FluentValidation;
using NZwalks.APi.Models.DTO;

namespace NZwalks.APi.Validatiors
{
    public class Loginreqvalidator :AbstractValidator<Loginreq>
    {
        public Loginreqvalidator()
        {
            RuleFor(x=>x.Username).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}
