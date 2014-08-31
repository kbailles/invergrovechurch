using System.Collections.Generic;
using System.Linq;
using InverGrove.Domain.Interfaces;
using System.Web.Mvc;

namespace InverGrove.Domain.Helpers
{
    public class StateList : IStateList
    {

        public static IStateList Instance()
        {
            return new StateList();
        }

        /// <summary>
        /// Gets the state select list.
        /// NOTE:  Had to cast the IDictionary object to a SelectListItem List because - MVC 3 only allows for Dictionary<int, string> not <string, string>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStateSelectList()
        {
            return this.stateList.Select(state => new SelectListItem {Text = state.Key, Value = state.Value, Selected = state.Value == "MN"}).ToList();
        }

        private readonly Dictionary<string, string> stateList = new Dictionary<string, string>
        {
            {"Alabama", "AL"},
            {"Alaska", "AK"},
            {"Arizona", "AZ"},
            {"Arkansas", "AR"},
            {"California", "CA"}, 
            {"Colorado", "CO"},
            {"Connecticut", "CT"},
            {"Deleware", "DE"},
            {"Florida", "FL"},
            {"Georgia", "GA"},
            {"Hawaii", "HI"},
            {"Idaho", "ID"}, 
            {"Illinois","IL"},
            {"Indiana", "IN"},
            {"Iowa","IA"},
            {"Kansas","KS"},
            {"Kentucky","KY"},
            {"Louisiana","LA"},
            {"Maine","ME"},
            {"Maryland","MD"},
            {"Massachusetts","MA"},
            {"Michigan","MI"},
            {"Minnesota","MN"},
            {"Mississippi", "MS"},
            {"Missouri","MO"},
            {"Montana","MT"},
            {"Nebraska","NE"},
            {"Nevada","NV"},
            {"New Hampshire","NH"},
            {"New Jersey","NJ"},
            {"New Mexico","NM"},
            {"New York","NY"},
            {"North Carolina","NC"},
            {"North Dakota","ND"},
            {"Ohio","OH"},
            {"Oklahoma","OK"},
            {"Oregon","OR"},
            {"Pennsylvania","PA"},
            {"Rhode Island","RI"},
            {"South Carolina","SC"},
            {"South Dakota","SD"},
            {"Tennessee","TN"},
            {"Texas","TX"},
            {"Utah","UT"},
            {"Vermont","VT"},
            {"Virginia","VA"},
            {"Washington","WA"},
            {"West Virgina","WV"},
            {"Wisconsin","WI"},
            {"Wyoming","WY"}
        };

    }
}
