<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EditMovie.aspx.cs" Inherits="s3357335_a2.EditMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HiddenField ID="MovieID" runat="server" />

    <div style="padding-bottom: 0.5em">
        <h4><span class="label label-default">Movie Edit</span></h4>
    </div>
    <div class="form-group" style="width: 600px">
        <label for="MovieTitle">Movie Title</label>
        <asp:RequiredFieldValidator ID="RequiredFieldMovieTitle" runat="server" ErrorMessage=" (Please enter movie title!)" ControlToValidate="MovieTitle" ForeColor="#FF3300" />
        <asp:TextBox ID="MovieTitle" runat="server" class="form-control" />
    </div>
    <div class="form-group" style="width: 600px">
        <label for="Description">Movie Description</label>
        <asp:RequiredFieldValidator ID="RequiredFieldDescription" runat="server" ErrorMessage=" (Please enter movie Description!)" ControlToValidate="Description" ForeColor="#FF3300" />
        <asp:TextBox ID="Description" TextMode="MultiLine" Rows="8" runat="server" class="form-control" />
    </div>
    <div class="form-group" style="width: 600px">
        <label for="PictureUpload" style="width: 100%">Movie Picture</label>
        <div style="padding-bottom: 1em">
            <asp:Image ID="Picture" runat="server" class="img-rounded" />
        </div>
        <asp:FileUpload ID="PictureUpload" runat="server" />
        <p class="help-block">Change a picture for the movie.</p>
    </div>
    <div>
        <asp:Button ID="saveBtn" runat="server" Text="Submit" OnClick="saveBtn_Click" class="btn btn-default" />
        &nbsp;&nbsp;
        <button type="reset" class="btn btn-default">Reset</button>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
