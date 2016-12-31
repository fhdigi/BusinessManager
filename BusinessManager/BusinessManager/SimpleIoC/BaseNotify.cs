using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
    public static class BaseNotify
    {
        public static bool SetProperty<T>(this PropertyChangedEventHandler handler, object sender, ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;

            currentValue = newValue;
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
