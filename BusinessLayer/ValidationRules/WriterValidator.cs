using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adını boş geçemezsiniz!");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soyadını boş geçemezsiniz!");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkımda kısmını boş geçemezsiniz!");
            RuleFor(x => x.WriterName).MinimumLength(3).WithMessage("Lütfen en az 2 karakter girişi yapınız!");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayınız!");
        }
    }
}
