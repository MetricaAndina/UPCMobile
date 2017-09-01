using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UPCMobile.LN
{
    public class LNNota
    {
        #region Singleton
        private static LNNota _Instancia;
        private LNNota() { }
        public static LNNota Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new LNNota();

                return _Instancia;
            }
        }
        #endregion

        #region Metodos

        public DC.NotaCollectionDC getNotas(string codUsuario, string codCurso)
        {
            string error = "";
            DC.NotaCollectionDC Notas;

            try
            {
                Collection<DC.NotaDC> lista = DA.DANota.Instancia.getNotas(codUsuario, codCurso, ref error);

                if (lista.Count <= 0)
                {
                    Notas = new DC.NotaCollectionDC();
                    Notas.CodError = "00021";
                    Notas.MsgError = "Ud. no cuenta con notas registradas para este curso.";

                }
                else
                {
                    Notas = new DC.NotaCollectionDC();
                    var s = lista.GroupBy(x => new DC.NotaCollectionDC()
                    {
                        CodError = "00000",
                        MsgError = "",
                        CodCurso = x.CodCurso,
                        Formula = x.Formula,
                        CursoNombre = x.NomCurso,
                        PorcentajeAvance = "0",
                        NotaFinal = "0"
                    });

                    Notas.CodError = s.FirstOrDefault().Key.CodError;
                    Notas.MsgError = s.FirstOrDefault().Key.MsgError;
                    Notas.CodCurso = s.FirstOrDefault().Key.CodCurso;
                    Notas.CursoNombre = s.FirstOrDefault().Key.CursoNombre;
                    Notas.Formula = s.FirstOrDefault().Key.Formula;

                    var ent = (lista.Where(x => x.NombreCorto.Equals("PF")).FirstOrDefault());
                    var sum = lista.Where(x => !x.NombreCorto.Equals("PF"));
                    Notas.NotaFinal = ent.Valor; 
                    Notas.PorcentajeAvance = ent.Peso;
                    Notas.Notas = sum.ToList();

                    if (Notas.NotaFinal.Equals("0"))
                    { 
                        var Porcentaje = sum.Where(x => !x.Valor.Equals("0")).Sum(p => double.Parse(p.Peso.Replace("%","")));
                        var AvanceNota = sum.Where(x => !x.Valor.Equals("0")).Sum(p => CstValorNota(p.Valor) * double.Parse(p.Peso.Replace("%", "")));

                        Notas.PorcentajeAvance = Porcentaje.ToString() + "%";

                        double res = Math.Round(AvanceNota/Porcentaje,2);

                        Notas.NotaFinal = (double.IsNaN(res)? "0":res.ToString()); 
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Notas = new DC.NotaCollectionDC();
                Notas.CodError = "11111";
                Notas.MsgError =  "Ocurrió un error con los servicios, por favor comuníquelo a IT Service al anexo 7799 (Cód. 204).";
                return Notas;

            }
            return Notas;//
        }

        private double CstValorNota(string nota) {
            double result;

            switch (nota) {
                case "RET":
                    result = 0;
                    break;
                case "NR":
                    result = 0;
                    break;
                case "SAN" :
                    result = 0;
                    break;
                case "DPI":
                    result = 0;
                    break;
                default:
                    result = double.Parse(nota);
                    break; 
            } 
            return result;
        }
        
        #endregion

        #region SS-2013-072
        
        public bool ValidaProfesorCurso(string codUsuario, string codAlumno, string codCurso)
        {
            return DA.DANota.Instancia.ValidaProfesorCurso(codUsuario, codAlumno, codCurso);
        }

        public bool ValidaHijoPadre(string codUsuario, string codAlumno) 
        {
            return DA.DANota.Instancia.ValidaHijoPadre(codUsuario, codAlumno);
        }

        public bool ValidaProfesorAlumno(string codUsuario, string codAlumno)
        {
            return DA.DANota.Instancia.ValidaProfesorAlumno(codUsuario, codAlumno);
        }

        #endregion
    }
}
