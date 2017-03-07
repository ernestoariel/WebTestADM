using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Common
{
    public class Error
    {
        public string ErrorCode { get; set; }
        public string UserMessage { get; set; }
        public string DeveloperMessage { get; set; }
        public string Type { get; set; }
        public string PropertyName { get; set; }
    }
}
