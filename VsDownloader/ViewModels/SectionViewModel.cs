using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class SectionViewModel : WizardPageBase
    {
        const string TITLE = "Components";
        const string DESCRIPTION = "Select component(s) which you want to download.";

        SectionModel _selectedSection;

        ICommand _getSections, _setSelectedSection;

        public SectionViewModel() : base(TITLE, DESCRIPTION)
        {

        }

        #region properties

        public IList<WorkloadModel> Workloads
        {
            get { return Bootstrapper.Instance.Workloads; }
        }

        public SectionModel SelectedSection
        {
            get { return _selectedSection; }
            private set { Set(nameof(SelectedSection), ref _selectedSection, value); }
        }

        #endregion

        #region commands

        public ICommand GetSectionsCommand
        {
            get
            {
                if (_getSections == null)
                    _getSections = new RelayCommand(GetSections);

                return _getSections;
            }
        }

        public ICommand SetSelectedSectionComand
        {
            get
            {
                if (_setSelectedSection == null)
                    _setSelectedSection = new RelayCommand<SectionModel>(SetSelectedSection) { IsSynchronous = true };

                return _setSelectedSection;
            }
        }

        #endregion

        void GetSections()
        {
            foreach (var workload in Workloads)
                if (workload.IsSelected)
                    workload.GetSections();
            RaisePropertyChanged(nameof(Workloads));
        }

        void SetSelectedSection(SectionModel section)
        {
            SelectedSection = section;
        }

    }
}
