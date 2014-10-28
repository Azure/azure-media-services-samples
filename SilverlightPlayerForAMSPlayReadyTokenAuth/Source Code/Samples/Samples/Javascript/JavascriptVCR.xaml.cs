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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Javascript
{
    [Sample(GroupNames.Javascript, "Basic Javascript Player")]
    public partial class JavascriptVCR : UserControl, ISupportHtmlDisplay
    {
        public JavascriptVCR()
        {
            InitializeComponent();
        }

        public string HtmlCode
        {
            get
            {
                return
                    @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" >

<head>
    <title>Microsoft.SilverlightMediaFramework.SimplePlayer</title>
    <style type=""text/css"">
    html, body {
	    height: 100%;
	    overflow: auto;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }
    </style>
    <script type=""text/javascript"" src=""Silverlight.js""></script>
    <script type=""text/javascript"">
        var slCtl = null;
        function pluginLoaded(sender, args) {  
            slCtl = sender.getHost().Content;
        }
    </script>
</head>
<body>
    <form id=""form1"" runat=""server"" style=""height:100%"">
    <input type=""button"" value=""Play"" onClick=""slCtl.Player.Play()"" />
    <input type=""button"" value=""Stop"" onClick=""slCtl.Player.Stop()"" />
    <input type=""button"" value=""Pause"" onClick=""slCtl.Player.Pause()"" />
    <input type=""button"" value=""Replay"" onClick=""slCtl.Player.Replay()"" />
    <input type=""button"" value=""StartFastForward"" onClick=""slCtl.Player.StartFastForward()"" />
    <input type=""button"" value=""StartFastForward"" onClick=""slCtl.Player.StartFastForward()"" />
    <input type=""button"" value=""StartRewind"" onClick=""slCtl.Player.StartRewind()"" />
    <input type=""button"" value=""StopSlowMotion"" onClick=""slCtl.Player.StopSlowMotion()"" />
    <input type=""button"" value=""GoToNextChapter"" onClick=""slCtl.Player.GoToNextChapter()"" />
    <input type=""button"" value=""GoToNextPlaylistItem"" onClick=""slCtl.Player.GoToNextPlaylistItem()"" />
    <input type=""button"" value=""GoToPreviousChapter"" onClick=""slCtl.Player.GoToPreviousChapter()"" />
    <input type=""button"" value=""GoToPreviousPlaylistItem"" onClick=""slCtl.Player.GoToPreviousPlaylistItem()"" />
    <input type=""button"" value=""GoToPlaylistItem(0)"" onClick=""slCtl.Player.GoToPlaylistItem(0)"" />
    <input type=""button"" value=""GetVolume"" onClick=""Alert(slCtl.Player.GetVolume())"" />
    <input type=""button"" value=""SetValume(50)"" onClick=""slCtl.Player.SetValume(50)"" />
    <br />
    <div id=""silverlightControlHost"">
        <object data=""data:application/x-silverlight-2,"" type=""application/x-silverlight-2"" width=""100%"" height=""95%"">
		  <param name=""source"" value=""ClientBin/Microsoft.SilverlightMediaFramework.SimplePlayer.xap""/>
          <param name=""onload"" value=""pluginLoaded"" />
		  <param name=""background"" value=""white"" />
		  <param name=""minRuntimeVersion"" value=""5.0.61118.0"" />
		  <param name=""autoUpgrade"" value=""true"" />
          <param name=""InitParams"" value=""autoplay=true,selectedcaptionstream=textstream_eng,deliverymethod=adaptivestreaming,mediaurl=http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" />
		  <a href=""http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0"" style=""text-decoration:none"">
 			  <img src=""http://go.microsoft.com/fwlink/?LinkId=161376"" alt=""Get Microsoft Silverlight"" style=""border-style:none""/>
		  </a>
	    </object><iframe id=""_sl_historyFrame"" style=""visibility:hidden;height:0px;width:0px;border:0px""></iframe></div>
    </form>
</body>
</html>
";
                    }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri currentUri = new Uri(Application.Current.Host.Source, @"../Samples/JavascriptVCRHtmlResources/player.html");
            HtmlPage.Window.Invoke("open", new object[] { currentUri.AbsolutePath, "login", "resizable=1,width=646,height=436" });
        }
    }
}
