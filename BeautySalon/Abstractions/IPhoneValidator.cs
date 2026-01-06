using BeautySalon.Models;

namespace BeautySalon.Abstractions
{
    public interface IPhoneValidator
    {
        public Task<PhoneValidationResultDto> ValidatePhone(string phoneNumber);
    }
}
