using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Innovaction.Modules.CustomerUpdate
{
    public partial class DynamicAddControl : System.Web.UI.UserControl
    {

        public List<ChildrenControl> ChildControlList = new List<ChildrenControl>();
        string ChildControlURL = "~/desktopmodules/CustomerUpdate/ChildrenControl.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                string fieldCount = tx_Qty.Text;
                int count = string.IsNullOrEmpty(fieldCount) ? 0 : Convert.ToInt32(fieldCount);

                for (int i = 0; i < count; i++)
                {
                    CreateTextBoxSet(i, DynamicPlaceHolder);
                }
            }
        }


        protected void Add_Click(object sender, EventArgs e)
        {
            string fieldCount = tx_Qty.Text;
            int count = string.IsNullOrEmpty(fieldCount) ? 0 : Convert.ToInt32(fieldCount);

            var MyControl = Add();


            MyControl.SelectedDate = DDD_picker.SelectedValue;
            MyControl.SelectedGender = dd_gender.SelectedValue;
            MyControl.SelectedKid = count + 1;


        }

        void CreateTextBoxSet(int count, PlaceHolder holder)
        {
            //PlaceHolder MyControl = new PlaceHolder();
            //MyControl.ID = "SubHolder_" + count;

            //for (int i = 0; i < 3; i++)
            //{
            //    TextBox txt1 = new TextBox();
            //    Literal lit1 = new Literal();
            //    txt1.ID = "Literal_" + i + "_" + count;
            //    lit1.ID = "TextBox_" + i + "_" + count;
            //    lit1.Text = "&nbsp;<br/>";
            //    MyControl.Controls.Add(txt1);
            //    MyControl.Controls.Add(lit1);
            //}
            ChildrenControl MyControl = (ChildrenControl)Page.LoadControl(ChildControlURL);


            MyControl.ID = "ChildControl_" + count;

            ChildControlList.Add(MyControl);
            holder.Controls.Add(MyControl);

        }

        public ChildrenControl Add()
        {
            string fieldCount = tx_Qty.Text;
            int count = string.IsNullOrEmpty(fieldCount) ? 0 : Convert.ToInt32(fieldCount);


            ChildrenControl MyControl = (ChildrenControl)Page.LoadControl(ChildControlURL);


            MyControl.ID = "ChildControl_" + count;

            count++;


            ChildControlList.Add(MyControl);
            DynamicPlaceHolder.Controls.Add(MyControl);

            tx_Qty.Text = count.ToString();

            return MyControl;
        }

        //protected void Add_Click(object sender, EventArgs e)
        //{
        //    //List<ChildrenControl> myList = new List<ChildrenControl>();
        //    ////// redibujo los anteriores
        //    //foreach (ChildrenControl prevControl in DynamicControls.Controls)
        //    //{
        //    //    myList.Add(prevControl);
        //    //}

        //    //foreach (var myCont in myList)
        //    //{
        //    //    DynamicControls.Controls.Add(myCont);
        //    //}

        //    //DynamicControls.Controls.Count;

        //    // agrego el nuevo control

        //    TextBox ToAdd = new TextBox();
        //    ToAdd.ID = "Test_" + DynamicControls.Controls.Count.ToString(); 
        //    DynamicControls.Controls.Add(ToAdd);
        //   // ControlList.Add(ToAdd);

        //    tx_Qty.Text = DynamicControls.Controls.Count.ToString();

        //}
    }
}