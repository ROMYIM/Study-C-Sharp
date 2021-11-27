namespace WcfSample.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}