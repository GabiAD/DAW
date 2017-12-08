<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ContentLayout.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Pages_Content_Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP (600) Id as TagId, Tag FROM Tags"></asp:SqlDataSource>

    <h3>Popular tags</h3>
    
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <asp:HyperLink ID="HyperLink6" runat="server" CssClass="label label-default" NavigateUrl=<%# "~/Pages/Content/Category.aspx?tagId=" + Eval("TagId") %> Text='<%# Eval("Tag") %>'></asp:HyperLink>
        </ItemTemplate>
    </asp:Repeater>

    <h3>Newest photos</h3>

    <asp:SqlDataSource ID="TagsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP (5) derivedtbl_1.TagId, derivedtbl_1.UltimaPostare, tt.Id, tt.Tag FROM (SELECT TOP (100) PERCENT t.Id AS TagId, MAX(p.DataPostare) AS UltimaPostare FROM Tags AS t INNER JOIN Poze AS p ON t.Id = p.TagId GROUP BY t.Id ORDER BY UltimaPostare DESC) AS derivedtbl_1 INNER JOIN Tags AS tt ON tt.Id = derivedtbl_1.TagId ORDER BY derivedtbl_1.UltimaPostare DESC"></asp:SqlDataSource>

    <asp:Repeater ID="TagsRepeater" runat="server" DataSourceID="TagsDataSource">
        <ItemTemplate>
            <asp:SqlDataSource ID="PhotosDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand=<%# "SELECT TOP(5) Id AS PozaId FROM Poze AS p WHERE (TagId = " + Eval("TagId") + ") ORDER BY DataPostare DESC" %> ></asp:SqlDataSource>
            
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl=<%# "~/Pages/Content/Category.aspx?tagId=" + Eval("TagId") %> >
                <h4><%# Eval("Tag") %></h4>
            </asp:HyperLink>
            <div class="pictures-row">
                <div class="picture-row-gradient"></div>

                <asp:Repeater ID="PhotosRepeater" runat="server" DataSourceID="PhotosDataSource">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl=<%# "~/Pages/Content/Photo.aspx?photoId=" + Eval("PozaId") %> >
                            <asp:Image ID="Image1" runat="server" ImageUrl=<%# "~/AlbumHandler.ashx?pozaId=" + Eval("PozaId") %> CssClass="galerry-picture"/>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
                
                
            </div>
        </ItemTemplate>
    </asp:Repeater>

    

    <div class="flex-gallery-small">
        <asp:Repeater ID="Repeater2" runat="server">
            <ItemTemplate>
                <asp:HyperLink CssClass="flex-gallery-thumbnail" ID="HyperLink1" runat="server" NavigateUrl="#">
                    <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4273/jeremiah-wilson-1.jpg" alt>
                </asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
