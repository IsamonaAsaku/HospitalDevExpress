using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceHospital" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServiceHospitalCallback))]
    public interface IServiceHospital
    {
        [OperationContract(IsOneWay = true)]
        void ProcessReport();
        [OperationContract()]
        int Divide(int a, int b);
    }
    public interface IServiceHospitalCallback
    {
        [OperationContract(IsOneWay = true)]
        void Process(int percentangeCompleted);
    }
}
