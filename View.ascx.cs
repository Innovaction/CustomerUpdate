/*
' Copyright (c) 2013  Innovaction.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using Innovaction.Modules.CustomerUpdate.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Web.UI;
using Innovaction;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace Innovaction.Modules.CustomerUpdate
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from UpdateCustomerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as ThePortalID, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CustomerUpdateModuleBase, IActionable
    {

        int ThePortalID;
        // esta variabnle la dejo para trabajar de manera local sin tener que loguearme al sitio para testear
        bool Debugging = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            DotNetNuke.Framework.jQuery.RequestDnnPluginsRegistration();
            //DotNetNuke.Framework.jQuery.RequestUIRegistration();
            //DotNetNuke.Framework.jQuery.RequestRegistration();


            Innovaction.CustomerDataWS.customerResponse TheUser;
            if (!IsPostBack)
            {

                if (Debugging)
                {
                    ThePortalID = 0;
                    TheUser = Innovaction.WSManager.ValidateLogin("nikmazza@gmail.com", "farmatodo123", ThePortalID);
                }
                else
                {
                    ThePortalID = PortalId;
                    TheUser = Innovaction.WSManager.ValidateLoginByUserID(ThePortalID, UserId);
                }


                PopulateDropdowns();

                if (TheUser.customer != null)
                {

                    var TheCustomer = Innovaction.WSManager.GetFullCustomer(TheUser.customer, ThePortalID).customer;

                    CompleteCustomerInfo(TheCustomer);
                }




            }
        }

        protected void rptItemListOnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lnkEdit = e.Item.FindControl("lnkEdit") as HyperLink;
                var lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;


                var pnlAdminControls = e.Item.FindControl("pnlAdmin") as Panel;

                var t = (Item)e.Item.DataItem;

                if (IsEditable && lnkDelete != null && lnkEdit != null && pnlAdminControls != null)
                {
                    pnlAdminControls.Visible = true;
                    lnkDelete.CommandArgument = t.ItemId.ToString();
                    lnkDelete.Enabled = lnkDelete.Visible = lnkEdit.Enabled = lnkEdit.Visible = true;

                    lnkEdit.NavigateUrl = EditUrl(string.Empty, string.Empty, "Edit", "tid=" + t.ItemId);

                    ClientAPI.AddButtonConfirm(lnkDelete, Localization.GetString("ConfirmDelete", LocalResourceFile));
                }
                else
                {
                    pnlAdminControls.Visible = false;
                }
            }
        }

        public void rptItemListOnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect(EditUrl(string.Empty, string.Empty, "Edit", "tid=" + e.CommandArgument));
            }

            if (e.CommandName == "Delete")
            {
                var tc = new ItemController();
                tc.DeleteItem(Convert.ToInt32(e.CommandArgument), ModuleId);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        protected void ActualizarButton_Click(object sender, EventArgs e)
        {
            // como deberia ser
            var ToUpdate = Innovaction.WSManager.EmptyCustomer(ThePortalID);

            // para debuguear:
            if(Debugging){
            ToUpdate = Innovaction.WSManager.GetFullCustomer(Innovaction.WSManager.ValidateLogin("nikmazza@gmail.com", "farmatodo123", ThePortalID).customer, ThePortalID).customer;
            }
                // end debug

            ToUpdate.id = Convert.ToInt32(tx_CustomerID.Text);
            ToUpdate.idSpecified = true;
            ToUpdate.nationalId = tx_NationalID.Text;

            #region datos personales
            ToUpdate.fistName = tx_FirstName.Text;
            ToUpdate.middleName = tx_SecondName.Text;
            ToUpdate.lastName = tx_FirstSurname.Text;
            ToUpdate.surname = tx_SecondSurname.Text;
            ToUpdate.gender = dd_Gender.SelectedValue;
            // fuera del sistema, no se puede actualizar! shiradit
            //ToUpdate.nationalId = dd_CI.SelectedValue + "-" + tx_CI.Text;
            ToUpdate.birthDay = dd_BirthDate.SelectedValue;

            ToUpdate.birthDaySpecified = true;



         //   ToUpdate.nickname = tx_Email.Text;
            
            //ToUpdate.socialNetwork[0].idNew = tx_Twitter.Text;
            //ToUpdate.socialNetwork[1].idNew = tx_Facebook.Text;

            ToUpdate.maritalStatus = new Innovaction.CustomerDataWS.maritalStatusTo();
            ToUpdate.maritalStatus.id = dd_MatrialState.SelectedValue;

            // de aca para adelante necesitan del old id para poder actualizar u.u

            // redes sociales, twiter y facebok
            ToUpdate.socialNetwork = new Innovaction.CustomerDataWS.socialNetworkTo[2];
            var FacebookSN = new Innovaction.CustomerDataWS.socialNetworkTo();
            var TwitterSN = new Innovaction.CustomerDataWS.socialNetworkTo();
            ToUpdate.socialNetwork[0] = FacebookSN;
            ToUpdate.socialNetwork[1] = TwitterSN;

            FacebookSN.idNew = tx_Facebook.Text;
            if (String.IsNullOrEmpty(tx_Facebook.Text))
            {
                FacebookSN.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
            }
            else
            {
                FacebookSN.operationType = Innovaction.CustomerDataWS.operationType.UPDATE;
            }
            FacebookSN.operationTypeSpecified = true;
            FacebookSN.socialNetworkType = new Innovaction.CustomerDataWS.socialNetworkTypeTo();
            FacebookSN.socialNetworkType.type = "FB";
            FacebookSN.primary = true;
            FacebookSN.validated = false;
            FacebookSN.idOld = tx_FacebookOLD.Text;

            TwitterSN.idNew = tx_Twitter.Text;
            if (String.IsNullOrEmpty(tx_Twitter.Text))
            {
                TwitterSN.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
            }
            else
            {
                TwitterSN.operationType = Innovaction.CustomerDataWS.operationType.UPDATE;
            }
            
            TwitterSN.operationTypeSpecified = true;
            TwitterSN.socialNetworkType = new Innovaction.CustomerDataWS.socialNetworkTypeTo();
            TwitterSN.socialNetworkType.type = "TW";
            TwitterSN.validated = false;
            TwitterSN.primary = true;
            TwitterSN.idOld = tx_TwitterOLD.Text;

            // celular
            ToUpdate.phone = new Innovaction.CustomerDataWS.phoneTo[1];
            var Phone = new Innovaction.CustomerDataWS.phoneTo();
            ToUpdate.phone[0] = Phone;
            Phone.numberNew = tx_CellPhone.Text;
            Phone.phoneType = new Innovaction.CustomerDataWS.phoneTypeTo();
            Phone.phoneType.id = "MOBILE";
            if (String.IsNullOrEmpty(tx_CellPhone.Text))
            {
                Phone.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
            }
            else {
                Phone.operationType = Innovaction.CustomerDataWS.operationType.UPDATE;
            }
            Phone.operationTypeSpecified = true;
            Phone.primary = true;
            Phone.numberOld = tx_CellPhoneOLD.Text;


            #endregion

            #region direccion
            // aparentemente tenemos que borrar las direcciones
            ToUpdate.address = new Innovaction.CustomerDataWS.addressTo[2];
            // borro el viejo


            //int atmpIndex = 0;
            //foreach (ListItem addtype in dd_Address.Items)
            //{
            //    if (!addtype.Selected)
            //    {
            var AddressToDelete = new Innovaction.CustomerDataWS.addressTo();
            AddressToDelete.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
            AddressToDelete.operationTypeSpecified = true;
            AddressToDelete.addressType = new Innovaction.CustomerDataWS.addressTypeTo();
            AddressToDelete.addressType.id = tx_AddressOLD.Text;
            AddressToDelete.city = new Innovaction.CustomerDataWS.cityTo();
            AddressToDelete.city.id = tx_CityOLD.Text;
            AddressToDelete.city.state = new Innovaction.CustomerDataWS.stateTo();
            AddressToDelete.city.state.id = tx_StateOLD.Text;
            // portal id
            AddressToDelete.city.state.country = new Innovaction.CustomerDataWS.countryTo();
            AddressToDelete.city.state.country.id = "VE";
            ToUpdate.address[0] = AddressToDelete;

            //        atmpIndex++;
            //    }

            //}


            // finalmente insertamos la nuestra
            var Address = new Innovaction.CustomerDataWS.addressTo();
            Address.operationTypeSpecified = true;
            Address.primary = true;
            Address.operationType = Innovaction.CustomerDataWS.operationType.UPDATE;

            Address.addressType = new Innovaction.CustomerDataWS.addressTypeTo();
            Address.addressType.id = dd_Address.SelectedValue;

            var Ciudad = new Innovaction.CustomerDataWS.cityTo();
            Ciudad.id = dd_City.SelectedValue;
            var State = new Innovaction.CustomerDataWS.stateTo();
            State.id = dd_State.SelectedValue;
            var Country = new Innovaction.CustomerDataWS.countryTo();
            // portal id
            Country.id = Innovaction.WSManager.CountryID(PortalId);
            State.country = Country;
            Ciudad.state = State;
            Address.city = Ciudad;
            Address.municipality = tx_Municipio.Text;
            Address.buildingHome = tx_Building.Text;
            Address.floor = tx_floor.Text;
            Address.apartmentHome = tx_apartmentNumber.Text;
            Address.street = tx_Street.Text;
            Address.neighborhood = tx_Urban.Text;
            Address.referencePoint = tx_RefPoint.Text;
            Address.zipCode = tx_CP.Text;
            ToUpdate.address[1] = Address;


            #endregion

            #region otros datos
            ToUpdate.nationality = new Innovaction.CustomerDataWS.countryTo();
            ToUpdate.nationality.id = dd_Country.SelectedValue;


            ToUpdate.occupation = new Innovaction.CustomerDataWS.occupationTo();
            ToUpdate.occupation.id = dd_Jobs.SelectedValue;

            //ToUpdate.childQty = Kid_DynamicAddControl.ChildControlList.Count;
            //ToUpdate.childQtySpecified = true;
            var ControlList = GetChildControlList();
            ToUpdate.children = new Innovaction.CustomerDataWS.customerChildTo[ControlList.Count];
            int tmpChild = 0;
            foreach (ChildrenControl elControl in ControlList)
            {

                //leo la info de cada control
                ToUpdate.children[tmpChild] = new Innovaction.CustomerDataWS.customerChildTo();
                var Child = ToUpdate.children[tmpChild];
                Child.customerChildNoSpecified = true;


                if ((elControl.Visible == false) && (!elControl.IsNew))
                {
                    Child.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
                    Child.customerChildNo = elControl.SelectedKid;
                }
                else if (elControl.IsNew)
                {
                    Child.customerChildNo = tmpChild;
                    Child.operationType = Innovaction.CustomerDataWS.operationType.INSERT;
                }

                Child.operationTypeSpecified = true;



                Child.birthDateSpecified = true;
                ToUpdate.children[tmpChild].birthDate = elControl.SelectedDate;


                if (elControl.SelectedGender == "F")
                {
                    ToUpdate.children[tmpChild].gender = Innovaction.CustomerDataWS.gender.F;
                }
                else
                {
                    ToUpdate.children[tmpChild].gender = Innovaction.CustomerDataWS.gender.M;
                }

                ToUpdate.children[tmpChild].genderSpecified = true;


                //  elControl.SelectedGender;
                tmpChild++;

            }


            #endregion

            #region preferencias de envio
            // shiradit dijo que para esta seccion nbo usamos update, sino que insert y delete -.-


            ToUpdate.communicationAllowanceSpecified = true;
            //var AllowPreferences = Convert.ToBoolean(rb_info.SelectedValue);
            //ToUpdate.communicationAllowance = AllowPreferences;
            // preferences
            // topics!
         ToUpdate.communicationPreference = new CustomerDataWS.communicationPreferenceTo();
         if (chbx_Topics.Items.Count > 0)
         {
             ToUpdate.communicationPreference.topics = new Innovaction.CustomerDataWS.topicTo[chbx_Topics.Items.Count];

             int tmpIndex = 0;
             foreach (ListItem TheTopic in chbx_Topics.Items)
             {

                 var Topic = new Innovaction.CustomerDataWS.topicTo();
                 Topic.id = TheTopic.Value;
                 Topic.active = TheTopic.Selected;
                 Topic.operationTypeSpecified = true;
                 if (TheTopic.Selected)
                 {
                     Topic.operationType = Innovaction.CustomerDataWS.operationType.INSERT;
                 }
                 else
                 {
                     Topic.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
                 }
                 ToUpdate.communicationPreference.topics[tmpIndex] = Topic;
                 tmpIndex++;
             }
         }

            //fequency
            // tengo que recorrerlos 1 por 1 por que los tienen en un valor de array separado si uso insert.. tengo q borrar y insertar el nuevo
            ToUpdate.communicationPreference.frequencies = new Innovaction.CustomerDataWS.frequencyTo[rb_Frequency.Items.Count];
            int tmpFCount = 0;
            foreach (ListItem TheFrequency in rb_Frequency.Items)
            {
                var Frequency = new Innovaction.CustomerDataWS.frequencyTo();
                Frequency.id = TheFrequency.Value;
                if (TheFrequency.Selected)
                {
                    Frequency.operationType = Innovaction.CustomerDataWS.operationType.INSERT;
                }
                else
                {
                    Frequency.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
                }
                Frequency.operationTypeSpecified = true;
                Frequency.active = true;


                ToUpdate.communicationPreference.frequencies[tmpFCount] = Frequency;
                tmpFCount++;
            }

            // channel
            // mismo caso que la frequency..

            ToUpdate.communicationPreference.channels = new Innovaction.CustomerDataWS.channelTo[rb_Channel.Items.Count];
            int tmpCounter = 0;
            foreach (ListItem TheChannel in rb_Channel.Items)
            {

                var Channel = new Innovaction.CustomerDataWS.channelTo();
                Channel.id = TheChannel.Value;
                if (TheChannel.Selected)
                {
                    Channel.operationType = Innovaction.CustomerDataWS.operationType.INSERT;
                }
                else
                {
                    Channel.operationType = Innovaction.CustomerDataWS.operationType.DELETE;
                }
                Channel.operationTypeSpecified = true;
                Channel.active = true;


                ToUpdate.communicationPreference.channels[tmpCounter] = Channel;
                tmpCounter++;
            }

            #endregion




            // we update the customer
            var respuesta = Innovaction.WSManager.UpdateCustomer(ToUpdate, ThePortalID);
            if (respuesta.responseCode == CustomerDataWS.responseCode.SUCCESS)
            {
            
                DnnAlert("Gracias! Sus datos han sido guardados exitosamente.", 300);
            }
            else
            {
             
                DnnAlert("Error al actualizar los datos: " + respuesta.responseMessage, 300);
            }
            
            // response output debugg.


        }

        private void PopulateDropdowns()
        {

            // nacionalidad
            dd_Country.DataSource = Innovaction.WSManager.GetCountries();
            dd_Country.DataValueField = "VALUE";
            dd_Country.DataTextField = "TEXT";
            dd_Country.DataBind();

            // estados
            dd_State.DataSource = Innovaction.WSManager.GetState(ThePortalID);
            dd_State.DataValueField = "VALUE";
            dd_State.DataTextField = "TEXT";
            dd_State.DataBind();

            // cuidades
            dd_City.DataSource = Innovaction.WSManager.GetCity(dd_State.SelectedValue);
            dd_City.DataValueField = "VALUE";
            dd_City.DataTextField = "TEXT";
            dd_City.DataBind();

            // matrial state
            dd_MatrialState.DataSource = Innovaction.WSManager.GetMatrialStatus();
            dd_MatrialState.DataValueField = "VALUE";
            dd_MatrialState.DataTextField = "TEXT";
            dd_MatrialState.DataBind();

            // address types
            dd_Address.DataSource = Innovaction.WSManager.GetAddressTypes();
            dd_Address.DataValueField = "VALUE";
            dd_Address.DataTextField = "TEXT";
            dd_Address.DataBind();

            // jobs ocupaciones
            dd_Jobs.DataSource = Innovaction.WSManager.GetOccupations();
            dd_Jobs.DataValueField = "VALUE";
            dd_Jobs.DataTextField = "TEXT";
            dd_Jobs.DataBind();


            // topics

            chbx_Topics.DataSource = Innovaction.WSManager.GetCommunicationTopic();
            chbx_Topics.DataValueField = "VALUE";
            chbx_Topics.DataTextField = "TEXT";
            chbx_Topics.DataBind();

            // frequency

            rb_Frequency.DataSource = Innovaction.WSManager.GetCommunicationFrequencies();
            rb_Frequency.DataValueField = "VALUE";
            rb_Frequency.DataTextField = "TEXT";
            rb_Frequency.DataBind();

            // channel

            rb_Channel.DataSource = Innovaction.WSManager.GetCommunicationChannels();
            rb_Channel.DataValueField = "VALUE";
            rb_Channel.DataTextField = "TEXT";
            rb_Channel.DataBind();


        }

        private void CompleteCustomerInfo(Innovaction.CustomerDataWS.customerTo TheCustomer)
        {

            tx_CustomerID.Text = TheCustomer.id.ToString();
            tx_NationalID.Text = TheCustomer.nationalId;
            //Datos personales
            #region datos personales
            tx_FirstName.Text = TheCustomer.fistName;
            tx_SecondName.Text = TheCustomer.middleName;
            tx_FirstSurname.Text = TheCustomer.lastName;
            tx_SecondSurname.Text = TheCustomer.surname;
            dd_Gender.SelectedValue = TheCustomer.gender;
            // aca no se sabe de a donde viene el valor V o E
            // quedo fuera del sistema segun shiradit
            //dd_CI.SelectedValue = TheCustomer.nationalId.Substring(0, 1) ;
            tx_CI.Text = TheCustomer.nationalId;
            tx_Email.Text = TheCustomer.nickname;

            dd_BirthDate.SelectedValue = TheCustomer.birthDay;
            if (TheCustomer.maritalStatus != null) { dd_State.SelectedValue = TheCustomer.maritalStatus.id; }
            if (TheCustomer.phone != null)
            {
                foreach (var phone in TheCustomer.phone)
                {
                    if (phone.phoneType.id == "MOBILE")
                    {
                        tx_CellPhone.Text = phone.numberOld;
                        tx_CellPhoneOLD.Text = phone.numberOld;
                    }
                }
            }




            if (TheCustomer.socialNetwork != null)
            {
                foreach (var SN in TheCustomer.socialNetwork)
                {
                    if (SN.socialNetworkType.type == "TW")
                    {
                        tx_Twitter.Text = SN.idOld;
                        tx_TwitterOLD.Text = SN.idOld;
                    }
                    if (SN.socialNetworkType.type == "FB")
                    {
                        tx_Facebook.Text = SN.idOld;
                        tx_FacebookOLD.Text = SN.idOld;
                    }
                }
            }



            if (TheCustomer.maritalStatus != null) { dd_MatrialState.SelectedValue = TheCustomer.maritalStatus.id; }
            tx_Email.Text = TheCustomer.nickname;
            #endregion

            //Direccion
            #region direccion
            //no tengo informacion de direccion por ningun lado...
            if (TheCustomer.address != null)
            {
                var Address = TheCustomer.address[0];


                dd_Address.SelectedValue = Address.addressType.id;
                tx_AddressOLD.Text = Address.addressType.id;
                if (Address.city != null)
                {
                    if (Address.city.state != null)
                    {

                        dd_State.SelectedValue = Address.city.state.id;
                        tx_StateOLD.Text = Address.city.state.id;
                        // cuidades
                        dd_City.DataSource = Innovaction.WSManager.GetCity(Address.city.state.id);
                        dd_City.DataValueField = "VALUE";
                        dd_City.DataTextField = "TEXT";
                        dd_City.DataBind();
                    }
                    dd_City.SelectedValue = Address.city.id;
                    tx_CityOLD.Text = Address.city.id;
                }
                tx_Municipio.Text = Address.municipality;
                tx_Building.Text = Address.buildingHome;
                tx_floor.Text = Address.floor;
                tx_apartmentNumber.Text = Address.apartmentHome;
                tx_Street.Text = Address.street;
                tx_Urban.Text = Address.neighborhood;
                tx_RefPoint.Text = Address.referencePoint;
                tx_CP.Text = Address.zipCode;




            }
            #endregion

            //Otros datos
            #region otros datos
            if (TheCustomer.nationality != null) { dd_Country.SelectedValue = TheCustomer.nationality.id; }
            if (TheCustomer.occupation != null) { dd_Jobs.SelectedValue = TheCustomer.occupation.id; }
            //tx_KidsQTY.Text = TheCustomer.childQty.ToString();
            //if(TheCustomer.children[0] != null){
            //    tx_Kid1Date.Text = TheCustomer.children[0].birthDate.ToShortDateString();
            //    dd_Kid1Gender.SelectedValue = TheCustomer.children[0].gender.ToString();
            //}

            if (TheCustomer.children != null)
            {
                //int tmpChild = 1;
                foreach (var Child in TheCustomer.children)
                {

                    var TheChildControl = Kid_DynamicAddControl.Add(); //dyAdd();
                    TheChildControl.SelectedDate = Child.birthDate;
                    TheChildControl.SelectedGender = Child.gender.ToString();
                    TheChildControl.SelectedKid = Child.customerChildNo;
                    TheChildControl.IsNew = false;
                    //tmpChild++;
                }
            }

            #endregion

            //preferencias de envio
            #region preferencias de envio
            rb_info.SelectedValue = TheCustomer.communicationAllowance.ToString();

            foreach (ListItem item in chbx_Topics.Items)
            {
                try
                {
                    if ((TheCustomer.communicationPreference != null) && (TheCustomer.communicationPreference.topics != null))
                    {
                        foreach (var topic in TheCustomer.communicationPreference.topics)
                        {
                            if (topic.id == item.Value)
                            {
                                item.Selected = topic.active;
                            }
                        }
                    }
                }
                catch { }
            }

            if (TheCustomer.communicationPreference.frequencies != null) { rb_Frequency.SelectedValue = TheCustomer.communicationPreference.frequencies[0].id; }
            //if (TheCustomer.communicationPreference.channels != null) { rb_Channel.SelectedValue = TheCustomer.communicationPreference.channels[0].id; }

            foreach (ListItem item in rb_Channel.Items)
            {
                try
                {
                    if ((TheCustomer.communicationPreference != null) && (TheCustomer.communicationPreference.channels != null))
                    {
                        foreach (var chann in TheCustomer.communicationPreference.channels)
                        {
                            if (chann.id == item.Value)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                catch { }
            }

            #endregion

        }

        private List<ChildrenControl> GetChildControlList()
        {
            var ToReturn = new List<ChildrenControl>();

            foreach (ChildrenControl elControl in Kid_DynamicAddControl.ChildControlList)
            {

                //trabajo todos los controles menos los que se usaron para "testear"
                if (((elControl.Visible == true) && (elControl.IsNew)) || ((elControl.Visible == false) && (!elControl.IsNew)))
                {

                    ToReturn.Add(elControl);
                }


            }


            return ToReturn;

        }



        protected void dd_State_SelectedIndexChanged(object sender, EventArgs e)
        {

            dd_City.DataSource = Innovaction.WSManager.GetCity(dd_State.SelectedValue);
            dd_City.DataBind();

        }

        // este metodo estaria bueno mandarlo a alguna dll innovaction
        void DnnAlert(string Text, int Width)
        {
            string jquery = @"jQuery(function ($) {
        
            $.dnnAlert({ 
                text: '" + Text + @"',
                width: " + Width + @"
                        });
                                        });";

            DotNetNuke.UI.Utilities.ClientAPI.RegisterStartUpScript(
               Page, "", "<script type=\"text/javascript\">" + jquery + "</script>");

        }


    }
}