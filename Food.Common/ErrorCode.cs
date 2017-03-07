using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Common
{
    public static class ErrorCode
    {
        public static readonly string ENTITY_WAS_NOT_FOUND  = "ENTITY_WAS_NOT_FOUND";
        public static readonly string FIELD_IS_EMPTY = "FIELD_IS_EMPTY";
        public static readonly string NEW_ENTITY_ID_WITH_VALUE = "NEW_ENTITY_ID_WITH_VALUE";
        public static readonly string DUPLICATED_VALUES = "DUPLICATED_VALUES";
        public static readonly string NEW_ENTITY_IS_ALREADY_IN_REPOSITORY = "NEW_ENTITY_IS_ALREADY_IN_REPOSITORY";
        
    }
}
