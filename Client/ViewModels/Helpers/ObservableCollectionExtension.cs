using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace OnlineCheckers.Client.ViewModels
{
    public static class ObservableCollectionExtension
    {
        public static void Remove<T>(this ObservableCollection<T> collection, Func<T, bool> condition)
        {
            var itemsToRemove = collection.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
                collection.Remove(itemToRemove);
        }
    }
}
