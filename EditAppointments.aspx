<%@ Page Language="C#" MasterPageFile="~/DentalOfficeTraining.master" AutoEventWireup="true" CodeFile="EditAppointments.aspx.cs" Inherits="EditAppointments" Title="Untitled Page" %>

<%@ Register Src="Controls/CalendarView.ascx" TagName="CalendarView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div>
        <div style="float: left">
            <asp:Label ID="Label1" runat="server" Text="Start Time:"></asp:Label>
            <asp:DropDownList ID="ddlStartTime" runat="server"></asp:DropDownList>
            
            <br />
            
            <asp:Label ID="Label4" runat="server" Text="Date:"></asp:Label>
            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>&nbsp;
            
            
        </div>
        
        <div style="float: right">
            <asp:Label ID="Label2" runat="server" Text="End Time:"></asp:Label>
            <asp:DropDownList ID="ddlEndTime" runat="server"></asp:DropDownList>
            
            <br />
            
            <asp:Label ID="Label3" runat="server" Text="Short Description"></asp:Label>    
            <asp:TextBox ID="txtSDesc" runat="server"></asp:TextBox>
        </div>
    </div>
    
    <hr style="clear: both;" />
    
    <div style="clear: both;">
        <uc1:CalendarView ID="CalendarView1" runat="server" />
    </div>
    
</asp:Content>

