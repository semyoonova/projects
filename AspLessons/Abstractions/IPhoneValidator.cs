using AspLessons.Models;

namespace AspLessons.Abstractions
{
    public interface IPhoneValidator
    {
        public Task<PhoneValidationResultDto> ValidatePhone(string phoneNumber);
    }
}
