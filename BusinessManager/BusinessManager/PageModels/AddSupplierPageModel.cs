﻿using System;
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

        private bool _editMode;
        public Supplier CurrentSupplier { get; set; }
        public string SaveButtonText { get; set; }

        #endregion

        #region Commands

        public Command SaveSupplierCommand { get; set; }
        public Command DeleteCommand { get; set; }

        #endregion

        public AddSupplierPageModel()
        {
            // this is the command to save a new supplier
            SaveSupplierCommand = new Command(async () => await SaveSupplier());

            // removes the selected supplier from the database
            DeleteCommand = new Command(async() => await DeleteSupplier());
        }

        public override void Init(object initData)
        {
            if (initData == null)
            {
                _editMode = false;
                CurrentSupplier = new Supplier();
                SaveButtonText = "Save Supplier";
            }
            else
            {
                _editMode = true;
                CurrentSupplier = initData as Supplier;
                SaveButtonText = "Update Supplier";
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
                    if (_editMode == false)
                    {
                        await App.SupplierService.SaveItem(CurrentSupplier);

                        // Just tell the user the save has been accomplished
                        await CoreMethods.DisplayAlert("Save Confirmation", "The supplier record has been saved",
                            "OK");
                    }
                    else
                    {
                        await App.SupplierService.UpdateItem(CurrentSupplier);

                        // Just tell the user the save has been accomplished
                        await CoreMethods.DisplayAlert("Update Confirmation", "The supplier record has been updated",
                            "OK");
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
                await CoreMethods.PopPageModel(CurrentSupplier);
            }
        }

        async Task DeleteSupplier()
        {
            // no need to do anything if we are not editing
            if (!_editMode) return;

            // make sure they want to do this
            bool confirmed =
                await
                    CoreMethods.DisplayAlert("Confirm",
                        "Are you sure you would like to delete this supplier?  Once confirmed, the process cannot be reversed.",
                        "Yes", "No");

            // delete from the database if confirmed 
            if (confirmed)
            {
                await App.SupplierService.DeleteItem(CurrentSupplier);
                await CoreMethods.PopPageModel(CurrentSupplier);
            }
        }

        #endregion

    }
}
