using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Shared.Interfaces;

namespace Shared.Abstractions
{
    public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        protected const string RowVersion = Shared.Helpers.Constants.RowVersionColumnName;
        /// <summary>
        /// Configure entity
        /// </summary>
        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Configure LocalizedText value object as OwnsOne type
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="column"></param>
        /// <param name="isRequired"></param>
        public virtual EntityTypeBuilder OwnsLocalizedText(EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, LocalizedText>> column, bool isRequired = true)
        {
            //Get column name from expression
            Expression expression = column.Body as Expression;
            MemberExpression memeberExpression = expression as MemberExpression;

            return builder.OwnsOne(typeof(LocalizedText), memeberExpression.Member.Name, e =>
            {
                e.Property("StringValue").HasColumnName(memeberExpression.Member.Name).IsUnicode(true).IsRequired(isRequired);
                e.Ignore("CurrentCultureText");
            });
        }

        /// <summary>
        /// Configure LocalizedText value object as a nested ownership
        /// </summary>
        /// <param name="builder"></param>
        public virtual OwnedNavigationBuilder OwnsLocalizedText<OEntity>(OwnedNavigationBuilder<TEntity, OEntity> builder,
            Expression<Func<OEntity, LocalizedText>> column) where OEntity : class
        {
            //Get column name from expression
            Expression expression = column.Body as Expression;
            MemberExpression memeberExpression = expression as MemberExpression;

            return builder.OwnsOne(typeof(LocalizedText), memeberExpression.Member.Name, e =>
            {
                e.Property("StringValue").HasColumnName(memeberExpression.Member.Name).IsUnicode(true).IsRequired();
                e.Ignore("CurrentCultureText");
            });
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.ConfigureEntity(builder);

            var abpContextType = typeof(AbpDbContext);
            var domainDrivenDesignContextType = typeof(IDomainDrivenDesignContext);
            var aggregateRootType = typeof(AggregateRoot<>);
            var entityType = typeof(Entity<>);
            var baseDbContextType = typeof(BaseDbContext<,>);

            var currentAbpContext = Assembly.GetAssembly(this.GetType()).GetTypes().ToList().Where(t => EntitiesRelationChecker.CheckRelationTypes(ref abpContextType, t)).FirstOrDefault();

            if (EntitiesRelationChecker.CheckRelationTypes(ref domainDrivenDesignContextType, currentAbpContext))
            {
                if (EntitiesRelationChecker.CheckRelationTypes(ref aggregateRootType, typeof(TEntity)))
                    builder.Property<byte[]>(Constants.RowVersionColumnName).IsRowVersion();
            }
            else if (EntitiesRelationChecker.CheckRelationTypes(ref baseDbContextType, currentAbpContext) && EntitiesRelationChecker.CheckRelationTypes(ref entityType, typeof(TEntity)))
            {
                builder.Property<byte[]>(Constants.RowVersionColumnName).IsRowVersion();
            }
        }
    }
}
