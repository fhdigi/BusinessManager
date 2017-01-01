using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessManager.Views
{
    public class LcListView : ListView
    {
        //private static BindableProperty itemClickCommandProperty = BindableProperty.Create<LcListView, ICommand>(x => x.ItemClickCommand, null);
        private static readonly BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickCommand", typeof(ICommand), typeof(LcListView));

        public LcListView()
        {
            this.ItemTapped += this.OnItemTapped;
        }


        public ICommand ItemClickCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && this.ItemClickCommand != null && this.ItemClickCommand.CanExecute(e))
            {
                this.ItemClickCommand.Execute(e.Item);
                this.SelectedItem = null;
            }
        }
    }
}
