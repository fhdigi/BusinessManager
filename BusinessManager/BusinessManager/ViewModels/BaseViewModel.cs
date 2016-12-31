using BusinessManager.Models;

namespace BusinessManager.ViewModels
{
    public class BaseViewModel : BaseModel
    {
        bool _isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>true if this instance is busy; otherwise, false.</value>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (ProcPropertyChanged(ref _isBusy, value))
                    IsNotBusy = !_isBusy;
            }
        }

        bool isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value>true if this instance is busy; otherwise, false.</value>
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            private set { ProcPropertyChanged(ref isNotBusy, value); }
        }
    }
}
