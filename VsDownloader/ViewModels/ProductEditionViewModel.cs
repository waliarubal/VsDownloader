using HtmlAgilityPack;
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

        IList<ProductEditionModel> _productEditions;

        ICommand _getProductEditions;

        public ProductEditionViewModel(): base(TITLE, DESCRIPTION)
        {
            _productEditions = new List<ProductEditionModel>();
        }

        #region properties

        public IList<ProductEditionModel> ProductEditions
        {
            get { return _productEditions; }
            private set { Set(nameof(ProductEditions), ref _productEditions, value); }
        }

        #endregion

        #region commands

        public ICommand GetProductEditionsCommand
        {
            get
            {
                if (_getProductEditions == null)
                    _getProductEditions = new RelayCommand<int, IList<ProductEditionModel>>(GetProductEditions, GetProductEditionsCallback);

                return _getProductEditions;
            }
        }

        #endregion

        IList<ProductEditionModel> GetProductEditions(int productsCount)
        {
            if (productsCount > 0)
                return null;

            var web = new HtmlWeb();
            var document = web.Load("https://docs.microsoft.com/en-us/visualstudio/install/use-command-line-parameters-to-install-visual-studio");

            var productEditions = new List<ProductEditionModel>();
            var productEditionsNodes = document.DocumentNode.SelectNodes(".//ul/li/a[@data-linktype='external' and substring(@href, string-length(@href) - string-length('exe') +1) = 'exe']");
            foreach (var productEditionNode in productEditionsNodes)
            {
                var language = new ProductEditionModel();
                language.BootstrapperUrl = productEditionNode.Attributes["href"].Value;
                language.Name = productEditionNode.InnerText;
                productEditions.Add(language);
            }
            return productEditions;
        }

        void GetProductEditionsCallback(IList<ProductEditionModel> productEditions)
        {
            if (productEditions != null)
                ProductEditions = productEditions;
        }
    }
}
