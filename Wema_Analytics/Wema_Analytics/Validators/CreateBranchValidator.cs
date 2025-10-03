using FluentValidation;
using Wema_Analytics.Commands;

namespace Wema_Analytics.Validators
{
    public class CreateBranchValidator : AbstractValidator<CreateBranch>
    {
        public CreateBranchValidator()
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.City).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Region).NotEmpty().MaximumLength(50);
        }
    }
}
