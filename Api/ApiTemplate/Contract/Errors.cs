using System;
using System.Threading.Tasks;

namespace ApiTemplate.Contract
{
    public static class Errors
    {
        public static async Task<Guid> ThrowError(string errorMessage) =>
            await Task.FromException<Guid>(exception: new Exception(errorMessage));
    }
}