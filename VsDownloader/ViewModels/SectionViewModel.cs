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

        string _details;

        ICommand _getSections, _setDetails;

        public SectionViewModel() : base(TITLE, DESCRIPTION)
        {

        }

        #region properties

        public IList<WorkloadModel> Workloads
        {
            get { return Bootstrapper.Instance.Workloads; }
        }

        public string Details
        {
            get { return _details; }
            private set { Set(nameof(Details), ref _details, value); }
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

        public ICommand SetDetailsComand
        {
            get
            {
                if (_setDetails == null)
                    _setDetails = new RelayCommand<string>(SetDetails) { IsSynchronous = true };

                return _setDetails;
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

        void SetDetails(string details)
        {
            Details = details;
        }

        public override bool Validate(out string errorMessage)
        {
            errorMessage = null;
            return true;
        }
    }
}
