using System.ComponentModel;
using System.Windows.Forms;

namespace WinForms.Framework.MVP
{
    public interface IView
    {
        Form MdiParent { get; set; }
        event CancelEventHandler Closing;
        void Show();
        void Close();
    }

    public interface IView<T> : IView where T:class
    {
        T ViewModel { get; set; }
        void BindModel();
    }
}