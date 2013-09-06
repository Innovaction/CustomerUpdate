<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="Innovaction.Modules.CustomerUpdate.DatePicker" %>
<div>
    <table>
        <tr>
            <td>
             
                        <asp:DropDownList CssClass="form2" ID="ddlMonth" runat="server" AutoPostBack="true" DataTextField="MonthName"
                            DataValueField="MonthNumber" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                
            </td>
            <td>
               
                        <asp:DropDownList CssClass="form2"  ID="ddlday" runat="server">
                        </asp:DropDownList>
                   
            </td>
            <td>
                <asp:DropDownList CssClass="form2"  ID="ddlYear" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>