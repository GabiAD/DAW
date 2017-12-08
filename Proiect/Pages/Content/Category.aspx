<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ContentLayout.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Pages_Content_Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">

    <asp:SqlDataSource ID="PhotosDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"></asp:SqlDataSource>

    <h3>
        <asp:Label ID="CategoryTitle" runat="server" Text="Category"></asp:Label>
    </h3>

    <div class="profile-content">
        <div class="flex-gallery">
            <asp:Repeater ID="PhotosRepeater" runat="server" DataSourceID="PhotosDataSource">
                <ItemTemplate>
                    <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink1" runat="server" NavigateUrl='<%#"~/Pages/Content/Photo.aspx?photoId=" + Eval("PozaId") %>'>
                <asp:Image runat="server" ImageUrl=<%# "~/AlbumHandler.ashx?pozaId=" + Eval("PozaId") %>/>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
