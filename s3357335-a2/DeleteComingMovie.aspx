<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DeleteComingMovie.aspx.cs" Inherits="s3357335_a2.DeleteComingMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HiddenField ID="ComingMovieID" runat="server" />

    <div>
        <div class='alert alert-info' role='alert'>
            Are you going to delete this information?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
            <asp:Button ID="deleteBtn" runat="server" Text="Delete" class="btn btn-default" OnClick="deleteBtn_Click" />&nbsp;&nbsp;
            <asp:Button ID="cancelBtn" runat="server" Text="Cancel" class="btn btn-default" OnClick="cancelBtn_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-xs-2">
            <div class="thumbnail">
                <asp:Image ID="Picture" runat="server" />
                <div class="caption" style="text-align: center;">
                    <h4>
                        <asp:Label ID="ComingMovieTitle" runat="server" Text="Title" />
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-xs-7">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 style="font-weight: bold; font-family: Arial">About Coming Movie</h4>
                </div>
                <div class="panel-body">
                    <p>
                        <asp:Label ID="MovieDescription" runat="server" Text="Content" />
                    </p>
                    <h4><span class="label label-info">Release Time:
                        <asp:Label ID="ReleaseTime" runat="server" Text="Title" /></span>
                    </h4>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
