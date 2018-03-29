using HtmlAgilityPack;
using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class WorkloadViewModel : WizardPageBase
    {
        const string TITLE = "Workload";
        const string DESCRIPTION = "Select workload which you want to download.";

        IList<WorkloadModel> _workloads;

        CommandBase _getWorkloads;

        public WorkloadViewModel() : base(TITLE, DESCRIPTION)
        {
            _workloads = new List<WorkloadModel>();
        }

        #region properties

        public IList<WorkloadModel> Workloads
        {
            get { return _workloads; }
            private set { Set(nameof(Workloads), ref _workloads, value); }
        }

        #endregion

        #region commands

        public ICommand GetWorkloadsCommand
        {
            get
            {
                if (_getWorkloads == null)
                    _getWorkloads = new RelayCommand<int, IList<WorkloadModel>>(GetWorkloads, GetWorkloadsCallback);

                return _getWorkloads;
            }
        }

        #endregion

        IList<WorkloadModel> GetWorkloads(int workloadsCount)
        {
            if (workloadsCount > 0)
                return null;

            var web = new HtmlWeb();
            var document = web.Load("https://docs.microsoft.com/en-us/visualstudio/install/workload-and-component-ids");

            var workloads = new List<WorkloadModel>();
            var workloadNodes = document.DocumentNode.SelectNodes(".//table/tbody/tr");
            foreach (var workloadNode in workloadNodes)
            {
                var informationNodes = workloadNode.SelectNodes("./td");
                var workload = new WorkloadModel();
                workload.Url = string.Format("https://docs.microsoft.com/en-us/visualstudio/install/{0}", informationNodes[0].SelectSingleNode("./a[@data-linktype='relative-path']").Attributes["href"].Value);
                workload.Name = informationNodes[0].InnerText.Replace("&nbsp;", " ");
                workload.Id = informationNodes[1].InnerText;
                workload.Description = informationNodes[2].InnerText;
                workloads.Add(workload);
            }
            return workloads;
        }

        void GetWorkloadsCallback(IList<WorkloadModel> workloads)
        {
            if (workloads != null)
                Workloads = workloads;
        }

    }
}
