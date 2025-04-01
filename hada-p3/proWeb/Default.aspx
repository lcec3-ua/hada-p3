<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Products management</h2>
    <table>
        <tr>
            <td>Code</td>
            <td><asp:TextBox ID="txtCode" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name</td>
            <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Amount</td>
            <td><asp:TextBox ID="txtAmount" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server">
                    <asp:ListItem>Computing</asp:ListItem>
                    <asp:ListItem>Telephony</asp:ListItem>
                    <asp:ListItem>Gaming</asp:ListItem>
                    <asp:ListItem>Home appliances</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Price</td>
            <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Creation Date</td>
            <td><asp:TextBox ID="txtCreationDate" runat="server"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
    <asp:Button ID="btnRead" runat="server" Text="Read" OnClick="btnRead_Click" />
    <asp:Button ID="btnReadFirst" runat="server" Text="Read First" OnClick="btnReadFirst_Click" />
    <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" OnClick="btnReadPrev_Click" />
    <asp:Button ID="btnReadNext" runat="server" Text="Read Next" OnClick="btnReadNext_Click" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
