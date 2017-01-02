using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessManager.Constants
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> value) where T : class
        {
            if (value == null)
            {
                return null;
            }

            var observableCollection = new ObservableCollection<T>(value);
            return observableCollection;
        }
    }
}
