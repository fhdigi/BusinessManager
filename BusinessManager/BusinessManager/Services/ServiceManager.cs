using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppServiceHelpers.Abstractions;
using AppServiceHelpers.Helpers;
using BusinessManager.Models;
using FreshMvvm;
using Microsoft.WindowsAzure.MobileServices;
using Syncfusion.Data.Extensions;

namespace BusinessManager.Services
{
    public static class ServiceManager
    {
        private static IEasyMobileServiceClient _client = null;
        private static bool _isInitialized = false;

        private static ConnectedObservableCollection<Supplier> Suppliers;

        private static void Initialize()
        {
            if (_isInitialized)
                return;

            // get a reference to the app helper class 
            _client = AppServiceHelpers.EasyMobileServiceClient.Current;

            // set the flag 
            _isInitialized = true;
        }

        #region Generic

        public static void Save<T>(T objToSave)
        {
            if (typeof(T).Name == nameof(Ledger))
            {
                SaveLedgerItem(objToSave as Ledger);
            }
            if (typeof(T).Name == nameof(Supplier))
            {
                SaveSupplierItem(objToSave as Supplier);
            }
        }

        #endregion

        #region Supplier

        public static void EstablishSuppliers()
        {
            Initialize();
            Suppliers = new ConnectedObservableCollection<Supplier>(_client.Table<Supplier>());
        }

        public static ConnectedObservableCollection<Supplier> GetSuppliers()
        {
            Initialize();
            return new ConnectedObservableCollection<Supplier>(_client.Table<Supplier>());
        }

        private static void SaveSupplierItem(Supplier obj)
        {
            Initialize();
            Suppliers.Add(obj);
        }

        #endregion

        #region Ledger

        private static void SaveLedgerItem(Ledger obj)
        {
            Initialize();
            _client.Table<Ledger>().AddAsync(obj);
        }

        #endregion


    }
}
