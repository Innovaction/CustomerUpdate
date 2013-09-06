<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicAddControl.ascx.cs" Inherits="Innovaction.Modules.CustomerUpdate.DynamicAddControl" %>
<%@ Register Src="DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>

<!-- display hijos  tituylo -->
<br />
<div class="container">
     <div class="row">
         <div class="column three">
             <asp:Label CssClass="texto" ID="Label1" runat="server" Text="Fecha de nacimiento"></asp:Label>

         </div>
          <div class="column five">
           
              <uc1:datepicker runat="server" id="DDD_picker" />
         </div>
         <div class="column two">
             <asp:Label CssClass="texto" ID="Label2" runat="server" Text="Sexo"></asp:Label>

         </div>
         <div class="column two">
             <asp:DropDownList CssClass="form2" ID="dd_gender" runat="server">
                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                 <asp:ListItem Value="F">Femenino</asp:ListItem>
             </asp:DropDownList>
         </div>

         <div class="column one">

             <asp:LinkButton ID="AddMe" runat="server" Text="Agregar" OnClick="Add_Click" />
         </div>
       <div class="column one">
      </div>

         </div>
    <br />
    <br />
            <div class="row">
             

                               
                                 

                                    <div class="column four">
                                      <asp:Label CssClass="texto" ID="Label101" runat="server" Text="Fecha" HelpText="It's the name of the thing" />
                              
                                    </div>
                                    <div class="column four">
                                        <asp:Label CssClass="texto" ID="Label100" runat="server" Text="Sexo" HelpText="It's the name of the thing" />
                                    </div>

                                    <div class="column four">
                                         
                                    </div>
                                     

                </div>
           
    <br />

              <!-- display hijos body -->

             <div class="row">
                <div class="column twelve">
                    <asp:PlaceHolder ID="DynamicPlaceHolder" runat="server"></asp:PlaceHolder>
                     </div>
                 


          </div>
</div>
<asp:TextBox CssClass="form" ID="tx_Qty" runat="server" Visible="False"></asp:TextBox>
