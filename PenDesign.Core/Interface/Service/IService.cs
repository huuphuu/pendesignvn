using PenDesign.Common.Paging;
using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PenDesign.Core.Interface.Service
{
    public interface IService<T> where T : class
    {
        /// <summary>
        /// trả về tất cả các phần tử thuộc kiểu T đang xét. Tùy theo trường
        /// hợp mà sẽ trả về các phần tử có Deleted=false hay là tất cả các pahanf tử
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// gọi lại Entities
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// tương tự như GetAll nhưng sự thay đổi trên tập trả về sẽ ko tác động đến dbcontext
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllReadOnly();

        /// <summary>
        /// Trả về phần tử có id cho trước, null nếu ko tìm thấy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        T GetById(int? id);

        /// <summary>
        /// Trả về 1 phần tử thỏa biểu thức điều kiện. Ném ra ngoại lệ
        /// nếu có nhiều hơn 1 kết quả
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> where);

        /// <summary>
        /// thêm mới 1 phần tử
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Cập nhật 1 phần tử
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Xóa 1 phần tử bằng cách đặt cờ Deleted = true
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Xóa nhiều phần tử thỏa điều kiện  bằng cách đặt cờ Deleted = true
        /// </summary>
        /// <param name="where"></param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Xoa THAT nhieu phan tu thoai dieu kien
        /// </summary>
        /// <param name="where"></param>
        void DeletePersistent(Expression<Func<T, bool>> where);

        /// <summary>
        /// Trả về tối đa MaxHints phần tử thỏa điều kiện
        /// </summary>
        /// <param name="where"></param>
        /// <param name="maxHints"></param>
        /// <returns></returns>
        IQueryable<T> GetMany(Expression<Func<T, bool>> where, int? maxHints = null);

        /// <summary>
        /// Lấy về dữ liệu thuộc về trang curentPage, với số phần tử trên 1
        /// trang là pageSize, điều kiện tìm kiếm cho bởi where, sắp xếp
        /// theo orderBy
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IPage<T> Page<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool ascending = true);

        IPage<T> Page<TKey>(IQueryable<T> data, Expression<Func<T, TKey>> orderBy, int currentPage, int pageSize, bool ascending = true);
    }
}
