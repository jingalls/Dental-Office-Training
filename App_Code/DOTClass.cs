using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for DOTClass
/// </summary>
public class DOTClass
{
	public DOTClass()
	{
	}

    private string _period;
    public string Period
    {
        get { return _period; }
        set { _period = value; }
    }

    private string _dayClassImage;
    public string DayClassImage
    {
        get { return _dayClassImage; }
        set { _dayClassImage = value; }
    }

    private string _eveningClassImage;
    public string EveningClassImage
    {
        get { return _eveningClassImage; }
        set { _eveningClassImage = value; }
    }

    private List<StudentProfile> _students;
    public List<StudentProfile> Students
    {
        get 
        { 
            _students.Sort(delegate(StudentProfile a, StudentProfile b)
            { return a.ClassTime.CompareTo(b.ClassTime); });
            
            return _students;
        }
        set { _students = value; }
    }
}
