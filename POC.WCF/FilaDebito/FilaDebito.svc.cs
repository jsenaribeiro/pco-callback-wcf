using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POC.DTO;
using POC.JOB;

namespace POC.WCF.FilaDebito
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FilaDebito" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FilaDebito.svc or FilaDebito.svc.cs at the Solution Explorer and start debugging.
    public class FilaDebito : IFilaDebito
    {
        public ServiceResult Dispatch(int sequencialPrestacao, string callbackURL = null)
        {
            ServicoFilaDebitoFake.PostarNaFila(sequencialPrestacao, callbackURL);

            return new ServiceResult();
        }

        public ServiceResult Listener(int sequencialPrestacao, bool pagou)
        {
            Console.WriteLine("Passou pelo listener");

            // se pagou, registra pagamentos
            // se nao pagou, muda de sucesso para pendente no status da fila

            return new ServiceResult();
        }
    }
}
