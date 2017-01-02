using System.Collections.ObjectModel;
using BusinessManager.Models;
using Xamarin.Forms;

namespace BusinessManager.Views
{
    public class LcTableView : ContentView
    {
        #region Properties

        public static readonly BindableProperty BudgetItemsProperty = BindableProperty.Create("BudgetItems",
            typeof(ObservableCollection<Budget>), typeof(LcTableView), null, BindingMode.OneWay, null,
            (bindable, oldValue, newValue) => { ((LcTableView) bindable).UpdateChildren(); });

        public ObservableCollection<Budget> BudgetItems
        {
            get { return (ObservableCollection<Budget>) GetValue(BudgetItemsProperty); }
            set { SetValue(BudgetItemsProperty, value); }
        }

        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText",
            typeof(string), typeof(LcTableView));

        public string HeaderText
        {
            get { return (string) GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly BindableProperty ListTotalProperty = BindableProperty.Create("ListTotal",
            typeof(double), typeof(LcTableView), double.Parse("0"), BindingMode.OneWay, null,
            (bindable, oldValue, newValue) => { ((LcTableView) bindable).UpdateChildren(); });

        public double ListTotal
        {
            get { return (double) GetValue(ListTotalProperty); }
            set { SetValue(ListTotalProperty, value); }
        }

        #endregion

        private void UpdateChildren()
        {
            if (BudgetItems != null)
            {
                BoxView topSeparatorLine = new BoxView {HeightRequest = 2, Color = Color.Green};
                BoxView bottomSeparatorLine = new BoxView { HeightRequest = 2, Color = Color.Green };

                Grid budgetGrid = new Grid();

                for (int i = 0; i <= BudgetItems.Count+3; i++)
                {
                    budgetGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }

                budgetGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star)});
                budgetGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)});

                // Write the header text 
                int rowCount = 0;
                budgetGrid.Children.Add(new Label { Text = HeaderText, FontSize = 18, TextColor = Color.Blue}, 0, rowCount);

                // Write out a line 
                rowCount++;
                budgetGrid.Children.Add(topSeparatorLine, 0, rowCount);
                Grid.SetColumnSpan(topSeparatorLine, 2);

                foreach (Budget bItem in BudgetItems)
                {
                    rowCount++;
                    budgetGrid.Children.Add(new Label { Text = bItem.Description }, 0, rowCount);
                    budgetGrid.Children.Add(new Label { Text = $"{bItem.Amount:f2}", HorizontalTextAlignment = TextAlignment.End}, 1, rowCount);
                }

                // Write out a line 
                rowCount++;
                budgetGrid.Children.Add(bottomSeparatorLine, 0, rowCount);
                Grid.SetColumnSpan(bottomSeparatorLine, 2);

                // Write out a summary line 
                rowCount++;
                budgetGrid.Children.Add(new Label { Text = "Total " + HeaderText, FontSize = 18, TextColor = Color.Blue }, 0, rowCount);
                budgetGrid.Children.Add(new Label { Text = $"{ListTotal:f2}", HorizontalTextAlignment = TextAlignment.End }, 1, rowCount);

                // set the content of the control
                Content = budgetGrid;                
            }
        }
    }
}
