using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Contracts.Dto;

namespace Toolkit.Models
{
    public class NavigationViewModel
    {
        #region INavigationViewDto

        #region prviate IInfo

        private string _id;
        private string _name;
        private string _description;

        #endregion prviate IInfo

        #region prviate IViewInfo

        private string _displayName;
        private string _icon;
        //private Assembly _assembly;
        //private bool _isAvailable;

        #endregion prviate IViewInfo

        #region prviate INavigationViewModel

        private string _content;
        private string _targetPageTag;
        private string _targetPageType;

        //private string _parentId;
        private INavigationViewDto _parentId;

        #endregion prviate INavigationViewModel

        #region public IInfo

        public string Id
        {
            get { return _id; }
            init { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion public IInfo

        #region public IViewInfo

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        //public Assembly Assembly
        //{
        //    get { return _assembly; }
        //    set { _assembly = value; }
        //}

        //public bool IsAvailable
        //{
        //    get { return _isAvailable; }
        //    set { _isAvailable = value; }
        //}

        #endregion public IViewInfo

        #region public INavigationViewModel

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public string TargetPageTag
        {
            get { return _targetPageTag; }
            set { _targetPageTag = value; }
        }

        public string TargetPageType
        {
            get { return _targetPageType; }
            set { _targetPageType = value; }
        }

        public INavigationViewDto ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        #endregion public INavigationViewModel

        #endregion INavigationViewDto
    }
}