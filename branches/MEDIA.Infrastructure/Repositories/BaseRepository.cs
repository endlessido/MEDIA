using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;

namespace MEDIA.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityObject, new()
    {
        public const int ISDELAYUPDATE = -1;
        private ObjectContext context = null;
        private ObjectSet<TEntity> dbSet = null;

        public BaseRepository(ObjectContext dbContext)
        {
            this.context = dbContext;
            this.dbSet = dbContext.CreateObjectSet<TEntity>();
        }

        public ObjectContext DbContext
        {
            get { return context; }
        }
      
        public int Add(TEntity model, bool isDelayUpdate = true)
        {
            this.dbSet.AddObject(model);
            if (isDelayUpdate)
            {
                return ISDELAYUPDATE;
            }

            return this.SaveChanges();
        }
       
        public int Delete(Expression<Func<TEntity, bool>> where, bool isDelayUpdate = true)
        {
            List<TEntity> modelList = this.Find(where).ToList();
            if (modelList.Any())
            {
                modelList.ForEach(m => this.dbSet.DeleteObject(m));
                return isDelayUpdate ? ISDELAYUPDATE : this.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> where, bool isNoTracking = false)
        {
            return this.Find(where, isNoTracking).FirstOrDefault();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> where, bool isNoTracking = false)
        {
            return this.GetQuery(isNoTracking).Where(where);
        }

       
        public IQueryable<TEntity> GetQuery(bool isNoTracking = false)
        {
            return isNoTracking ? this.dbSet.AsQueryable <TEntity>() : this.dbSet;
        }

       
        public BindingList<TEntity> GetBindingList()
        {
            throw new NotImplementedException();
        }

      
        public IList<TEntity> GetAll(bool isNoTracking = false)
        {
            return this.GetQuery(isNoTracking).ToList();
        }

      
        public int Update(TEntity model, bool isDelayUpdate = true)
        {
            TEntity oldModel = context.GetObjectByKey(model.EntityKey) as TEntity;
            oldModel = model;
            if (!isDelayUpdate)
            {
                return this.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
       
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.dbSet = null;
                this.context.Dispose();
                this.context = null;
            }
        }
    }
}
