using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class WorkloadViewModel : WizardPageBase
    {
        const string TITLE = "Workload";
        const string DESCRIPTION = "Select workload which you want to download.";

        WorkloadModel _selectedWorkload;

        ICommand _getWorkloads, _setSelectedWorkload;

        public WorkloadViewModel() : base(TITLE, DESCRIPTION)
        {
            
        }

        #region properties

        public IList<WorkloadModel> Workloads
        {
            get { return Bootstrapper.Instance.Workloads; }
        }

        public WorkloadModel SelectedWorkload
        {
            get { return _selectedWorkload; }
            private set { Set(nameof(SelectedWorkload), ref _selectedWorkload, value); }
        }

        #endregion

        #region commands

        public ICommand GetWorkloadsCommand
        {
            get
            {
                if (_getWorkloads == null)
                    _getWorkloads = new RelayCommand(GetWorkloads);

                return _getWorkloads;
            }
        }

        public ICommand SetSelectedWorkloadComand
        {
            get
            {
                if (_setSelectedWorkload == null)
                    _setSelectedWorkload = new RelayCommand<WorkloadModel>(SetSelectedWorkload) { IsSynchronous = true };

                return _setSelectedWorkload;
            }
        }

        #endregion

        void GetWorkloads()
        {
            Bootstrapper.Instance.GetWorkloads();
            RaisePropertyChanged(nameof(Workloads));
        }

        void SetSelectedWorkload(WorkloadModel workload)
        {
            SelectedWorkload = workload;
            SelectedWorkload.GetSections();
        }

    }
}
