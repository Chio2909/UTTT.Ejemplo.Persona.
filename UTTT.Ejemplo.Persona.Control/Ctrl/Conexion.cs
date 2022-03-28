using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class Conexion
    {
        #region Variables
     
        #endregion

        #region Constructores
        public Conexion()
        {
        }       
        #endregion


        public SqlConnection sqlConnection()
        {
            try
            {
                SqlConnection conexion = new SqlConnection("workstation id=PersonaWeb.mssql.somee.com;packet size=4096;user id=mar1298_SQLLogin_1;pwd=rixqnfjqbi;data source=PersonaWeb.mssql.somee.com;persist security info=False;initial catalog=PersonaWeb");
                return conexion;
            }
            catch (Exception _e)
            { 
            
            }
            return null;
        }
    }
}
