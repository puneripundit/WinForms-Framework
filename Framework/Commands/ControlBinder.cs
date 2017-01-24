using System.Windows.Forms;

namespace WinForms.Framework.Commands
{
    public class ControlBinder: CommandBinder<Control>
    {
        protected override void Bind(ICommand command, Control source)
        {
            source.DataBindings.Add("Enabled", command, nameof(CommandBase.Enabled));
            source.DataBindings.Add("Text", command, nameof(CommandBase.DisplayName));
            source.Click += (o, e) => command.Execute();
        }
    }
}