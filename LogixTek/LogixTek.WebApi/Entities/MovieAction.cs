namespace LogixTek.WebApi.Entities
{
    public class MovieAction
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public int ByUserId { get; set; }

        public int MovieId { get; set; }

        public bool? IsActive { get; set; }
    }
}
