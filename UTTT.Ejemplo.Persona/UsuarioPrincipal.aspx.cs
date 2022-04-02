using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuarioPrincipal : System.Web.UI.Page
    {
        #region Variables
        public SqlConnection cn = new SqlConnection("Data Source=PersonaWeb.mssql.somee.com;" +
                "Initial Catalog=PersonaWeb;Persist Security Info=True;User " +
            "ID=mar1298_SQLLogin_1;Password=rixqnfjqbi");
        private SessionManager session = new SessionManager();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombreUsuario"] == null)
            {
                Response.Redirect("Login.aspx");

                lblUserDetails.Text = "strNombreUsuario : " + Session["strNombreUsuario"];
            }
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
                if (!this.IsPostBack)
                {
                    List<CatUsuario> lista = dcTemp.GetTable<CatUsuario>().ToList();
                    CatUsuario catTemp = new CatUsuario();
                    catTemp.id = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlStatus.DataTextField = "strValor";
                    this.ddlStatus.DataValueField = "id";
                    this.ddlStatus.DataSource = lista;
                    this.ddlStatus.DataBind();

                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/UsuarioManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }

        protected void DataSourcePersona_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool statusBool = false;


                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlStatus.Text != "-1")
                {
                    statusBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Usuario, bool>>
                    predicate =
                    (c =>
                    ((statusBool) ? c.idStatus == int.Parse(this.ddlStatus.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombreUsuario.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Usuario> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
                        break;
                    case "Persona":
                        this.persona(idPersona);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        #endregion 

        #region Metodos

        private void editar(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/UsuarioManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idPersona)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario persona = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(
                    c => c.id == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().DeleteOnSubmit(persona);
                dcDelete.SubmitChanges();
                this.showMessage("Se elimino correctamente.");
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void persona(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/PersonaManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #endregion

        protected void dgvPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvPersonas_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void buscarTextBox(object sender, EventArgs e)
        {
            this.DataSourcePersona.RaiseViewChanged();
        }

        protected void onTxtNombreTextChange(object sender, EventArgs e)
        {
            this.DataSourcePersona.RaiseViewChanged();
        }

        protected void btnMenu_Click(object sender, EventArgs e)
        {

            Response.Redirect("MenuPrincipal.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

    }
}