using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using UPCMobile.DC;

namespace UPCMobile.SC
{
    [ServiceContract]
    public interface IUPCMobile
    {
        #region Implementados
       

        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/TramiteRealizado/?CodAlumno={sCodAlumno}&Token={sToken}")]
        [OperationContract]
        TramiteRealizadoCollectionDC TramiteRealizadoListar(String sCodAlumno, String sToken);


        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Excepcion/?CodAlumno={sCodAlumno}&Token={sToken}&Mensaje={sMensaje}&Stactrace={sStacktrace}&Fecha={sFecha}&Hora={sHora}&Plataforma={cPlataforma}")]
        ExcepcionDispositivoDC ExcepcionDispositivoRegistrar(String sCodAlumno, String sToken, String sMensaje, String sStacktrace, String sFecha, String sHora, Char cPlataforma);
        
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Logoff/?CodAlumno={sCodAlumno}&Token={sToken}")]
        AlumnoDC AlumnoLogoff(String sCodAlumno, String sToken);
        #endregion

        #region CSC-00263690-00

        //***************************************************************************************************
        //Ticket        :   CSC-00263690-00
        //Responsable   :   Felix Miranda
        //Fecha         :   01/09/2017
        //Funcionalidad :   Web Service UPC Mobile > Excluir Páginas del compilado.
        //--***************************************************************************************************


        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/Horario/?CodAlumno={sCodAlumno}&Token={sToken}")]
        //[OperationContract]
        //HorarioDiaCollectionDC HorarioDiaListar(String sCodAlumno, String sToken);

        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/Inasistencia/?CodAlumno={sCodAlumno}&Token={sToken}")]
        //[OperationContract]
        //InasistenciaCollectionDC InasistenciaListar(String sCodAlumno, String sToken);

        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/CursoAlumno/?CodAlumno={sCodAlumno}&Token={sToken}")]
        //[OperationContract]
        //CursoAlumnoCollectionDC CursoAlumnoListar(String sCodAlumno, String sToken);


        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/Nota/?CodAlumno={sCodAlumno}&CodCurso={sCodCurso}&Token={sToken}")]
        //[OperationContract]
        //NotaCollectionDC NotaListar(String sCodAlumno, String sCodCurso, String sToken);

        /* Web Service que devuelve el listado de cursos que dicta el profesor en el ciclo actual */

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/ListadoCursosProfesor/?Codigo={sCodigo}&Token={sToken}")]
        //ListadoCursosDC ListadoCursosProfesor(String sCodigo, String sToken);

        ///* Web Service que devuelve el listado de alumnos matriculados en un curso determinado del profesor */
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/ListadoAlumnosProfesor/?Codigo={sCodigo}&Token={sToken}&Modalidad={sModal}&Periodo={sPeriodo}&Curso={sCurso}&Seccion={sSeccion}&Grupo={sGrupo}")]
        //ListadoAlumnosCursoDC ListadoAlumnosProfesor(String sCodigo, String sToken, String sModal, String sPeriodo, String sCurso, String sSeccion, String sGrupo);

        ///* Web Service que devuelve el horario actual del profesor */
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/HorarioProfesor/?Codigo={sCodigo}&Token={sToken}")]
        //HorarioProfesorDC HorarioProfesor(String sCodigo, String sToken);

        ///* Interfaz que permite que un profesor pueda consultar las notas de un alumno */
        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/NotaProfesor/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&CodCurso={sCodCurso}&Token={sToken}")]
        //[OperationContract]
        //NotaCollectionDC NotaListarProfesor(String sCodigo, String sCodAlumno, String sCodCurso, String sToken);

        ///* Interfaz que permite que un profesor pueda consultar las inasistencias de un alumno */
        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           UriTemplate = "/InasistenciaProfesor/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&Token={sToken}")]
        //[OperationContract]
        //InasistenciaCollectionDC InasistenciaListarProfesor(String sCodigo, String sCodAlumno, String sToken);

        #endregion

        #region SS-2013-072

        /* Web Service que devuelve listado de compañeros de clase de un alumno en un determinado curso */
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Companeros/?CodAlumno={sCodAlumno}&CodCurso={sCodCurso}&Token={sToken}")]
        AlumnoCollectionDC CompanerosClase(String sCodAlumno, String sCodCurso, String sToken);

        /* Web Service que devuelve listado de espacios deportivos y sus actividades, agrupados por sede */
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/PoblarED/?CodAlumno={sCodAlumno}&Token={sToken}")]
        EspacioDeportivoCollectionDC PoblarEspaciosDeportivos(String sCodAlumno, String sToken);

        /* Web Service que devuelve disponibilidad de un espacio deportivo especificado como parametro */
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/DisponibilidadED/?CodSede={sCodSede}&CodED={sCodED}&NumHoras={sNumHoras}&CodAlumno={sCodAlumno}&FechaIni={sFechaIni}&FechaFin={sFechaFin}&Token={sToken}")]
        DiasLibresEDCollectionDC DisponibilidadEspacioDeportivo(String sCodSede, String sCodED, String sNumHoras, String sCodAlumno, String sFechaIni, String sFechaFin, String sToken);

        /* Web Service que permite reservar un espacio deportivo para una actividad especifica */
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/ReservarED/?CodSede={sCodSede}&CodED={sCodED}&CodActiv={sCodActiv}&NumHoras={sNumHoras}&CodAlumno={sCodAlumno}&Fecha={sFecha}&HoraIni={sHoraIni}&HoraFin={sHoraFin}&Detalles={sDetalles}&Token={sToken}")]
        ReservaEspacioDepDC ReservarEspacioDeportivo(String sCodSede, String sCodED, String sCodActiv, String sNumHoras, String sCodAlumno, String sFecha, String sHoraIni, String sHoraFin, String sDetalles, String sToken);

        /* Web Service que permite devolver los hijos asociados de un padre de familia */
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/ListadoHijos/?Codigo={sCodigo}&Token={sToken}")]
        ListadoHijosDC ListadoHijos(String sCodigo, String sToken);

        /* Web service que provee una nueva version del login (Metodo GET) para habilitar permisos a padres de familia y docentes */
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Autenticar2/?Codigo={sCodigo}&Contrasena={sContrasena}&Plataforma={cPlataforma}")]
        UsuarioDC UsuarioAutenticar(String sCodigo, String sContrasena, Char cPlataforma);

        /* Web service que provee una nueva version del login (Metodo POST) para habilitar permisos a padres de familia y docentes */
        [WebInvoke(Method = "POST",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json,
                   UriTemplate = "/AutenticarP2")]
        UsuarioDC UsuarioAutenticarPost(Aut sAut);
        
      

        /* Interfaz que permite que un padre de familia pueda ver el horario de clase de su hijo */
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/HorarioPadre/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&Token={sToken}")]
        [OperationContract]
        HorarioDiaCollectionDC HorarioDiaListarPadre(String sCodigo, String sCodAlumno, String sToken);

        /* Interfaz que permite que un padre de familia pueda consultar los cursos que lleva su hijo */
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/CursoAlumnoPadre/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&Token={sToken}")]
        [OperationContract]
        CursoAlumnoCollectionDC CursoAlumnoListarPadre(String sCodigo, String sCodAlumno, String sToken);

        /* Interfaz que permite que un padre de familia pueda consultar las notas de su hijo */
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/NotaPadre/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&CodCurso={sCodCurso}&Token={sToken}")]
        [OperationContract] 
        NotaCollectionDC NotaListarPadre(String sCodigo, String sCodAlumno, String sCodCurso, String sToken);
        
        /* Interfaz que permite que un padre de familia pueda consultar las inasistencias de su hijo */
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/InasistenciaPadre/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&Token={sToken}")]
        [OperationContract]
        InasistenciaCollectionDC InasistenciaListarPadre(String sCodigo, String sCodAlumno, String sToken);

        /* Interfaz que permite que un padre de familia pueda consultar los pagos pendientes de su hijo */
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/PagoPendientePadre/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&Token={sToken}")]
        PagoPendienteCollectionDC PagoPendienteListarPadre(String sCodigo, String sCodAlumno, String sToken);

        /* Interfaz que permite que un padre de familia pueda consultar los tramites de su hijo */
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/TramiteRealizadoPadre/?Codigo={sCodigo}&CodAlumno={sCodAlumno}&Token={sToken}")]
        [OperationContract]
        TramiteRealizadoCollectionDC TramiteRealizadoListarPadre(String sCodigo, String sCodAlumno, String sToken);

        /* Web service que devuelve listado de sedes donde esta estudiando el alumno */
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/SedesAlumno/?CodAlumno={sCodAlumno}&Token={sToken}")]
        [OperationContract]
        ListadoSedesAlumnoDC ListadoSedesAlumno(String sCodAlumno, String sToken);

        #endregion

        #region pendientes
        // PENDIENTE PARA DESPUES
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Autenticar/?CodAlumno={sCodAlumno}&Contrasena={sContrasena}&Plataforma={cPlataforma}")]
        [OperationContract]
        AlumnoDC AlumnoAutenticar(String sCodAlumno, String sContrasena, Char cPlataforma);

        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/PagoPendiente/?CodAlumno={sCodAlumno}&Token={sToken}")]
        PagoPendienteCollectionDC PagoPendienteListar(String sCodAlumno, String sToken);
        
        /*METODO DE AUTENTICACION EN PRODUCCION */
        [WebInvoke(Method = "POST",
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json,
                   //BodyStyle = WebMessageBodyStyle.Wrapped,
                   UriTemplate = "/AutenticarP")]
        UsuarioDC AlumnoAutenticarPost(Aut sAut);
        /*METODO DE AUTENTICACION EN PRODUCCION */

        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/GetAut")]
        Aut getAut();
        #endregion

        #region UPCInfo
        //nuevos servicios
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
                  UriTemplate = "/ActivarReserva/?CodReserva={sCodReserva}&CodAlumno={sCodAlumno}&CodAlumno2={sCodAlumno2}&Token={sToken}")]
        DCReservaRecurso ActivarReserva(string sCodReserva, string sCodAlumno, string sCodAlumno2, string sToken);

        //Verifica Reserva Alumno
        [OperationContract]
        [WebInvoke(Method = "GET",
                  ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "/VerificaReserva/?CodReserva={sCodReserva}&CodRecurso={sCodRecurso}&CodAlumno={sCodAlumno}&Token={sToken}")]
        DCReservaRecurso VerificaReserva(string sCodReserva, string sCodRecurso, string sCodAlumno, string sToken);
        #endregion

    }
}
