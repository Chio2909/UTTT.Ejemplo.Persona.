using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Linq.Data.Entity
{
   public partial class Empleado
    {
        public override string ToString()
        {
            return this.strNombre;
        }
    }
}
