namespace Grilo.Api.Helper
{
    public class StatusCodeHelper
    {
        private static readonly IDictionary<string, int> StatusCode = new Dictionary<string, int>(){
            {"OK", 200},
            {"CREATED", 201},
            {"NO_CONTENT", 204},
            {"OPERATIONAL_ERROR", 400},
            {"NOT_FOUND", 404},
            {"INTERNAL_ERROR", 500}
        };
        public static int Get(string status)
        {
            return StatusCode[status];
        }
    }
}