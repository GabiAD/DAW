<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/AuthenticationLayout.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Pages_Authentication_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAuthentication" Runat="Server">

    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/Pages/Content/Profile.aspx">
        <TitleTextStyle CssClass="login-title"/>
        <ValidatorTextStyle ForeColor="Red" />
        <TextBoxStyle CssClass="form-control" />
        <LabelStyle CssClass="login-label"/>
        <CancelButtonStyle CssClass="btn btn-default" />
        <CreateUserButtonStyle CssClass="btn btn-warning"/>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>

</asp:Content>
