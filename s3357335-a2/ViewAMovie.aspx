<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewAMovie.aspx.cs" Inherits="s3357335_a2.ViewAMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <h4><span class="label label-default">Movie Detail</span></h4>
    <div class="row">
        <div class="col-xs-3">
            <div class="thumbnail">
                <asp:Image ID="Picture" runat="server" />
                <div class="caption" style="text-align: center;">
                    <h4>
                        <asp:Label ID="MovieTitle" runat="server" Text="Title"></asp:Label>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-xs-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 style="font-weight: bold; font-family: Arial">About Movie</h4>
                </div>
                <div class="panel-body">
                    <p>
                        <asp:Label ID="MovieDescription" runat="server" Text="Content" />
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
