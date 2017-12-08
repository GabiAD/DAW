<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/BaseLayout.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="Pages_Content_Photo" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBase" Runat="Server">

    <asp:SqlDataSource ID="CommentsDataSource" runat="server" ></asp:SqlDataSource>

    <div style="margin:15px auto; width: 90%">

        <div class="image-page-container">
            <asp:LinkButton ID="DeletePhoto" runat="server" CssClass="delete-button" OnClick="DeletePhoto_Click" Visible="false"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>

            <div class="img-container">
                <asp:Image ID="ImageFull" runat="server" />
            </div>

            <div class="comment-section-container">
                <div class="photo-description">
                
                    <div class="photo-view-profile-picture">
                        <asp:Image ID="ImageUser" runat="server" />
                    </div>

                    <h4 class="photo-user-name">
                        <asp:Label ID="UserPhoto" runat="server" Text="Label"></asp:Label>
                    </h4>
                    
                    <div class="caption">
                        <asp:Label ID="Caption" runat="server"></asp:Label>
                    </div>
                </div>
                    
                <div class="comment-section">

                    <asp:Repeater ID="ComentariiRepeater" runat="server" DataSourceID="CommentsDataSource">
                        <ItemTemplate>
                            <div class="comment-container">

                                <asp:HiddenField ID="comentariuHidVal" runat="server" Value=<%# Eval("ComentariuId") %>  />
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="DeleteComment_Click" Visible='<%# (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated && (Roles.IsUserInRole(User.Identity.Name, "Administrator")  || Membership.GetUser().ProviderUserKey.ToString() == Eval("UserId").ToString()) %>' ><span class="glyphicon glyphicon-remove delete-button delete-small"></span></asp:LinkButton>
                                
                                <div class="comment-name"><%# Eval("Prenume") + " " + Eval("Nume") %></div>
                                <div class="comment-text">
                                    <%# Eval("TextComentariu") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>

                <div class="own-comment-container">

                    <asp:LoginView ID="LoginView1" runat="server">
                        <LoggedInTemplate>
                            <asp:TextBox ID="CommentBox" CssClass="comment-box" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="PostComment" CssClass="post-comment-button" runat="server" OnClick="PostComment_Click"><span class="glyphicon glyphicon-send"></span></asp:LinkButton>
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            Please log in to leave a comment...
                        </AnonymousTemplate>
                    </asp:LoginView>

                </div>

            </div>
        </div>
    </div>

</asp:Content>

