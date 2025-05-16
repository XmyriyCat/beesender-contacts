using Contact.Application.Variables;
using Contacts.Contracts.Requests.Contact;
using FluentValidation;

namespace Contact.Application.Infrastructure.Validators.Contact;

public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(1).WithMessage("Name must be at least 1 character long.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 character long.");
            
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .MinimumLength(10).WithMessage("Phone number must be at least 10 characters long.")
            .MaximumLength(20).WithMessage("Phone number exceed 20 character long.");
        
        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage("Job title is required.")
            .MinimumLength(1).WithMessage("Job title must be at least 1 character long.")
            .MaximumLength(50).WithMessage("Job title exceed 50 character long.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.")
            .Must(BeValidBirthDate)
            .WithMessage("BirthDate must be a valid date, in the past or today, " +
                         "and the person must be between 0 and 120 years old.");
    }
    
    private bool BeValidBirthDate(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        
        var minDate = today.AddYears(ValidationValueOptions.MaximumAge * -1);
        
        var maxDate = today;
    
        return birthDate >= minDate && birthDate <= maxDate;
    }
}