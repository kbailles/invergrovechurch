using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    public class Contact
    {
        [Key]
        public int ContactsId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(4)]
        public string State { get; set; }

        [StringLength(12)]
        public string Zip { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public bool IsVisitorCard { get; set; }

        public bool IsOnlineContactForm { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        [Column(TypeName = "ntext"), StringLength(200)]
        public string Comments { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}