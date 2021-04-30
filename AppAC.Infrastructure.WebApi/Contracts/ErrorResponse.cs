using System.Collections.Generic;

namespace AppAC.Infrastructure.WebApi.Contracts
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}