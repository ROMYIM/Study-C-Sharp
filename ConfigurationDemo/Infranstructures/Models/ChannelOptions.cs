namespace ConfigurationDemo.Infranstructures.Models
{
    public class ChannelOptions<T>
    {
        public string PostType { get; set; }

        public string Channel { get; set; }

        public T Options { get; set; }
    }
}