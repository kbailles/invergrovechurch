using System;

namespace InverGrove.Domain.Interfaces
{
    public interface INotification
    {

        int NotificationId { get; set; }

        int PersonId { get; set; }

        /// <summary>
        /// Auto-generated GUID, serves as access token when notification goes out.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Identifier { get; set; }

        DateTime DateSent { get; set; }
        DateTime DateAccessed { get; set; } 
    }
}