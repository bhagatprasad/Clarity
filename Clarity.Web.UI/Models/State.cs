using Clarity.Web.UI.Utility;

namespace Clarity.Web.UI.Models
{
    public class State : Common
    {
        public long StateId { get; set; }
        public long? CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StateCode { get; set; }
    }
}
