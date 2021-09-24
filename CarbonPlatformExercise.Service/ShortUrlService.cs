using CarbonPlatformExercise.Data;
using CarbonPlatformExercise.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonPlatformExercise.Service
{
    /// <summary>
    /// Interface to generate short url
    /// </summary>
    public interface IShortUrlService
    {
        /// <summary>
        /// This method generates random url with a given length
        /// </summary>
        /// <param name="length">The length of the string to be generated.</param>
        /// <returns>String</returns>
        Task<string> GenerateUrlCode(int length = 8);
    }

    /// <summary>
    /// The implementation
    /// </summary>
    public class ShortUrlService : IShortUrlService
    {
        private readonly IRepository<Book> _repository;
        public ShortUrlService(IRepository<Book> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// This method generates random url with a given length
        /// </summary>
        /// <param name="length">The length of the string to be generated.</param>
        /// <returns>String</returns>
        public async Task<string> GenerateUrlCode(int length = 8)
        {
            var code = GetCode(length);
            while (true)
            {
                code = GetCode(length);
                if ((await _repository.GetAll()).Any(itm => itm.Url == code))
                {
                    continue;
                }
                break;
            }
            return code;
        }

        private string GetCode(int length)
        {
            return Guid.NewGuid().ToString("n").Substring(0, length);
        }
    }
}
