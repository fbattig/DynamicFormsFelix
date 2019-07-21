using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CrudInMVC.Models
{
    public class StudentDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);
        }






        // **************** ADD NEW STUDENT *********************
        public bool AddStudent(StudentModel smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@City", smodel.City);
            cmd.Parameters.AddWithValue("@Address", smodel.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        // **************** CREATE MODEL *********************
        public object CreateModel()
        {
            connection();
            SqlCommand cmd = new SqlCommand("CreateModel", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TableName", "StudentReg");
            cmd.Parameters.AddWithValue("@CLASSNAME", "Public Class");

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            var datarow = dt.Rows[0].ItemArray;

          //  var xx = dt.Rows[0].ItemArray;

          //  foreach (DataRow dr in dt.Rows[0].ItemArray)
         ///      datarow = dr.ItemArray.ToString();
                //studentlist.Add(
                //    new StudentModel
                //    {
                //        Id = Convert.ToInt32(dr["Id"]),
                //        Name = Convert.ToString(dr["Name"]),
                //        City = Convert.ToString(dr["City"]),
                //        Address = Convert.ToString(dr["Address"])
                //    });
        //   }
            return datarow;
        }



        // ********** VIEW STUDENT DETAILS ********************
        public List<StudentModel> GetStudent()
        {
            connection();
            List<StudentModel> studentlist = new List<StudentModel>();

            SqlCommand cmd = new SqlCommand("GetStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new StudentModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"])
                    });
            }
            return studentlist;
        }


        // ***************** UPDATE STUDENT DETAILS *********************
        public DataTable GetStudentsDT()
        {
            connection();

            SqlCommand cmd = new SqlCommand("GetStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

         
            return dt;
        }


        // ***************** CustomFields *********************
        public DataTable CustomFieldsDT()
        {
            connection();

            SqlCommand cmd = new SqlCommand("GetCustomFields", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();


            return dt;
        }


        //public void CreateDynamicControls()
        //{
        //    DataTable dt = new DataTable();
        //    dt = CustomFieldsDT();  //calling the function which describe the fieldname and fieldtype
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (Int32 i = 0; i < dt.Rows.Count; i++)
        //        {
        //            HtmlGenericControl tr = new HtmlGenericControl("tr");
        //            HtmlGenericControl td = new HtmlGenericControl("td");
        //            HtmlGenericControl td1 = new HtmlGenericControl("td");

        //            String FieldName = Convert.ToString(dt.Rows[i]["FieldName"]);
        //            String FieldType = Convert.ToString(dt.Rows[i]["FieldType"]);
        //            String FieldValue = Convert.ToString(dt.Rows[i]["FieldValue"]);

        //            Label lbcustomename = new Label();
        //            lbcustomename.ID = "lb" + FieldName;
        //            lbcustomename.Text = FieldName;
        //            td.Controls.Add(lbcustomename);
        //            tr.Controls.Add(td);

        //            if (FieldType.ToLower().Trim() == "textbox")
        //            {
        //                TextBox txtcustombox = new TextBox();
        //                txtcustombox.ID = "txt" + FieldName;
        //                txtcustombox.Text = FieldValue;
        //                td1.Controls.Add(txtcustombox);
        //            }
        //            else if (FieldType.ToLower().Trim() == "checkbox")
        //            {
        //                CheckBox chkbox = new CheckBox();
        //                chkbox.ID = "chk" + FieldName;
        //                if (FieldValue == "1")
        //                {
        //                    chkbox.Checked = true;
        //                }
        //                else
        //                {
        //                    chkbox.Checked = false;
        //                }
        //                td1.Controls.Add(chkbox);
        //            }
        //            else if (FieldType.ToLower().Trim() == "radiobutton")
        //            {
        //                RadioButtonList rbnlst = new RadioButtonList();
        //                rbnlst.ID = "rbnlst" + FieldName;
        //                rbnlst.Items.Add(new ListItem("Male", "1"));
        //                rbnlst.Items.Add(new ListItem("Female", "2"));
        //                if (FieldValue != String.Empty)
        //                {
        //                    rbnlst.SelectedValue = FieldValue;
        //                }
        //                else
        //                {
        //                    rbnlst.SelectedValue = "1";
        //                }
        //                rbnlst.RepeatDirection = RepeatDirection.Horizontal;
        //                td1.Controls.Add(rbnlst);
        //            }
        //            else if (FieldType.ToLower().Trim() == "dropdownlist")
        //            {
        //                DropDownList ddllst = new DropDownList();
        //                ddllst.ID = "ddl" + FieldName;
        //                ddllst.Items.Add(new ListItem("Select", "0"));

        //                if (FieldName.ToLower().Trim() == "state")
        //                {
        //                    ddllst.Items.Add(new ListItem("Alabama", "AL"));
        //                    ddllst.Items.Add(new ListItem("Alaska", "AK"));
        //                    ddllst.Items.Add(new ListItem("Arizona", "AZ"));
        //                    ddllst.Items.Add(new ListItem("California", "CA"));
        //                    ddllst.Items.Add(new ListItem("New York", "NY"));
        //                }
        //                else if (FieldName.ToLower().Trim() == "job")
        //                {
        //                    ddllst.Items.Add(new ListItem("Developer", "1"));
        //                    ddllst.Items.Add(new ListItem("Tester", "2"));
        //                }
        //                if (FieldValue != String.Empty)
        //                {
        //                    ddllst.SelectedValue = FieldValue;
        //                }
        //                else
        //                {
        //                    ddllst.SelectedValue = "0";
        //                }
        //                td1.Controls.Add(ddllst);
        //            }
        //            tr.Controls.Add(td1);

        //       //     PlaceHolder.Controls.Add(tr);
        //            //   placeholder.Controls.Add(tr);

        //            ////Add button  after last record  
        //            //if (i == dt.Rows.Count - 1)
        //            //{
        //            //    tr = new HtmlGenericControl("tr");
        //            //    td = new HtmlGenericControl("td");
        //            //    Button btnSubmit = new Button();
        //            //    btnSubmit.ID = "btnSubmit";
        //            //    btnSubmit.Click += btnsubmit_Click;
        //            //    btnSubmit.OnClientClick = "return ValidateForm();";
        //            //    btnSubmit.Text = "Submit";
        //            //    td.Controls.Add(btnSubmit);
        //            //    td.Attributes.Add("Colspan", "2");
        //            //    td.Attributes.Add("style", "text-align:center;");
        //            //    tr.Controls.Add(td);
        //            //    placeholder.Controls.Add(tr);
        //            //}
        //        }

        //    }

        //}




        // ***************** UPDATE STUDENT DETAILS *********************
        public bool UpdateDetails(StudentModel smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StdId", smodel.Id);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@City", smodel.City);
            cmd.Parameters.AddWithValue("@Address", smodel.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE STUDENT DETAILS *******************
        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StdId", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}