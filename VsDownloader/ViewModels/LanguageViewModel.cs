using HtmlAgilityPack;
using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class LanguageViewModel: WizardPageBase
    {
        const string TITLE = "Language";
        const string DESCRIPTION = "Select language(s) which you want to download.";

        IList<LanguageModel> _languages;

        CommandBase _getLanguages;

        public LanguageViewModel(): base(TITLE, DESCRIPTION)
        {
            _languages = new List<LanguageModel>();
        }

        #region properties

        public IList<LanguageModel> Languages
        {
            get { return _languages; }
            private set { Set(nameof(Languages), ref _languages, value); }
        }

        #endregion

        #region commands

        public ICommand GetLanguagesCommand
        {
            get
            {
                if (_getLanguages == null)
                    _getLanguages = new RelayCommand<int, IList<LanguageModel>>(GetLanguages, GetLanguagesCallback);

                return _getLanguages;
            }
        }

        #endregion

        IList<LanguageModel> GetLanguages(int languagesCount)
        {
            if (languagesCount > 0)
                return null;

            var web = new HtmlWeb();
            var document = web.Load("https://docs.microsoft.com/en-us/visualstudio/install/use-command-line-parameters-to-install-visual-studio");

            var languages = new List<LanguageModel>();
            var languageNodes= document.DocumentNode.SelectSingleNode(".//h2[@id='list-of-language-locales']/following-sibling::table").SelectNodes("./tbody/tr");
            foreach (var languageNode in languageNodes)
            {
                var informationNodes =  languageNode.SelectNodes("./td");
                var language = new LanguageModel();
                language.Locale = informationNodes[0].InnerText;
                language.Name = informationNodes[1].InnerText;
                languages.Add(language);
            }
            return languages;
        }

        void GetLanguagesCallback(IList<LanguageModel> languages)
        {
            if (languages != null)
                Languages = languages;
        }

    }
}
