namespace VsDownloader
{
    sealed class Bootstrapper
    {
        static object _syncRoot;
        static Bootstrapper _instance;

        const string URL = "https://docs.microsoft.com/en-us/visualstudio/install/";

        static Bootstrapper()
        {
            _syncRoot = new object();
        }

        private Bootstrapper()
        {

        }

        #region properties

        public static Bootstrapper Instance
        {
            get
            {
                lock(_syncRoot)
                {
                    if (_instance == null)
                        _instance = new Bootstrapper();

                    return _instance;
                }
            }
        }

        #endregion
    }
}
