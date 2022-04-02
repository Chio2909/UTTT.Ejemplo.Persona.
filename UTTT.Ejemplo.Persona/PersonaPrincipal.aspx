<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal"  debug="false"%>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Usuario Principal</title>
      <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
       <style type="text/css"> 
#form1 {

  background-color: #F3D0E5;
 font:bold
}
</style>
    
</head>
<body>
        <form id="form2" runat="server" class="well form-horizontal"  onsubmit="return camposValid()">

   <div class="container" style="font-family: 'Century Gothic'; ">
          <div id="form1" class="well">
                <div class="container-fluid">     
       

          </div> 
                   <legend><center><h2><b>Usuario Principal </b></h2></center></legend><br>
                     </div>

       <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label>
             
          <div >
                <asp:ScriptManager runat="server" />   
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <input type="submit" name="btnTrick" value="" id="btnTrick" style="display: none;" />
            </ContentTemplate>
                </asp:UpdatePanel>


                 <div >

                </div>
            </div>

                  <div id="form1" class="well">

         <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
         <asp:Label runat="server" Text="Nombre de Usuario:" CssClass="control-label col-sm-2" for="nombre" Font-Bold="True"></asp:Label>
         <asp:TextBox CssClass="form-control border border-secondary " ID="txtNombre"   runat="server" ViewStateMode="Disabled" Width="211px"   AutoPostBack="True"  ></asp:TextBox>
         </div>

      


       <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
       <asp:Label runat="server" Text="Status:" CssClass="control-label col-sm-2" for="sexo" Font-Bold="True"></asp:Label>   
       <asp:DropDownList ID="ddlSexo" CssClass="form-control form-select border border-secondary " runat="server" Width="209px" >  </asp:DropDownList> 
</div>

       <%--<div class="form-group" style="font-family: 'Century Gothic'; " align="left">
           <asp:Label runat="server" Text="Estado Civil:" CssClass="control-label col-sm-2" for="estadoCivil" Font-Bold="True" ></asp:Label>
           <asp:DropDownList runat="server" ID="ddlEstadoCivil" CssClass="form-control form-select border border-secondary " Width="209px" ></asp:DropDownList>
           </div>--%>

          &nbsp;<br />
 <div  style="font-family: 'Century Gothic'; font-weight: bold">         
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         
       <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" Text="Buscar" onclick="btnBuscar_Click" ViewStateMode="Disabled" Height="42px" Width="95px" />
       &nbsp;&nbsp;&nbsp;
       <asp:Button ID="Button2" CssClass="btn btn-success" runat="server" Text="Agregar" onclick="btnAgregar_Click" ViewStateMode="Disabled" Height="42px" Width="95px" />     
           </div>
         <br />
          <br />
       <div  style="font-family: 'Century Gothic'; " align="center">
        <asp:Label ID="lblDetalle" runat="server" Text="Detalle" CssClass="control-label " for="detail"  Font-Size="X-Large"></asp:Label>
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
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="strNombreUsuario" HeaderText="Nombre Usuario" ReadOnly="True" SortExpression="strNombreUsuario" />
                 <%--   <asp:BoundField DataField="constrasenia" HeaderText="Contraseña" ReadOnly="True" SortExpression="constrasenia" />--%>
                    <asp:BoundField DataField="CatUsuario" HeaderText="Status" ReadOnly="True" SortExpression="CatUsuario" />                
                    <asp:BoundField DataField="Empleado" HeaderText="Empleado" ReadOnly="True" SortExpression="Empleado" />
                    <asp:BoundField DataField="CatPerfil" HeaderText="Perfil" ReadOnly="True" SortExpression="CatPerfil" /> 
                   
                   
                 
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

                      <asp:TemplateField HeaderText="Direccion">
                      <ItemTemplate>
                      <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" Width="50px" />         
                     </asp:TemplateField>
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
                      <br />
                      <br />
                      <br />
                      <br />

        <asp:LinqDataSource ID="DataSourcePersona" runat="server" 
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" 
        onselecting="DataSourcePersona_Selecting" 
        Select="new (strNombreUsuario, strPassword, CatUsuario, CatPerfil, Empleado,id)" 
        TableName="Persona" EntityTypeName="">
        </asp:LinqDataSource>
   

        
                


        
         <script type="text/javascript">
             var nombre = document.getElementById("txtNombre").value;
             document.querySelector('#txtNombre').addEventListener('keyup', function () {
                 const btnTrick = document.querySelector('#btnTrick');
                 btnTrick.click();
             });

         </script>
                      </div>
       <br />
    </div>
            </form>
</body>
</html>
