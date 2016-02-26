<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewAComingMovie.aspx.cs" Inherits="s3357335_a2.ViewAComingMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <h4><span class="label label-default">Coming Movie Detail</span></h4>
    <div class="row">
        <div class="col-xs-3">
            <div class="thumbnail">
                <asp:Image ID="Picture" runat="server" />
                <div class="caption" style="text-align: center;">
                    <h4>
                        <asp:Label ID="ComingMovieTitle" runat="server" Text="Title" />
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-xs-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 style="font-weight: bold; font-family: Arial">About Coming Movie</h4>
                </div>
                <div class="panel-body">
                    <h4><span class="label label-info">Release Time:
                        <asp:Label ID="ReleaseTime" runat="server" Text="Title" /></span>
                    </h4>
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
