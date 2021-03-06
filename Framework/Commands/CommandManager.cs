﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WinForms.Framework.Commands
{
    public class CommandManager: Component
    {
        private IList<ICommand> Commands { get; set; }
        private IList<ICommandBinder> Binders { get; set; }

        public CommandManager()
        {
            Commands = new List<ICommand>();

            Binders = new List<ICommandBinder>
            {
                new ControlBinder(),
                new MenuItemCommandBinder()
            };

            Application.Idle += UpdateCommandState;
        }

        private void UpdateCommandState(object sender, EventArgs e)
        {
            Commands.Do(c => c.Enabled);
        }

        public CommandManager Bind(ICommand command, IComponent component)
        {
            if (!Commands.Contains(command))
                Commands.Add(command);

            FindBinder(component).Bind(command, component);
            return this;
        }

        protected ICommandBinder FindBinder(IComponent component)
        {
            var binder = GetBinderFor(component);

            if (binder == null)
                throw new Exception(string.Format("No binding found for component of type {0}", component.GetType().Name));

            return binder;
        }

        private ICommandBinder GetBinderFor(IComponent component)
        {
            var type = component.GetType();
            while (type != null)
            {
                var binder = Binders.FirstOrDefault(x => x.SourceType == type);
                if (binder != null)
                    return binder;

                type = type.BaseType;
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Application.Idle -= UpdateCommandState;

            base.Dispose(disposing);
        }
    }
}