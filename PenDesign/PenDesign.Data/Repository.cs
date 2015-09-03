using PenDesign.Common.Paging;
using PenDesign.Core.Interface.Data;
using PenDesign.Core.Model.BaseClass;
using PenDesign.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PenDesign.Data
{
    public class Repository<T>: IRepository<T> where T: EditableEntity
    {
        private IDataContext dataContext;
        private readonly IDbSet<T> dbSet;
        private readonly IDatabaseFactory databaseFactory;


        protected IDbSet<T> DbSet
        {
            get { return dbSet; }
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get { return databaseFactory; }
        }

        protected IDataContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public Repository(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            this.dbSet = DataContext.DbSet<T>();
        }

        public virtual bool AllowDeletedItem
        {
            get { return false; }
        }

        public IQueryable<T> Entities
        {
            get
            {
                return AllowDeletedItem ? DbSet : DbSet.Where(p => p.Status != 2);
            }
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public IQueryable<T> GetAllReadOnly()
        {
            return Entities.AsNoTracking();
        }

        public T GetById(int id)
        {
            T result = DbSet.Find(id);
            if (result == null) return null;
            if (AllowDeletedItem) return result;

            return result.Status == 2 ? null : result;
        }

        public T GetById(int? id)
        {
            if (id == null) return null;
            return GetById((int)id);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return Entities.Where(where).SingleOrDefault();
        }

        public void Add(T entity)
        {
            if (entity == null) throw new NullReferenceException("Add");
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new NullReferenceException("Update");
            DataContext.EntryGet(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new NullReferenceException("Delete");
            entity.Status = 2;
            entity.ModifiedDateTime = DateTime.Now;
            Update(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            IQueryable<T> list = Entities.Where(where);
            foreach (var obj in list)
                Delete(obj);
        }

        public void DeletePersistent(T entity)
        {
            if (entity == null) throw new NullReferenceException("DeletePersistent");
            DataContext.EntryGet(entity).State = EntityState.Deleted;
        }

        public void DeletePersistent(Expression<Func<T, bool>> where)
        {
            IQueryable<T> list = Entities.Where(where);
            foreach (var obj in list)
                DeletePersistent(obj);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> where, int? maxHints = null)
        {
            var result = Entities.Where(where);
            if (maxHints != null)
            {
                int value = (int)maxHints;
                result = result.Take(value);
            }
            return result;
        }

        public IPage<T> Page<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool ascending = true)
        {
            return this.Page<TKey>(Entities.Where(where), orderBy, currentPage, pageSize, ascending);
        }

        public IPage<T> Page<TKey>(IQueryable<T> data, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool ascending = true)
        {
            Page<T> page = new Page<T>(currentPage, pageSize, data.Count());

            if (ascending) data = data.OrderBy(orderBy);
            else data = data.OrderByDescending(orderBy);

            data = data.Skip((page.CurrentPage - 1) * page.PageSize)
                .Take(page.PageSize);
            page.Entities = data.AsEnumerable();
            return page;
        }
    }
}
