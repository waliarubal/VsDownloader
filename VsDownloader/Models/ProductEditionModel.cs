using NullVoidCreations.WpfHelpers.Base;

namespace VsDownloader.Models
{
    class ProductEditionModel : NotificationBase
    {
        string _bootstrapperUrl, _name;
        bool _isSelected;

        public string BootstrapperUrl
        {
            get { return _bootstrapperUrl; }
            set { Set(nameof(BootstrapperUrl), ref _bootstrapperUrl, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(nameof(IsSelected), ref _isSelected, value); }
        }
    }
}
