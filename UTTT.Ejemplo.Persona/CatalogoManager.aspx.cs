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
    public partial class CatalogoManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Catalogo baseEntity;
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
                    this.baseEntity = new Linq.Data.Entity.Catalogo();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Catalogo>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatCategoria> lista = dcGlobal.GetTable<CatCategoria>().ToList();
                    this.ddlCategoria.DataTextField = "strValor";
                    this.ddlCategoria.DataValueField = "id";

                    if (this.idPersona == 0)
                    {

                        CatCategoria catTemp = new CatCategoria();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlCategoria.DataSource = lista;
                        this.ddlCategoria.DataBind();
                        this.lblAccion.Text = "Agregar";



                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.Nombre;
                        this.txtCodigo.Text = this.baseEntity.Codigo;
                        this.txtDescripcion.Text = this.baseEntity.Descripcion;
                        this.txtMarca.Text = this.baseEntity.Marca;
                     
                        this.ddlCategoria.DataSource = lista;
                        this.ddlCategoria.DataBind();
                        this.setItem(ref this.ddlCategoria, baseEntity.CatCategoria.strValor);

                    }
                    this.ddlCategoria.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlCategoria.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/CatalogoPrincipal.aspx", false);
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
                UTTT.Ejemplo.Linq.Data.Entity.Catalogo persona = new Linq.Data.Entity.Catalogo();
                if (this.idPersona == 0)
                {
                    persona.Codigo = this.txtCodigo.Text.Trim();
                  
                    persona.Nombre = this.txtNombre.Text.Trim();
                    persona.Descripcion = this.txtDescripcion.Text.Trim();
                    persona.Marca = this.txtMarca.Text.Trim();
                    persona.idCategoria = int.Parse(this.ddlCategoria.Text);

                   
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


                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Catalogo>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");

                    this.Response.Redirect("~/CatalogoPrincipal.aspx", false);



                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Catalogo>().First(c => c.id == idPersona);
                    persona.Codigo = this.txtCodigo.Text.Trim();
                    persona.Nombre = this.txtNombre.Text.Trim();
                    persona.Descripcion = this.txtDescripcion.Text.Trim();
                    persona.Marca = this.txtMarca.Text.Trim();
                    
                    persona.idCategoria = int.Parse(this.ddlCategoria.Text);
                   
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
                    this.Response.Redirect("~/CatalogoPrincipal.aspx", false);

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
                this.Response.Redirect("~/CatalogoPrincipal.aspx", false);
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
                int idSexo = int.Parse(this.ddlCategoria.Text);
                Expression<Func<CatCategoria, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatCategoria> lista = dcGlobal.GetTable<CatCategoria>().Where(predicateSexo).ToList();
                CatCategoria catTemp = new CatCategoria();
                this.ddlCategoria.DataTextField = "strValor";
                this.ddlCategoria.DataValueField = "id";
                this.ddlCategoria.DataSource = lista;
                this.ddlCategoria.DataBind();
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

        public bool validacion(Linq.Data.Entity.Catalogo _persona, ref string _mensaje)
        {
            if (_persona.idCategoria == -1)
            {
                _mensaje = "Selecciona la categoria";
                return false;
            }

            int i = 0;
            if (int.TryParse(_persona.Codigo, out i) == false)
            {
                _mensaje = "El codigo no es un numero";
                return false;
            }

          

            if (_persona.Nombre.Equals(String.Empty))
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
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre de producto", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }


            if (valida.htmlInyectionValida(this.txtDescripcion.Text.Trim(), ref mensajefuncion, "Descripcion", ref this.txtDescripcion))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.htmlInyectionValida(this.txtCodigo.Text.Trim(), ref mensajefuncion, "Codigo", ref this.txtCodigo))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtMarca.Text.Trim(), ref mensajefuncion, "Marca", ref this.txtMarca))
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
            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajefuncion, "Nombre de producto", ref this.txtNombre))
            {
                _mensaje = mensajefuncion;
                return false;
            }


            if (valida.sqlInyectionValida(this.txtDescripcion.Text.Trim(), ref mensajefuncion, "Descripcion", ref this.txtDescripcion))
            {
                _mensaje = mensajefuncion;
                return false;
            }

            if (valida.sqlInyectionValida(this.txtCodigo.Text.Trim(), ref mensajefuncion, "Codigo", ref this.txtCodigo))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtMarca.Text.Trim(), ref mensajefuncion, "Marca", ref this.txtMarca))
            {
                _mensaje = mensajefuncion;
                return false;
            }
            return true;
        }
        #endregion

    }
}
