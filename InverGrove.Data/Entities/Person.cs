﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("Person")]
    public class Person
    {
        public Person()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Attendances = new HashSet<Attendance>();
            this.Profiles = new HashSet<Profile>();
            this.Relatives = new HashSet<Relative>();
            this.Relatives1 = new HashSet<Relative>();
            this.PhoneNumbers = new HashSet<PhoneNumber>();
        }

        public int PersonId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(5)]
        public string MiddleInitial { get; set; }

        [StringLength(200)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(254)]
        public string EmailPrimary { get; set; }

        [StringLength(254)]
        public string EmailSecondary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string GroupPhoto { get; set; }

        [StringLength(100)]
        public string IndividualPhoto { get; set; }

        public bool IsBaptized { get; set; }

        public bool IsMember { get; set; }

        public bool IsVisitor { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int? ModifiedByUserId { get; set; }

        public int MaritalStatusId { get; set; }

        public int? ChurchRoleId { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }

        public virtual ChurchRole ChurchRole { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual ICollection<Relative> Relatives { get; set; }

        public virtual ICollection<Relative> Relatives1 { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}