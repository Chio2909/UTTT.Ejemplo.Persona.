using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UTTT.Ejemplo.Persona
{
    public class CypherHelper
    {
        public static List<String> CifradoHash(String text)
        {
            UnicodeEncoding converter = new UnicodeEncoding();
            byte[] input = converter.GetBytes(text);
            List<String> output = new List<String>();
            // Tipos de cifrado a utilizar
            SHA1Managed sha1 = new SHA1Managed(); // No es recomendable.
            SHA256Managed sha256 = new SHA256Managed(); // Recomendado.
            MD5Cng md5 = new MD5Cng(); // No es recomendable.
            SHA512Managed sha512 = new SHA512Managed(); // Muy recomendado. 
                                                        // Añadimos los resultados a la lista de Strings
            output.Add(converter.GetString(sha1.ComputeHash(input)));
            output.Add(converter.GetString(md5.ComputeHash(input)));
            output.Add(converter.GetString(sha256.ComputeHash(input)));
            output.Add(converter.GetString(sha512.ComputeHash(input)));
            // Devolvemos la lista
            return output;
        }
        private static bool CompararBytes(byte[] ar1, byte[] ar2)
        {
            // Si no coincide la longitud de los arrays devolvemos false
            if (ar1.Length != ar2.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < ar1.Length; i++)
                {
                    // Mientras coincidan nunca entrará aqui.
                    if (!(ar1[i].Equals(ar2[i])))
                    {
                        // Si no coinciden, devolvemos false
                        return false;
                    }
                }
                // Si realiza todo el bucle devolvemos true.
                return true;
            }
        }
        public static List<String> CompararTexto(String text, List<String> textoscifrados)
        {
            List<String> resultados = new List<String>();
            List<String> textos = CypherHelper.CifradoHash(text);
            int contador = 0;
            // Por cada texto cifrado
            foreach (String txt in textos)
            {
                UnicodeEncoding converter = new UnicodeEncoding();
                byte[] arrayIntroducido = converter.GetBytes(txt);
                byte[] arrayAComparar = converter.GetBytes(textoscifrados[contador]);
                if (CypherHelper.CompararBytes(arrayIntroducido, arrayAComparar))
                {
                    // Si devuelve true, es correcto
                    resultados.Add("Es correcto");
                }
                else
                {
                    // Si no, los valores no coinciden
                    resultados.Add("No coincide");
                }
                contador++;
            }
            // Devolvemos la lista resultados
            return resultados;
        }
    }
}