using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace POC.DTO
{
    public static class DBFake
    {
        public static List<DebitarPrestacaoCommand> postados;
        public static List<DebitarPrestacaoStatus> controlados;
        public static List<DebitarPrestacaoResult> processados;
    }
}