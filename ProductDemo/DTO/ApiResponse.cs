using System.ComponentModel;
namespace ProductDemo.DTO
{
    public class ApiResponse
    {
        public required string Status { get; set; }
        public dynamic? Data { get; set; }
    }

    public enum ApiStatusCode
    {
        [Description("Success")]
        Success,
        [Description("Fail")]
        Fail,
        [Description("Error")]
        Error,
        [Description("Data Unavailable")]
        DataUnavailable
    }
}
