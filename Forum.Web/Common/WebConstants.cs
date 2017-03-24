namespace Forum.Web.Common
{
    public class WebConstants
    {
        public const int PageSize = 3;

        public const int ThreadListCount = 7;
        public const string PagerPartialView = "_Pager";
        public const string HomeController = "Home";
        public const string SearchController = "Search";


        //Users Area Constants
        public const int ActivitySubString = 100;
        public const int ActivityPageSize = 3;
        public const int UsersPageSize = 3;
        public const string UpdateTarget = "forum-activity";
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
    }
}