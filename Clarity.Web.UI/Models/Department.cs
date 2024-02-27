using Clarity.Web.UI.Utility;

namespace Clarity.Web.UI.Models
{
    public class Department : Common
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
