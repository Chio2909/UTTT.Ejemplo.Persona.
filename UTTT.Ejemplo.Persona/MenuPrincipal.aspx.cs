using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
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

        protected void btnProveedor_Click(object sender, EventArgs e)
        {

           

                    Response.Redirect("ProveedorPrincipal.aspx");
                
               
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

    }
}