<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Login" %>

<!DOCTYPE html>
<%--<link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">--%>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

     <style type="text/css">
		#form1 {

   background-image: url("https://fondosmil.com/fondo/28066.jpg");
 font:bold
}

 
</style>
    
         <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
    <link href="Login.css" rel="stylesheet" />
    <link href="StyleSheet1.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio de sesion</title>
</head>
<body>
     
   <div class="login-form">
	   <br />
	   <br />
	<h1 class="text-center">Inicio de Sesión</h1>
    <form id="form1" runat="server">
		<div class="avatar">
			   <asp:Image ID="imgUser" ImageUrl="~/Images/user.png"  AlternateText="No Image available" runat="server" />

		</div>           
        <div class="form-group">
        	<asp:TextBox runat="server" id="txtUser" type="text" class="form-control input-lg" name="Nombre de usuario" placeholder="Nombre de usuario" required="required">	</asp:TextBox>
        </div>
        
		<div class="form-group">
            <asp:TextBox runat="server" id="txtPassword" type="password" class="form-control input-lg" name="Contraseña" placeholder="Contraseña" required="required"></asp:TextBox>
        </div>
		<br />

        <div class="form-group clearfix">
			
            <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Entrar" type="submit" class="btn btn btn-success" />
        </div>	
		
		
         <asp:Label Visible="false" CssClass="alert alert-danger" Text="La Contraseña/nombre de usuario son Incorrectos o tu Usuario no está Activo" ID="lblErrorMessage" runat="server"  
             Font-Size="Medium" ForeColor="Red"   Width="450px" align="center"></asp:Label>

    </form>
	<div class="hint-text">Olvidaste tu Contraseña <a href="#"  OnClick="window.open('Recuperarcontraseña.aspx','FP','width=800,height=800,top=300, left=500,fullscreen=no,resizable=0');"> Recuperar ahora </a>
</div>
	 
</div>
</body>
</html>

