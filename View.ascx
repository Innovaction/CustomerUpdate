<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Innovaction.Modules.CustomerUpdate.View" %>
<%@ Register Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ Register Src="DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Register Src="DynamicAddControl.ascx" TagPrefix="uc1" TagName="DynamicAddControl" %>







<script type="text/javascript">
    jQuery(function ($) {
        $('#UpdateTabs').dnnTabs();
    });
</script>



 
<!-- vamos a usar un div para cada tab-->
<div id="MainPanel" runat="server">
<div class="dnnForm" id="UpdateTabs">
    <div class="container">
        <div class="row">
            <div class="column twelve">
                <ul class="dnnAdminTabNav">

                    <li><a href="#DatosPersonales">Datos Personales</a></li>
                    <li><a href="#Direccion">Direccion</a></li>
                    <li><a href="#OtrosDatos">Otros Datos</a></li>
                    <li><a href="#PreferenciasEmail">Preferencias de envio de correo</a></li>

                </ul>
            </div>

        </div>
    </div>

       

    <!-- TAB-->
    <!-- Datos personales tab-->
    <div id="DatosPersonales" class="dnnClear">
          <br />Datos Personales
          <br />
        <br />
        <div class="container">

            <!-- Primer Nombre -->
            <div class="row">
                <div class="column three">
                   
                   
                    <asp:Label CssClass="texto" ID="Label1" runat="server" Text="*Primer Nombre" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_FirstName" runat="server" />
                </div>
                <div class="column six">
             
                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo obligatorio" ControlToValidate="tx_FirstName" CssClass="dnnFormError"></asp:RequiredFieldValidator>
                       </div>
            </div>

            <!-- segundo Nombre -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label2" runat="server" Text="Segundo Nombre" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_SecondName" runat="server" />
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- Primer apellido -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label3" runat="server" Text="*Primer Apellido" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form "  ID="tx_FirstSurname" runat="server" />
                </div>
                <div class="column six">
                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo obligatorio" ControlToValidate="tx_FirstSurname" CssClass="dnnFormError"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- segundo apellido -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label4" runat="server" Text="Segundo Apellido" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_SecondSurname" runat="server" />
                </div>
                <div class="column six">
                </div>
            </div>


            <!-- genero -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label5" runat="server" Text="*Genero" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:DropDownList CssClass="form2" ID="dd_Gender" runat="server" ReadOnly="True">
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="column six">
                </div>
            </div>

                  <!-- CI -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label416" runat="server" Text="*Documento de Identidad" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
               
                <asp:TextBox CssClass="form" ID="tx_CI" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

    
            <!-- birth date -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label7" runat="server" Text="*Fecha de Nacimiento" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">



                    <uc1:DatePicker runat="server" id="dd_BirthDate" />


                </div>
                <div class="column six">
                </div>
            </div>

            <!-- matrial state -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label43" runat="server" Text="Estado Civil" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">


                    <asp:DropDownList CssClass="form2" ID="dd_MatrialState" runat="server"></asp:DropDownList>


                </div>
                <div class="column six">
                </div>
            </div>

            <!-- cell -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label9" runat="server" Text="Celular" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form "  ID="tx_CellPhone" runat="server" />
                   
                </div>
                <div class="column six">
                </div>
            </div>


                        <!-- email -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label4516" runat="server" Text="*Email" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form" ID="tx_Email" runat="server" Enabled="False"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>


            <!-- twitter -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label11" runat="server" Text="Twitter" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_Twitter" runat="server" />
              
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- facebook -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label12" runat="server" Text="Facebook" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_Facebook" runat="server" />
                      
                </div>
                <div class="column six">
                </div>
            </div>




        </div>
    </div>







    <!-- TAB-->
    <!-- Direccion-->

    <div id="Direccion" class="dnnClear">
         <br /> Direccion
          <br />
        <br />
        <div class="container">

            <!-- Direccion -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label8" runat="server" Text="*Direccion" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:DropDownList CssClass="form2" ID="dd_Address" runat="server">
                    </asp:DropDownList>
                    
                </div>
                <div class="column six">
                </div>
            </div>


            <!-- estado -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label13" runat="server" Text="*Estado" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:DropDownList CssClass="form2" ID="dd_State" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dd_State_SelectedIndexChanged"></asp:DropDownList>
                      
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- ciudad -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label14" runat="server" Text="*Cuidad" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList CssClass="form2" ID="dd_City" runat="server"></asp:DropDownList>
                          
                            </ContentTemplate>
                         <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dd_State" EventName="SelectedIndexChanged" />
                    </Triggers>
                        </asp:UpdatePanel>

                </div>
                <div class="column six">
                </div>
            </div>

            <!-- municipio -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label15" runat="server" Text="Municipio" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_Municipio" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- edificio/casa -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label16" runat="server" Text="Edificio / Casa" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_Building" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- piso -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label17" runat="server" Text="Piso" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_floor" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- departamento numero -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label18" runat="server" Text="Departamento Numero" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_apartmentNumber" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>


            <!-- calle -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label19" runat="server" Text="Calle" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_Street" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- urbanizacion -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label20" runat="server" Text="Urbanizacion" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_Urban" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>


            <!-- punto de referencia -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label21" runat="server" Text="Punto de referencia" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_RefPoint" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- CP -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label22" runat="server" Text="Codigo Postal" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:TextBox CssClass="form " ID="tx_CP" runat="server"></asp:TextBox>
                </div>
                <div class="column six">
                </div>
            </div>

        </div>
    </div>







    <!-- TAB-->
    <!-- Otros Datos-->
    <div id="OtrosDatos" class="dnnClear">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
          <br />Otros datos
          <br />
        <br />
        <div class="container">

            <!-- Nacionalidad -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label23" runat="server" Text="Nacionalidad" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:DropDownList CssClass="form2" ID="dd_Country" runat="server"></asp:DropDownList>
                </div>
                <div class="column six">
                </div>
            </div>

            <!-- Ocupacion -->
            <div class="row">
                <div class="column three">
                    <asp:Label CssClass="texto" ID="Label24" runat="server" Text="Ocupacion" HelpText="It's the name of the thing" />
                </div>
                <div class="column three">
                    <asp:DropDownList CssClass="form2" ID="dd_Jobs" runat="server"></asp:DropDownList>
                </div>
                <div class="column six">
                </div>
            </div>
                <br />    <br />
             <!-- hijos -->
            <div class="row">
                <div class="column twelve">
                    Hijos
                
                    </div>
            </div>
          
               
            <!-- hijos -->
          <div class="row">
                <div class="column twelve">

                     <uc1:DynamicAddControl runat="server" id="Kid_DynamicAddControl" />

                </div>
           
            </div>

            

                         
               



        </div>
         </ContentTemplate>

</asp:UpdatePanel>
    </div>









    <!-- TAB-->
    <!-- Preferencias email-->

    <div id="PreferenciasEmail" class="dnnClear">
        <br />
        Preferencias de envio de correo
        <br />  <br />
        <div class="container">

            <!-- Row Label queres info?-->
            <div class="row">
                <div class="column twelve">
                    <asp:Label CssClass="texto" ID="Label29" runat="server" Text="*¿Que informacion de nuestros productos y servicios desea recibir?" HelpText="It's the name of the thing" />
                </div>
            </div>
            <!-- Row Radiobutton si o no 
            <div class="row">
                <div class="column twelve">
                    <asp:RadioButtonList ID="rb_info" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">Si</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:RadioButtonList>
                  </div>
             </div>
       -->
                    <!-- topic checkbox list -->
                    <div class="row">
                        <div class="column twelve">
                            <asp:CheckBoxList CssClass="texto" ID="chbx_Topics" runat="server"></asp:CheckBoxList>

                        </div>
                    </div>
                  
             
               <br />   

             <!-- Row Label Frecuencia?-->
            <div class="row">
                <div class="column twelve">
                    <asp:Label CssClass="texto" ID="Label30" runat="server" Text="*Frecuencia" HelpText="It's the name of the thing" />
                </div>
            </div>

               <!-- Row Radiobutton para la frecuencia -->
            <div class="row">
                <div class="column twelve">
                    <asp:RadioButtonList CssClass="texto" ID="rb_Frequency" runat="server" RepeatDirection="Horizontal">
                       
                    </asp:RadioButtonList>
                  </div>
             </div>
            

                     
               <br />  
            
             <!-- Row Label Canal?-->
            <div class="row">
                <div class="column twelve">
                    <asp:Label CssClass="texto" ID="Label31" runat="server" Text="*Canal" HelpText="It's the name of the thing" />
                </div>
            </div>

               <!-- canal -->
            <div class="row">
                <div class="column twelve">
                    <asp:CheckBoxList CssClass="texto" ID="rb_Channel" runat="server" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                  </div>
             </div>
            


               </div>
            </div>
       

              
</div>




    <!-- mi boton de actualizar y cancelar deben ir aca (este es el footer del modulo en realidad)-->
       <br  /><br  /><br  />
      <asp:Label CssClass="texto" runat="server" ID="Label32" Text="Los campos señalados con asteristo (*) son obligatorios"  />
    <br  />
      <br  />
    <asp:LinkButton runat="server" ID="ActualizarButton" Text="Actualizar" OnClick="ActualizarButton_Click" />
    <asp:Label CssClass="texto" runat="server" ID="separadorI" Text=" | " />
    <asp:LinkButton runat="server" ID="CancelarButton" Text="Cancelar" />

     <br  />
      <br  />
     <asp:Label CssClass="texto" runat="server" ID="Label33" Text="Si deseas cambiar tu usuario y/o cedula de identidad comunicate con el 0-800-FARMATODO" />
  

    </div>


 <!-- es la unica manera de hacer persisntetes las cosas que encontre por ahora, se puede cambiar! -->
<div id="invisibleStuff">
     <asp:Label CssClass="texto" ID="tx_NationalID" runat="server" Text="" Visible="false"/>
     <asp:Label CssClass="texto" ID="tx_CustomerID" runat="server" Text="" Visible="false"/>
      <asp:Label CssClass="texto" ID="tx_CityOLD" runat="server" Visible="False"/>
      <asp:Label CssClass="texto" ID="tx_CellPhoneOLD" runat="server" Visible="False"/>
                    <asp:Label CssClass="texto" ID="tx_TwitterOLD" runat="server" Visible="False"/>
                    <asp:Label CssClass="texto" ID="tx_FacebookOLD" runat="server" Visible="False"/>
                    <asp:Label CssClass="texto" ID="tx_AddressOLD" runat="server" Visible="False"/>
                     <asp:Label CssClass="texto" ID="tx_StateOLD" runat="server" Visible="False"/>
</div>


