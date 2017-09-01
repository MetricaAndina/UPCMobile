using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  UPCCInfo.Impl;
using UPCCInfo.DC;

namespace UPCCInfo.SV
{
    public partial class proxi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string usuario = Request.Params["usuario"] == null ? string.Empty : Request.Params["usuario"];
            string contrasena = Request.Params["contrasena"] == null ? string.Empty : Request.Params["contrasena"];
            string plataforma = Request.Params["plataforma"] == null ? string.Empty : Request.Params["plataforma"];
            /*
            Aut oAut=new Aut();
            oAut.Usuario = usuario;
            oAut.Contrasena = contrasena;
            //oAut.Plataforma = null;
            DCAlumno oDCAlumno = new DCAlumno(); 
            UPCCinfoImp oUPCCinfoImp = new UPCCinfoImp();
            oDCAlumno=oUPCCinfoImp.AlumnoAutenticarPost(oAut);
            */
            Response.AddHeader("Access-Control-Allow-Origin", "*");
            Response.AddHeader("access-control-allow-headers", "content-type");
            Response.ContentType = "text/plain";
            Response.Write("usuario: "+usuario+" contraseña: "+contrasena+" plataforma: "+plataforma);
            Response.End();
        }
    }
}