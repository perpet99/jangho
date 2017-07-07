<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    알람 등록 날짜 리스트
    <br />
    <asp:label id="myLabel" runat="server"/>

    <br />
    <br />
    <form id="form1" >
                RemoveDate:
                <input type="text" id="txtName" name="Name" value="" />
                
            <asp:Button Text="Submit"  runat="server" OnClick="Submit" />
        <br />
    </form>
    <br />

     <form id="form2" >
                AddDate:
                <input type="text" id="txtName2" name="Name" value="" />
                
            <asp:Button Text="Submit2"  runat="server" OnClick="Submit2" />
         <br />
    </form>
</asp:Content>
