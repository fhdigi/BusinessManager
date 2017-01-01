using System;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class AddSupplierPageModel : BasePageModel
    {
        #region Properties

        public bool EditMode;
        public Supplier CurrentSupplier { get; set; }

        #endregion

        #region Commands

        public Command SaveSupplierCommand { get; set; }
        public Command CancelCommand { get; set; }

        #endregion

        public AddSupplierPageModel()
        {
            // this is the command to save a new supplier
            SaveSupplierCommand = new Command(async () => await SaveSupplier());

            // this gets us out 
            CancelCommand = new Command(async() => await CoreMethods.PopPageModel());
        }

        public override void Init(object initData)
        {
            if (initData == null)
            {
                EditMode = false;
                CurrentSupplier = new Supplier();
            }
            else
            {
                EditMode = true;
                CurrentSupplier = initData as Supplier;
            }
        }

        #region Methods

        async Task SaveSupplier()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (CurrentSupplier != null)
                {
                    // Save the supplier 
                    var service = new AzureService<Supplier>();

                    if (EditMode == false)
                    {
                        await service.SaveItem(CurrentSupplier);

                        // Just tell the user the save has been accomplished
                        await CoreMethods.DisplayAlert("Save Confirmation", "The supplier record has been saved",
                            "OK", "");
                    }
                    else
                    {
                        await service.UpdateItem(CurrentSupplier);

                        // Just tell the user the save has been accomplished
                        await CoreMethods.DisplayAlert("Update Confirmation", "The supplier record has been updated",
                            "OK", "");
                    }
                }
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;

                // Close the screen
                await CoreMethods.PopPageModel();
            }
        }

        #endregion

    }
}
