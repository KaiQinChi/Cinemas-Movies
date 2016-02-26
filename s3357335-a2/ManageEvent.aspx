<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ManageEvent.aspx.cs" Inherits="s3357335_a2.ManageEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HyperLink ID="InsertCoporateEvent" runat="server" NavigateUrl="~/InsertCoporateEvent.aspx" Target="_self" class="btn btn-default btn-sm active" role="button">Create Coporate Event</asp:HyperLink>
    <br />
    <br />
    <h4><span class="label label-default">All the Corporate Events</span></h4>
    <asp:ListView ID="EventsListView" runat="server">
        <LayoutTemplate>
            <table class="table table-hover">
                <tr>
                    <th>Event ID</th>
                    <th>Event Title</th>
                    <th>Actions</th>
                </tr>
                <tr runat="server" id="itemPlaceHolder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="itemPlaceHolder">
                <td><%#Eval("EventID")%></td>
                <td><%#Eval("Title")%></td>
                <td>
                    <asp:HyperLink ID="ViewEvent" runat="server" Target="_self" NavigateUrl='<%# Eval("EventID","~/ViewAEvent.aspx?EventID={0}") %>'>View</asp:HyperLink>&nbsp|
                    <asp:HyperLink ID="EditEvent" runat="server" Target="_self" NavigateUrl='<%# Eval("EventID","~/EditEvent.aspx?EventID={0}") %>'>Edit</asp:HyperLink>&nbsp|
                    <asp:HyperLink ID="DeleteEvent" runat="server" Target="_self" NavigateUrl='<%# Eval("EventID","~/DeleteEvent.aspx?EventID={0}") %>'>Delete</asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
