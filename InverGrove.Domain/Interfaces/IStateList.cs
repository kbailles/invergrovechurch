using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace InverGrove.Domain.Interfaces
{
    public interface IStateList
    {
        IEnumerable<SelectListItem> GetStateSelectList();
    }
}
