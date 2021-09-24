using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarbonPlatformExercise.Data
{
   public class Book : BaseEntity
    {
        [Required]
        public string Text { get; set; }
        [Required]
        [MaxLength(8)]
        public string Url { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
