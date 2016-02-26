<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewAEvent.aspx.cs" Inherits="s3357335_a2.ViewAEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <h4><span class="label label-default">Event Detail</span></h4>
    <div class="row">
        <div class="col-xs-2">
            <div class="thumbnail">
                <asp:Image ID="Picture" runat="server" />
                <div class="caption" style="text-align: center;">
                    <h4>
                        <asp:Label ID="EventTitle" runat="server" Text="Title"></asp:Label>
                    </h4>
                </div>
            </div>
        </div>
        <div class="col-xs-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 style="font-weight: bold; font-family: Arial">About Event</h4>
                </div>
                <div class="panel-body">
                    <p>
                        <asp:Label ID="ShortDescription" runat="server" Text="Content" />
                    </p>
                    <p>
                        <asp:Label ID="LongDescription" runat="server" Text="Content" />
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
