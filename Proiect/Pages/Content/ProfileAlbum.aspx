<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ProfileLayout.master" AutoEventWireup="true" CodeFile="ProfileAlbum.aspx.cs" Inherits="Pages_Content_Profile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" Runat="Server">
    
    <h3>
        <asp:Label ID="albumName" runat="server" Text="Album Name"></asp:Label>
    </h3>

    <div class="profile-content">
        <div class="flex-gallery">
            <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink1" runat="server" NavigateUrl="#">
                <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4273/jeremiah-wilson-1.jpg" alt>
            </asp:HyperLink>
            <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink2" runat="server" NavigateUrl="#">
                <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4273/jeremiah-wilson-2.jpg" alt>
            </asp:HyperLink>
            <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink3" runat="server" NavigateUrl="#">
                <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4273/jeremiah-wilson-3.jpg" alt>
            </asp:HyperLink>
            <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink4" runat="server" NavigateUrl="#">
                <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4273/jeremiah-wilson-4.jpg" alt>
            </asp:HyperLink>
            <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink5" runat="server" NavigateUrl="#">
                <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4273/jeremiah-wilson-5.jpg" alt>
            </asp:HyperLink>
        </div>
    </div>

</asp:Content>

