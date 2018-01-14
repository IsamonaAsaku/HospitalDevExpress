using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceHospital" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceHospital.svc or ServiceHospital.svc.cs at the Solution Explorer and start debugging.
    public class ServiceHospital : IServiceHospital
    {
        public int Divide(int a, int b)
        {
            if (b == 0) throw new FaultException("Denominator cannot be ZERO", new FaultCode("DivideByZeroFault"));
            return a / b;
        }

        public void ProcessReport()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(50);
                OperationContext.Current.GetCallbackChannel<IServiceHospitalCallback>().Process(i + 1);
            }
        }
    }
}
