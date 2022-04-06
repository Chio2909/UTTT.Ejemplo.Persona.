using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;

namespace UTTT.Ejemplo.Persona
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            

            using (SqlConnection sqlCon = new SqlConnection("Data Source=PersonaWeb.mssql.somee.com;" +
                "Initial Catalog=PersonaWeb;Persist Security Info=True;User ID=mar1298_SQLLogin_1;Password=rixqnfjqbi"))

            {

                
                string query = "SELECT COUNT(1) FROM Usuario WHERE strNombreUsuario=@strNombreUsuario AND strPassword=@strPassword AND idStatus=1";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);

                sqlcmd.Parameters.AddWithValue("@strNombreUsuario", txtUser.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@strPassword",Seguridad.Encriptar(txtPassword.Text.Trim()));
                

                sqlCon.Open();
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {
                    Session["strNombreUsuario"] = txtUser.Text.Trim();
                    Response.Redirect("MenuPrincipal.aspx");

                }
                else
                {
                    lblErrorMessage.Visible = true;
                }
            }
            String mensaje = String.Empty;
            DataContext dcGuardar = new DcGeneralDataContext();
            UTTT.Ejemplo.Linq.Data.Entity.Usuario us = new Linq.Data.Entity.Usuario();


            if (!this.validacionSQL(ref mensaje))
            {
                this.lblErrorMessage.Text = mensaje;
                this.lblErrorMessage.Visible = true;
                return;
            }
            if (!this.validacionHTML(ref mensaje))
            {
                this.lblErrorMessage.Text = mensaje;
                this.lblErrorMessage.Visible = true;
                return;
            }


        }
        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/RecuperarContraseña.aspx");
        }


        public bool validacionHTML(ref String _mensaje)
        {
            CtrlValidaInyeccion valida = new CtrlValidaInyeccion();
            string mensajefuncion = string.Empty;
            if (valida.htmlInyectionValida(this.txtUser.Text.Trim(), ref mensajefuncion, "Username", ref this.txtUser))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtPassword.Text.Trim(), ref mensajefuncion, "Password", ref this.txtPassword))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            return true;

        }
        public bool validacionSQL(ref String _mensaje)
        {
            CtrlValidaInyeccion valida = new CtrlValidaInyeccion();
            string mensajefuncion = string.Empty;
            if (valida.sqlInyectionValida(this.txtUser.Text.Trim(), ref mensajefuncion, "Username", ref this.txtUser))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtPassword.Text.Trim(), ref mensajefuncion, "Password", ref this.txtPassword))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            return true;
        }
    }
}




