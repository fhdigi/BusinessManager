using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    public class ApHomePageModel : BasePageModel
    {
        public Command ShowEnterNewBillPageCommand { get; set; }
        public Command ShowSuppliersViewCommand { get; set; }

        public ApHomePageModel()
        {
            // When the user wants to enter a bill
            ShowEnterNewBillPageCommand = new Command(async () => await CoreMethods.PushPageModel<BillPageModel>(null, true));

            // Show the supplier listing 
            ShowSuppliersViewCommand = new Command(async () => await CoreMethods.PushPageModel<SupplierListingPageModel>());
        }
    }
}
