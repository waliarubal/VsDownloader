using NullVoidCreations.WpfHelpers.Base;
using NullVoidCreations.WpfHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace VsDownloader.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        List<WizardPageBase> _pages;
        WizardPageBase _page;
        Control _pageView;
        int _index;

        CommandBase _next, _previous, _first;

        #region properties

        IList<WizardPageBase> Pages
        {
            get
            {
                if (_pages == null)
                {
                    _pages = new List<WizardPageBase>
                    {
                        new ProductEditionViewModel(),
                        new LanguageViewModel(),
                        new WorkloadViewModel(),
                        new SectionViewModel()
                    };
                }

                return _pages;
            }
        }

        public Control PageView
        {
            get { return _pageView; }
            private set { Set(nameof(PageView), ref _pageView, value); }
        }

        public WizardPageBase Page
        {
            get { return _page; }
            private set { Set(nameof(Page), ref _page, value); }
        }

        #endregion

        #region commands

        public ICommand NextCommand
        {
            get
            {
                if (_next == null)
                    _next = new RelayCommand(Next) { IsSynchronous = true };

                return _next;
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                if (_previous == null)
                    _previous = new RelayCommand(Previous) { IsSynchronous = true };

                return _previous;
            }
        }

        public ICommand FirstCommand
        {
            get
            {
                if (_first == null)
                    _first = new RelayCommand(First) { IsSynchronous = true };

                return _first;
            }
        }

        #endregion

        void Next()
        {
            if (_index < Pages.Count - 1)
                _index++;

            WizardPageBase page;
            PageView = ChangePage(_index, out page);
            Page = page;
        }

        void Previous()
        {
            if (_index > 0)
                _index--;

            WizardPageBase page;
            PageView = ChangePage(_index, out page);
            Page = page;
        }

        void First()
        {
            _index = 0;

            WizardPageBase page;
            PageView = ChangePage(_index, out page);
            Page = page;
        }

        Control ChangePage(int index, out WizardPageBase page)
        {
            page = Pages[index];

            var viewName = page.GetType().FullName.Replace("ViewModel", "View");

            var pageView = Activator.CreateInstance(Type.GetType(viewName)) as Control;
            pageView.DataContext = page;
            return pageView;
        }
    }
}
