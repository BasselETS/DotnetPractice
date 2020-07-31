namespace WebApp_Core.Helpers
{
    public class PartsParams
    {
        private const int maxPageSize = 50;
        public int pageNumber { get; set; }= 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value>maxPageSize ? maxPageSize : value); }
        }

        public bool ByPrice {get;set;} = false;
        public bool ByDate {get;set;} = false;

    }
}