using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for StudentProfile
/// </summary>
public class StudentProfile
{
    public StudentProfile()
    { }

    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private string _image;
    public string Image
    {
        get { return _image; }
        set { _image = value; }
    }

    private string _hometown;
    public string Hometown
    {
        get { return _hometown; }
        set { _hometown = value; }
    }

    private string _testimonial;
    public string Testimonial
    {
        get { return _testimonial; }
        set { _testimonial = value; }
    }

    private string _classTime;
    public string ClassTime
    {
        get { return _classTime; }
        set { _classTime = value; }
    }
}
