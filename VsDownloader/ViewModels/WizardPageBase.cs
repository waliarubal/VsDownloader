using NullVoidCreations.WpfHelpers.Base;

namespace VsDownloader.ViewModels
{
    abstract class WizardPageBase: ViewModelBase
    {
        string _title, _description;

        protected WizardPageBase(string title, string description)
        {
            Title = title;
            Description = description;
        }

        protected WizardPageBase(string title): this(title, default(string))
        {

        }

        #region properties

        public string Title
        {
            get { return _title; }
            private set { Set(nameof(Title), ref _title, value); }
        }

        public string Description
        {
            get { return _description; }
            protected set { Set(nameof(Description), ref _description, value); }
        }

        #endregion
    }
}
