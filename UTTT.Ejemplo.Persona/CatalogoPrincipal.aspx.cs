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
    public partial class CatalogoPrincipal : System.Web.UI.Page
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
                    List<CatCategoria> lista = dcTemp.GetTable<CatCategoria>().ToList();
                    CatCategoria catTemp = new CatCategoria();
                    catTemp.id = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlCategoria.DataTextField = "strValor";
                    this.ddlCategoria.DataValueField = "id";
                    this.ddlCategoria.DataSource = lista;
                    this.ddlCategoria.DataBind();

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
                this.session.Pantalla = "~/CatalogoManager.aspx";
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
                bool categoriaBool = false;
              

                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlCategoria.Text != "-1")
                {
                    categoriaBool = true;
                }
              
                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Catalogo, bool>>
                    predicate =
                    (c =>
                    ((categoriaBool) ? c.idCategoria == int.Parse(this.ddlCategoria.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.Nombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Catalogo> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Catalogo>().Where(predicate).ToList();
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
                this.session.Pantalla = "~/CatalogoManager.aspx";
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
                UTTT.Ejemplo.Linq.Data.Entity.Catalogo persona = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Catalogo>().First(
                    c => c.id == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Catalogo>().DeleteOnSubmit(persona);
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