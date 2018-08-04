using NullVoidCreations.WpfHelpers;
using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class ProductEditionViewModel : WizardPageBase
    {
        const string TITLE = "Product Edition";
        const string DESCRIPTION = "Select download directory and the edition of Visual Studio which you want to download.";

        string _downloadDirectory;

        ICommand _getProductEditions, _selectDownloadDirectory;

        public ProductEditionViewModel() : base(TITLE, DESCRIPTION)
        {

        }

        #region properties

        public IList<ProductEditionModel> ProductEditions
        {
            get { return Bootstrapper.Instance.ProductEditions; }
        }

        public string DownloadDirectory
        {
            get { return _downloadDirectory; }
            set
            {
                if (Set(nameof(DownloadDirectory), ref _downloadDirectory, value))
                    Bootstrapper.Instance.DownloadDirectory = _downloadDirectory;
            }
        }

        #endregion

        #region commands

        public ICommand GetProductEditionsCommand
        {
            get
            {
                if (_getProductEditions == null)
                    _getProductEditions = new RelayCommand(GetProductEditions);

                return _getProductEditions;
            }
        }

        public ICommand SelectDownloadDirectoryCommand
        {
            get
            {
                if (_selectDownloadDirectory == null)
                    _selectDownloadDirectory = new RelayCommand(SelectDownloadDirectory) { IsSynchronous = true };

                return _selectDownloadDirectory;
            }
        }

        #endregion

        void GetProductEditions()
        {
            Bootstrapper.Instance.GetProductEditions();
            RaisePropertyChanged(nameof(ProductEditions));
        }

        void SelectDownloadDirectory()
        {
            var path = new PlatformInvoke().SelectFolder("Select directory where you want to download installer files.");
            if (!string.IsNullOrEmpty(path))
                DownloadDirectory = path;
        }

        public override bool Validate(out string errorMessage)
        {
            errorMessage = null;
            var flag = false;
            foreach (var edition in ProductEditions)
            {
                if (edition.IsSelected)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                errorMessage = "No Visual Studio edition selected. Please select Visual Studio edition which you want to download.";
                return flag;
            }

            if (string.IsNullOrEmpty(DownloadDirectory))
            {
                errorMessage = "Download directoty not specified. Please select directory where you want installer files to be downloaded.";
                return false;
            }

            if (!Directory.Exists(DownloadDirectory))
            {
                errorMessage = "Download directoty does not exist. Please select a valid directory where you want installer files to be downloaded.";
                return false;
            }

            return true;
        }
    }
}
