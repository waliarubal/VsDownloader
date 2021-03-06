﻿using HtmlAgilityPack;
using System.Collections.Generic;
using VsDownloader.Models;
using System;

namespace VsDownloader
{
    sealed class Bootstrapper
    {
        static object _syncRoot;
        static Bootstrapper _instance;
        IList<ProductEditionModel> _productEditions;
        IList<LanguageModel> _languages;
        IList<WorkloadModel> _workloads;

        const string URL = "https://docs.microsoft.com/en-us/visualstudio/install/";

        static Bootstrapper()
        {
            _syncRoot = new object();
        }

        private Bootstrapper()
        {
            _productEditions = new List<ProductEditionModel>();
            _languages = new List<LanguageModel>();
            _workloads = new List<WorkloadModel>();
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

        public IList<ProductEditionModel> ProductEditions
        {
            get { return _productEditions; }
            private set { _productEditions = value; }
        }

        public IList<LanguageModel> Languages
        {
            get { return _languages; }
            private set { _languages = value; }
        }

       

        public IList<WorkloadModel> Workloads
        {
            get { return _workloads; }
            private set { _workloads = value; }
        }

        public string DownloadDirectory { get; set; }

        #endregion

        public void GetProductEditions()
        {
            if (ProductEditions.Count > 0)
                return;

            var web = new HtmlWeb();
            var document = web.Load(string.Format("{0}use-command-line-parameters-to-install-visual-studio", URL));

            var productEditions = new List<ProductEditionModel>();
            var productEditionsNodes = document.DocumentNode.SelectNodes(".//ul/li/a[@data-linktype='external' and substring(@href, string-length(@href) - string-length('exe') +1) = 'exe']");
            foreach (var productEditionNode in productEditionsNodes)
            {
                var language = new ProductEditionModel();
                language.BootstrapperUrl = productEditionNode.Attributes["href"].Value;
                language.Name = productEditionNode.InnerText;
                productEditions.Add(language);
            }
            ProductEditions = productEditions;
        }

        public void GetLanguages()
        {
            if (Languages.Count > 0)
                return;

            var web = new HtmlWeb();
            var document = web.Load(string.Format("{0}use-command-line-parameters-to-install-visual-studio", URL));

            var languages = new List<LanguageModel>();
            var languageNodes = document.DocumentNode.SelectSingleNode(".//h2[@id='list-of-language-locales']/following-sibling::table").SelectNodes("./tbody/tr");
            foreach (var languageNode in languageNodes)
            {
                var informationNodes = languageNode.SelectNodes("./td");
                var language = new LanguageModel();
                language.Locale = informationNodes[0].InnerText;
                language.Name = informationNodes[1].InnerText;
                languages.Add(language);
            }
            Languages = languages;
        }

        public void GetWorkloads()
        {
            if (Workloads.Count > 0)
                return;

            var web = new HtmlWeb();
            var document = web.Load(string.Format("{0}workload-and-component-ids", URL));

            var workloads = new List<WorkloadModel>();
            var workloadNodes = document.DocumentNode.SelectNodes(".//table/tbody/tr");
            foreach (var workloadNode in workloadNodes)
            {
                var informationNodes = workloadNode.SelectNodes("./td");
                var workload = new WorkloadModel();
                workload.Url = string.Format("{0}{1}", URL, informationNodes[0].SelectSingleNode("./a[@data-linktype='relative-path']").Attributes["href"].Value);
                workload.Name = informationNodes[0].InnerText.Replace("&nbsp;", " ");
                workload.Id = informationNodes[1].InnerText;
                workload.Description = informationNodes[2].InnerText;
                workloads.Add(workload);
            }
            Workloads = workloads;
        }

        public IList<SectionModel> GetSections(WorkloadModel workload)
        {
            var web = new HtmlWeb();
            var document = web.Load(workload.Url);

            var sections = new List<SectionModel>();

            var sectionNodes = document.DocumentNode.SelectNodes(".//h2[@id and following::p/strong]");
            foreach(var sectionNode in sectionNodes)
            {
                var section = new SectionModel();
                section.Name = sectionNode.InnerText;

                var tempNode = sectionNode.SelectSingleNode("following::p[child::strong]");
                section.Id = tempNode.LastChild.InnerText;

                tempNode = tempNode.SelectSingleNode("following::p[child::strong]");
                section.Description = tempNode.LastChild.InnerText;

                tempNode = tempNode.SelectSingleNode("following::table/tbody");
                foreach(var componentNode in tempNode.SelectNodes("./tr"))
                {
                    var dataNode = componentNode.SelectNodes("./td");

                    var component = new ComponentModel();
                    component.Id = dataNode[0].InnerText;
                    component.Name = dataNode[1].InnerText;
                    component.Version = Version.Parse(dataNode[2].InnerText);

                    var type = dataNode[3].InnerText;
                    switch(type)
                    {
                        case "Recommended":
                            component.Type = ComponentModel.DependencyType.Recommended;
                            break;

                        case "Optional":
                            component.Type = ComponentModel.DependencyType.Optional;
                            break;

                        case "Required":
                            component.Type = ComponentModel.DependencyType.Required;
                            break;
                    }

                    section.Components.Add(component);
                }

                sections.Add(section);
            }
            return sections;
        }
    }
}
