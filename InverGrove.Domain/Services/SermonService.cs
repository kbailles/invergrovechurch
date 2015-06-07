using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class SermonService : ISermonService
    {
        private readonly ISermonRepository sermonRepository;

        public SermonService(ISermonRepository sermonRepository)
        {
            this.sermonRepository = sermonRepository;
        }

        /// <summary>
        /// Adds the sermon.
        /// </summary>
        /// <param name="newSermon">The new sermon.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">newSermon</exception>
        public int AddSermon(ISermon newSermon)
        {
            if (newSermon == null)
            {
                throw new ParameterNullException("newSermon");
            }

            var sermonId = this.sermonRepository.Add(newSermon);
            newSermon.SermonId = sermonId;

            if ((HttpContext.Current.Cache != null) && (HttpContext.Current.Cache["Sermons"] != null))
            {
                var sermonsCollection = (List<ISermon>)HttpContext.Current.Cache["Sermons"];
                sermonsCollection.Add(newSermon);
                HttpContext.Current.Cache["Sermons"] = sermonsCollection;
            }

            return sermonId;
        }

        /// <summary>
        /// Deletes the sermon.
        /// </summary>
        /// <param name="sermonId">The sermon identifier.</param>
        public void DeleteSermon(int sermonId)
        {
            Guard.ParameterNotOutOfRange(sermonId, "sermonId");

            this.sermonRepository.Delete(sermonId);
            this.sermonRepository.Save();

            this.RemoveCollectionFromCache();
        }

        /// <summary>
        /// Gets the sermon.
        /// </summary>
        /// <param name="sermonId">The identifier.</param>
        /// <returns></returns>
        public ISermon GetSermon(int sermonId)
        {
            Guard.ParameterNotOutOfRange(sermonId, "sermonId");

            return this.sermonRepository.GetById(sermonId).ToModel();
        }

        /// <summary>
        /// Gets the sermons.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISermon> GetSermons()
        {
            var sermons = new List<ISermon>();

            if ((HttpContext.Current.Cache != null) && (HttpContext.Current.Cache["Sermons"] != null))
            {
                sermons = (List<ISermon>)HttpContext.Current.Cache["Sermons"];
            }
            else
            {
                var sermonsCollection = this.sermonRepository.Get();

                sermons.AddRange(sermonsCollection.ToModelCollection());

                this.AddCollectionToCache(sermons);
            }

            return sermons;
        }

        /// <summary>
        /// Updates the sermon.
        /// </summary>
        /// <param name="sermonToUpdate">The sermon to update.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">sermonToUpdate</exception>
        public bool UpdateSermon(ISermon sermonToUpdate)
        {
            if (sermonToUpdate == null)
            {
                throw new ParameterNullException("sermonToUpdate");
            }

            try
            {
                this.sermonRepository.Update(sermonToUpdate);

                this.RemoveCollectionFromCache();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void AddCollectionToCache(IEnumerable<ISermon> sermons)
        {
            if (HttpContext.Current.Cache != null)
            {
                HttpContext.Current.Cache.Add("Sermons", sermons, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 5, 0, 0),
                    CacheItemPriority.Normal, null);
            }
        }

        private void RemoveCollectionFromCache()
        {
            if ((HttpContext.Current.Cache != null) && (HttpContext.Current.Cache["Sermons"] != null))
            {
                HttpContext.Current.Cache.Remove("Sermons");
            }
        }
    }
}