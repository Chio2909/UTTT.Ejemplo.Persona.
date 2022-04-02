<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpleadoManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.EmpleadoManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
   
    <script type="text/javascript">

        //solo numeros
        function validaNumeros(evt) {
            var code = (evt.which) ? evt.which : evt.keycode;
            if (code == 8) {
                return true;
            } else if (code >= 48 && code <= 57) {
                return true;
            } else {
                return false;
            }
        }

        //solo letras
        function validaLetras(e) {
            key = e.keycode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
            especiales = "8-37-39-46"
            teclas_especiales = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !teclas_especiales) {
                return false;
            }
        }
        //solo numeros y letras

        function validaLetrasYNumeros(evt) {
            var code = (evt.which) ? evt.which : evt.keyCode;
            console.log(code)

            if (code == 8) {
                return true;
            } else if (code >= 48 && code <= 57) {
                return true;
            } else if (code >= 65 && code <= 90) {
                return true;
            }
            else {
                return false;
            }
        }

</script> 

     <style type="text/css">
         #form1 {
             background-color: #c6b1c9;
             font: bold
         }
     </style>

     <title>Empleado Manager</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
</head>
 
    
   <body>
       <form id="form2" runat="server" class="well form-horizontal">
       <div class="container" style="font-family: 'Century Gothic'; Font-Bold="True"" >       
       <div id="form1" class="well"> 
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <legend><center><h2><b>Empleado</b></h2></center></legend>          
       <div  style="font-family: 'Century Gothic'; " align="center">
       <asp:Label ID="lblAccion" runat="server" Text="Accion" CssClass="control-label " for="accion"  Font-Size="Large" Font-Bold="True"></asp:Label>
       </div>
       </div>

             <div id="form1" class="well">

     <fieldset>

         <%--Sexo--%>
          <div class="form-group"  style="font-family: 'Century Gothic'; "   >
        <asp:Label runat="server" Text="Sexo:" Class="control-label col-sm-4" for="sexo" Font-Bold="True"></asp:Label>
        <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
         <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:DropDownList ID="ddlSexo" runat="server" Class="form-control border border-secondary" onselectedindexchanged="ddlSexo_SelectedIndexChanged"   >
        </asp:DropDownList>
          
             </div>
            
             <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlSexo" ErrorMessage="Selecciona femenino o masculino" MaximumValue="3" MinimumValue="0" Type="Integer" EnableClientScript="False"></asp:RangeValidator>

</div>
              </div>

         <%--Clave unica--%>
          <div class="form-group"  style ="font-family: 'Century Gothic'; ">
        <asp:Label runat="server" Text="Clave Única:" CssClass="control-label col-sm-4 " for="txtClaveUnica" Font-Bold="True"></asp:Label> 
            <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-certificate"></i></span>
        <asp:TextBox Class="form-control border border-secondary "  ID="txtClaveUnica" 
            onkeypress="return validaNumeros(event);" pattern=".{1,3}" title="1 a 3 es la longitud maxima que se permite ingresar" runat="server" 
            ViewStateMode="Disabled" minlength="3" MaxLength="3"   ></asp:TextBox>
             </div>
      <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="Clave unica es obligatorio" EnableClientScript="False"></asp:RequiredFieldValidator>
        
                </div>
           </div>

        <%-- Nombre--%>
          <div class="form-group" style="font-family: 'Century Gothic'; " >
           <asp:Label  runat="server" Text="Nombre:" CssClass="control-label col-sm-4" for="nombre" Font-Bold="True"></asp:Label>
                 <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-font"></i></span>
           <asp:TextBox Class="form-control border border-secondary  " ID="txtNombre" minlength="3" MaxLength="50" onkeypress="return validaLetras(event);" runat="server"  ViewStateMode="Disabled"   ></asp:TextBox>
              
             </div>
           <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Nombre es obligatorio" ControlToValidate="txtNombre" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
</div>
         </div>

         <%--Apellido paterno--%>
           <div class="form-group"  style="font-family: 'Century Gothic'; " >
          <asp:Label runat="server" Text="Apellido Paterno:" CssClass="control-label col-sm-4" for="APaterno" Font-Bold="True"></asp:Label>
                <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-font"></i></span>
          <asp:TextBox Class="form-control border border-secondary " ID="txtAPaterno" minlength="3" MaxLength="50"
              onkeypress="return validaLetras(event);" runat="server" ViewStateMode="Disabled" ></asp:TextBox>
             </div>
         <asp:RequiredFieldValidator ID="rfvAPaterno" runat="server" ErrorMessage="Apellido Paterno es obligatorio" ControlToValidate="txtAPaterno" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>

                </div>
        </div>

         <%--Apellido Materno--%>
          <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
           <asp:Label runat="server" Text="Apellido Materno:" CssClass="control-label col-sm-4" for="AMaterno" Font-Bold="True"></asp:Label>
                <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-font"></i></span>
           <asp:TextBox Class="form-control border border-secondary " ID="txtAMaterno" minlength="3" MaxLength="50"
               onkeypress="return validaLetras(event);" runat="server" ViewStateMode="Disabled"  ></asp:TextBox>
             </div>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Apellido Materno es obligatorio" 
               ControlToValidate="txtAMaterno" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
        </div>
        </div> 

          <%--RFC--%>

         <div class="form-group"  style="font-family: 'Century Gothic'; " align="left">
             <asp:Label runat="server" Text="RFC:" CssClass="control-label col-sm-4" for="rfc" Font-Bold="True"></asp:Label>       
                    <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
             <asp:TextBox CssClass="form-control border border-secondary " ID="txtRFC" runat="server"  ></asp:TextBox>
             </div>
             <asp:RequiredFieldValidator ID="rfvRFC"  runat="server" ControlToValidate="txtRFC" EnableClientScript="False" ErrorMessage="RFC  Obligatorio*"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="revRFCi" runat="server" ControlToValidate="txtRFC" ErrorMessage="RFC inválido!" ValidationExpression="(([A-Z]|\s){1})(([A-Z]){3})([0-9]{6})((([A-Z]|[0-9]){3}))" EnableClientScript="False"></asp:RegularExpressionValidator>
             </div>
                 </div>

          <%--Estado civil--%>

                     <div class="form-group"  style="font-family: 'Century Gothic'; " align="left">
                   <asp:Label runat="server" Text="Estado Civil:" CssClass="control-label col-sm-4" for="estadoCivil" Font-Bold="True"></asp:Label>       
                   <div class="col-md-4 inputGroupContainer">
                    <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
             <asp:DropDownList ID="ddlEstadoCivil" runat="server" Class="form-control border border-secondary" >
        </asp:DropDownList>
             </div>
                      <asp:RangeValidator ID="rvEstado" runat="server" ControlToValidate="ddlEstadoCivil" ErrorMessage="Selecciona el estado civil" MaximumValue="5" MinimumValue="0" Type="Integer" EnableClientScript="False"></asp:RangeValidator>

                       </div>
                     </div>


         <%--Fecha Nacimiento--%>
         
        <div class="form-group"  style="font-family: 'Century Gothic'; " align="left">        
          <asp:Label runat="server" Text="Fecha de Nacimiento:" CssClass="control-label col-sm-4"  
              for="FechaNacimiento" Font-Bold="True"></asp:Label>
              <div class="col-md-4 inputGroupContainer">

         <div class="input-group date">
             
             <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>

          
          <asp:TextBox ID="txtFechaNacimiento"   runat="server" CssClass="form-control border border-secondary" ></asp:TextBox>
             
              </div>
               <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="Fecha de Nacimiento Obligatoria*" EnableClientScript="False"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="revFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="Fecha Inválida*" ValidationExpression="(((19|20)([2468][048]|[13579][26]|0[48])|2000)[/-]02[/-]29|((19|20)[0-9]{2}[/-](0[4678]|1[02])[/-](0[1-9]|[12][0-9]|30)|(19|20)[0-9]{2}[/-](0[1359]|11)[/-](0[1-9]|[12][0-9]|3[01])|(19|20)[0-9]{2}[/-]02[/-](0[1-9]|1[0-9]|2[0-8])))"></asp:RegularExpressionValidator>
             

          <ajaxToolkit:CalendarExtender   ID="Calendar1" runat="server" Format="yyyy-MM-dd" PopupPosition="BottomRight" 
              BehaviorID="Calendar1" PopupButtonID="imgPopup" TargetControlID="txtFechaNacimiento" ></ajaxToolkit:CalendarExtender>

<ajaxToolkit:TextBoxWatermarkExtender  
    ID="WatermarkExtender1"
    runat="server" 
   TargetControlID="txtFechaNacimiento" 
   WatermarkCssClass="watermarked" 
   
   WatermarkText="yyyy-mm-dd" />
                  
            </div>
</div>
          

        <%-- Label--%>
            <div class="form-group" style="font-family: 'Century Gothic';  ">
         <div  style="font-family: 'Century Gothic'; font-weight: bold" align="center">     
         <asp:Label Visible="false" CssClass="alert alert-danger" ID="lblmensaje" runat="server"  
             Font-Size="Medium" ForeColor="Red"     Width="500px"></asp:Label>
         </div>
        </div> 

        <%-- Botones--%>
          <div runat="server" class="navbar-form navbar-right">
          <div  style="font-family: 'Century Gothic'; font-weight: bold" align="right">     
         
          <asp:Button CssClass="btn btn-success" ID="btnAceptar" runat="server" Text="Aceptar"
              OnClick="btnAceptar_Click" ViewStateMode="Disabled"   Height="46px" Width="123px" >          </asp:Button>
                       
                        <asp:Button CssClass="btn btn-danger" ID="btnCancelar" runat="server"  CausesValidation="False"
                            Text="Cancelar" OnClick="btnCancelar_Click" ViewStateMode="Disabled"   Height="46px" Width="123px"/>
               <br />
           <br />
        
          </div>
        </div>
          </fieldset>
              </div>
      </div>
 
    </form>
</body>
</html>