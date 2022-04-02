using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            Response.Redirect("EmpleadoPrincipal.aspx");
        }

        protected void btnCatalog_Click(object sender, EventArgs e)
        {
            Response.Redirect("CatalogoPrincipal.aspx");
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("UsuarioPrincipal.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

    }
}