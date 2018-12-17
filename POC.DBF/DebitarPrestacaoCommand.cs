using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace POC.DTO
{
    public class DebitarPrestacaoCommand
    {
        [DataMember]
        public int SequencialPrestacao { get; set; }

        [DataMember]
        public int SequencialOperacao { get; set; }

        [DataMember]
        public decimal Valor { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
}