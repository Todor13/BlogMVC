using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Factories
{
    public interface IPagerViewModelFactory
    {
        IPagerViewModel CreatePagerViewModel(string controllerName, int currentPage, int itemsCount, int pageSize);

        IAjaxPagerViewModel CreateAjaxPagerViewModel(string controllerName, string actionName, string updateTarget, int currentPage, int itemsCount, int pageSize);
    }
}