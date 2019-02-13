<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Planų pasirinkimas</h1>
        <p class="lead">koks tau planas labiau tinka</p>
   </div>

    <div class="row">
         <div class="col-md-6">
        <div class="col-md-12">
            SMS: <asp:TextBox ID="sms" runat="server" MaxLength="6" FilterType="Numbers">-1</asp:TextBox>
            Prioritetai: <asp:TextBox ID="sms_s" runat="server" MaxLength="6" FilterType="Numbers">1</asp:TextBox>
             </div>
        <div class="col-md-12">
            MIN: <asp:TextBox ID="min" runat="server" MaxLength="6">-1</asp:TextBox>
            Prioritetai: <asp:TextBox ID="min_s" runat="server" MaxLength="6" FilterType="Numbers">1</asp:TextBox>
             </div>
        <div class="col-md-12">
            GB:&nbsp; <asp:TextBox ID="gb" runat="server" MaxLength="6">4</asp:TextBox>
            Prioritetai: <asp:TextBox ID="gb_s" runat="server" MaxLength="6" FilterType="Numbers">1</asp:TextBox>
             </div>
        <div class="col-md-12">
            EUR: <asp:TextBox ID="eur" runat="server" MaxLength="6">0</asp:TextBox>
            Prioritetai: <asp:TextBox ID="eur_s" runat="server" MaxLength="6" FilterType="Numbers">1</asp:TextBox>
        </div>
              </div>
        <div class="col-md-4">
            <asp:Button runat="server" id="btnLogin" Text="Geriausias Jums" OnClick="btnLogin_Click" />
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="Planas" runat="server"></asp:TextBox>

            
        </div>
    </div>
    <div class="row" style="padding-top:30px">
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
