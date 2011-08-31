<%@ Page Language="C#" MasterPageFile="~/DentalOfficeTraining.master" AutoEventWireup="true"
    CodeFile="StudentProfiles.aspx.cs" Inherits="StudentProfiles" Title="Student Profiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="0"
        HeaderCssClass="accordianHeader"
        ContentCssClass="accordianContent"
        AutoSize="None"
        FadeTransitions="true"
        TransitionDuration="250"
        FramesPerSecond="40"
        RequireOpenedPane="true"
        SuppressHeaderPostbacks="true">
        <Panes>
        </Panes>
    </cc1:Accordion>
</asp:Content>
