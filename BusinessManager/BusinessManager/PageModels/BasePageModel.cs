using PropertyChanged;

namespace BusinessManager.PageModels
{
    [ImplementPropertyChanged]
    public class BasePageModel : FreshMvvm.FreshBasePageModel
    {
        public bool IsBusy { get; set; }
    }
}
