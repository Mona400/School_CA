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
            public const string Roles = Prefix + "Roles";
            public const string Claims = Prefix + "Claims";
            public const string List = Roles + "/List";
            public const string GetByID = Roles + "/GetRoleById/{id}";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string ManageUserRoles = Roles + "/ManageUserRoles/{id}";
            public const string ManageUserClaims = Claims + "/ManageUserClaims/{id}";
            public const string Delete = Roles + "/Delete/{id}";
            public const string UpdateUserClaims = Claims + "/UpdateUserClaims";
            public const string Paginated = Roles + "/Paginated";

        }


    }
}
