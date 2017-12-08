<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ProfileLayout.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Pages_Content_Profile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" Runat="Server">

    <div class="profile-content">
        <div class="flex-gallery">

            <asp:Repeater ID="AlbumeRepeater" runat="server">
                <ItemTemplate>
                    <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink1" runat="server" NavigateUrl=<%#"~/Pages/Content/Photo.aspx?photoId=" + Eval("PozaId") %> >
                        <asp:Image runat="server" ImageUrl=<%# "~/AlbumHandler.ashx?pozaId=" + Eval("PozaId") %>/>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>

</asp:Content>

