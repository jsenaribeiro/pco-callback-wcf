using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POC.DTO;

namespace POC.DTO
{
    [ServiceContract]
    public interface IFilaDebito
    {
        [OperationContract]
        ServiceResult Dispatch(int sequencialPrestacao);

        [OperationContract]
        ServiceResult Listener(int sequencialPrestacao, bool pago);
    }
}
