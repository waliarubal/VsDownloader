using NullVoidCreations.WpfHelpers.Base;

namespace VsDownloader.Models
{
    class SectionModel : NotificationBase
    {
        string _id, _name, _description;
        bool _isSelected;

        public string Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { Set(nameof(Description), ref _description, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(nameof(IsSelected), ref _isSelected, value); }
        }
    }
}
