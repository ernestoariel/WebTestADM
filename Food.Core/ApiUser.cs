using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core
{
    /// <summary>
    /// Application User
    /// </summary>
    public class ApiUser
    {
        [Key]
        public int ApiUserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
