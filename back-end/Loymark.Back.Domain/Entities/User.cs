using Loymark.Back.Domain.Entities.Base;
using Loymark.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Loymark.Back.Domain.Entities
{
    public class User : EntityBase<int>
    {
        private const string PATTERN_EMAIL = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private const string PATTERN_NAMES = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$";

        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime BirthDay { get; protected set; }
        public long? Number { get; protected set; }
        public string CountryCode { get; protected set; }
        public bool ReceiveInformation { get; protected set; }

        public virtual IEnumerable<Activity> Activities { get; set; } = default!;

        public User(
            string name,
            string lastName,
            string email,
            DateTime birthDay,
            string countryCode,
            bool receiveInformation,
            long? number
        )
        {
            Name = ValidateName(name);
            LastName = ValidateName(lastName);
            Email = ValidateEmail(email);
            BirthDay = birthDay;
            CountryCode = countryCode;
            ReceiveInformation = receiveInformation;
            Number = number;
        }

        public User(
            int id,
            string name,
            string lastName,
            string email,
            DateTime birthDay,
            string countryCode,
            bool receiveInformation,
            long? number
        )
        {
            Id = id;
            Name = ValidateName(name);
            LastName = ValidateName(lastName);
            Email = ValidateEmail(email);
            BirthDay = birthDay;
            CountryCode = countryCode;
            ReceiveInformation = receiveInformation;
            Number = number;
        }

        private string ValidateEmail(string email) =>
            Regex.IsMatch(email, PATTERN_EMAIL) ? email : throw new AppException("Invalid email.");
        private string ValidateName(string name) =>
            Regex.IsMatch(name, PATTERN_NAMES) ? name : throw new AppException("Invalid name.");

    }
}
