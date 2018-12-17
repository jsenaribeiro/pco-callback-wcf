using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.DTO
{
    public class ServiceResult
    {
        [DataMember]
        public Erro Erro { get; set; }
    }
}