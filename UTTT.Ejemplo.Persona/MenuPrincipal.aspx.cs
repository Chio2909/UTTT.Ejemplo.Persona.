using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class MenuPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombreUsuario"] == null)
            {
                Response.Redirect("Login.aspx");

                lblUserDetails.Text = "strNombreUsuario : " + Session["strNombreUsuario"];
            }

        }

        protected void btnEmpleado_Click(object sender, EventArgs e)
        {

            using (SqlConnection sqlCon = new SqlConnection("Data Source=PersonaWeb.mssql.somee.com;" +
              "Initial Catalog=PersonaWeb;Persist Security Info=True;User ID=mar1298_SQLLogin_1;Password=rixqnfjqbi"))

            {
                string query = "SELECT COUNT(1) FROM Usuario WHERE idPerfil=1 ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);


                sqlCon.Open();
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {

                    Response.Redirect("EmpleadoPrincipal.aspx");
                }
                else
                {
                    this.showMessage("No tienes acceso al Menu Empleado");
                }

            }
        }

        protected void btnCatalog_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection("Data Source=PersonaWeb.mssql.somee.com;" +
                "Initial Catalog=PersonaWeb;Persist Security Info=True;User ID=mar1298_SQLLogin_1;Password=rixqnfjqbi"))

            {
                string query = "SELECT COUNT(1) FROM Usuario WHERE idPerfil=2 ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);


                sqlCon.Open();
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {

                    Response.Redirect("CatalogoPrincipal.aspx");
                }
                else
                {
                    this.showMessage("No tienes acceso al Menu Catalogo");
                }
            }
            }

        protected void btnUser_Click(object sender, EventArgs e)
        {

            using (SqlConnection sqlCon = new SqlConnection("Data Source=PersonaWeb.mssql.somee.com;" +
                 "Initial Catalog=PersonaWeb;Persist Security Info=True;User ID=mar1298_SQLLogin_1;Password=rixqnfjqbi"))

            {
                string query = "SELECT COUNT(1) FROM Usuario WHERE idPerfil=1 ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);


                sqlCon.Open();
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {

                    Response.Redirect("UsuarioPrincipal.aspx");
                }
                else
                {
                    this.showMessage("No tienes acceso al Menu Usuario");
                }
            }
            }

        protected void btnProveedor_Click(object sender, EventArgs e)
        {

            using (SqlConnection sqlCon = new SqlConnection("Data Source=PersonaWeb.mssql.somee.com;" +
              "Initial Catalog=PersonaWeb;Persist Security Info=True;User ID=mar1298_SQLLogin_1;Password=rixqnfjqbi"))

            {
                string query = "SELECT COUNT(1) FROM Usuario WHERE idPerfil=2 ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);


                sqlCon.Open();
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {

                    Response.Redirect("ProveedorPrincipal.aspx");
                }
                else
                {
                    this.showMessage("No tienes acceso al Menu Empleado");
                }

            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

    }
}