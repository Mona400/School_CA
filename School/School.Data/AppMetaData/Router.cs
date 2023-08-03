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


    }
}
