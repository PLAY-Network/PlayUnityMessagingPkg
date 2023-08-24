using RGN.ImplDependencies.Core.Messaging;
using System;
using FirebaseMessaging = Firebase.Messaging.FirebaseMessaging;
using FirebaseMessageReceivedEventArgs = Firebase.Messaging.MessageReceivedEventArgs;

namespace RGN.Modules.Messaging.Runtime
{
    internal sealed class MessageReceivedEventArgsBinder : IDisposable
    {
        internal Action<object, IMessageReceivedEventArgs> ToSubcribe { get; private set; }

        public MessageReceivedEventArgsBinder(Action<object, IMessageReceivedEventArgs> toSubcribe)
        {
            ToSubcribe = toSubcribe;
            FirebaseMessaging.MessageReceived += OnMessageReceived;
        }
        public void Dispose()
        {
            ToSubcribe = null;
            FirebaseMessaging.MessageReceived -= OnMessageReceived;
        }

        private void OnMessageReceived(
            object sender,
            FirebaseMessageReceivedEventArgs eventArgs)
        {
            ToSubcribe.Invoke(sender, new MessageReceivedEventArgs(eventArgs));
        }
    }
}
