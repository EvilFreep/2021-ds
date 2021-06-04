using NATS.Client;

namespace ClassesLibrary
{
    public static class NatsFactory
    {
        public static IConnection GetNatsConnection()
        {
            var options = ConnectionFactory.GetDefaultOptions();
            options.Url = Constants.HostName;
            return new ConnectionFactory().CreateConnection(options);
        }
    }
}