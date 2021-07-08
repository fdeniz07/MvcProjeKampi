using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public  class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.AboutDetails1).NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.");
            RuleFor(x => x.AboutDetails1).NotNull().WithMessage("Açıklama alanı boş bırakılamaz.");
            RuleFor(x => x.AboutDetails1).MinimumLength(3).WithMessage("Açıklama alanına en az 3 karakter yazılmalıdır.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Durum alanı boş bırakılamaz.");
        }
    }
}
