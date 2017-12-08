<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ProfileLayout.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="Pages_Content_EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderProfile" runat="Server">

    <asp:Label ID="message" CssClass="alert alert-success" role="alert" Visible="false" runat="server" style="display: block"></asp:Label>

    <div class="profile-edit">
        <h3>Edit profile
        </h3>

        <div class="form-group">
            <label for="Nume">Forename</label>
            <asp:RequiredFieldValidator ControlToValidate="Prenume" ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is mandatory" CssClass="alert-danger"></asp:RequiredFieldValidator>
            <asp:TextBox ID="Prenume" runat="server" CssClass="form-control" placeholder="Forename"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="Prenume">Surname</label>
            <asp:RequiredFieldValidator ControlToValidate="Nume" ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is mandatory" CssClass="alert-danger"></asp:RequiredFieldValidator>
            <asp:TextBox ID="Nume" runat="server" CssClass="form-control" placeholder="Surname"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <label for="exampleInputEmail1">Birthday</label>
            <asp:CompareValidator ControlToValidate="DataNasterii" ID="CompareValidator1" runat="server" ErrorMessage="Please insert a valid date (MM/dd/yyyy)" CssClass="alert-danger" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
            <asp:TextBox ID="DataNasterii" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="Oras">City</label>
            <br />
            <asp:DropDownList CssClass="btn btn-default dropdown-toggle" ID="Oras" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label>Profile picture</label>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label" AssociatedControlID="PhotoUpload">
                <div class="btn btn-default">Upload</div>
                <asp:FileUpload ID="PhotoUpload" runat="server" style="position: absolute; left: -9999px"/>
            </asp:Label>
        </div>
        
        <asp:Button ID="Submit" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="Submit_Click" />
    </div>

    <br />  
    <br />
</asp:Content>

