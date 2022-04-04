using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Windows.Forms;
using System.ComponentModel;

namespace UTTT.Ejemplo.Persona
{
    public partial class ProveedorManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Proveedor baseEntity;
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
                    this.baseEntity = new Linq.Data.Entity.Proveedor();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Proveedor>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatTipo> lista = dcGlobal.GetTable<CatTipo>().ToList();
                    this.ddlTipo.DataTextField = "strValor";
                    this.ddlTipo.DataValueField = "id";

                    if (this.idPersona == 0)
                    {

                        CatTipo catTemp = new CatTipo();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlTipo.DataSource = lista;
                        this.ddlTipo.DataBind();
                        this.lblAccion.Text = "Agregar";


                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.Nombre;
                        this.txtRFC.Text = this.baseEntity.RFC;
                        this.txtEncargado.Text = this.baseEntity.Encargado;
                        this.txtClave.Text = this.baseEntity.Clave;

                        this.ddlTipo.DataSource = lista;
                        this.ddlTipo.DataBind();
                        this.setItem(ref this.ddlTipo, baseEntity.CatTipo.strValor);

                    }
                    this.ddlTipo.SelectedIndexChanged += new EventHandler(ddlTipo_SelectedIndexChanged);
                    this.ddlTipo.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/ProveedorPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (
                     txtClave.Text == "" &&
                    txtNombre.Text == "" &&
                    txtEncargado.Text == "" &&
                   
                    ddlTipo.Text == "-1" &&
                    
                    txtRFC.Text == "")
                {
                    this.Response.Redirect("~/ProveedorPrincipal.aspx", false);
                }

                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }
               
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Proveedor persona = new Linq.Data.Entity.Proveedor();
                if (this.idPersona == 0)
                {
                    persona.Clave = this.txtClave.Text.Trim();
                    persona.RFC = this.txtRFC.Text.Trim();
                    persona.Nombre = this.txtNombre.Text.Trim();
                    persona.Encargado = this.txtEncargado.Text.Trim();
                    persona.idTipo = int.Parse(this.ddlTipo.Text);

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

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Proveedor>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");

                    this.Response.Redirect("~/ProveedorPrincipal.aspx", false);



                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Proveedor>().First(c => c.id == idPersona);
                    persona.Clave = this.txtClave.Text.Trim();
                    persona.Nombre = this.txtNombre.Text.Trim();
                    persona.RFC = this.txtRFC.Text.Trim();
                    persona.Encargado = this.txtEncargado.Text.Trim();
                    persona.idTipo = int.Parse(this.ddlTipo.Text);
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
                    this.Response.Redirect("~/ProveedorPrincipal.aspx", false);

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
                this.Response.Redirect("~/ProveedorPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlTipo.Text);
                Expression<Func<CatTipo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatTipo> lista = dcGlobal.GetTable<CatTipo>().Where(predicateSexo).ToList();
                CatTipo catTemp = new CatTipo();
                this.ddlTipo.DataTextField = "strValor";
                this.ddlTipo.DataValueField = "id";
                this.ddlTipo.DataSource = lista;
                this.ddlTipo.DataBind();
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



        protected void txtClaveUnica_TextChanged(object sender, EventArgs e)
        {

        }

        public bool validacion(Linq.Data.Entity.Proveedor _persona, ref string _mensaje)
        {
            if (_persona.idTipo == -1)
            {
                _mensaje = "Selecciona la nacioanlidad";
                return false;
            }

            int i = 0;
            if (int.TryParse(_persona.Clave, out i) == false)
            {
                _mensaje = "La clave unica no es un numero";
                return false;
            }

            if (int.Parse(_persona.Clave) < 100 && int.Parse(_persona.Clave) > 999)
            {
                _mensaje = "La clave unica esta fuera de rango";
                return false;
            }

            if (_persona.Nombre.Equals(String.Empty))
            {
                _mensaje = "El nombre esta vacio";
                return false;
            }

            if (_persona.Nombre.Length > 50)
            {
                _mensaje = "El nombre sale del rango establecido de caracteres";
                return false;
            }

            if (_persona.RFC.Equals(String.Empty))
            {
                _mensaje = "El RFC esta vacio";
                return false;
            }

            if (_persona.RFC.Length > 13)
            {
                _mensaje = "El RFC sale del rango establecido de caracteres";
                return false;
            }

            return true;
        }
        public bool validacionHTML(ref String _mensaje)
        {
            CtrlValidaInyeccion valida = new CtrlValidaInyeccion();
            string mensajefuncion = string.Empty;
            if (valida.htmlInyectionValida(this.txtClave.Text.Trim(), ref mensajefuncion, "Clave Unica", ref this.txtClave))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }

           

            if (valida.htmlInyectionValida(this.txtEncargado.Text.Trim(), ref mensajefuncion, "Encargado", ref this.txtEncargado))
            {
                _mensaje = mensajefuncion;
                return false;
            }
           
            if (valida.htmlInyectionValida(this.txtRFC.Text.Trim(), ref mensajefuncion, "RFC", ref this.txtRFC))
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
            if (valida.sqlInyectionValida(this.txtClave.Text.Trim(), ref mensajefuncion, "Clave Unica", ref this.txtClave))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }
           
            if (valida.sqlInyectionValida(this.txtEncargado.Text.Trim(), ref mensajefuncion, "Encargado", ref this.txtEncargado))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.sqlInyectionValida(this.txtRFC.Text.Trim(), ref mensajefuncion, "RFC", ref this.txtRFC))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            return true;
        }

        #endregion

    }
}