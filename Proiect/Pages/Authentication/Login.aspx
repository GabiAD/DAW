<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/AuthenticationLayout.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Authentication_Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAuthentication" Runat="Server">

    <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Pages/Content/Home.aspx">
        <FailureTextStyle CssClass="" />
        <TitleTextStyle CssClass="login-title"/>
        <ValidatorTextStyle ForeColor="Red" />
        <TextBoxStyle CssClass="form-control" />
        <LoginButtonStyle CssClass="btn btn-primary" />
        <LabelStyle CssClass="login-label"/>
        <CheckBoxStyle CssClass="login-CheckBoxStyle"/>
    </asp:Login>
    
</asp:Content>