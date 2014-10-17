using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.HtmlIntegration
{
    [Sample(GroupNames.HtmlIntegration, "Setup Smooth Streaming Player")]
    public partial class SetupBasicPlayer : UserControl, ISupportHtmlDisplay
    {
        public SetupBasicPlayer()
        {
            InitializeComponent();
        }

        public string HtmlCode
        {
            get
            {
                return
                    @"    <div id=""silverlightControlHost"">
        <object data=""data:application/x-silverlight-2,"" type=""application/x-silverlight-2"" width=""100%"" height=""100%"">
		  <param name=""source"" value=""ClientBin/Microsoft.SilverlightMediaFramework.SimplePlayer.xap""/>
		  <param name=""onError"" value=""onSilverlightError"" />
		  <param name=""background"" value=""white"" />
		  <param name=""minRuntimeVersion"" value=""5.0.61118.0"" />
		  <param name=""autoUpgrade"" value=""true"" />
          <param name=""InitParams"" value=""autoplay=true,selectedcaptionstream=textstream_eng,deliverymethod=adaptivestreaming,mediaurl=http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" />
		  <a href=""http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0"" style=""text-decoration:none"">
 			  <img src=""http://go.microsoft.com/fwlink/?LinkId=161376"" alt=""Get Microsoft Silverlight"" style=""border-style:none""/>
		  </a>
	    </object><iframe id=""_sl_historyFrame"" style=""visibility:hidden;height:0px;width:0px;border:0px""></iframe></div>";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri currentUri = new Uri(Application.Current.Host.Source, @"../Samples/SetupBasicPlayerHtmlResources/player.html");
            HtmlPage.Window.Invoke("open", new object[] { currentUri.AbsolutePath, "login", "resizable=1,width=646,height=436" });
        }
    }
}
