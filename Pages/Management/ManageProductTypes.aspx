﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageProductTypes.aspx.cs" Inherits="Pages_Management_ManageProductTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
    Name:</p>
<p>
    <asp:TextBox ID="txtName" runat="server" Width="186px"></asp:TextBox>
</p>
<p>
    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Width="68px" />
</p>
<p>
    <asp:Label ID="lblResult" runat="server"></asp:Label>
</p>
<p>
    &nbsp;</p>
</asp:Content>

