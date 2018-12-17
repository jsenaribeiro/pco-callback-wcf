using POC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace POC.JOB
{
    public static class ServicoFilaDebitoFake
    {
        public static void PostarNaFila(int sequencialPrestacao, string callbackURL)
        {
            // PROCESSAR DEBITO NO S134
            // consulta dados a partir do sequencial da prestacao (conta, agencia, operacao)
            bool pagou = true; // checa se realmente foi pago

            // CHAMA FILA DE RETORNOS

            if (callbackURL == null) Console.WriteLine("posta na fila de retornos");
            else CallbackWCF(callbackURL, sequencialPrestacao, pagou);
        }

        public static void CallbackWCF(string wcf, int seq, bool pagou)
        {
            var uri = new Uri(wcf);

            var mybinding = new BasicHttpBinding();
            var myendpoint = new EndpointAddress(wcf);
            var mychannelfactory = new ChannelFactory<IFilaDebito>(mybinding, myendpoint);

            var instance = mychannelfactory.CreateChannel();
            var result = instance.Listener(seq, pagou);

            mychannelfactory.Close();

            //new DynamicWCF(wcf, "IFilaDebito", "Listener", new object[] { res });
        }
    }
}
