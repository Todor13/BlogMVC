namespace Forum.Web.Models.Common.Contracts
{
    public interface IPagerViewModel
    {
        string ControllerName { get; set; }
        int CurrentPage { get; set; }
        int ItemsCount { get; set; }
        int PagesCount { get; }
        int PageSize { get; set; }
    }
}