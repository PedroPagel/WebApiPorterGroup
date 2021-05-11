using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao.Base
{
    public interface IBaseDAO
    {
        Task Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        Task Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        Task Remove<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
