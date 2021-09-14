using Nancy;

namespace Strimm.AutomationWinService
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider
        {
            get { return new NancyPathProvider(); }
        }
    }
}