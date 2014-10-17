using System.ComponentModel;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private UIElement _activeUi;
        public UIElement ActiveUI
        {
            get { return _activeUi; }
            set { _activeUi = value; InvokePropertyChanged("ActiveUI"); }
        }

        private string _xamlCode;
        public string XamlCode
        {
            get { return _xamlCode; }
            set { _xamlCode = value; InvokePropertyChanged("XamlVisibility"); InvokePropertyChanged("XamlCode"); }
        }

        private string _cSharpCode;
        public string CSharpCode
        {
            get { return _cSharpCode; }
            set { _cSharpCode = value; InvokePropertyChanged("CSharpVisiblity"); InvokePropertyChanged("CSharpCode"); }
        }

        private string _blendInstructions;
        public string BlendInstructions
        {
            get { return _blendInstructions; }
            set { _blendInstructions = value; InvokePropertyChanged("BlendInstructionsVisiblity"); InvokePropertyChanged("BlendInstructions"); }
        }

        private string _htmlCode;
        public string HtmlCode
        {
            get { return _htmlCode; }
            set { _htmlCode = value; InvokePropertyChanged("HtmlVisibility"); InvokePropertyChanged("HtmlCode"); }
        }

        private Group[] _groups;
        public Group[] Groups
        {
            get
            {
                return _groups ?? MetadataStore.GetGroupsBasedOnReflectionData().ToArray();
            }
            set { _groups = value; InvokePropertyChanged("Groups"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Visibility BlendInstructionsVisiblity
        {
            get
            {
                return string.IsNullOrEmpty(BlendInstructions) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility CSharpVisiblity
        {
            get
            {
                return string.IsNullOrEmpty(CSharpCode) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility XamlVisibility
        {
            get
            {
                return string.IsNullOrEmpty(XamlCode) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility HtmlVisibility
        {
            get
            {
                return string.IsNullOrEmpty(HtmlCode) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}