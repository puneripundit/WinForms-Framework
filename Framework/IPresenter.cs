using WinForms.Framework.MVP;

namespace WinForms.Framework
{
    public interface IPresenter<out TV, TM> where TV:IView
    {
        TV Show();
    }
}
