using System;
using System.ComponentModel;

namespace WinForms.Framework.Commands
{
    public interface ICommand : INotifyPropertyChanged
    {
        bool Enabled { get; }
        string Key { get; }
        string DisplayName { get; }
        Action Execute { get; set; }
    }
}