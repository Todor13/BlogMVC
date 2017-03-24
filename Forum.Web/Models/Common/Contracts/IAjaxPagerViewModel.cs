namespace Forum.Web.Models.Common.Contracts
{
    public interface IAjaxPagerViewModel
    {
        string ActionName { get; set; }
        string ControllerName { get; set; }
        int CurrentPage { get; set; }
        int ItemsCount { get; set; }
        int PagesCount { get; }
        int PageSize { get; set; }
        string UpdateTarget { get; set; }
    }
}