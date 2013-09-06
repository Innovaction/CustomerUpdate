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
    public partial class DatePicker : System.Web.UI.UserControl
    {

           


        protected void Page_Load(object sender, EventArgs e)
        {
            // this has to be doulbe checked, it works fine with DNN this way, gotta check if it works without another scriptmanager.
            //try
            //{
            //    this.Controls.Add(new ScriptManager());
            //}

            //catch { }








            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-VE");
            
            // esto rompe el botonsito de config y edit"!!!!!!!!  <--- si, lo encontre!
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-VE");






            //  if ((!IsPostBack)|| (ddlday.Items.Count <= 0) || (ddlMonth.Items.Count <= 0) || (ddlYear.Items.Count <= 0))
            if (!IsPostBack)
            {
                //Populate DropDownLists
                ddlMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
                {
                    MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                    MonthNumber = a
                });
                ddlMonth.DataBind();

                var ds = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
                //if(!ds.Contains(Convert.ToInt32(ddlYear.SelectedValue))){
                //    ds = ds.Concat(new[] { Convert.ToInt32(ddlYear.SelectedValue) });
                //}

                ddlYear.DataSource = ds;
                ddlYear.DataBind();
                ddlday.DataSource = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(ddlMonth.SelectedValue)));
                ddlday.DataBind();
            }

        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlday.DataSource = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(ddlMonth.SelectedValue)));
            ddlday.DataBind();

        }



        public DateTime SelectedValue
        {
            get
            {
                DateTime ToReturn;
                string input = string.Format("{0:00}", (ddlday.SelectedIndex + 1)) + "/" +
                                string.Format("{0:00}", (ddlMonth.SelectedIndex + 1)) + "/" +
                                ddlYear.SelectedValue;
                ToReturn = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return ToReturn;
            }

            set
            {
                ddlMonth.SelectedIndex = Convert.ToInt32(value.Month.ToString()) - 1;
                ddlday.SelectedIndex = Convert.ToInt32(value.Day.ToString()) - 1;
                //int tmpIndex = 0;
                // bug con los años 1001!! revisar esto para que no se genere ese error

                ddlYear.SelectedValue = value.Year.ToString();


            }
        }
        //public void SetDate(DateTime ToSet) {

        //    ddlMonth.SelectedIndex = Convert.ToInt32(ToSet.Month.ToString()) -1;
        //    ddlday.SelectedIndex = Convert.ToInt32(ToSet.Day.ToString()) - 1;
        //    //int tmpIndex = 0;
        //    ddlYear.SelectedValue = ToSet.Year.ToString();
        //    //foreach (ListItem Year in ddlYear.Items)
        //    //{
        //    //    if(Year.Text == ToSet.Year.ToString()){
        //    //        ddlYear.SelectedValue = tmpIndex;
        //    //    }
        //    //    tmpIndex++;
        //    //}
        //    //try
        //    //{
        //    //    ddlYear.SelectedValue = ToSet.Year.ToString();
        //    //}
        //    //catch { }

        //}
    }
}