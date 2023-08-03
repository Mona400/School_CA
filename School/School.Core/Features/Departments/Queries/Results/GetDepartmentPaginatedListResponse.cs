namespace School.Core.Features.Departments.Queries.Results
{
    public class GetDepartmentPaginatedListResponse
    {
        public GetDepartmentPaginatedListResponse(int deptID, string? name)
        {
            DeptID = deptID;
            Name = name;
        }

        public int DeptID { get; set; }
        public string? Name { get; set; }
    }

}
