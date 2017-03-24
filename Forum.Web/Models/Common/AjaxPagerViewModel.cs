using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Models.Common
{
    public class AjaxPagerViewModel : IAjaxPagerViewModel
    {
        public AjaxPagerViewModel(string controllerName, string actionName, string updateTarget, int currentPage, int itemsCount, int pageSize)
        {
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.UpdateTarget = updateTarget;
            this.CurrentPage = currentPage;
            this.ItemsCount = itemsCount;
            this.PageSize = pageSize;
        }

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