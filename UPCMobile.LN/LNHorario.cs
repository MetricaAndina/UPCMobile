using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNHorario
    {
        #region Singleton
        private static LNHorario _Instancia;
        private LNHorario() { }
        public static LNHorario Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNHorario();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos
        public DC.HorarioDiaCollectionDC ObtenerHorario(string codUsuario)
        {
            DC.HorarioDiaCollectionDC Horario;
            string errMensaje = "";
            try
            {
                Collection<DC.ClaseDC> lstClases = DA.DAHorario.Instancia.ObtenerClases(codUsuario, ref errMensaje);
                if (lstClases.Count <= 0)
                {
                    Horario = new DC.HorarioDiaCollectionDC();
                    Horario.CodError = "00011";
                    Horario.MsgError = "Ud. no tiene clases programadas para esta semana.";
                    return Horario;
                }
                else
                {
                    Horario = new DC.HorarioDiaCollectionDC();

                    var s = lstClases.GroupBy(x => x.Fecha).Select(lg => new DC.HorarioDiaDC()
                    {
                        Clases = ProcesarCruces(lg.ToList()),
                        Fecha = lg.Key,
                        CodAlumno = codUsuario,
                        CodDia = DiaFecha(lg.Key)
                    });

                    Horario.CodError = "00000";
                    Horario.MsgError = "";
                    Horario.HorarioDia = s.ToList();

                    return Horario;
                }
            }
            catch (Exception ex)
            {
                Horario = new DC.HorarioDiaCollectionDC();
                Horario.CodError = "11111";
                //Horario.MsgError = ex.Message;//"Ocurrió un error con los servicios: " + ex.Message;";
                Horario.MsgError = "Ocurrió un error con los servicios: " + ex.Message;
                return Horario;
            }

        }


        #endregion

        #region Private
        private List<DC.ClaseDC> ProcesarCruces(List<DC.ClaseDC> ListaClases)
        {
            FusionarCursosMismoHorario(ref ListaClases);
            SegmentarCruceHorarioAnidado(ref ListaClases);
            DividirHorarioCompartido(ref ListaClases);
            return ListaClases;
        }
        


        private void FusionarCursosMismoHorario(ref List<DC.ClaseDC> Lista){
            var remov = new List<DC.ClaseDC>();
            var temLista = Lista;
            foreach (DC.ClaseDC n in Lista) { 
                foreach (var itemClases in temLista) {
                    if (n != itemClases && !remov.Contains(n) 
                            && n.HoraInicio == itemClases.HoraInicio && n.HoraFin == itemClases.HoraFin)
                    {
                        n.CodClase = 0;
                        n.CodCurso = n.CodCurso + "-" + itemClases.CodCurso;
                        n.CursoNombre = n.CursoNombre + "-" + itemClases.CursoNombre;
                        n.CursoNombreCorto = n.CursoNombreCorto + "-" + itemClases.CursoNombreCorto;
                        n.Salon = n.Salon + "-" + itemClases.Salon;
                        n.Seccion = n.Seccion + "-" + itemClases.Seccion;
                        n.Sede = n.Sede + "-" + itemClases.Sede;
                        remov.Add(itemClases);
                        //cruce.Add(n);
                    }
                }
            }
            foreach(var objQuitar in remov){
                Lista.Remove(objQuitar);
            }
        }

        private void SegmentarCruceHorarioAnidado(ref List<DC.ClaseDC> Lista)
        {
            var cruce = new List<DC.ClaseDC>();
            var remov = new List<DC.ClaseDC>();
            var temLista = Lista;
            int horaini,horafin;
            foreach (DC.ClaseDC n in Lista)
            {
                horaini = int.Parse(n.HoraInicio);
                horafin = int.Parse(n.HoraFin);

                foreach (var itemClases in temLista)
                {
                    int ini = int.Parse(itemClases.HoraInicio);
                    int fin = int.Parse(itemClases.HoraFin);
                    int dif = 0;
                    if (    n != itemClases && !remov.Contains(n)
                            && horaini >= ini && horaini <= fin 
                            && horafin >= ini && horafin <= fin)
                    {
                        if (horaini == ini && horafin < fin) {
                            //n.HoraInicio = (horafin).ToString();
                            remov.Add(itemClases);
                            n.CodClase = 0;
                            n.CodCurso = n.CodCurso + "-" + itemClases.CodCurso;
                            n.CursoNombre = n.CursoNombre + "-" + itemClases.CursoNombre;
                            n.CursoNombreCorto = n.CursoNombreCorto + "-" + itemClases.CursoNombreCorto;
                            n.Salon = n.Salon + "-" + itemClases.Salon;
                            n.Seccion = n.Seccion + "-" + itemClases.Seccion;
                            n.Sede = n.Sede + "-" + itemClases.Sede;
                            itemClases.HoraInicio = n.HoraFin;
                            cruce.Add(itemClases);
                        }
                        else if(horaini == ini && horafin > fin){
                            remov.Add(itemClases);
                            n.HoraInicio = itemClases.HoraFin;
                            itemClases.CodClase = 0;
                            itemClases.CodCurso = itemClases.CodCurso + "-" + n.CodCurso;
                            itemClases.CursoNombre = itemClases.CursoNombre + "-" + n.CursoNombre;
                            itemClases.CursoNombreCorto = itemClases.CursoNombreCorto + "-" + n.CursoNombreCorto;
                            itemClases.Salon = itemClases.Salon + "-" + n.Salon;
                            itemClases.Seccion = itemClases.Seccion + "-" + n.Seccion;
                            itemClases.Sede = itemClases.Sede + "-" + n.Sede;
                            cruce.Add(itemClases);
                        }
                        else if (horaini < ini && horafin == fin)
                        {
                            n.HoraFin = itemClases.HoraInicio;
                            remov.Add(itemClases);

                            itemClases.CodClase = 0;
                            itemClases.CodCurso = n.CodCurso + "-" + itemClases.CodCurso;
                            itemClases.CursoNombre = n.CursoNombre + "-" + itemClases.CursoNombre;
                            itemClases.CursoNombreCorto = n.CursoNombreCorto + "-" + itemClases.CursoNombreCorto;
                            itemClases.Salon = n.Salon + "-" + itemClases.Salon;
                            itemClases.Seccion = n.Seccion + "-" + itemClases.Seccion;
                            itemClases.Sede = n.Sede + "-" + itemClases.Sede;
                            cruce.Add(itemClases);
                        }
                        else if (horaini > ini && horafin == fin)
                        {
                            remov.Add(itemClases);
                            n.CodClase = 0;
                            n.CodCurso = itemClases.CodCurso + "-" + n.CodCurso;
                            n.CursoNombre = itemClases.CursoNombre + "-" + n.CursoNombre;
                            n.CursoNombreCorto = itemClases.CursoNombreCorto + "-" + n.CursoNombreCorto;
                            n.Salon = itemClases.Salon + "-" + n.Salon;
                            n.Seccion = itemClases.Seccion + "-" + n.Seccion;
                            n.Sede = itemClases.Sede + "-" + n.Sede;
                            itemClases.HoraFin = n.HoraInicio;
                            cruce.Add(itemClases);
                        }
                        else if (horaini > ini && horafin < fin) {
                            remov.Add(itemClases);
                            cruce.Add(this.ClaseCruce(n.CodAlumno, n.CodClase,
                                    n.CodCurso,
                                    n.CursoNombre,
                                    n.CursoNombreCorto,
                                    n.Fecha,
                                    itemClases.HoraFin,
                                    n.HoraFin,
                                    n.Salon,
                                    n.Seccion,
                                    n.Sede
                                    ));

                            n.HoraFin = itemClases.HoraInicio;
                            itemClases.CodClase = 0;
                            itemClases.CodCurso = n.CodCurso + "-" + itemClases.CodCurso;
                            itemClases.CursoNombre = n.CursoNombre + "-" + itemClases.CursoNombre;
                            itemClases.CursoNombreCorto = n.CursoNombreCorto + "-" + itemClases.CursoNombreCorto;
                            itemClases.Salon = n.Salon + "-" + itemClases.Salon;
                            itemClases.Seccion = n.Seccion + "-" + itemClases.Seccion;
                            itemClases.Sede = n.Sede + "-" + itemClases.Sede;

                            cruce.Add(itemClases);
                        }
                    }
                }
            }

            foreach (var objQuitar in remov)
            {
                Lista.Remove(objQuitar);
            }

            Lista.AddRange(cruce);
        }

        private void DividirHorarioCompartido(ref List<DC.ClaseDC> Lista) {

            int horaini = 0, horafin = 0;
            var cruce = new List<DC.ClaseDC>();
            var remov = new List<DC.ClaseDC>();
            var temLista = Lista;
            foreach (DC.ClaseDC n in Lista)
            {
                horaini = int.Parse(n.HoraInicio);
                horafin = int.Parse(n.HoraFin);

                Lista.ForEach(delegate(DC.ClaseDC itemClases)
                {

                    int ini = int.Parse(itemClases.HoraInicio);
                    int fin = int.Parse(itemClases.HoraFin);
                    int dif = 0;

                    if (!remov.Contains(n) && horaini < ini
                                && horaini < fin 
                                    && horafin > ini
                                    )
                    {
                        remov.Add(itemClases);

                        cruce.Add(this.ClaseCruce(itemClases.CodAlumno, 0,
                            itemClases.CodCurso + "-" + n.CodCurso,
                            itemClases.CursoNombre + "-" + n.CursoNombre,
                            itemClases.CursoNombreCorto + "-" + n.CursoNombreCorto,
                            n.Fecha,
                            itemClases.HoraInicio,
                            n.HoraFin,
                            itemClases.Salon + "-" + n.Salon,
                            itemClases.Seccion + "-" + n.Seccion,
                            itemClases.Sede + "-" + n.Sede
                            ));

                        string valorHora = n.HoraFin;

                        n.HoraFin = itemClases.HoraInicio;
                        itemClases.HoraInicio = valorHora;
                        cruce.Add(itemClases);
                        //itemClases.HoraInicio = n.HoraFin;

                    }
                    else if (!remov.Contains(n) && horaini > ini
                             && horaini < fin && horafin > fin)
                    {
                        /*dif = horafin - ini;
                        itemClases.HoraInicio = (horafin).ToString();
                        n.HoraFin = (horafin - dif).ToString();*/
                        remov.Add(itemClases);
                         cruce.Add(this.ClaseCruce(n.CodAlumno, 0,
                            n.CodCurso + "-" + itemClases.CodCurso,
                            n.CursoNombre + "-" + itemClases.CursoNombre,
                            n.CursoNombreCorto + "-" + itemClases.CursoNombreCorto,
                            n.Fecha,
                            n.HoraInicio,
                            itemClases.HoraFin,
                            n.Salon + "-" + itemClases.Salon,
                            n.Seccion + "-" + itemClases.Seccion,
                            n.Sede + "-" + itemClases.Sede
                            ));

                         string valorHora = n.HoraInicio;
                        n.HoraInicio = itemClases.HoraFin;
                        itemClases.HoraFin = valorHora;

                        cruce.Add(itemClases);
                        
                    }



                
                });

                
            }
            foreach (var objQuitar in remov)
            {
                Lista.Remove(objQuitar);
            }

            Lista.AddRange(cruce);


        }

        private DC.ClaseDC ClaseCruce(string sCodAlumno, Int64 iCodClase, string sCodCurso,
                                        string sCursoNombre, string sCursoNombreCorto, string sFecha,
                                        string sHoraini, string sHoraFin, string sSalon,
                                        string sSeccion, string sSede)
        {

            return new DC.ClaseDC()
            {
                CodAlumno = sCodAlumno,
                CodClase = iCodClase,
                CodCurso = sCodCurso,
                CursoNombre = sCursoNombre,
                CursoNombreCorto = sCursoNombreCorto,
                Fecha = sFecha,
                HoraFin = sHoraFin,
                HoraInicio = sHoraini,
                Salon = sSalon,
                Seccion = sSeccion,
                Sede = sSede
            };


        }
        private int DiaFecha(string fecha)
        {
            int dia = 0;
            DateTime dt = DateTime.ParseExact(fecha, "yyyyMMdd", null);
            dia = (int)dt.DayOfWeek;
            return (dia == 0 ? 7 : dia);
        }

        #endregion
        
    }
}
