namespace Forum.Web.Models.Common
{
    public class PagingViewModel
    {
        public int CurrentPage { get; set; }

        public int ItemsCount { get; set; }

        public int PageSize { get; set; }

        public int PagesCount
        {
            get
            {
                return (ItemsCount / PageSize) + (ItemsCount % PageSize == 0 ? 0 : 1);
            }
        }

        public string ControllerName { get; set; }
    }
}