namespace AspLessons.Models
{
    public class PhoneValidationResultDto
    {
        public bool Valid { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        public string LineType { get; set; }
    }
}
