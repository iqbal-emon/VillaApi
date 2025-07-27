namespace VillaApi.Utility
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
