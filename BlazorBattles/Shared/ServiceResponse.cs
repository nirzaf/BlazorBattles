using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorBattles.Shared
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}
