namespace Loymark.Back.Application.DTOs
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty;
    }
}
