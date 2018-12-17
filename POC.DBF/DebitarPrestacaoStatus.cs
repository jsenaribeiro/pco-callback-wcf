using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.DTO
{
    public class DebitarPrestacaoStatus
    {
        public DebitarPrestacaoStatus() { }

        public DebitarPrestacaoStatus(DebitarPrestacaoCommand cmd)
        {
            this.SequencialPrestacao = cmd.SequencialPrestacao;
            this.StatusPrestacaoFila = DebitarPrestacaoEnum.Sucesso;
        }

        [DataMember]
        public int SequencialPrestacao { get; set; }

        [DataMember]
        public DebitarPrestacaoEnum StatusPrestacaoFila { get; set; }
    }
}