using System;

namespace CarbonPlatformExercise.Data
{
    public abstract class BaseEntity<TPrimaryKey>
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
        public TPrimaryKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<int>
    { }

}
