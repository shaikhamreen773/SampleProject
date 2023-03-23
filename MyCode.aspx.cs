using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleProject
{
    public partial class MyCode : System.Web.UI.Page
    {
        int empid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowData();
            }
        }

        protected void Insert_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            string connectionString = "Data Source=DESKTOP-GS69617;Initial Catalog=dummy;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);
                    command.ExecuteNonQuery();
                    connection.Close();
                    lblstatus.Text = "Insert Successful";
                    ShowData();
                }
                txtName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtAddress.Text = "";
            }
        }

        protected void gvEmployees_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            lblstatus.Text = "";
            gvEmployees.EditIndex = e.NewEditIndex;
            int index = gvEmployees.EditIndex;
            int editid = index + 1;
            empid = editid;
            GridViewRow row = gvEmployees.Rows[e.NewEditIndex];
            ShowData();
        }

        protected void gvEmployees_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            string id = Convert.ToString((int)e.Keys["Id"]);
            string name = (row.Cells[2].Controls[0] as TextBox).Text;
            string email = (row.Cells[3].Controls[0] as TextBox).Text;
            string phone = (row.Cells[4].Controls[0] as TextBox).Text;
            string address = (row.Cells[5].Controls[0] as TextBox).Text;
            string connectionString = "Data Source=DESKTOP-GS69617;Initial Catalog=dummy;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);
                    command.ExecuteNonQuery();
                    connection.Close();
                    lblstatus.Text = "Update Successful";
                    ShowData();

                }
            }
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblstatus.Text = "";
            gvEmployees.EditIndex = -1;
            ShowData();
        }
        protected void gvEmployees_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = (int)e.Keys["Id"];
            //ShowData();
            string connectionString = "Data Source=DESKTOP-GS69617;Initial Catalog=dummy;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DeleteEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    lblstatus.Text = "Delete Successful";
                    ShowData();

                }
            }
            gvEmployees.DataBind();

        }

        public void ShowData()
        {
            string connectionString = "Data Source=DESKTOP-GS69617;Initial Catalog=dummy;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("ReadEmployee", connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    gvEmployees.DataSource = ds.Tables[0];
                    gvEmployees.DataBind();
                }
                connection.Close();
            }
        }
    }
}