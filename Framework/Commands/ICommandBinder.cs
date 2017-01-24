using System;

namespace WinForms.Framework.Commands
{
    public interface ICommandBinder
    {
        void Bind(ICommand command, object component);
        Type SourceType { get; }
    }
}