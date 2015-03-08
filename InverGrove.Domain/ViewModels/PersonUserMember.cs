using InverGrove.Domain.Models;
using System;
using System.Collections.Generic;

namespace InverGrove.Domain.ViewModels
{
    public  class PersonUserMember
    {

        int PersonId { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string MiddleInitial { get; set; }

        string AddressOne { get; set; }

        string AddressTwo { get; set; }

        string City { get; set; }

        string State { get; set; }

        string ZipCode { get; set; }

        string PrimaryEmail { get; set; }

        string SecondaryEmail { get; set; }

        IList<PhoneNumber> PhoneNumbers { get; set; }

        DateTime? DateOfBirth { get; set; }

        string GroupPhotoFilePath { get; set; }

        string IndividualPhotoFilePath { get; set; }

        string Gender { get; set; }

        int MaritalStatusId { get; set; }

        int PersonTypeId { get; set; }
    }
}
