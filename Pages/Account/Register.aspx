<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Pages_Account_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Literal ID="litStatus" runat="server"></asp:Literal>
<br />
User Name:<br />
<asp:TextBox ID="txtUserName" runat="server" CssClass="inputs" Width="202px"></asp:TextBox>
<br />
<br />
Password:<br />
<asp:TextBox ID="txtPassword" runat="server" CssClass="inputs" TextMode="Password" Width="202px"></asp:TextBox>
<br />
<br />
Confirm Password:<br />
<asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="inputs" TextMode="Password" Width="201px"></asp:TextBox>
<br />
<br />
First Name:<br />
<asp:TextBox ID="txtFirstName" runat="server" CssClass="inputs" Width="209px"></asp:TextBox>
<br />
<br />
Last Name:<br />
<asp:TextBox ID="txtLastName" runat="server" CssClass="inputs" Width="204px"></asp:TextBox>
<br />
<br />
Address:<br />
<asp:TextBox ID="txtAddress" runat="server" CssClass="inputs" Height="82px" TextMode="MultiLine" Width="199px"></asp:TextBox>
<br />
<br />
<br />
Postal code:<br />
<asp:TextBox ID="txtPostalCode" runat="server" CssClass="inputs" Width="199px"></asp:TextBox>
<br />
<asp:Button ID="Button1" runat="server" CssClass="button" OnClick="Button1_Click" Text="Register" Width="86px" />
<br />
<br />
</asp:Content>

