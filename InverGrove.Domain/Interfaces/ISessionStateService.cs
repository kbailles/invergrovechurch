using System;

namespace InverGrove.Domain.Interfaces
{
    public interface ISessionStateService
    {
        /// <summary>
        /// Adds the specified value to Session using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Add(string key, object value);

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(string key);

        /// <summary>
        /// Removes the specified value from Session using the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

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
        TResult TryGet<TResult>(string key, Func<TResult> serviceFunc = null, bool cacheResult = true);

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
        TResult TryGet<T1, TResult>(string key, T1 t1, Func<T1, TResult> serviceFunc = null, bool cacheResult = true);

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
        TResult TryGet<T1, T2, TResult>(string key, T1 t1, T2 t2, Func<T1, T2, TResult> serviceFunc);

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
        TResult TryGet<T1, T2, T3, TResult>(string key, T1 t1, T2 t2, T3 t3, Func<T1, T2, T3, TResult> serviceFunc);

        /// <summary>
        /// Tries to get the value from Session with the specified key; if it does not exist just 
        /// returns the default return type value.
        /// The value is NOT added to Session if it does NOT exist.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        TResult TryGetValue<TResult>(string key);

        /// <summary>
        /// Gets the object from Session for the specified key.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        TResult Get<TResult>(string key);
    }
}