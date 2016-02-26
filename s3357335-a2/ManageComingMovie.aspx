<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ManageComingMovie.aspx.cs" Inherits="s3357335_a2.ManageComingMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadSection" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/InsertComingMovie.aspx" Target="_self" class="btn btn-default btn-sm active" role="button">Create Coming Movie</asp:HyperLink>
    <br />
    <br />
    <h4><span class="label label-default">All the Coming Movies</span></h4>
    <asp:ListView ID="ComingMovieListView" runat="server">
        <LayoutTemplate>
            <table class="table table-hover">
                <tr>
                    <th>Coming Movie ID</th>
                    <th>Coming Movie Title</th>
                    <th>Actions</th>
                </tr>
                <tr runat="server" id="itemPlaceHolder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="itemPlaceHolder">
                <td><%#Eval("MovieComingSoonID")%></td>
                <td><%#Eval("Title")%></td>
                <td>
                    <asp:HyperLink ID="ViewComingMovie" runat="server" Target="_self" NavigateUrl='<%# Eval("MovieComingSoonID","~/ViewAComingMovie.aspx?MovieComingSoonID={0}") %>'>View</asp:HyperLink>&nbsp|
                    <asp:HyperLink ID="EditComingMovie" runat="server" Target="_self" NavigateUrl='<%# Eval("MovieComingSoonID","~/EditComingMovie.aspx?MovieComingSoonID={0}") %>'>Edit</asp:HyperLink>&nbsp|
                    <asp:HyperLink ID="DeleteComingMovie" runat="server" Target="_self" NavigateUrl='<%# Eval("MovieComingSoonID","~/DeleteComingMovie.aspx?MovieComingSoonID={0}") %>'>Delete</asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndSection" runat="server">
</asp:Content>
