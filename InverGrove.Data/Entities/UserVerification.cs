using System;
using System.ComponentModel.DataAnnotations;

namespace InverGrove.Data.Entities
{
    public class UserVerification
    {
        [Key]
        public int UserVerificationId { get; set; }
        [Required]
        public int PersonId { get; set; }

        /// <summary>
        /// Auto-generated GUID, serves as access token when notification goes out.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Identifier { get; set; }
        [Required]
        public DateTime DateSent { get; set; }
        public DateTime? DateAccessed { get; set; }

    }
}