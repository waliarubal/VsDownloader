using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class ProductEditionViewModel: WizardPageBase
    {
        const string TITLE = "Product Edition";
        const string DESCRIPTION = "Select download directory and the edition of Visual Studio which you want to download.";

        string _path;

        ICommand _getProductEditions;

        public ProductEditionViewModel(): base(TITLE, DESCRIPTION)
        {
            
        }

        #region properties

        public IList<ProductEditionModel> ProductEditions
        {
            get { return Bootstrapper.Instance.ProductEditions; }
        }

        public string Path
        {
            get { return Bootstrapper.Instance.Path; }
            set
            {
                if(Set(nameof(Path), ref _path, value))
                    Bootstrapper.Instance.Path = _path;
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

        #endregion

        void GetProductEditions()
        {
            Bootstrapper.Instance.GetProductEditions();
            RaisePropertyChanged(nameof(ProductEditions));
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
            
            if (string.IsNullOrEmpty(Path))
            {
                errorMessage = "Download directoty not specified. Please select directory where you want installer files to be downloaded.";
                return false;
            }

            if (!Directory.Exists(Path))
            {
                errorMessage = "Download directoty does not exist. Please select a valid directory where you want installer files to be downloaded.";
                return false;
            }

            return true;
        }
    }
}
