using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.DTO
{
    public class EndPoint
    {
        public string URL { get; set; }

        public string ContractName { get; set; }

        public string OperationName { get; set; }

        public object[] Parameters { get; set; }
    }
}
