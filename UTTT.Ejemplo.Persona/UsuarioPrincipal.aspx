<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioPrincipal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Usuario Principal</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
    <script type="text/javascript" src="//netdna.bootstrapcdn.com/bootstrap/3.1.0/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <style type="text/css"> 
#form1 {

  background-color: #c6b1c9;
 font:bold
}


</style>
   
</head>
<body>     
          <form id="form2" class="well form-horizontal" runat="server">
              <%--<asp:ScriptManager runat="server" />--%>
      <div class="container" style="font-family: 'Century Gothic'; ">
          <div id="form1" class="well">
               <div class="container-fluid">     
          <div runat="server" class="navbar-form navbar-right">
          <div  style="font-family: 'Century Gothic'; font-weight: bold" align="right">     
           <asp:ImageButton ID="ImageButton2" ImageUrl="~/Images/menu.png"  AlternateText="No Image available" OnClick="btnMenu_Click" runat="server" Width="50px" Height="50px" />
           <asp:ImageButton ID="Image" ImageUrl="~/Images/ta.png"  AlternateText="No Image available" 
              runat="server" Width="50px" Height="50px"  />
              <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/log.png"  AlternateText="No Image available" 
              OnClick="btnLogout_Click" runat="server" Width="50px" Height="50px"  />
         <%-- <asp:Button ID="btnLogout" runat="server" class="btn btn-danger" Text="Cerrar Sesión"   OnClick="btnLogout_Click"  />--%>
          </div>
          </div>

          </div>
                   <legend><center><h2><b>Usuario Principal </b></h2></center></legend><br>
              <fieldset>
                  <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label>
          
        <asp:ScriptManager runat="server" />
                    </div>
          
          
            

                       <div id="form1" class="well">

                          <%-- Nombre de usuario--%>
         <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
         <asp:Label runat="server" Text="Nombre de Usuario:" CssClass="control-label col-sm-2" for="nombre" Font-Bold="True"></asp:Label>
         <asp:TextBox CssClass="form-control border border-secondary " ID="txtNombre"   runat="server" ViewStateMode="Disabled" 
             Width="211px"   AutoPostBack="True" Height="34px"  OnTextChanged="buscarTextBox" ></asp:TextBox>
             <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100"
                 EnableCaching="false" minimumPrefixLength="5" ServiceMethod="onTxtNombreTextChange" TargetControlID="txtNombre"></ajaxToolkit:AutoCompleteExtender>
         </div>

      
                         <%--   Estatus--%>

       <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
       <asp:Label runat="server" Text="Status:" CssClass="control-label col-sm-2" for="sexo" Font-Bold="True"></asp:Label>   
       <asp:DropDownList ID="ddlStatus" CssClass="form-control form-select border border-secondary " runat="server" Width="209px" >  </asp:DropDownList> 
</div>

                <%-- Botones--%>

                           <div runat="server" class="navbar-form navbar-left">
          <div  style="font-family: 'Century Gothic'; font-weight: bold" align="left">     
         
           <asp:Button ID="btnBuscar" CssClass="btn btn-primary" runat="server" Text="Buscar"
           onclick="btnBuscar_Click" ViewStateMode="Disabled" Height="42px" Width="95px" > </asp:Button>
                       
                         <asp:Button ID="btnAgregar" CssClass="btn btn-success" runat="server" Text="Agregar" 
           onclick="btnAgregar_Click" ViewStateMode="Disabled" Height="42px" Width="95px"/>
            
              <br />
               <br />
          </div>
        </div>
 <br />
                            <br />
                            <br />
                            <br />


 <%-- Detalle--%>

       <div  style="font-family: 'Century Gothic'; " align="center">
        <asp:Label ID="lblDetalle" runat="server" Text="Detalle" CssClass="control-label "
            for="detail"  Font-Size="X-Large"></asp:Label>
        </div>
        <br />
       
       <div  class="table-responsive">
        <asp:UpdatePanel ID="UpdatePanelll" runat="server" UpdateMode="Always">
                                <ContentTemplate>

                <asp:GridView style="font-family: 'Century Gothic'" CssClass="table" ID="dgvPersonas" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona" 
                Width="1100px" CellPadding="3" GridLines="Horizontal" 
                onrowcommand="dgvPersonas_RowCommand" BackColor="White" 
                  
                ViewStateMode="Disabled">
                <AlternatingRowStyle BackColor="#BBA9BB" />
                <Columns>
                    <asp:BoundField DataField="strNombreUsuario" HeaderText="Nombre de Usuario" ReadOnly="True" SortExpression="strNombreUsuario" />
                    <asp:BoundField DataField="strPassword" HeaderText="Contraseña" ReadOnly="True" SortExpression="strPassword" />
                <asp:BoundField DataField="CatUsuario" HeaderText="Satus" ReadOnly="True" SortExpression="CatUsuario" /> 
                    <asp:BoundField DataField="CatPerfil" HeaderText="Perfil" ReadOnly="True" SortExpression="CatPerfil" />  
                    <asp:BoundField DataField="Empleado" HeaderText="Empleado" SortExpression="Empleado" />

                  <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                    
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Eliminar" Visible="True">
                    <ItemTemplate>
                    <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>

                      <%--<asp:TemplateField HeaderText="Persona">
                      <ItemTemplate>
                      <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" Width="50px" />         
                     </asp:TemplateField>--%>
                </Columns>

                <FooterStyle BackColor="Gray" ForeColor="Gray" />
                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="Silver" ForeColor="Black" HorizontalAlign="Center" Font-Bold="True" />
                <RowStyle BackColor="#CCCCCC" ForeColor="Black" />
                <SelectedRowStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="Gray" />
                <SortedDescendingCellStyle BackColor="Gray" />
                <SortedDescendingHeaderStyle BackColor="Gray" />
            </asp:GridView>     
                                    </ContentTemplate>
              </asp:UpdatePanel>
        </div>



        <asp:LinqDataSource ID="DataSourcePersona" runat="server" 
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" 
        onselecting="DataSourcePersona_Selecting" 
        Select="new (strNombreUsuario,strPassword,CatUsuario, CatPerfil, Empleado ,id)" 
        TableName="Persona" EntityTypeName="">
        </asp:LinqDataSource>
   

        
               
        
        <%-- <script type="text/javascript">
             var nombre = document.getElementById("txtNombre").value;
             document.querySelector('#txtNombre').addEventListener('keyup', function () {
                 const btnTrick = document.querySelector('#btnTrick');
                 btnTrick.click();
             });

         </script>--%>
                           </fieldset>
    </div>
              
              </div>
    </form>
    </body>
    </html>