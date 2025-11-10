namespace SalesWebMvc.Services.Exceptions
{
    public class NotFoundExeption: ApplicationException
    {
        public NotFoundExeption(string message) : base(message) 
            {
                
            }
    }
}
