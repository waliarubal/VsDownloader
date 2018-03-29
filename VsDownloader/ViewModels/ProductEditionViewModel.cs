using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class ProductEditionViewModel: WizardPageBase
    {
        const string TITLE = "Product Edition";
        const string DESCRIPTION = "Select edition of Visual Studio which you want to download.";

        ICommand _getProductEditions;

        public ProductEditionViewModel(): base(TITLE, DESCRIPTION)
        {
            
        }

        #region properties

        public IList<ProductEditionModel> ProductEditions
        {
            get { return Bootstrapper.Instance.ProductEditions; }
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
    }
}
