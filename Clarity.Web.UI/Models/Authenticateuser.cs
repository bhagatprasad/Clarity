using System.ComponentModel.DataAnnotations;

namespace Clarity.Web.UI.Models
{
    public class Authenticateuser
    {
        public string username { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
