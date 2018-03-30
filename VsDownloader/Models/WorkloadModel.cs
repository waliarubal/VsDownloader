using NullVoidCreations.WpfHelpers.Base;
using System.Collections.Generic;

namespace VsDownloader.Models
{
    class WorkloadModel : NotificationBase
    {
        string _bootstrapperUrl, _name, _id, _description;
        bool _isSelected;
        IList<SectionModel> _sections;

        public WorkloadModel()
        {
            _sections = new List<SectionModel>();
        }

        #region properties

        public string Url
        {
            get { return _bootstrapperUrl; }
            set { Set(nameof(Url), ref _bootstrapperUrl, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public string Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public string Description
        {
            get { return _description; }
            set { Set(nameof(Description), ref _description, value); }
        }

        public IList<SectionModel> Sections
        {
            get { return _sections; }
            private set { Set(nameof(Sections), ref _sections, value); }
        }


        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(nameof(IsSelected), ref _isSelected, value); }
        }

        #endregion

        public void GetSections()
        {
            if (Sections.Count > 0)
                return;

            Sections = Bootstrapper.Instance.GetSections(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
