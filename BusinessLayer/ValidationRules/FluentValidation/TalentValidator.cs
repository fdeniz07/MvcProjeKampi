using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class TalentValidator : AbstractValidator<Talent>
    {
        public TalentValidator()
        {
            RuleFor(x => x.SkillName).NotEmpty().WithMessage("Yetenek adı alanı boş bırakılamaz");
            RuleFor(x => x.SkillName).NotNull().WithMessage("Yetenek adı alanı boş bırakılamaz");
            RuleFor(x => x.SkillLevel).NotEmpty().WithMessage("Yetenek seviyesi alanı boş bırakılamaz.");
            RuleFor(x => x.SkillLevel).NotNull().WithMessage("Yetenek seviyesi alanı boş bırakılamaz.");
        }
    }
}
