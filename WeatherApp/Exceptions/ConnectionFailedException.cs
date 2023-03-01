namespace WeatherApp.Exceptions
{
    public class ConnectionFailedException: Exception
    {
        public ConnectionFailedException(string message) : base(message)
        {

        }
    }
}
