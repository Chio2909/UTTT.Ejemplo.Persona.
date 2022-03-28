using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public static class CtrlMessage
    {
        public static void showMessage(this System.Web.UI.Page _page, String _message)
        {
            _page.ClientScript.RegisterStartupScript(_page.GetType(),
                   Guid.NewGuid().ToString(),
                   "alert( '" + _message + "');", true);

            //_page.ClientScript.RegisterClientScriptBlock(_page.GetType(), "ClientScript", "<script type='text/javascript'> $(function(){ $('#dlgResultado').dialog({ modal: true, resizable: false, autoOpen: true, draggable: false, open: function(type, data){$(this).parent().appendTo('form')} }); }); </script>");

        }

        public static void showMessageException(this System.Web.UI.Page _page, String _message)
        {
            String mensaje = "Error " + _message + "Contacto";
            _page.ClientScript.RegisterStartupScript(_page.GetType(),
                "ClientScript",
                "<SCRIPT>alert('" + mensaje + "');</SCRIPT>");


        }

    }
   
//        {
//            // Get the error details
//            HttpException lastErrorWrapper =
//                server.GetLastError() as HttpException;

    //            Exception lastError = lastErrorWrapper;
    //            if (lastErrorWrapper.InnerException != null)
    //                lastError = lastErrorWrapper.InnerException;

    //            string lastErrorTypeName = lastError.GetType().ToString();
    //            string lastErrorMessage = lastError.Message;
    //            string lastErrorStackTrace = lastError.StackTrace;

    //            const string ToAddress = "support@example.com";
    //            const string FromAddress = "support@example.com";
    //            const string Subject = "An Error Has Occurred!";

    //            // Create the MailMessage object
    //            MailMessage mm = new MailMessage(FromAddress, ToAddress);
    //            mm.Subject = Subject;
    //            mm.IsBodyHtml = true;
    //            mm.Priority = MailPriority.High;
    //            mm.Body = string.Format(@"
    //<html>
    //<body>
    //  <h1>An Error Has Occurred!</h1>
    //  <table cellpadding=""5"" cellspacing=""0"" border=""1"">
    //  <tr>
    //  <tdtext-align: right;font-weight: bold"">URL:</td>
    //  <td>{0}</td>
    //  </tr>
    //  <tr>
    //  <tdtext-align: right;font-weight: bold"">User:</td>
    //  <td>{1}</td>
    //  </tr>
    //  <tr>
    //  <tdtext-align: right;font-weight: bold"">Exception Type:</td>
    //  <td>{2}</td>
    //  </tr>
    //  <tr>
    //  <tdtext-align: right;font-weight: bold"">Message:</td>
    //  <td>{3}</td>
    //  </tr>
    //  <tr>
    //  <tdtext-align: right;font-weight: bold"">Stack Trace:</td>
    //  <td>{4}</td>
    //  </tr> 
    //  </table>
    //</body>
    //</html>",
    //                Request.RawUrl,
    //                User.Identity.Name,
    //                lastErrorTypeName,
    //                lastErrorMessage,
    //                lastErrorStackTrace.Replace(Environment.NewLine, "<br />"));

    //            // Attach the Yellow Screen of Death for this error   
    //            string YSODmarkup = lastErrorWrapper.GetHtmlErrorMessage();
    //            if (!string.IsNullOrEmpty(YSODmarkup))
    //            {
    //                Attachment YSOD =
    //                    Attachment.CreateAttachmentFromString(YSODmarkup, "YSOD.htm");
    //                mm.Attachments.Add(YSOD);
    //            }

    //            // Send the email
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Send(mm);
    //        }
}
