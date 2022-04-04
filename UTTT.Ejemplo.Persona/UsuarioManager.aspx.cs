using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuarioManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Usuario();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatUsuario> lista = dcGlobal.GetTable<CatUsuario>().ToList();
                    this.ddlStatus.DataTextField = "strValor";
                    this.ddlStatus.DataValueField = "id";

                    List<CatPerfil> listaa = dcGlobal.GetTable<CatPerfil>().ToList();
                    this.ddlPerfil.DataTextField = "strValor";
                    this.ddlPerfil.DataValueField = "id";

                    List<Empleado> listaaa = dcGlobal.GetTable<Empleado>().ToList();
                    this.ddlEmpleado.DataTextField = "strNombre";
                    this.ddlEmpleado.DataValueField = "id";

                    if (this.idPersona == 0)
                    {

                        CatUsuario catTemp = new CatUsuario();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlStatus.DataSource = lista;
                        this.ddlStatus.DataBind();


                        CatPerfil catTemp1 = new CatPerfil();
                        catTemp1.id = -1;
                        catTemp1.strValor = "Seleccionar";
                        listaa.Insert(0, catTemp1);
                        this.ddlPerfil.DataSource = listaa;
                        this.ddlPerfil.DataBind();


                        Empleado catTemp2 = new Empleado();
                        catTemp2.id = -1;
                        catTemp2.strNombre = "Seleccionar";
                        listaaa.Insert(0, catTemp2);
                        this.ddlEmpleado.DataSource = listaaa;
                        this.ddlEmpleado.DataBind();
                        this.lblAccion.Text = "Agregar";



                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombreUsuario;
                        this.txtPassword.Text = (this.baseEntity.strPassword);
                        this.txtreContra.Text = (this.baseEntity.strPassword);

                        this.ddlStatus.DataSource = lista;
                        this.ddlStatus.DataBind();
                        this.setItem(ref this.ddlStatus, baseEntity.CatUsuario.strValor);

                        this.ddlPerfil.DataSource = listaa;
                        this.ddlPerfil.DataBind();
                        this.setItem(ref this.ddlPerfil, baseEntity.CatPerfil.strValor);

                        this.ddlEmpleado.DataSource = listaaa;
                        this.ddlEmpleado.DataBind();
                        this.setItem(ref this.ddlEmpleado, baseEntity.Empleado.strNombre);

                    }
                    this.ddlStatus.SelectedIndexChanged += new EventHandler(ddlStatus_SelectedIndexChanged);
                    this.ddlStatus.AutoPostBack = true;

                    this.ddlPerfil.SelectedIndexChanged += new EventHandler(ddlPerfil_SelectedIndexChanged);
                    this.ddlPerfil.AutoPostBack = true;

                    this.ddlEmpleado.SelectedIndexChanged += new EventHandler(ddlEmpleado_SelectedIndexChanged);
                    this.ddlEmpleado.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario persona = new Linq.Data.Entity.Usuario();
                if (this.idPersona == 0)
                {
                    var comprobar = dcGlobal.GetTable<Usuario>().Where(a => a.strNombreUsuario == txtNombre.Text).FirstOrDefault();
                    var comprobare = dcGlobal.GetTable<Usuario>().Where(a => a.idEmpleado == int.Parse(ddlEmpleado.Text)).FirstOrDefault();
                    if (comprobar != null)
                    {


                        this.lblmensaje.Visible = true;
                        this.lblmensaje.Text = "El nombre de usuario ya esta en uso";

                    }
                    else if (comprobare != null)
                    {


                        this.lblmensaje.Visible = true;
                        this.lblmensaje.Text = "El empleado seleccionado ya cuenta con un usuario ";

                    }
                    else
                    {

                        persona.strNombreUsuario = this.txtNombre.Text.Trim();
                        persona.strPassword = this.txtPassword.Text.Trim();

                        persona.idStatus = int.Parse(this.ddlStatus.Text);
                        persona.idPerfil = int.Parse(this.ddlPerfil.Text);
                        persona.idEmpleado = int.Parse(this.ddlEmpleado.Text);



                        String mensaje = String.Empty;

                        if (!this.validacion(persona, ref mensaje))
                        {
                            this.lblmensaje.Text = mensaje;
                            this.lblmensaje.Visible = true;
                            return;
                        }
                        if (!this.validacionSQL(ref mensaje))
                        {
                            this.lblmensaje.Text = mensaje;
                            this.lblmensaje.Visible = true;
                            return;
                        }
                        if (!this.validacionHTML(ref mensaje))
                        {
                            this.lblmensaje.Text = mensaje;
                            this.lblmensaje.Visible = true;
                            return;
                        }


                        dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().InsertOnSubmit(persona);
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se agrego correctamente.");

                        this.Response.Redirect("~/UsuarioPrincipal.aspx", false);


                    }
                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(c => c.id == idPersona);
                    persona.strNombreUsuario = this.txtNombre.Text.Trim();
                    persona.strPassword = this.txtPassword.Text.Trim();

                    persona.idStatus = int.Parse(this.ddlStatus.Text);
                    persona.idPerfil = int.Parse(this.ddlPerfil.Text);
                    persona.idEmpleado = int.Parse(this.ddlEmpleado.Text);

                    String mensaje = String.Empty;

                    if (!this.validacion(persona, ref mensaje))
                    {
                        this.lblmensaje.Text = mensaje;
                        this.lblmensaje.Visible = true;
                        return;
                    }
                    if (!this.validacionSQL(ref mensaje))
                    {
                        this.lblmensaje.Text = mensaje;
                        this.lblmensaje.Visible = true;
                        return;
                    }
                    if (!this.validacionHTML(ref mensaje))
                    {
                        this.lblmensaje.Text = mensaje;
                        this.lblmensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/UsuarioPrincipal.aspx", false);

                }
            }
            catch (Exception _e)
            {
                this.Response.Redirect("~/ErrorPage.aspx", false);

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("19300623@uttt.edu.mx", "Error page", System.Text.Encoding.UTF8);//Correo de salida
                correo.To.Add("rozzi.gmd@gmail.com"); //Correo destino?
                correo.Subject = "Error"; //Asunto
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
                smtp.Port = 587; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential("19300623@uttt.edu.mx", "MGD1630M");//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl    
                correo.Body = "Ha ocurrido un error en el sitio Persona " + _e.GetType().ToString() + _e.Message + _e.StackTrace;
                smtp.Send(correo);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlStatus.Text);
                Expression<Func<CatUsuario, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatUsuario> lista = dcGlobal.GetTable<CatUsuario>().Where(predicateSexo).ToList();
                CatUsuario catTemp = new CatUsuario();
                this.ddlStatus.DataTextField = "strValor";
                this.ddlStatus.DataValueField = "id";
                this.ddlStatus.DataSource = lista;
                this.ddlStatus.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlPerfil.Text);
                Expression<Func<CatPerfil, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatPerfil> listaa = dcGlobal.GetTable<CatPerfil>().Where(predicateSexo).ToList();
                CatPerfil catTemp1 = new CatPerfil();
                this.ddlPerfil.DataTextField = "strValor";
                this.ddlPerfil.DataValueField = "id";
                this.ddlPerfil.DataSource = listaa;
                this.ddlPerfil.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlEmpleado.Text);
                Expression<Func<Empleado, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<Empleado> listaaa = dcGlobal.GetTable<Empleado>().Where(predicateSexo).ToList();
                Empleado catTemp2 = new Empleado();
                this.ddlEmpleado.DataTextField = "strNombre";
                this.ddlEmpleado.DataValueField = "id";
                this.ddlEmpleado.DataSource = listaaa;
                this.ddlEmpleado.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }


        public bool validacion(Linq.Data.Entity.Usuario _persona, ref string _mensaje)
        {
            if (_persona.idStatus == -1)
            {
                _mensaje = "Selecciona el status";
                return false;
            }

            if (_persona.strNombreUsuario.Equals(String.Empty))
            {
                _mensaje = "El nombre esta vacio";
                return false;
            }

            return true;
        }

        public bool validacionHTML(ref String _mensaje)
        {
            CtrlValidaInyeccion valida = new CtrlValidaInyeccion();
            string mensajefuncion = string.Empty;
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre de Usuario", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.htmlInyectionValida(this.txtreContra.Text.Trim(), ref mensajefuncion, "Confirmar Contraseña", ref this.txtreContra))
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
            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre de Usuario", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }


            if (valida.sqlInyectionValida(this.txtreContra.Text.Trim(), ref mensajefuncion, "Confirmar Contraseña", ref this.txtreContra))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            return true;
        }
        #endregion

    }
}