using System;
using System.Reactive;
using System.Reactive.Disposables;

namespace ReactiveBits.CreatingObservables.UsingObservableBase
{
    public class ObservableConnection : ObservableBase<string>
    {
        private readonly IChatConnection _chatConnection;

        public ObservableConnection(IChatConnection chatConnection)
        {
            _chatConnection = chatConnection;
        }

        protected override IDisposable SubscribeCore(IObserver<string> observer)
        {
            Action<string> onReceived = message => observer.OnNext(message);
            Action onCompleted = () => observer.OnCompleted();
            Action<Exception> onError = exception => observer.OnError(exception);

            _chatConnection.Received += onReceived;
            _chatConnection.Closed += onCompleted;
            _chatConnection.Error += onError;

            return Disposable.Create(() =>
            {
//                _chatConnection.Disconnect();

                _chatConnection.Received -= onReceived;
                _chatConnection.Closed -= onCompleted;
                _chatConnection.Error -= onError;
            });

        }
    }
}