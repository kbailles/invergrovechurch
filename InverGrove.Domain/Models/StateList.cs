using System.Collections.Generic;
using System.Linq;

namespace InverGrove.Domain.Models
{
    public static class StateList
    {
        private static readonly IDictionary<string, string> stateDictionary;

        static StateList()
        {
            stateDictionary = BuildStateDictionary();
        }

        /// <summary>
        /// Gets the state dictionary.
        /// </summary>
        /// <value>The state dictionary.</value>
        public static IDictionary<string, string> StateDictionary
        {
            get { return stateDictionary; }
        }

        private static IDictionary<string, string> BuildStateDictionary()
        {
            var stateCollection = new Dictionary<string, string>
            {
                {"AL", "Alabama"},
                {"AK", "Alaska"},
                {"AS", "American Samoa"},
                {"AZ", "Arizona"},
                {"AR", "Arkansas"},
                {"CA", "California"},
                {"CO", "Colorado"},
                {"CT", "Connecticut"},
                {"DE", "Delaware"},
                {"DC", "District Of Columbia"},
                {"FM", "Federated States Of Micronesia"},
                {"FL", "Florida"},
                {"GA", "Georgia"},
                {"GU", "Guam"},
                {"HI", "Hawaii"},
                {"ID", "Idaho"},
                {"IL", "Illinois"},
                {"IN", "Indiana"},
                {"IA", "Iowa"},
                {"KS", "Kansas"},
                {"KY", "Kentucky"},
                {"LA", "Louisiana"},
                {"ME", "Maine"},
                {"MH", "Marshall Islands"},
                {"MD", "Maryland"},
                {"MA", "Massachusetts"},
                {"MI", "Michigan"},
                {"MN", "Minnesota"},
                {"MS", "Mississippi"},
                {"MO", "Missouri"},
                {"MT", "Montana"},
                {"NE", "Nebraska"},
                {"NV", "Nevada"},
                {"NH", "New Hampshire"},
                {"NJ", "New Jersey"},
                {"NM", "New Mexico"},
                {"NY", "New York"},
                {"NC", "North Carolina"},
                {"ND", "North Dakota"},
                {"MP", "Northern Mariana Islands"},
                {"OH", "Ohio "},
                {"OK", "Oklahoma"},
                {"OR", "Oregon"},
                {"PW", "Palau"},
                {"PA", "Pennsylvania"},
                {"PR", "Puerto Rico"},
                {"RI", "Rhode Island"},
                {"SC", "South Carolina"},
                {"SD", "South Dakota"},
                {"TN", "Tennessee"},
                {"TX", "Texas"},
                {"UT", "Utah"},
                {"VT", "Vermont"},
                {"VI", "Virgin Islands"},
                {"VA", "Virginia"},
                {"WA", "Washington"},
                {"WV", "West Virginia"},
                {"WI", "Wisconsin"},
                {"WY", "Wyoming"}
            };

            return stateCollection.OrderBy(p => p.Value).ToDictionary(s => s.Key, s => s.Value);
        }
    }
}