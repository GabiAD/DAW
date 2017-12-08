<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ProfileLayout.master" AutoEventWireup="true" CodeFile="AddAlbum.aspx.cs" Inherits="Pages_Content_AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderProfile" Runat="Server">

    <div class="profile-add-photo">
        <h3>Add new album
        </h3>

        <div class="form-group">
            <label for="albumName">New Album Name</label>
            <asp:RequiredFieldValidator ControlToValidate="albumName" ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is mandatory" CssClass="alert-danger"></asp:RequiredFieldValidator>
            <asp:TextBox ID="albumName" runat="server" CssClass="form-control" placeholder="New Album Name"></asp:TextBox>
        </div>
        
        <asp:Button ID="Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="Submit_Click" />
    </div>    

</asp:Content>

