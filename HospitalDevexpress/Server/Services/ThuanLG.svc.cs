using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        private readonly Dictionary<Guid, IChatServiceCallback> clients = new Dictionary<Guid, IChatServiceCallback>();

        #region IChatService
        Guid IChatService.Subscribe()
        {
            IChatServiceCallback callback =
                OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

            Guid clientId = Guid.NewGuid();

            if (callback != null)
            {
                lock (clients)
                {
                    clients.Add(clientId, callback);
                }
            }

            return clientId;
        }
        void IChatService.Unsubscribe(Guid clientId)
        {
            lock (clients)
            {
                if (clients.ContainsKey(clientId))
                {
                    clients.Remove(clientId);
                }
            }
        }

        void IChatService.KeepConnection()
        {
        }
        void IChatService.SendMessage(Guid senderClientId, string message, Guid recieverClientId)
        {
            BroadcastMessage(senderClientId, message, recieverClientId);
        }

        #endregion
        private void BroadcastMessage(Guid senderId, string message, Guid recieverId)
        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (clients)
                    {
                        List<Guid> disconnectedClientGuids = new List<Guid>();
                        foreach (KeyValuePair<Guid, IChatServiceCallback> client in clients)
                        {
                            if (client.Key == recieverId)
                            {
                                try
                                {
                                    client.Value.HandleMessage(message);
                                }
                                catch (Exception)
                                {
                                    disconnectedClientGuids.Add(client.Key);
                                }
                                break;
                            }
                        }
                        foreach (Guid clientGuid in disconnectedClientGuids) clients.Remove(clientGuid);
                    }
                }
            );
        }
    }
}
