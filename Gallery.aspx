<%@ Page Title="" Language="C#" MasterPageFile="~/DentalOfficeTraining.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Gallery" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    <div style="width: 100%;" class="PinkHeaderText">
        Gallery
    </div>
    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    <br />
    <asp:Repeater ID="rptGalleryImages" runat="server" 
    onitemdatabound="rptGalleryImages_ItemDataBound">
        <ItemTemplate>
            <div style="width: 100%; vertical-align: middle; text-align: center;">
                <div style="float: left; width: 50%;">
                    <div style="float: left; width: 50%;">
                        <a id="imgLinkCol1" rel="lightbox[gallery]" runat="server"><img id="imgGalleryItemCol1" runat="server" /></a>
                    </div>
                    <div style="float: right; width: 50%;">
                        <a id="imgLinkCol2" rel="lightbox[gallery]" runat="server"><img id="imgGalleryItemCol2" runat="server" /></a>
                    </div>
                </div>
                <div style="float: right; width: 50%;">
                    <div style="float: left; width: 50%;">
                        <a id="imgLinkCol3" rel="lightbox[gallery]" runat="server"><img id="imgGalleryItemCol3" runat="server" /></a>
                    </div>
                    <div style="float: right; width: 50%;">
                        <a id="imgLinkCol4" rel="lightbox[gallery]" runat="server"><img id="imgGalleryItemCol4" runat="server" /></a>
                    </div>
                </div>
            </div>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>



