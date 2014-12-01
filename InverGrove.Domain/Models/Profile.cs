using System;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InverGrove.Domain.Models
{
    public class Profile : Resource, IProfile
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IProfile Create()
        {
            return new Profile();
        }

        /// <summary>
        /// Gets or sets the profile id.
        /// </summary>
        /// <value>
        /// The profile id.
        /// </value>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>
        /// The person id.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [receive email notification].
        /// </summary>
        /// <value>
        /// <c>true</c> if [receive email notification]; otherwise, <c>false</c>.
        /// </value>
        public bool ReceiveEmailNotification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is baptized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is baptized; otherwise, <c>false</c>.
        /// </value>
        public bool IsBaptized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is local.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is local; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is validated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is validated; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidated { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>
        /// The person.
        [JsonConverter(typeof(InverGrove.Domain.Utils.ModelConverter<Person, IPerson>))]
        public IPerson Person { get; set; }


        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonConverter(typeof(InverGrove.Domain.Utils.ModelCollectionConverter<Role, IRole>))]
        public IList<IRole> Roles { get; set; }
    }
}