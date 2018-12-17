using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.DTO
{
    public class Erro
    {
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}