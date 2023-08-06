namespace School.Core.Features.AppicationUser.Queries.Results
{
    public class GetUserPaginationListResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
