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
    public partial class EmpleadoPrincipal : System.Web.UI.Page
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
                    List<CatSexo> lista = dcTemp.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    catTemp.id = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();

                    List<CatEstadoCivil> listaE = dcTemp.GetTable<CatEstadoCivil>().ToList();
                    CatEstadoCivil catTemp1 = new CatEstadoCivil();
                    catTemp1.id = -1;
                    catTemp1.strValor = "Todos";
                    listaE.Insert(0, catTemp1);
                    this.ddlEstadoCivil.DataTextField = "strValor";
                    this.ddlEstadoCivil.DataValueField = "id";
                    this.ddlEstadoCivil.DataSource = listaE;
                    this.ddlEstadoCivil.DataBind();
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
                this.session.Pantalla = "~/EmpleadoManager.aspx";
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
                bool sexoBool = false;
                bool EstadoCivilBool = false;

                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlSexo.Text != "-1")
                {
                    sexoBool = true;
                }
                if (this.ddlEstadoCivil.Text != "-1")
                {
                    EstadoCivilBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Empleado, bool>>
                    predicate =
                    (c =>
                    ((sexoBool) ? c.idCatSexo == int.Parse(this.ddlSexo.Text) : true) &&
                    ((EstadoCivilBool) ? c.idCatEstadoCivil == int.Parse(this.ddlEstadoCivil.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Empleado> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().Where(predicate).ToList();
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
                    case "Direccion":
                        this.direccion(idPersona);
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
                this.session.Pantalla = "~/EmpleadoManager.aspx";
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
                UTTT.Ejemplo.Linq.Data.Entity.Empleado persona = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().First(
                    c => c.id == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().DeleteOnSubmit(persona);
                dcDelete.SubmitChanges();
                this.showMessage("Se elimino correctamente.");
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void direccion(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/DireccionManager.aspx";
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
