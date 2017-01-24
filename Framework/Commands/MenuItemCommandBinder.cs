using System.Windows.Forms;

namespace WinForms.Framework.Commands
{
    public class MenuItemCommandBinder : CommandBinder<ToolStripItem>
    {
        protected override void Bind(ICommand command, ToolStripItem source)
        {
            source.Text = command.DisplayName;
            source.Enabled = command.Enabled;
            source.Click += (o, e) => command.Execute();

            command.PropertyChanged += (o, e) => source.Enabled = command.Enabled;
        }
    }
}