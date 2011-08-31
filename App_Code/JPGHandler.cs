using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;

/// <summary>
/// Summary description for JPGHandler
/// </summary>
public class JPGHandler : IHttpHandler
{
    HttpContext _context;
    static string bad_photo = HttpContext.Current.Server.MapPath("") + "/badimage_92.gif";
    static string imagesFolderDir;
    static string uploadedContentDir;

    public JPGHandler()
    {
        imagesFolderDir = HttpContext.Current.Server.MapPath("").Replace('\\', '/') + "/";
        uploadedContentDir = HttpContext.Current.Server.MapPath("").Replace('\\', '/') + "/";
    }

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        _context = context;
        string requestedFile = context.Request.Url.OriginalString.Remove(0, context.Request.Url.OriginalString.IndexOf("images/")).Replace("images/", "");
        context.Response.WriteFile(ImgRetrieval(requestedFile));
    }

    public string ImgRetrieval(string requested_filename)
    {
        string[] str;
        string base_pic, appended_value, appended_Imgfile, appended_TxtFile;
        int border = 10;

        string[] fileParts = requested_filename.Split('/', '\\');
        string filename = (fileParts[fileParts.Length - 1]);
        string subPath = requested_filename.Replace(filename, "");

        if(!string.IsNullOrEmpty(subPath))
            uploadedContentDir = uploadedContentDir.Replace(subPath, "");


        if (File.Exists(uploadedContentDir + subPath + filename))
        {
            return (uploadedContentDir + subPath + filename);
        }
        else if (subPath == "Gallery/" && requested_filename.Contains("_"))
        {
            requested_filename = requested_filename.Replace(subPath, "");
            uploadedContentDir = uploadedContentDir.Replace(subPath, "");

            str = requested_filename.Split('_', '.');
            base_pic = str[0] + "." + str[str.Length - 1];
            appended_value = str[1];
            int result;

            if (File.Exists((uploadedContentDir + "/" + subPath + base_pic)))
            {
                if (int.TryParse(appended_value, out result))
                {
                    ImgControl.ScalePic((uploadedContentDir + "/" + subPath + base_pic), result, (uploadedContentDir + "/" + subPath + requested_filename));
                    return (uploadedContentDir + "/" + subPath + requested_filename);
                }
                else
                {
                    return (bad_photo);
                }
            }
            else
            {
                return (bad_photo);
            }
        }
        else
        {
            return (bad_photo);
        }
    }
}

