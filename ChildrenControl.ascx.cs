using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

namespace Innovaction.Modules.CustomerUpdate
{
    public partial class ChildrenControl : System.Web.UI.UserControl
    {

        //public DateTime SelectedDate
        //{
        //    get
        //    {
        //        return KidDropDownDatePicker.SelectedValue;
        //    }
        //    set {
        //        KidDropDownDatePicker.SelectedValue  = value;
        //    }
        //}


        //public string SelectedGender{
        //    get {
        //        return dd_KidGender.SelectedValue;
        //    }
        //    set {
        //        dd_KidGender.SelectedValue = value;
        //    }
        //}

        public string SelectedGender
        {
            get
            {
                return ParseGender(tx_KidGender.Text);
            }
            set
            {
                tx_KidGender.Text = ParseGender(value);
            }
        }

        public int SelectedKid
        {
            get
            {
                return Convert.ToInt32(tx_KidNO.Text);
            }
            set
            {

                tx_KidNO.Text = value.ToString();
            }
        }


        public bool IsNew {

            get {
                return chk_IsNew.Checked;
            }
            set {
                chk_IsNew.Checked = value;
            }

                           }


        public DateTime SelectedDate
        {
            get
            {
                DateTime ToReturn = new DateTime();
                try {ToReturn = DateTime.ParseExact(tx_KidDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                catch { }

                return ToReturn;
            }
            set
            {
                tx_KidDate.Text = value.ToShortDateString();

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private string ParseGender(string ToParse)
        {

            string ToReturn = ToParse;
            if (ToParse == "F")
            {
                ToReturn = "Femenino";
            }
            else if (ToParse == "M")
            {
                ToReturn = "Masculino";
            }
            else if (ToParse == "Masculino")
            {
                ToReturn = "M";
            }
            else if (ToParse == "Femenino")
            {
                ToReturn = "F";
            }

            return ToReturn;
        }

    }
}