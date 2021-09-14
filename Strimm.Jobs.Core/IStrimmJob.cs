using Quartz;

namespace Strimm.Jobs.Core
{
    public interface IStrimmJob : IJob
    {
        ITrigger GetTrigger();
    }
}
