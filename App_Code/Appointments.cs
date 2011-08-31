using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using App_Code;

/// <summary>
/// Summary description for Appointments
/// </summary>
public class Appointments
{
    #region CTor

    public Appointments()
	{ }

    #endregion

    #region Properties / Members

    private int _id;
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private int _day;
    public int Day
    {
        get { return _day; }
        set { _day = value; }
    }

    private string _appointmentStart;
    public string AppointmentStart
    {
        get { return _appointmentStart; }
        set { _appointmentStart = value; }
    }

    private string _appointmentEnd;
    public string AppointmentEnd
    {
        get { return _appointmentEnd; }
        set { _appointmentEnd = value; }
    }

    private string _description;
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    private string _popupDescription;
    public string PopupDescription
    {
        get { return _popupDescription; }
        set { _popupDescription = value; }
    }

    private int _year;
    public int Year
    {
        get { return _year; }
        set { _year = value; }
    }

    private Helper.Month _month;
    public Helper.Month Month
    {
        get { return _month; }
        set { _month = value; }
    }

    public string SortExpression
    {
        get { return Year.ToString() + ((int)Month).ToString().PadLeft(2, '0') + Day.ToString().PadLeft(2, '0'); }
    }

    #endregion

    #region Static Methods

    /// <summary>
    /// Loads the appointments.
    /// </summary>
    /// <returns></returns>
    public static List<Appointments> LoadAppointments()
    {
        List<Appointments> appts = new List<Appointments>();

        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("Appointments.xml"));
        XmlNode rootNode = doc.SelectSingleNode("appointments");

        foreach (XmlNode yearNode in rootNode.ChildNodes)
        {
            foreach (XmlNode monthNode in yearNode.ChildNodes)
            {
                foreach (XmlNode apptNode in monthNode.ChildNodes)
                {
                    Appointments appointment = new Appointments();
                    appointment.Id = int.Parse(apptNode.Attributes["id"].Value.ToString());
                    appointment.Year = int.Parse(yearNode.Attributes["value"].Value.ToString());
                    appointment.Month = (Helper.Month)Enum.Parse(typeof(Helper.Month), monthNode.Name, true);
                    appointment.Day = int.Parse(apptNode.Attributes["day"].Value.ToString());
                    appointment.Description = apptNode.Attributes["description"].Value.ToString();
                    appointment.AppointmentStart = apptNode.Attributes["begin"].Value.ToString();
                    appointment.AppointmentEnd = apptNode.Attributes["end"].Value.ToString();
                    appointment.PopupDescription = apptNode.Attributes["popupdescription"].Value.ToString();
                    appts.Add(appointment);
                }
            }
        }

        appts.Sort(delegate(Appointments a, Appointments b) { return a.SortExpression.CompareTo(b.SortExpression); });
        return appts;
    }

    /// <summary>
    /// Loads the appointments.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    public static List<Appointments> LoadAppointments(DateTime date)
    {
        List<Appointments> appts = new List<Appointments>();

        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("Appointments.xml"));

        string xpath = "appointments/year[@value='" + date.Year.ToString() + "']/" + ((Helper.Month)date.Month).ToString().ToLower();
        XmlNode node = doc.SelectSingleNode(xpath);

        if (node != null)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                Appointments appointment = new Appointments();
                appointment.Id = int.Parse(childNode.Attributes["id"].Value.ToString());
                appointment.Year = date.Year;
                appointment.Month = (Helper.Month)date.Month;
                appointment.Day = int.Parse(childNode.Attributes["day"].Value.ToString());
                appointment.Description = childNode.Attributes["description"].Value.ToString();
                appointment.AppointmentStart = childNode.Attributes["begin"].Value.ToString();
                appointment.AppointmentEnd = childNode.Attributes["end"].Value.ToString();
                appointment.PopupDescription = childNode.Attributes["popupdescription"].Value.ToString();
                appts.Add(appointment);
            }
        }

        appts.Sort(delegate(Appointments a, Appointments b) { return a.Day.CompareTo(b.Day); });
        return appts;
    }

    /// <summary>
    /// Adds the appointment.
    /// </summary>
    /// <param name="appt">The appt.</param>
    /// <param name="apptDate">The appt date.</param>
    public static void AddAppointment(Appointments appt, DateTime apptDate)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("Appointments.xml"));

        string xpath = "appointments/year[@value='" + apptDate.Year.ToString() + "']/" + ((Helper.Month)apptDate.Month).ToString().ToLower();
        XmlNode node = doc.SelectSingleNode(xpath);

        if (node != null)
        {
            XmlNode childNode = doc.CreateNode(XmlNodeType.Element, "appt", "");

            XmlAttribute attribDay = doc.CreateAttribute("day");
            attribDay.Value = appt.Day.ToString();

            XmlAttribute attribBegin = doc.CreateAttribute("begin");
            attribBegin.Value = appt.AppointmentStart;

            XmlAttribute attribEnd = doc.CreateAttribute("end");
            attribEnd.Value = appt.AppointmentEnd;

            XmlAttribute attribDesc = doc.CreateAttribute("description");
            attribDesc.Value = appt.Description;

            XmlAttribute attribPopUpDesc = doc.CreateAttribute("popupdescription");
            attribPopUpDesc.Value = appt.PopupDescription;

            childNode.Attributes.Append(attribDay);
            childNode.Attributes.Append(attribBegin);
            childNode.Attributes.Append(attribEnd);
            childNode.Attributes.Append(attribDesc);
            childNode.Attributes.Append(attribPopUpDesc);

            node.AppendChild(childNode);
        }

        doc.Save(HttpContext.Current.Server.MapPath("Appointments.xml"));
    }

    public static void DeleteAppointment(int apptId)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(HttpContext.Current.Server.MapPath("Appointments.xml"));

        string xpath = "appointments//appt[@id='" + apptId.ToString() + "']";
        XmlNode node = doc.SelectSingleNode(xpath);

        if (node != null)
            node.ParentNode.RemoveChild(node);

        doc.Save(HttpContext.Current.Server.MapPath("Appointments.xml"));
    }

    #endregion
}
