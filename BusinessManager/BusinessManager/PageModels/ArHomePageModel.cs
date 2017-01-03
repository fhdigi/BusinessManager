using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    public class ArHomePageModel : BasePageModel
    {
        public Command ShowClientListingCommand { get; set; }
        public Command ShowProjectListingCommand { get; set; }

        //public Command CreateInvoiceCommand { get; set; }
        //public Command OpenReceivableReportCommand { get; set; }

        public ArHomePageModel()
        {
            ShowClientListingCommand = new Command(async () => await CoreMethods.PushPageModel<ClientListingPageModel>());
            ShowProjectListingCommand = new Command(async () => await CoreMethods.PushPageModel<ProjectListingPageModel>());
        }
    }
}
