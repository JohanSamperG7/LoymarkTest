using Loymark.Back.Application.Feature.Users.Commands;
using Loymark.Back.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Loymark.Back.WebApi.Tests
{
    public class UserBuilder
    {
        int Id;
        string Name;
        string LastName;
        string Email;
        DateTime BirthDay;
        string CountryCode;
        bool ReceiveInformation;
        long? Number;

        public UserBuilder()
        {
            Id = 1;
            Name = "Test";
            LastName = "Test";
            Email = "Test@test.com";
            BirthDay = DateTime.Now;
            CountryCode = "COL";
            ReceiveInformation = false;
            Number = 3222222;
        }

        public User Build()
        {
            return new User(
                Id,
                Name,
                LastName,
                Email,
                BirthDay,
                CountryCode,
                ReceiveInformation,
                Number
            );
        }

        public IEnumerable<User> BuildList()
        {
            return new List<User>()
            {
                Build(),
                Build(),
                Build()
            };
        }

        public UserBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public UserBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        public UserBuilder WithBirthDay(DateTime birthDay)
        {
            BirthDay = birthDay;
            return this;
        }

        public UserBuilder WithCountryCode(string countryCode)
        {
            CountryCode = countryCode;
            return this;
        }

        public UserBuilder WithReceiveInformation(bool receiveInformation)
        {
            ReceiveInformation = receiveInformation;
            return this;
        }

        public UserBuilder WithNumber(long? number)
        {
            Number = number;
            return this;
        }

        public UserCreateCommand BuildCreateCommand()
        {
            return new(
                Name,
                LastName,
                Email,
                BirthDay,
                CountryCode,
                ReceiveInformation,
                Number
            );
        }

        public UserUpdateCommand BuildUpdateCommand()
        {
            return new(
                Id,
                Name,
                LastName,
                Email,
                BirthDay,
                CountryCode,
                ReceiveInformation,
                Number
            );
        }

        public UserDeleteCommand BuildDeleteCommand()
        {
            return new(
                Id
            );
        }
    }
}
