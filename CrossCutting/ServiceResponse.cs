namespace CrossCutting
{
    public class ServiceResponse<T>
    {
        public T Entity { get; set; }
        public string Details { get; set; }
        public bool IsSucess { get; set; }
        public int Code { get; set; }
        public IList<string> Errors { get; set; }

        public static ServiceResponse<T> CreateSucessResponse()
        {
            return CreateSucessResponse(default(T), "Sucess");
        }
        public static ServiceResponse<T> CreateSucessResponse(T entity)
        {
            return CreateSucessResponse(entity, "Sucess");
        }
        public static ServiceResponse<T> CreateSucessResponse(string statusDetail)
        {
            return CreateSucessResponse(default(T), statusDetail);
        }
        public static ServiceResponse<T> CreateSucessResponse(string statusDetail, int code)
        {
            return CreateSucessResponse(default(T), statusDetail, code);
        }

        public static ServiceResponse<T> CreateSucessResponse(T entity, string statusDetail)
        {
            ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
            serviceResponse.Entity = entity;
            serviceResponse.Details = statusDetail;
            serviceResponse.IsSucess = true;
            return serviceResponse;
        }
        public static ServiceResponse<T> CreateSucessResponse(T entity, string statusDetail, int code)
        {
            ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
            serviceResponse.Entity = entity;
            serviceResponse.Details = statusDetail;
            serviceResponse.Code = code;
            serviceResponse.IsSucess = true;
            return serviceResponse;
        }
        public static ServiceResponse<T> CreateFailedResponse()
        {
            return CreateFailedResponse(default(T), "Failed");
        }
        public static ServiceResponse<T> CreateFailedResponse(T entity)
        {
            return CreateFailedResponse(entity, "Failed");
        }
        public static ServiceResponse<T> CreateFailedResponse(string statusDetail)
        {
            return CreateFailedResponse(default(T), statusDetail);
        }
        public static ServiceResponse<T> CreateFailedResponse(string statusDetail, int code)
        {
            return CreateFailedResponse(default(T), statusDetail, code);
        }

        public static ServiceResponse<T> CreateFailedResponse(T entity, string statusDetail, int code = 999)
        {
            ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
            serviceResponse.Entity = entity;
            serviceResponse.Details = statusDetail;
            serviceResponse.IsSucess = false;
            serviceResponse.Code = code;
            return serviceResponse;
        }
        public static ServiceResponse<T> CreateFailedResponse(T entity, string statusDetail, IList<string> errors, int code = 999)
        {
            ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
            serviceResponse.Entity = entity;
            serviceResponse.Details = statusDetail;
            serviceResponse.Code = code;
            serviceResponse.IsSucess = false;
            serviceResponse.Errors = errors;
            return serviceResponse;
        }
    }
}
