using System;

namespace InverGrove.Domain.Interfaces
{
    public interface ISermon
    {
        /// <summary>
        /// Gets or sets the sermon identifier.
        /// </summary>
        /// <value>
        /// The sermon identifier.
        /// </value>
        int SermonId { get; set; }

        /// <summary>
        /// Gets or sets the sermon date.
        /// </summary>
        /// <value>
        /// The sermon date.
        /// </value>
        DateTime SermonDate { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        string Tags { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the sound cloud identifier.
        /// </summary>
        /// <value>
        /// The sound cloud identifier.
        /// </value>
        int SoundCloudId { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the modified by user identifier.
        /// </summary>
        /// <value>
        /// The modified by user identifier.
        /// </value>
        int ModifiedByUserId { get; set; }
    }
}