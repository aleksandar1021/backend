namespace Backend.ViewModels
{
    public class ResponseMessage<T>
        where T : class
    {
        public T Data { get; set; }
        public string Error { get; set; }

    }
}
