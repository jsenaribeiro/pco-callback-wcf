using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.DTO
{
    public enum DebitarPrestacaoEnum
    {
        [DataMember] Pendente = 0,
        [DataMember] Sucesso = 1,
        [DataMember] Falha = 2
    }
}