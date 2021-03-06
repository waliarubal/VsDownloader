﻿using NullVoidCreations.WpfHelpers.Commands;
using System.Collections.Generic;
using System.Windows.Input;
using VsDownloader.Models;

namespace VsDownloader.ViewModels
{
    class LanguageViewModel: WizardPageBase
    {
        const string TITLE = "Language";
        const string DESCRIPTION = "Select language locale(s) which you want to download.";

        ICommand _getLanguages;

        public LanguageViewModel(): base(TITLE, DESCRIPTION)
        {
            
        }

        #region properties

        public IList<LanguageModel> Languages
        {
            get { return Bootstrapper.Instance.Languages; }
        }

        #endregion

        #region commands

        public ICommand GetLanguagesCommand
        {
            get
            {
                if (_getLanguages == null)
                    _getLanguages = new RelayCommand(GetLanguages);

                return _getLanguages;
            }
        }

        #endregion

        void GetLanguages()
        {
            Bootstrapper.Instance.GetLanguages();
            RaisePropertyChanged(nameof(Languages));  
        }

        public override bool Validate(out string errorMessage)
        {
            errorMessage = null;
            foreach (var language in Languages)
                if (language.IsSelected)
                    return true;

            errorMessage = "No language selected. Please select at least one language locale.";
            return false;
        }
    }
}
