using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Food.Common
{
    public class GeneralError
    {
        public IList<Error> Errors { get; set; }
    }
}
