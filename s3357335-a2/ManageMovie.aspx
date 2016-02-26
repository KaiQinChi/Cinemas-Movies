<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ManageMovie.aspx.cs" Inherits="s3357335_a2.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <h4><span class="label label-default">All the Movies</span></h4>
    <asp:ListView ID="MovieListView" runat="server">
        <LayoutTemplate>
            <table class="table table-hover">
                <tr>
                    <th>Movie ID</th>
                    <th>Movie Title</th>
                    <th>Actions</th>
                </tr>
                <tr runat="server" id="itemPlaceHolder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="itemPlaceHolder">
                <td><%#Eval("MovieID")%></td>
                <td><%#Eval("Title")%></td>
                <td>
                    <asp:HyperLink ID="ViewMovie" runat="server" Target="_self" NavigateUrl='<%# Eval("MovieID","~/ViewAMovie.aspx?MovieID={0}") %>'>View</asp:HyperLink>&nbsp|
                    <asp:HyperLink ID="EditMovie" runat="server" Target="_self" NavigateUrl='<%# Eval("MovieID","~/EditMovie.aspx?MovieID={0}") %>'>Edit</asp:HyperLink>
                    <%--<asp:HyperLink ID="DeleteMovie" runat="server" Target="_self" NavigateUrl="~/DeleteMovie.aspx">Delete</asp:HyperLink>--%>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
