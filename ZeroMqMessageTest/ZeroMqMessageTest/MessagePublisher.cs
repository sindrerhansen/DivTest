using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace ZeroMqMessageTest
{
    public static class MessagePublisher
    {
        #region Private Fields
        private static bool isRunning;
        private static CancellationTokenSource cts;
        private static ConcurrentQueue<object> outbox;
        private static Task taskP;
        private static ZContext context;
        private const string AppConfigPublishEndpointKey = "MessageBusPublishEndpoint";
        private static string endpointUrlPublisher;

        #endregion

        #region Constructor
        static MessagePublisher()
        {
            cts = new CancellationTokenSource();
            outbox = new ConcurrentQueue<object>();

            if (!ConfigurationManager.AppSettings.AllKeys.Any(k => k == AppConfigPublishEndpointKey))
                throw new Exception(AppConfigPublishEndpointKey + " setting is missing in app.config");

            endpointUrlPublisher = ConfigurationManager.AppSettings[AppConfigPublishEndpointKey];
            Console.WriteLine("Sending on " + endpointUrlPublisher);
        }
        #endregion

        #region Public Methods
        public static void Start()
        {
            try
            {
                if (!isRunning)
                {
                    cts.Dispose();
                    cts = new CancellationTokenSource();
                    isRunning = true;
                    context = new ZContext();
                    taskP = Task.Factory.StartNew(() => Publisher(context), cts.Token);
                }

            }
            catch (Exception ex)
            {
                //TODO: Add logging
            }
        }

        public static void Stop()
        {
            cts.Cancel();
        }
        public static void Publish<T>(T message)
        {
            if (message == null)
                return;
            outbox.Enqueue(message);
        }

        #endregion

        #region Private Metods
        private static void Publisher(ZContext context)
        {
            using (var publisher = new ZSocket(context, ZSocketType.PUB))
            {
                publisher.Bind(endpointUrlPublisher);

                while (isRunning && !cts.IsCancellationRequested)
                {
                    object currentMessage;
                    if (outbox.TryDequeue(out currentMessage))
                    {
                        using (var message = new ZMessage())
                        {
                            try
                            {
                                var json = JsonConvert.SerializeObject(currentMessage);
                                message.Add(new ZFrame("info"));
                                message.Add(new ZFrame(json));
                                publisher.Send(message);
                            }
                            catch (Exception ex)
                            {
                                //TODO: Add logging
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
                isRunning = false;
            }
        }
        #endregion
    }
}
