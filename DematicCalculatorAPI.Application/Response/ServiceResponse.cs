namespace DematicCalculatorAPI.Application.Response
{
    public class ServiceResponse<T> where T : class
    {
        /// <summary>
        /// Entity to return 
        /// </summary>
        public T Entity { get; set; }
        /// <summary>
        /// Flag if response is successful or not
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Error message to return. This error message must be user friendly.
        /// The error should not contain sensitive information
        /// The error will be returned to the user to help them identify the issue
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Create a full service response 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="entity"></param>
        public ServiceResponse(bool success, string message, T entity)
        {
            Entity = entity;
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Create a success service response with entity
        /// </summary>
        /// <param name="entity"></param>
        public ServiceResponse(T entity) : this(true, string.Empty, entity){}
        /// <summary>
        /// Create an error service response
        /// </summary>
        /// <param name="message"></param>
        public ServiceResponse(string message) : this(false, message, null){}
    }
}
