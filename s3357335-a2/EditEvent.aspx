<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EditEvent.aspx.cs" Inherits="s3357335_a2.EditEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HiddenField ID="EventID" runat="server" />

    <div style="padding-bottom: 0.5em">
        <h4><span class="label label-default">Edit Event</span></h4>
    </div>
    <div class="form-group" style="width: 600px">
        <label for="EventTitle">Event Title</label>
        <asp:RequiredFieldValidator ID="RequiredFieldMovieTitle" runat="server" ErrorMessage=" (Please enter Event title!)" ControlToValidate="EventTitle" ForeColor="#FF3300" />
        <asp:TextBox ID="EventTitle" runat="server" class="form-control" />
    </div>
    <div class="form-group" style="width: 600px">
        <label for="ShortDescription">Event Short Description</label>
        <asp:RequiredFieldValidator ID="RequiredFieldSDescription" runat="server" ErrorMessage=" (Please enter Event Short Description!)" ControlToValidate="ShortDescription" ForeColor="#FF3300" />
        <asp:TextBox ID="ShortDescription" TextMode="MultiLine" Rows="6" runat="server" class="form-control" />
    </div>
    <div class="form-group" style="width: 600px">
        <label for="LongDescription">Event Long Description</label>
        <asp:RequiredFieldValidator ID="RequiredFieldLDescription" runat="server" ErrorMessage=" (Please enter Event Long Description!)" ControlToValidate="LongDescription" ForeColor="#FF3300" />
        <asp:TextBox ID="LongDescription" TextMode="MultiLine" Rows="8" runat="server" class="form-control" />
    </div>
    <div class="form-group" style="width: 600px">
        <label for="PictureUpload" style="width: 100%">Event Picture</label>
        <div style="padding-bottom: 1em">
            <asp:Image ID="Picture" runat="server" class="img-rounded" />
        </div>
        <asp:FileUpload ID="PictureUpload" runat="server" />
        <p class="help-block">Change a picture for the Event.</p>
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
