using System;
using System.ComponentModel;

namespace WinForms.Framework.Commands
{
    public abstract class CommandBase : ICommand
    {
        private bool _enabled;
        public string Key { get; protected set; }
        public string DisplayName { get; protected set; }

        public bool Enabled
        {
            get
            {
                if (_enabled == CanExecute())
                    return _enabled;

                _enabled = !_enabled;
                OnPropertyChanged(nameof(Enabled));

                return _enabled;
            }
        }

        public Action Execute { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Func<bool> CanExecute { get; protected set; }
    }

    public class DelegateCommand : CommandBase
    {
        public DelegateCommand(string key, string displayName, Action execute, Func<bool> canExecute )
        {
            Key = key;
            DisplayName = displayName;
            Execute = execute;
            CanExecute = canExecute;
        }
    }
}