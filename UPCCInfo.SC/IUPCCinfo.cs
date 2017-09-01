using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using UPCCInfo.DC;
namespace UPCCInfo.SC
{
    [ServiceContract]
    public interface IUPCCinfo
    {
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //   ResponseFormat = WebMessageFormat.Json,
        //   RequestFormat = WebMessageFormat.Json,
        //   UriTemplate = "/AutenticarP1")]
        ////DCAlumno AlumnoAutenticarPost(Aut sAut);
        //string AlumnoAutenticarPost1(string sparametro1);


        //AUTENTICAR
        [OperationContract]
        [WebInvoke(Method = "POST",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           UriTemplate = "/AutenticarP/")]
        DCAlumno AlumnoAutenticarPost(Aut sAut);
        
        //RESERVO CUBICULO
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/Reservar/?CodRecurso={sCodRecurso}&NomRecurso={sNomRecurso}&CodAlumno={sCodAlumno}&CanHoras={sCanHoras}&fecIni={sfecIni}&fecFin={sfecFin}&Token={sToken}")]
        DCReservaRecurso ReservarRecurso(string sCodRecurso, string sNomRecurso, string sCodAlumno, string sCanHoras, string sfecIni, string sfecFin, string sToken);
        
        //LISTADO DE RECURSOS DISPONIBLES
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/RecursosDisponible/?TipoRecurso={sTipoRecurso}&Local={sLocal}&FecIni={sFecIni}&CanHoras={sCanHoras}&FechaFin={sFechaFin}&CodAlumno={sCodAlumno}&Token={sToken}")]
        DCListaRecurso ListadoRecursos(string sTipoRecurso, string sLocal, string sFecIni, string sCanHoras, string sFechaFin, string sCodAlumno, string sToken);

        //RECURSO DISPONIBLE ALEATORIO
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/RndRD/?TipoRecurso={sTipoRecurso}&Local={sLocal}&FecIni={sFecIni}&CanHoras={sCanHoras}&FechaFin={sFechaFin}&CodAlumno={sCodAlumno}&Token={sToken}")]
        DCListaRecurso RandomRecursoDisp(string sTipoRecurso, string sLocal, string sFecIni, string sCanHoras, string sFechaFin, string sCodAlumno, string sToken);

        //LISTADO DE RECURSOS RESERVADOS
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/ReservaAlumno/?FecIni={sFecIni}&FechaFin={sFechaFin}&CodAlumno={sCodAlumno}&Token={sToken}")]
        DCListaReservaAlumno ReservasAlumno(string sFecIni, string sFechaFin, string sCodAlumno, string sToken);

        //Activar Cubiculo/PC
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/ActivarReserva/?CodRecurso={sCodRecurso}&CodAlumno={sCodAlumno}&Token={sToken}")]
        DCReservaRecurso ActivarReserva(string sCodRecurso,string sCodAlumno, string sToken);

        //Verifica Reserva Alumno
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/VerificaReserva/?CodReserva={sCodRecurso}&CodAlumno={sCodAlumno}&Token={sToken}")]
        DCReservaRecurso VerificaReserva(string sCodRecurso, string sCodAlumno, string sToken);
    }
}
