﻿using NullVoidCreations.WpfHelpers.Base;
using System;

namespace VsDownloader.Models
{
    class ComponentModel : NotificationBase
    {
        public enum DependencyType : byte
        {
            Required,
            Recommended,
            Optional
        }

        string _name, _id;
        bool _isSelected;
        DependencyType _dependencyType;
        Version _version;

        #region properties

        public DependencyType Type
        {
            get { return _dependencyType; }
            set
            {
                Set(nameof(Type), ref _dependencyType, value);
                IsSelected = value == DependencyType.Required || value == DependencyType.Recommended;
            }
        }

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public string Id
        {
            get { return _id; }
            set { Set(nameof(Id), ref _id, value); }
        }

        public Version Version
        {
            get { return _version; }
            set { Set(nameof(Version), ref _version, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(nameof(IsSelected), ref _isSelected, value); }
        }

        public bool IsNotRequired
        {
            get { return Type != DependencyType.Required; }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
