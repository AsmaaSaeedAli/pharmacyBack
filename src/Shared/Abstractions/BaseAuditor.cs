using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Abstractions
{
    /// <summary>
    /// It tracks the Auditing information across bounded contexts
    /// </summary>
    public abstract class BaseAuditor<TAuditor> : AggregateRoot<long>
        where TAuditor : BaseAuditor<TAuditor>
    {
        protected BaseAuditor()
        {

        }

        public static TAuditor New(long userId, string name)
        {
            var auditor = (TAuditor)Activator.CreateInstance(typeof(TAuditor));
            auditor.UserId = userId;
            auditor.SetName(name);
            return auditor;
        }


        public virtual long UserId { get; private set; }
        public virtual string Name { get; private set; }

        public void SetName(string name)
        {
            Guard.Guard.AssertArgumentNotNullOrEmptyOrWhitespace(name, nameof(name));
            Name = name;
        }
    }
}
