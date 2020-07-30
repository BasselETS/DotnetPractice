namespace WebApp_Core.Models
{
    public class PartAndUser
    {
        public int ID { get; set; }
        public int UserId { get; set; } 
        public int PartId { get; set; }

        public bool wishList { get; set; }

        public virtual User User { get; set; }
        public virtual Part Part  { get; set; }
    }
}