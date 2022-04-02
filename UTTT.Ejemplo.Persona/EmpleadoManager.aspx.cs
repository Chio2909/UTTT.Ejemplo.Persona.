using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    public partial class EmpleadoManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Empleado baseEntity;
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
                    this.baseEntity = new Linq.Data.Entity.Empleado();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Empleado>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";

                    List<CatEstadoCivil> listaE = dcGlobal.GetTable<CatEstadoCivil>().ToList();
                    this.ddlEstadoCivil.DataTextField = "strValor";
                    this.ddlEstadoCivil.DataValueField = "id";

                    if (this.idPersona == 0)
                    {

                        CatSexo catTemp = new CatSexo();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();

                        CatEstadoCivil catTemp1 = new CatEstadoCivil();
                        catTemp1.id = -1;
                        catTemp1.strValor = "Seleccionar";
                        listaE.Insert(0, catTemp1);
                        this.ddlEstadoCivil.DataSource = listaE;
                        this.ddlEstadoCivil.DataBind();

                        this.lblAccion.Text = "Agregar";


                        DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.txtFechaNacimiento.Text = DateTime.Today.ToString("yyyy-MM-dd");
                        this.Calendar1.SelectedDate = time;
                        this.Calendar1.EndDate = time;

                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtRFC.Text = this.baseEntity.strRFC;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        //this.txtFechaNacimiento.Text = this.baseEntity.dteFechaNacimiento;

                        Calendar1.SelectedDate = this.baseEntity.dteFechaNacimiento.Value.Date;

                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);

                        this.ddlEstadoCivil.DataSource = listaE;
                        this.ddlEstadoCivil.DataBind();
                        this.setItem1(ref this.ddlEstadoCivil, baseEntity.CatEstadoCivil.strValor);

                    }
                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;

                    this.ddlEstadoCivil.SelectedIndexChanged += new EventHandler(ddlEstado_SelectedIndexChanged);
                    this.ddlEstadoCivil.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/EmpleadoPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (
                     txtClaveUnica.Text == "" &&
                    txtNombre.Text == "" &&
                    txtAPaterno.Text == "" &&
                    txtAMaterno.Text == "" &&
                    ddlSexo.Text == "-1" &&
                    ddlEstadoCivil.Text == "-1" &&
                    txtFechaNacimiento.Text == "" &&
                    txtRFC.Text == "")
                {
                    this.Response.Redirect("~/EmpleadoPrincipal.aspx", false);
                }

                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }
                //obtener fecha de nacimiento
                string date = Request.Form[this.txtFechaNacimiento.UniqueID];
                DateTime fechaNacimiento = Convert.ToDateTime(date);

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleado persona = new Linq.Data.Entity.Empleado();
                if (this.idPersona == 0)
                {
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strRFC = this.txtRFC.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    persona.idCatEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);

                    //asignar fecha

                    persona.dteFechaNacimiento = fechaNacimiento;

                    #region Validacion
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
                    #endregion

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");

                    this.Response.Redirect("~/EmpleadoPrincipal.aspx", false);



                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().First(c => c.id == idPersona);
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strRFC = this.txtRFC.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    persona.idCatEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);
                    persona.dteFechaNacimiento = fechaNacimiento;

                    #region Validaciones
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
                    #endregion

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/EmpleadoPrincipal.aspx", false);

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
                this.Response.Redirect("~/EmpleadoPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlEstadoCivil.Text);
                Expression<Func<CatEstadoCivil, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatEstadoCivil> listaE = dcGlobal.GetTable<CatEstadoCivil>().Where(predicateSexo).ToList();
                CatEstadoCivil catTemp1 = new CatEstadoCivil();
                this.ddlEstadoCivil.DataTextField = "strValor";
                this.ddlEstadoCivil.DataValueField = "id";
                this.ddlEstadoCivil.DataSource = listaE;
                this.ddlEstadoCivil.DataBind();
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
        public void setItem1(ref DropDownList _control, String _value)
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

        public bool validacion(Linq.Data.Entity.Empleado _persona, ref string _mensaje)
        {
            if (_persona.idCatSexo == -1)
            {
                _mensaje = "Selecciona Masculino o Femenino";
                return false;
            }

            int i = 0;
            if (int.TryParse(_persona.strClaveUnica, out i) == false)
            {
                _mensaje = "La clave unica no es un numero";
                return false;
            }

            if (int.Parse(_persona.strClaveUnica) < 100 && int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La clave unica esta fuera de rango";
                return false;
            }

            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "El nombre esta vacio";
                return false;
            }

            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "El nombre sale del rango establecido de caracteres";
                return false;
            }

            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "El apelido paterno esta vacio";
                return false;
            }

            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "El apellido paterno sale del rango establecido de caracteres";
                return false;
            }

            if (_persona.strAMaterno.Equals(String.Empty))
            {
                _mensaje = "El apelido materno esta vacio";
                return false;
            }

            if (_persona.strAMaterno.Length > 50)
            {
                _mensaje = "El apellido materno sale del rango establecido de caracteres";
                return false;
            }

            if (_persona.strRFC.Equals(String.Empty))
            {
                _mensaje = "El RFC esta vacio";
                return false;
            }

            if (_persona.strRFC.Length > 13)
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
            if (valida.htmlInyectionValida(this.txtClaveUnica.Text.Trim(), ref mensajefuncion, "Clave Unica", ref this.txtClaveUnica))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.htmlInyectionValida(this.txtFechaNacimiento.Text.Trim(), ref mensajefuncion, "Fecha de Nacimiento", ref this.txtFechaNacimiento))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.htmlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajefuncion, "A. Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajefuncion, "A. Materno", ref this.txtAMaterno))
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
            if (valida.sqlInyectionValida(this.txtClaveUnica.Text.Trim(), ref mensajefuncion, "Clave Unica", ref this.txtClaveUnica))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtFechaNacimiento.Text.Trim(), ref mensajefuncion, "Fecha de Nacimiento", ref this.txtFechaNacimiento))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.sqlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajefuncion, "A. Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajefuncion, "A. Materno", ref this.txtAMaterno))
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
