<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal"  debug="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Persona Principal</title>
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
              <asp:ScriptManager runat="server" />
      <div class="container" style="font-family: 'Century Gothic'; ">
          <div id="form1" class="well">
                   <legend><center><h2><b>Persona Principal </b></h2></center></legend><br>
                     </div>

        

                       <div id="form1" class="well">

                          <%-- Nombre--%>
         <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
         <asp:Label runat="server" Text="Nombre:" CssClass="control-label col-sm-2" for="nombre" Font-Bold="True"></asp:Label>
         <asp:TextBox CssClass="form-control border border-secondary " ID="txtNombre"   runat="server" ViewStateMode="Disabled" 
             Width="211px"   AutoPostBack="True" Height="34px"  OnTextChanged="buscarTextBox" ></asp:TextBox>
             <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100"
                 EnableCaching="false" minimumPrefixLength="2" ServiceMethod="onTxtNombreTextChange" TargetControlID="txtNombre"></ajaxToolkit:AutoCompleteExtender>
         </div>

      
                         <%--  sexo--%>

       <div class="form-group" style="font-family: 'Century Gothic'; " align="left">
       <asp:Label runat="server" Text="Sexo:" CssClass="control-label col-sm-2" for="sexo" Font-Bold="True"></asp:Label>   
       <asp:DropDownList ID="ddlSexo" CssClass="form-control form-select border border-secondary " runat="server" Width="209px" >  </asp:DropDownList> 
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
                    <asp:BoundField DataField="strClaveUnica" HeaderText="Clave Unica" ReadOnly="True" SortExpression="strClaveUnica" />
                    <asp:BoundField DataField="strCURP" HeaderText="Curp" ReadOnly="True" SortExpression="strCURP" />
                    <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True" SortExpression="strNombre" />
                    <asp:BoundField DataField="strAPaterno" HeaderText="Apellido Paterno" ReadOnly="True" SortExpression="strAPaterno" />
                    <asp:BoundField DataField="strAMaterno" HeaderText="Apellido Materno" ReadOnly="True" SortExpression="strAMaterno" />                 
                    <asp:BoundField DataField="dteFechaNacimiento" HeaderText="Fecha Nacimiento" ReadOnly="True" SortExpression="dteFechaNacimiento" />     
                    <asp:BoundField DataField="CatSexo" HeaderText="Sexo" SortExpression="CatSexo" />
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



        <asp:LinqDataSource ID="DataSourcePersona" runat="server" 
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" 
        onselecting="DataSourcePersona_Selecting" 
        Select="new (strNombre,strCURP, dteFechaNacimiento, strAPaterno, strAMaterno, CatSexo, strClaveUnica,id)" 
        TableName="Persona" EntityTypeName="">
        </asp:LinqDataSource>
   

        
               
  <%--      
         <script type="text/javascript">
             var nombre = document.getElementById("txtNombre").value;
             document.querySelector('#txtNombre').addEventListener('keyup', function () {
                 const btnTrick = document.querySelector('#btnTrick');
                 btnTrick.click();
             });

         </script>--%>
    </div>
              </div>
    </form>
    </body>
    </html>


















<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal"  debug="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>
    <script type="text/javascript" src="//netdna.bootstrapcdn.com/bootstrap/3.1.0/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.11.1.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" />   

    <div style="color: #000000; font-size: medium; font-family: Arial; font-weight: bold">    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Persona</div>
    <div>
    <p>
        Normbre:&nbsp;&nbsp;&nbsp;

        <asp:TextBox ID="txtNombre" runat="server" Width="174px" OnTextChanged="buscarTextBox" AutoPostBack="true"
           ></asp:TextBox>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100" EnableCaching="false" 
            MinimumPrefixLength="2" ServiceMethod="txtNombre_TextChanged" TargetControlID="txtNombre"></ajaxToolkit:AutoCompleteExtender>
       
        
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
            onclick="btnBuscar_Click" ViewStateMode="Disabled" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
            onclick="btnAgregar_Click" ViewStateMode="Disabled" />
    </p>
    </div>
    
    <div>
    
        Sexo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlSexo" runat="server" Height="22px" Width="177px">
        </asp:DropDownList>
    
    </div>
    <div style="font-weight: bold">
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Detalle</div>

        <div>
        
        </div>
       
        <div>
        
             <asp:GridView ID="dgvPersonas" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona" 
                Width="1067px" CellPadding="3" GridLines="Horizontal" 
                onrowcommand="dgvPersonas_RowCommand" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                ViewStateMode="Disabled">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="strClaveUnica" HeaderText="Clave Unica" 
                        ReadOnly="True" SortExpression="strClaveUnica" />
                     <asp:BoundField DataField="strCURP" HeaderText="Curp" 
                        ReadOnly="True" SortExpression="strCURP" />
                    <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True" 
                        SortExpression="strNombre" />
                    <asp:BoundField DataField="strAPaterno" HeaderText="APaterno" ReadOnly="True" 
                        SortExpression="strAPaterno" />
                    <asp:BoundField DataField="strAMaterno" HeaderText="AMaterno" ReadOnly="True" 
                        SortExpression="strAMaterno" />
                    <asp:BoundField DataField="CatSexo" HeaderText="Sexo" 
                        SortExpression="CatSexo" />
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
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
        
        </div>
    <asp:LinqDataSource ID="DataSourcePersona" runat="server" 
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" 
        onselecting="DataSourcePersona_Selecting" 
        Select="new (strNombre,strCURP, strAPaterno, strAMaterno, CatSexo, strClaveUnica,id)" 
        TableName="Persona" EntityTypeName="">
    </asp:LinqDataSource>
    </form>
</body>
</html>--%>