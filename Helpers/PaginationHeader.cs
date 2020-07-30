namespace WebApp_Core.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }    
        public int ItemsPerPage { get; set; }   
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public PaginationHeader(int curentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            this.CurrentPage = curentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
        }
    }
}