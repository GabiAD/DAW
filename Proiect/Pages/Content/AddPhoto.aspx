<%@ Page Title="" Language="C#" MasterPageFile="~/Layouts/ProfileLayout.master" AutoEventWireup="true" CodeFile="AddPhoto.aspx.cs" Inherits="Pages_Content_AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderProfile" runat="Server">

    <div class="profile-add-photo">
        <h3>Add new photo
        </h3>

        <asp:Label ID="message" CssClass="alert alert-success" role="alert" Visible="false" runat="server" style="display: block"><b>Great!</b>  The picture has been added.</asp:Label>

        <div class="row">
            <div class="col-sm-8">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label>Picture</label>
                            <asp:CustomValidator ID="PhotoValidator" CssClass="alert-danger" runat="server" ErrorMessage="You have to select a photo" OnServerValidate="PhotoValidate"></asp:CustomValidator>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Label" AssociatedControlID="PhotoUpload">
                                <div class="btn btn-default">Upload</div>
                                <asp:FileUpload ID="PhotoUpload" runat="server" Style="position: absolute; left: -9999px" OnClick="OnFileUploadChange"/>
                            </asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="AlbumDD">Album</label>
                            <asp:RequiredFieldValidator ControlToValidate="AlbumDD" ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is mandatory" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <br />
                            <asp:DropDownList CssClass="btn btn-default dropdown-toggle" ID="AlbumDD" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label for="TagDD">Category</label>
                            <asp:RequiredFieldValidator ControlToValidate="TagDD" ID="RequiredFieldValidator3" runat="server" ErrorMessage="This field is mandatory" CssClass="alert-danger"></asp:RequiredFieldValidator>
                            <br />
                            <asp:DropDownList CssClass="btn btn-default dropdown-toggle" ID="TagDD" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 form-group">
                        <asp:RequiredFieldValidator ControlToValidate="TitluPozaNoua" ID="RequiredFieldValidator4" runat="server" ErrorMessage="This field is mandatory" CssClass="alert-danger"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="TitluPozaNoua" runat="server" CssClass="form-control" placeholder="Title"></asp:TextBox>
                        <br />
                        <TextArea ID="Descriere" class="form-control" runat="server" style="width: 100%; height: 100px; text-align: left;" maxlength=500 placeholder="Description"></TextArea>
                    </div>
                </div>

            </div>
            <div class="col-sm-4">
                <label>Preview</label>
                <br />
                <asp:Image class="PreviewImage" ID="PreviewImage" Style="width: 100%; height: auto;" ImageUrl="~/Content/imgs/no-photo.png" runat="server"/>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <asp:Button ID="Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="Submit_Click" />
            </div>
            <div class="col-sm-offset-4 col-sm-5">
                <label for="FlipOpt">Flip</label>
                <asp:CheckBox ID="FlipOpt" AutoPostBack="true" runat="server" />
                &ensp;
                <label for="BlackAndWhite">Black&White</label>
                <asp:CheckBox ID="BlackAndWhite" AutoPostBack="true" runat="server" />
                &ensp;
                <label for="Contrast">Contrast</label>
                <asp:TextBox AutoPostBack="true" ID="Contrast" runat="server" min="-100" max="100" type="range" CssClass="inline-range"></asp:TextBox>

            </div>
        </div>
    </div>

    <br />
    <br />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">
</script>

<script>
    function readURL(input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(".PreviewImage").attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
        
    }

    $("input[id$='PhotoUpload']").change(function () {
        readURL(this);
    });
</script>

</asp:Content>

