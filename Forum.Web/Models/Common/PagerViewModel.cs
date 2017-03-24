using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Models.Common
{
    public class PagerViewModel : IPagerViewModel
    {

        public PagerViewModel(string controllerName, int currentPage, int itemsCount, int pageSize)
        {
            this.ControllerName = controllerName;
            this.CurrentPage = currentPage;
            this.ItemsCount = itemsCount;
            this.PageSize = pageSize;
        }

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