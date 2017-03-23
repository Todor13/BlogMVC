namespace Forum.Web.Areas.Users.Models
{
    public class AjaxPagerViewModel
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int ItemsCount { get; set; }

        public int PagesCount
        {
            get { return (ItemsCount / PageSize) + (ItemsCount % PageSize == 0 ? 0 : 1); }
        }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string UpdateTarget { get; set; }
    }
}