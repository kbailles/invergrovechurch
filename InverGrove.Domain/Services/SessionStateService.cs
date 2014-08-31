using System;
using System.Web;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Services
{
    public class SessionStateService : ISessionStateService
    {
        /// <summary>
        /// Adds the specified value to Session using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (this.CurrentSessionExists())
            {
                HttpContext.Current.Session[key] = value;

                //this.logService.WriteToLog("Value was added to Session with key of: " + key);
            }
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (this.CurrentSessionExists() && (HttpContext.Current.Session[key] != null))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog("Exception thrown when checking Session Contains");
                //this.logService.WriteToErrorLog(e.StackTrace);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified value from Session using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (this.CurrentSessionExists())
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        /// <summary>
        /// Tries to get the value from Session with the specified key, if it does not exist, 
        /// it will call the Func and set the Session[key] to that returned value if the Func is not null and cacheResult is true.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="serviceFunc">The service function.</param>
        /// <param name="cacheResult">if set to <c>true</c> [cache result].</param>
        /// <returns></returns>
        /// <exception cref="Patterson.Infrastructure.Exceptions.ParameterNullException">key</exception>
        public TResult TryGet<TResult>(string key, Func<TResult> serviceFunc = null, bool cacheResult = true)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            try
            {
                if (this.Contains(key))
                {
                    return (TResult)HttpContext.Current.Session[key];
                }

                if (serviceFunc != null)
                {
                    TResult result = serviceFunc.Invoke();

                    if (cacheResult)
                    {
                        this.AddToSession(key, result);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(e);
            }

            return default(TResult);
        }

        /// <summary>
        /// Tries to get the value from Session with the specified key, if it does not exist, 
        /// it will call the Func and set the Session[key] to that returned value if the Func is not null and cacheResult is true.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter used in the Func.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="t1">The t1 (first parameter value of the Func).</param>
        /// <param name="serviceFunc">The service func.</param>
        /// <param name="cacheResult">if set to <c>true</c> [cache result].</param>
        /// <returns></returns>
        public TResult TryGet<T1, TResult>(string key, T1 t1, Func<T1, TResult> serviceFunc = null, bool cacheResult = true)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            try
            {
                if (this.Contains(key))
                {
                    return (TResult)HttpContext.Current.Session[key];
                }

                if (serviceFunc != null)
                {
                    TResult result = serviceFunc.Invoke(t1);

                    if (cacheResult)
                    {
                        this.AddToSession(key, result);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(e);
            }

            return default(TResult);
        }

        /// <summary>
        /// Tries to get the value from Session with the specified key, if it does not exist, 
        /// it will call the Func and set the Session[key] to that returned value.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter used in the Func.</typeparam>
        /// <typeparam name="T2">The type of the second parameter used in the Func.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="serviceFunc">The service func.</param>
        /// <returns></returns>
        public TResult TryGet<T1, T2, TResult>(string key, T1 t1, T2 t2, Func<T1, T2, TResult> serviceFunc)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (serviceFunc == null)
            {
                throw new ArgumentNullException("serviceFunc");
            }

            try
            {
                if (this.Contains(key))
                {
                    return (TResult)HttpContext.Current.Session[key];
                }

                TResult result = serviceFunc.Invoke(t1, t2);

                this.AddToSession(key, result);

                return result;

            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(e);
            }

            return default(TResult);
        }

        /// <summary>
        /// Tries to get the value from Session with the specified key, if it does not exist, 
        /// it will call the Func and set the Session[key] to that returned value.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <param name="t3">The t3.</param>
        /// <param name="serviceFunc">The service func.</param>
        /// <returns></returns>
        public TResult TryGet<T1, T2, T3, TResult>(string key, T1 t1, T2 t2, T3 t3, Func<T1, T2, T3, TResult> serviceFunc)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (serviceFunc == null)
            {
                throw new ArgumentNullException("serviceFunc");
            }

            try
            {
                if (this.Contains(key))
                {
                    return (TResult)HttpContext.Current.Session[key];
                }

                TResult result = serviceFunc.Invoke(t1, t2, t3);

                this.AddToSession(key, result);

                return result;

            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(e);
            }

            return default(TResult);
        }

        /// <summary>
        /// Tries to get the value from Session with the specified key; if it does not exist just 
        /// returns the default return type value.
        /// The value is NOT added to Session if it does NOT exist.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TResult TryGetValue<TResult>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            try
            {
                if (this.Contains(key))
                {
                    return (TResult)HttpContext.Current.Session[key];
                }
            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(e);
            }

            return default(TResult);
        }

        /// <summary>
        /// Gets the object from Session for the specified key.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TResult Get<TResult>(string key)
        {
            if (this.CurrentSessionExists() && (HttpContext.Current.Session[key] != null))
            {
                return (TResult)HttpContext.Current.Session[key];
            }
            return default(TResult);
        }

        private void AddToSession<T>(string key, T value)
        {
            // ReSharper disable RedundantCast
            if ((object)value != null)
            // ReSharper restore RedundantCast
            {
                this.Add(key, value);
            }
        }

        private bool CurrentSessionExists()
        {
            return (HttpContext.Current != null) && (HttpContext.Current.Session != null);
        } 
    }
}