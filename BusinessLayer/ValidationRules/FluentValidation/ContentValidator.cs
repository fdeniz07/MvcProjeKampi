using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class ContentValidator : AbstractValidator<Content>
    {
        public ContentValidator()
        {
            RuleFor(x => x.ContentValue).NotEmpty().WithMessage("Yorum alanı boş geçilemez");
            RuleFor(x => x.ContentValue).NotNull().WithMessage("Yorum alanı boş geçilemez");
        }
    }
}
