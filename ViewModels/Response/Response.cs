namespace FinaControl.ViewModels.Response;

public class Response<T>
{
    public T Data { get; private set; }
    public List<string> Errors { get; private set; } = new();
    
    public Response(T data)
    {
        Data = data;
    }
    
    public Response(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }
    
    public Response(List<string> errors)
    {
        Errors = errors;
    }
}