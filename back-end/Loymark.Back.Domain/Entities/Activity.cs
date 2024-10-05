using Loymark.Back.Domain.Entities.Base;

namespace Loymark.Back.Domain.Entities
{
    public class Activity : EntityBase<int>
    {
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string ActivityType { get; set; } = string.Empty;

        public virtual User User { get; set; } = default!;
    }
}
