namespace Forum.Web.Common
{
    public class WebConstants
    {
        public const int PageSize = 3;

        public const int ThreadListCount = 7;
        public const string PagerPartialView = "_Pager";
        public const string AjaxPartialView = "_AjaxPager";
        public const int IndexPageTitleSubstring = 70;

        //Forum Area Constants
        public const string HomeController = "Home";
        public const string IndexAction = "Index";
        public const string SearchController = "Search";
        public const string ThreadController = "Thread";
        public const string AnswerPartialView = "_Answer";
        public const string CommentPartialView = "_Comment";

        //Users Area Constants
        public const int ActivitySubString = 100;
        public const int ActivityPageSize = 3;
        public const int UsersPageSize = 3;
        public const string UsersActivityUpdateTarget = "forum-activity";
        public const string Profile = "Profile";
        public const string GetUserThreads = "GetUserThreads";
        public const string GetUserAnswers = "GetUserAnswers";
        public const string GetUserComments = "GetUserComments";
        public const string ThreadsPartialView = "_Threads";
        public const string AnswersPartialView = "_Answers";
        public const string CommentsPartialView = "_Comments";
        public const string UsersListPartialView = "_UsersList";
        public const string UsersSearchPartialView = "_UsersSearch";

        //Messages
        public const string UserNotFound = "User not found";
        public const string IUowDataNullMessage= "An instance of IUowData is required to use this repository!";
        public const string IPagerViewModelFactoryNullMessage = "An instance of IPagerViewModelFactory is required to use this repository!";
        public const string IViewModelFactoryNullMessage = "An instance of IViewModelFactory is required to use this repository!";
        public const string IMappingServiceNullMessage = "An instance of IMappingService is required to use this repository!";
    }
}