<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.MenuPrincipal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menu Principal</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
   

     <style type="text/css"> 


#form1{
    background-image: url("https://fondosmil.com/fondo/28066.jpg");
     font:bold
}

</style>
   
 
    
</head>

<body>
        <form id="form2" runat="server" class="well form-horizontal"  onsubmit="return camposValid()">

     <div class="container" style="font-family: 'Century Gothic'; " >

          <div id="form1" class="well">
      <div class="container-fluid">     
          <div runat="server" class="navbar-form navbar-right">
          <div  style="font-family: 'Century Gothic'; font-weight: bold" align="right">     
         <%-- <asp:Button ID="btnMenu" runat="server" class="btn btn-warning" Text="Menú"  OnClick="btnMenu_Click"  />--%>
        <%-- <asp:Button ID="btnLogout" runat="server" class="btn btn-danger" Text="Cerrar Sesión"   OnClick="btnLogout_Click"  />--%>
          <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/log.png"  AlternateText="No Image available" 
              OnClick="btnLogout_Click" runat="server" Width="50px" Height="50px"  />
              </div>
          </div>

          </div> 
      <legend><center><h2><b>Menú Principal</b></h2></center></legend>
       
       <fieldset>
           <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label>
       <br />
           <br />
           <h2 align="center">¿A donde vamos?</h2>
           <br />
           <br />
           <br />
          <div class="row" align="center">
              <div class="col-sm-4">
                  <div class="card">
 <div class="card-body">
     <asp:Label  runat="server" Text="Catalogo" CssClass="control-label " for="accion"  Font-Size="Large" Font-Bold="True" ></asp:Label>
     <p class="card-text">Ver productos y agregar estos</p>
      <asp:ImageButton CssClass="botonk"   ID="ImageButton1" ImageUrl="~/Images/Catalog.png" class="button"  AlternateText="No Image available" OnClick="btnCatalog_Click" runat="server" Width="60px" Height="60px"  />
                  </div>
                  </div>
              </div>
               <div class="col-sm-4">
                  <div class="card">
 <div class="card-body">
     <asp:Label  runat="server" Text="Empleado" CssClass="control-label " for="accion"  Font-Size="Large" Font-Bold="True" ></asp:Label>
       
     <p class="card-text">Agregar y visualizar empleados dados de alta</p>
      <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/employee.PNG"  AlternateText="No Image available" OnClick="btnEmpleado_Click" runat="server" Width="70px" Height="70px" />
                  </div>
                  </div>
              </div>
              <div class="col-sm-4">
                  <div class="card">
 <div class="card-body">
     <asp:Label  runat="server" Text="Usuario" CssClass="control-label " for="accion"  Font-Size="Large" Font-Bold="True" ></asp:Label>
       
     <p class="card-text">Agregar usuarios relacionados a los empleados o administradores</p>
       <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/user.PNG"  AlternateText="No Image available" OnClick="btnUser_Click" runat="server" Width="60px" Height="60px" />
                  </div>
                  </div>
              </div>
          </div>
      
         


       </fieldset>
 </div>
         </div>
            </form>
</body>
</html>