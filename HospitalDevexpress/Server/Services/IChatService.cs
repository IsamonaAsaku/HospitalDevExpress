using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Services
{
    /// <summary>
    /// Service contract containing chat-related operations.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        Guid Subscribe();
        [OperationContract(IsOneWay = true)]
        void Unsubscribe(Guid clientId);

        [OperationContract(IsOneWay = true)]
        void KeepConnection();
        [OperationContract]
        void SendMessage(Guid senderClientId, string message, Guid recieverClientId);
    }
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void HandleMessage(string message);
    }
}
