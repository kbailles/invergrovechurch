using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace InverGrove.Domain.Factories
{
    public class PersonFactory : IPersonFactory
    {
        private readonly IMaritalStatusRepository maritalStatusRepository;
        private readonly IChurchRoleRepository churchRoleRepository;

        public PersonFactory(IChurchRoleRepository churchRoleRepository, IMaritalStatusRepository maritalStatusRepository)
        {
            this.churchRoleRepository = churchRoleRepository;
            this.maritalStatusRepository = maritalStatusRepository;
        }

        /// <summary>
        /// Creates the base/default person.
        /// </summary>
        /// <returns></returns>
        public IPerson CreatePerson()
        {
            var person = new Person();

            var maritalStatusList = this.maritalStatusRepository.Get();
            var churchRoles = this.churchRoleRepository.Get();

            var churchRoleSelectList = new List<SelectListItem>();
            var maritalSelectList = new List<SelectListItem>();

            foreach (var maritalStatus in maritalStatusList)
            {
                maritalSelectList.Add(new SelectListItem
                {
                    Text = maritalStatus.MaritalStatusDescription,
                    Value = maritalStatus.MaritalStatusId.ToString(CultureInfo.InvariantCulture)
                });
            }

            foreach (var churchRole in churchRoles)
            {
                churchRoleSelectList.Add(new SelectListItem
                {
                    Text = churchRole.ChurchRoleDescription,
                    Value = churchRole.ChurchRoleId.ToString(CultureInfo.InvariantCulture)
                });
            }

            person.MaritalStatusList = maritalSelectList;
            person.ChurchRoleList = churchRoleSelectList;

            return person;
        }
    }
}
