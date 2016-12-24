using Syncfusion.SfDataGrid.XForms.UWP;

namespace BusinessManager.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            SfDataGridRenderer.Init();
            LoadApplication(new BusinessManager.App());
        }
    }
}
