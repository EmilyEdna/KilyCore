/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.QueryExtend
{
    /// <summary>
    /// 查询封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageParamList<T> where T : class, new()
    {
        public PageParamList()
        {
            QueryParam = new T();
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public int pageSize { get; set; }

        public T QueryParam { get; set; }
    }

    /// <summary>
    /// 单个值查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleParam<T>
    {
        public T Id { get; set; }
        public T Parameter { get; set; }
    }
}