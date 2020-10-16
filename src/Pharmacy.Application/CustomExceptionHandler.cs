using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;

namespace Pharmacy
{
    public class CustomExceptionHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            var exception = eventData.Exception;
        }
    }
}
