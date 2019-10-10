/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点29分
/// </summary>
namespace KilyCore.Extension.ApplicationService.IocManager
{
    public interface IAutoFacManager
    {
        void CompleteBuiler();

        T Resolve<T>();
    }
}