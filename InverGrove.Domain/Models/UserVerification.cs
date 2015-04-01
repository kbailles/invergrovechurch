﻿using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class UserVerification : IUserVerification
    {
        public int UserVerificationId { get; set; }

        public int PersonId { get; set; }

        /// <summary>
        /// Auto-generated GUID, serves as access token when notification goes out.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Identifier { get; set; }

        public DateTime DateSent { get; set; }
        public DateTime DateAccessed { get; set; }

        public string PersonName { get; set; }
    }
}