using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Model
{
    public class AuthToken
    {
        [Key]
        public int AuthTokenId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int ApiUserId { get; set; }
        public ApiUser ApiUser { get; set; }
    }
}
