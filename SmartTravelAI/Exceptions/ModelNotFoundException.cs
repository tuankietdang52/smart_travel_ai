namespace SmartTravelAI.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException() : base()
        {
            
        }

        public ModelNotFoundException(string message) : base(message)
        {
            
        }
    }
}