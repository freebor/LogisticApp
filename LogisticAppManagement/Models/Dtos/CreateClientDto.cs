namespace LogisticAppManagement.Models.Dtos
{
    public class CreateClientDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}