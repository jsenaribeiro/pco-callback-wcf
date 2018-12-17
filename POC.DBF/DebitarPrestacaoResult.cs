using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.DTO
{
    public class DebitarPrestacaoResult
    {
        [DataMember]
        public int NSU { get; set; }

        [DataMember]
        public Erro Erro { get; set; }

        [DataMember]
        public int SequencialPrestacao { get; set; }
    }
}