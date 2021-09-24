using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarbonPlatformExercise.Api.Dto
{
    public class BookDto
    {
        public string Text { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
