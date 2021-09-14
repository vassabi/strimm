using System;
namespace Strimm.WebApi.Services
{
    public interface IStrimmIdentityService
    {
        string CurrentUser { get; }
    }
}
