using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_Manager.SharedFunctions
{
    public abstract class HelperController : ControllerBase
    {
        internal string ToLike(string? value)
        {
            return "%" + value + "%";
        }
    }
}
