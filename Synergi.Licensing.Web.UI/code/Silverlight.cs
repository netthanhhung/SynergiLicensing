using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using Synergi.Licensing.Web.UI.Shared;
using Synergi.Licensing.Common;

namespace Synergi.Licensing.Web.UI.Controls
{
    public class Silverlight : Control
    {
        [DescriptionAttribute("Gets and sets the TypeName")]
        [DefaultValueAttribute("MainPage")]
        public virtual string TypeName
        {
            get { return ViewState["TypeName"] != null ? (string)ViewState["TypeName"] : "MainPage"; }
            set { ViewState["TypeName"] = value; }
        }

        [DescriptionAttribute("Gets and sets the ContentTypeName")]
        public virtual string ContentTypeName
        {
            get { return ViewState["ContentTypeName"] != null ? (string)ViewState["ContentTypeName"] : String.Empty; }
            set { ViewState["ContentTypeName"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderBeginTag(writer);
            RenderContent(writer);
            RenderEndTag(writer);

            base.Render(writer);
        }

        protected void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        protected void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        protected void RenderContent(HtmlTextWriter writer)
        {
            string content = string.Format(
                @"<object data=""data:application/x-silverlight-2,"" type=""application/x-silverlight-2""
                    width=""100%"" height=""100%"">
                    <param name=""source"" value=""/ClientBin/Synergi.Licensing.Silverlight.UI.xap?v={0}"" />
                    <param name=""onError"" value=""onSilverlightError"" />
                    <param name=""background"" value=""white"" />
                    <param name=""minRuntimeVersion"" value=""{1}"" />
                    <param name=""autoUpgrade"" value=""true"" />
                    <param name=""InitParams"" value=""TypeName={2}{3}"" />
                    <a href=""http://go.microsoft.com/fwlink/?LinkID=149156&v={1}"" style=""text-decoration: none"">
                        <img src=""http://go.microsoft.com/fwlink/?LinkId=108181"" alt=""Get Microsoft Silverlight"" style=""border-style: none"" />
                    </a>
                </object>
                <iframe id=""_sl_historyFrame"" style=""visibility: hidden; height: 0px; width: 0px; border: 0px""></iframe>"
                , DeploymentInfo.XapGuid
                , DeploymentInfo.MinRuntimeVersion
                , TypeName
                , String.IsNullOrEmpty(ContentTypeName) ? "" : ",ContentTypeName=" + ContentTypeName);

            writer.Write(content);

            SilverlightMinWidthResizer ctl = new SilverlightMinWidthResizer();
            ctl.RenderControl(writer);
        }
    }
}