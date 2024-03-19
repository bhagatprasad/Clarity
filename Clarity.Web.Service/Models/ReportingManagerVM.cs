namespace Clarity.Web.Service.Models
{
    public class ReportingManagerVM
    {
        public long RepotingManagerId { get; set; }
        public long EmployeeId { get; set; }

        public long ManagerId { get; set;}
        public string EmployeeCode { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeName { get; set; }
        public string ManagerCode { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerName { get; set; }
    }
}
