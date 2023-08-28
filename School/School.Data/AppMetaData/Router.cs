namespace School.Data.AppMetaData
{
    public static class Router
    {
        public const string SingleRoute = "/{id}";
        public const string root = "Api";
        public const string vertion = "V1";
        public const string Rule = root + "/" + vertion + "/";

        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + SingleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
            public const string Paginated = Prefix + "/Paginated";

        }
        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "Department";
            public const string GetByID = Prefix + SingleRoute;
            public const string List = Prefix + "/List";
            public const string Paginated = Prefix + "/Paginated";


        }
        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule + "User";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + SingleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
            public const string Paginated = Prefix + "/Paginated";
            public const string ChangePassword = Prefix + "/ChangePassword";

        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Rule + "Authentication";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + SingleRoute;
            public const string SignIn = Prefix + "/SignIn";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
            public const string Paginated = Prefix + "/Paginated";
            public const string ChangePassword = Prefix + "/ChangePassword";
            public const string RefreshToken = Prefix + "/RefreshToken";
            public const string ValidateToken = Prefix + "/ValidateToken";

        }

        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "Authorization";
            public const string List = Prefix + "Role/List";
            public const string GetByID = Prefix + "/Role/GetRoleById/{id}";
            public const string Create = Prefix + "/Role/Create";
            public const string Edit = Prefix + "/Role/Edit";
            public const string Delete = Prefix + "/Role/Delete/{id}";
            public const string Paginated = Prefix + "/Paginated";

        }


    }
}
