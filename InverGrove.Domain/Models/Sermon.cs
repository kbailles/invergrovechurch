using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class Sermon : ISermon
    {
        /// <summary>
        /// Gets or sets the sermon identifier.
        /// </summary>
        /// <value>
        /// The sermon identifier.
        /// </value>
        public int SermonId { get; set; }

        /// <summary>
        /// Gets or sets the sermon date.
        /// </summary>
        /// <value>
        /// The sermon date.
        /// </value>
        public DateTime SermonDate { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the sound cloud identifier.
        /// </summary>
        /// <value>
        /// The sound cloud identifier.
        /// </value>
        public int SoundCloudId { get; set; }

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
        /// Gets or sets the modified by user identifier.
        /// </summary>
        /// <value>
        /// The modified by user identifier.
        /// </value>
        public int ModifiedByUserId { get; set; }
    }
}