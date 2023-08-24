using RGN.ImplDependencies.Attributes;
using RGN.ImplDependencies.Core.Messaging;
using System;
using System.Collections.Generic;

namespace RGN.Modules.Messaging.Runtime
{
    [InjectImplDependency(typeof(IMessaging))]
    public sealed class Messaging : IMessaging
    {
        private readonly List<TokenReceivedEventArgsBinder> mTokenListeners = new List<TokenReceivedEventArgsBinder>();
        private readonly List<MessageReceivedEventArgsBinder> mMessageListeners = new List<MessageReceivedEventArgsBinder>();

        event Action<object, ITokenReceivedEventArgs> IMessaging.TokenReceived
        {
            add
            {
                Action<object, ITokenReceivedEventArgs> toSubscribe = value;
                mTokenListeners.Add(new TokenReceivedEventArgsBinder(toSubscribe));
            }
            remove
            {
                Action<object, ITokenReceivedEventArgs> toUnsubcribe = value;
                var listener = mTokenListeners.Find((binder) => binder.ToSubcribe == toUnsubcribe);
                if (listener == null)
                {
                    return;
                }
                listener.Dispose();
                mTokenListeners.Remove(listener);
            }
        }

        event Action<object, IMessageReceivedEventArgs> IMessaging.MessageReceived
        {
            add
            {
                Action<object, IMessageReceivedEventArgs> toSubscribe = value;
                mMessageListeners.Add(new MessageReceivedEventArgsBinder(toSubscribe));
            }
            remove
            {
                Action<object, IMessageReceivedEventArgs> toUnsubcribe = value;
                var listener = mMessageListeners.Find((binder) => binder.ToSubcribe == toUnsubcribe);
                if (listener == null)
                {
                    return;
                }
                listener.Dispose();
                mMessageListeners.Remove(listener);
            }
        }
    }
}
