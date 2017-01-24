using System;
using System.Collections.Generic;

namespace WinForms.Framework.Commands
{
    public static class Extensions
    {
        public static void Do<T>(this IEnumerable<T> @this, Func<T, object> lambda)
        {
            foreach (var item in @this)
                lambda(item);
        }
    }
}