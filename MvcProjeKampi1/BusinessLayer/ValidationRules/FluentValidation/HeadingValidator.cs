using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class HeadingValidator : AbstractValidator<Heading>
    {
        public HeadingValidator()
        {
            RuleFor(x => x.HeadingName).NotEmpty().WithMessage("Başlık adını boş geçemezsiniz!");
            RuleFor(x => x.HeadingName).NotNull().WithMessage("Başlık adını boş geçemezsiniz!");
            RuleFor(x => x.HeadingName).MinimumLength(3).WithMessage("Başlık adına en az 3 karakter girişi yapınız!");
            RuleFor(x => x.HeadingName).MaximumLength(32).WithMessage("Başlık adına 32 karakterden fazla değer girişi yapmayınız!");
        }
    }
}
