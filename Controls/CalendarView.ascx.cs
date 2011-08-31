using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_CalendarView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Controls.Add(CreateCalendar());
    }

    private Control CreateCalendar()
    {
        List<Appointments> appts = new List<Appointments>();
        UpdatePanel pnlUpdate = new UpdatePanel();

        Table table = new Table();
        table.Style.Add("width", "100%");

        #region Create Header

        TableRow headerRow = new TableRow();

        if (Session["MonthView"] == null)
            Session["MonthView"] = DateTime.Today;

        DateTime currentMonth = (DateTime)Session["MonthView"];
        appts = Helper.LoadAppointments(currentMonth);

        TableCell headerPrevMonthCell = new TableCell();
        headerPrevMonthCell.CssClass = "CalendarNavigation";

        LinkButton prevMonth = new LinkButton();
        prevMonth.ID = "lnkPrevMonth";
        prevMonth.Text = ((Helper.Month)currentMonth.AddMonths(-1).Month).ToString();
        prevMonth.CommandArgument = currentMonth.AddMonths(-1).ToString();
        prevMonth.Command += new CommandEventHandler(ChangMonth);
        

        headerPrevMonthCell.Controls.Add(prevMonth);

        TableCell headerNextMonthCell = new TableCell();
        headerNextMonthCell.CssClass = "CalendarNavigation";

        LinkButton nextMonth = new LinkButton();
        nextMonth.ID = "lnkNextMonth";
        nextMonth.Text = ((Helper.Month)currentMonth.AddMonths(1).Month).ToString();
        nextMonth.CommandArgument = currentMonth.AddMonths(1).ToString();
        nextMonth.Command += new CommandEventHandler(ChangMonth);

        headerNextMonthCell.Controls.Add(nextMonth);

        TableCell headerMonthCell = new TableCell();
        headerMonthCell.ColumnSpan = 5;
        headerMonthCell.CssClass = "CalendarHeader";

        LiteralControl month = new LiteralControl();
        month.Text = ((Helper.Month)currentMonth.Month).ToString() + " " + currentMonth.Year.ToString();
        
        headerMonthCell.Controls.Add(month);

        headerRow.Cells.Add(headerPrevMonthCell);
        headerRow.Cells.Add(headerMonthCell);
        headerRow.Cells.Add(headerNextMonthCell);
        table.Rows.Add(headerRow);

        #endregion

        #region Create Day Header

        TableRow dayHeaderRow = new TableRow();

        for (int i = 0; i < 7; i++)
        {
            TableCell dayCell = new TableCell();
            dayCell.CssClass = "CalendarDayHeader";

            LiteralControl day = new LiteralControl();
            day.Text = ((DayOfWeek)i).ToString();

            dayCell.Controls.Add(day);
            dayHeaderRow.Cells.Add(dayCell);
        }

        table.Rows.Add(dayHeaderRow);

        #endregion

        #region Create Days

        DayOfWeek firstOfMonth = Helper.GetFirstDayOfMonth((DateTime)Session["MonthView"]);
        int totalDays = Helper.NumOfDaysInMonth((DateTime)Session["MonthView"]);
        int count = 0;

        TableRow dayRow = null;

        for (int i = (1 - (int)firstOfMonth); i <= totalDays; i++)
        {
            if (dayRow == null || (count % 7) == 0)
            {
                if (dayRow != null)
                    table.Rows.Add(dayRow);

                dayRow = new TableRow();
            }

            TableCell day = new TableCell();
            LiteralControl lit1 = new LiteralControl();
            LiteralControl Lit2 = new LiteralControl();

            if (i < 1)
            {
                day.CssClass = "CalendarDayBlank";
                lit1.Text = "&nbsp";

                day.Controls.Add(lit1);
            }
            else
            {
                List<Appointments> dayAppts = appts.FindAll(delegate(Appointments target) { return target.Day == i; });
                day.CssClass = "CalendarDay";
                
                lit1.Text = "<div class='CalendarNum'>" + i.ToString() + "</div>";
                day.Controls.Add(lit1);

                Panel pnlEventContainer = new Panel();
                pnlEventContainer.ID = "pnlEventContainer" + i.ToString();
                pnlEventContainer.CssClass = "CalendarEventContainer";

                foreach (Appointments appt in dayAppts)
                {
                    Panel pnlDisplay = new Panel();
                    pnlDisplay.ID = "pnlDis" + i.ToString() + dayAppts.IndexOf(appt).ToString();
                    pnlDisplay.CssClass = "CalendarEvent";

                    LiteralControl lit = new LiteralControl();
                    if (!string.IsNullOrEmpty(appt.AppointmentStart) && !string.IsNullOrEmpty(appt.AppointmentEnd))
                        lit.Text = appt.AppointmentStart + " - " + appt.AppointmentEnd + " " + appt.Description;
                    else
                        lit.Text = appt.Description;

                    pnlDisplay.Controls.Add(lit);
                    pnlEventContainer.Controls.Add(pnlDisplay);

                    //AjaxControlToolkit.RoundedCornersExtender rndCorners = new AjaxControlToolkit.RoundedCornersExtender();
                    //rndCorners.TargetControlID = pnlDisplay.ID;
                    //rndCorners.Radius = 10;
                    //rndCorners.Corners = AjaxControlToolkit.BoxCorners.All;

                    if (!string.IsNullOrEmpty(appt.PopupDescription))
                    {
                        Panel pnlMenu = new Panel();
                        pnlMenu.ID = "pnlPopUp" + i.ToString() + dayAppts.IndexOf(appt).ToString();
                        pnlMenu.CssClass = "CalendarPopUp";
                        
                        LiteralControl menu = new LiteralControl();
                        menu.Text = appt.PopupDescription;

                        pnlMenu.Controls.Add(menu);

                        AjaxControlToolkit.HoverMenuExtender popUp = new AjaxControlToolkit.HoverMenuExtender();
                        popUp.TargetControlID = pnlDisplay.ID;
                        popUp.PopupControlID = pnlMenu.ID;
                        popUp.PopupPosition = AjaxControlToolkit.HoverMenuPopupPosition.Right;
                        popUp.PopDelay = 50;
                        popUp.OffsetY = -30;
                        popUp.OffsetX = -5;

                        pnlEventContainer.Controls.Add(pnlMenu);
                        pnlEventContainer.Controls.Add(popUp);
                    }

                    LiteralControl spacer = new LiteralControl();
                    spacer.Text = "<br />";
                    
                    pnlEventContainer.Controls.Add(spacer);
                    //day.Controls.Add(rndCorners);
                }

                day.Controls.Add(pnlEventContainer);
            }

            dayRow.Cells.Add(day);

            count++;
        }

        if (dayRow != null)
            table.Rows.Add(dayRow);

        #endregion

        pnlUpdate.ContentTemplateContainer.Controls.Add(table);

        AsyncPostBackTrigger trigger1 = new AsyncPostBackTrigger();
        trigger1.ControlID = prevMonth.ID;
        trigger1.EventName = "Command";

        AsyncPostBackTrigger trigger2 = new AsyncPostBackTrigger();
        trigger2.ControlID = nextMonth.ID;
        trigger2.EventName = "Command";

        pnlUpdate.Triggers.Add(trigger1);
        pnlUpdate.Triggers.Add(trigger2);

        return pnlUpdate;
    }

    protected void ChangMonth(object sender, CommandEventArgs e)
    {
        Session["MonthView"] = DateTime.Parse(e.CommandArgument.ToString());
        this.Controls.Clear();
        this.Controls.Add(CreateCalendar());
    }
}
