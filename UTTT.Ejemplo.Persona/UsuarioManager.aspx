<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioManager" %>
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

     <title>Usuario Manager</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
</head>
 
    
   <body>
       <form id="form2" runat="server" class="well form-horizontal">
       <div class="container" style="font-family: 'Century Gothic'; Font-Bold="True"" >       
       <div id="form1" class="well"> 
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          
        <legend><center><h2><b>Usuario</b></h2></center></legend>          
       <div  style="font-family: 'Century Gothic'; " align="center">
       <asp:Label ID="lblAccion" runat="server" Text="Accion" CssClass="control-label " for="accion"  Font-Size="Large" Font-Bold="True"></asp:Label>
       </div>
       </div>

             <div id="form1" class="well">

     <fieldset>
         <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label>

         <%--Empleado--%>
          <div class="form-group"  style="font-family: 'Century Gothic'; "   >
        <asp:Label runat="server" Text="Empleado:" Class="control-label col-sm-4" for="empleado" Font-Bold="True"></asp:Label>
        <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
         <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:DropDownList ID="ddlEmpleado" runat="server" Class="form-control border border-secondary" onselectedindexchanged="ddlEmpleado_SelectedIndexChanged"   >
        </asp:DropDownList>
          
             </div>
             <asp:RequiredFieldValidator  ID="rfvEmpleado" runat="server" ControlToValidate="ddlEmpleado" EnableClientScript="False" ErrorMessage="Selecciona un valor*" InitialValue="-1"></asp:RequiredFieldValidator>       
            
</div>
              </div>


        <%-- Nombre--%>
          <div class="form-group" style="font-family: 'Century Gothic'; " >
           <asp:Label  runat="server" Text="Nombre de usuario:" CssClass="control-label col-sm-4" for="nombre" Font-Bold="True"></asp:Label>
                 <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-font"></i></span>
           <asp:TextBox Class="form-control border border-secondary  " ID="txtNombre" onkeypress="return validaLetras(event);" minlength="3" MaxLength="15" pattern=".{3,15}" title="3 a 15 es la longitud maxima que se permite ingresar" runat="server"  ViewStateMode="Disabled"   ></asp:TextBox>
              
             </div>
           <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Nombre es obligatorio" ControlToValidate="txtNombre" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
</div>
         </div>


         <%--Contraseñas --%>
         <div class="form-group"  style="font-family: 'Century Gothic'; " >
          <asp:Label runat="server" Text="Contraseña:" CssClass="control-label col-sm-4"  for="contra" Font-Bold="True"></asp:Label>
                <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
             <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
          <asp:TextBox Class="form-control border border-secondary " type="Password" ID="txtPassword" minlength="8" MaxLength="16" onkeypress="return validaLetras(event);" runat="server" ViewStateMode="Disabled"  ValidationGroup="edit"  ></asp:TextBox>            
            
             </div>

                    
          <asp:RequiredFieldValidator ID="rfvContra" runat="server" ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Contraseña Obligatoria*"></asp:RequiredFieldValidator>        
          </div>
        </div>

          <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
           <asp:Label ID="lblRecontra" runat="server" Text="Confirmar Contraseña:" CssClass="control-label col-sm-4" for="reContras" Font-Bold="True"></asp:Label>
                <div id="k2" class="col-md-4 inputGroupContainer">
         <div id="k1" class="input-group">
             <span id="k3" class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
           <asp:TextBox Class="form-control border border-secondary " type="Password" ID="txtreContra" onkeypress="return validaLetras(event);" runat="server" ViewStateMode="Disabled"   ></asp:TextBox>
             </div>
           <asp:RequiredFieldValidator ID="rfvreContra" runat="server" ControlToValidate="txtreContra" EnableClientScript="False" ErrorMessage="Campo Obligatorio*"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="txtPassword" ControlToValidate="txtreContra" 
            ErrorMessage="No coinciden las contraseñas"></asp:CompareValidator>
               </div>
         </div>


          <%--Perfil--%>
          <div class="form-group"  style="font-family: 'Century Gothic'; "   >
        <asp:Label runat="server" Text="Perfil:" Class="control-label col-sm-4" for="perfil" Font-Bold="True"></asp:Label>
        <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
         <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:DropDownList ID="ddlPerfil" runat="server" Class="form-control border border-secondary" onselectedindexchanged="ddlPerfil_SelectedIndexChanged"   >
        </asp:DropDownList>
          
             </div>
            
             <asp:RangeValidator ID="rvPerfil" runat="server" ControlToValidate="ddlPerfil" ErrorMessage="Selecciona el perfil" MaximumValue="3" MinimumValue="0" Type="Integer" EnableClientScript="False"></asp:RangeValidator>

</div>
              </div>

          <%--Status--%>
          <div class="form-group"  style="font-family: 'Century Gothic'; "   >
        <asp:Label runat="server" Text="Status:" Class="control-label col-sm-4" for="status" Font-Bold="True"></asp:Label>
        <div class="col-md-4 inputGroupContainer">
         <div class="input-group">
         <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:DropDownList ID="ddlStatus" runat="server" Class="form-control border border-secondary" onselectedindexchanged="ddlStatus_SelectedIndexChanged"   >
        </asp:DropDownList>
          
             </div>
            
             <asp:RangeValidator ID="rvStatus" runat="server" ControlToValidate="ddlStatus" ErrorMessage="Selecciona el status" MaximumValue="3" MinimumValue="0" Type="Integer" EnableClientScript="False"></asp:RangeValidator>

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
