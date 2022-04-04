<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProveedorManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.ProveedorManager" %>
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

     <title>Proveedor Manager</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
</head>
 
    
   <body>
       <form id="form2" runat="server" class="well form-horizontal">
       <div class="container" style="font-family: 'Century Gothic'; Font-Bold="True"" >       
       <div id="form1" class="well"> 
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <legend><center><h2><b>Proveedor</b></h2></center></legend>          
       <div  style="font-family: 'Century Gothic'; " align="center">
       <asp:Label ID="lblAccion" runat="server" Text="Accion" CssClass="control-label " for="accion"  Font-Size="Large" Font-Bold="True"></asp:Label>
       </div>
       </div>

             <div id="form1" class="well">

     <fieldset>

         <%--Tipo--%>
          <div class="form-group"  style="font-family: 'Century Gothic'; "   >
        <asp:Label runat="server" Text="Tipo:" Class="control-label col-sm-4" for="sexo" Font-Bold="True"></asp:Label>
        <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
         <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:DropDownList ID="ddlTipo" runat="server" Class="form-control border border-secondary" onselectedindexchanged="ddlTipo_SelectedIndexChanged"   >
        </asp:DropDownList>
          
             </div>
            
             <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlTipo" ErrorMessage="Selecciona nacional o internacional" MaximumValue="3" MinimumValue="0" Type="Integer" EnableClientScript="False"></asp:RangeValidator>

</div>
              </div>

         <%--Clave --%>
          <div class="form-group"  style ="font-family: 'Century Gothic'; ">
        <asp:Label runat="server" Text="Clave:" CssClass="control-label col-sm-4 " for="txtClave" Font-Bold="True"></asp:Label> 
            <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-certificate"></i></span>
        <asp:TextBox Class="form-control border border-secondary "  ID="txtClave" 
            onkeypress="return validaNumeros(event);" pattern=".{1,4}" title="1 a 4 es la longitud maxima que se permite ingresar" runat="server" 
            ViewStateMode="Disabled" minlength="4" MaxLength="4"   ></asp:TextBox>
             </div>
      <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave" ErrorMessage="Clave es obligatorio" EnableClientScript="False"></asp:RequiredFieldValidator>
        
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

         <%--Encargado--%>
           <div class="form-group"  style="font-family: 'Century Gothic'; " >
          <asp:Label runat="server" Text="Nombre del encargado:" CssClass="control-label col-sm-4" for="Encargado" Font-Bold="True"></asp:Label>
                <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-font"></i></span>
          <asp:TextBox Class="form-control border border-secondary " ID="txtEncargado" minlength="3" MaxLength="50"
              onkeypress="return validaLetras(event);" runat="server" ViewStateMode="Disabled" ></asp:TextBox>
             </div>
         <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ErrorMessage="Encargado es obligatorio" ControlToValidate="txtEncargado" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>

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
