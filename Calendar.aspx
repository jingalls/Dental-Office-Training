<%@ Page Language="C#" MasterPageFile="~/DentalOfficeTraining.master" AutoEventWireup="true"
    CodeFile="Calendar.aspx.cs" Inherits="Calendar" Title="Calendar" %>

<%@ Register Src="Controls/CalendarView.ascx" TagName="CalendarView" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding: 15px;">
        <uc1:CalendarView ID="CalendarView1" runat="server" />
    </div>
</asp:Content>
