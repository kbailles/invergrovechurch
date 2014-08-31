using System;
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
            this.Profiles = new HashSet<Profile>();
            this.Relatives = new HashSet<Relative>();
            this.Relatives1 = new HashSet<Relative>();
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

        [StringLength(100)]
        public string EmailPrimary { get; set; }

        [StringLength(100)]
        public string EmailSecondary { get; set; }

        [StringLength(20)]
        public string PhonePrimary { get; set; }

        [StringLength(20)]
        public string PhoneSecondary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBrith { get; set; }

        [Required]
        [StringLength(8)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string GroupPhoto { get; set; }

        [StringLength(100)]
        public string IndividualPhoto { get; set; }

        public int MaritalStatusId { get; set; }

        public int PersonTypeId { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }

        public virtual PersonType PersonType { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual ICollection<Relative> Relatives { get; set; }

        public virtual ICollection<Relative> Relatives1 { get; set; }
    }
}