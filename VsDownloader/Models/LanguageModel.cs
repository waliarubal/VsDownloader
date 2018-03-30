using NullVoidCreations.WpfHelpers.Base;

namespace VsDownloader.Models
{
    class LanguageModel: NotificationBase
    {
        string _locale, _name;
        bool _isSelected;

        #region properties

        public string Locale
        {
            get { return _locale; }
            set { Set(nameof(Locale), ref _locale, value); }
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

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
