namespace Loymark.Back.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public long? Number { get; set; }
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
        public DateTime BirthDay { get; set; }
        public bool ReceiveInformation { get; set; }
    }
}
