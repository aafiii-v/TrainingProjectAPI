using FluentValidation;
using System.Text.RegularExpressions;
using TrainingProjectAPI.Models.DTO;

namespace TrainingProjectAPI.Validator
{
    public class ValidatorRequestCustomer : AbstractValidator<CustomerRequestDTO>
    {
        public ValidatorRequestCustomer()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5).WithMessage("Name is Not Valid!");
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(9).MaximumLength(13).Must(ValidPhoneNumber);
            RuleFor(x => x.Address).NotEmpty().Must(ValidAddress);
        }

        public bool ValidAddress(string address)
        {
            return true;
        }

        public bool ValidPhoneNumber(string phoneNumber)
        {
            string regexNumberOnly = @"^\d+$";
            if (Regex.IsMatch(phoneNumber, regexNumberOnly))
                return true;
            else
                return false;
        }
    }
}