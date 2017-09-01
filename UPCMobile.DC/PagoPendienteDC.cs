using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace UPCMobile.DC
{
    [DataContract]
    public class PagoPendienteDC
    {
        
        private String _CodAlumno; // SI HAY LO REFERENCIO DE LO ENVIADO
        private int _NroCuota; // NO HAY -- MUCHAS REFERENCIAS PARA VER

        private String _TipoDocumento;//LISTO
        private String _NroDocumento;//LISTO
        private String _Moneda;// LO, EX REVISAR, SI HAY

        
        private String _Importe;//MontoTotal
        private String _Descuento;
        private String _Impuesto;//preguntar


        private String _ImporteCancelado;//MONTOPAGADO
        private String _Saldo;// DE LA DEUDA EN SI -- //MONTOTOTAL - MONTOPAGADO CON MORA SIN MORA

        private String _FecEmision;//FECHADOCUMENTO
        private String _FecVencimiento;//MONTOPAGADO
        private String _Mora;//EL CALCULO - SALDO
        private String _Total;//SALDO  + MORA

        [DataMember(Order = 1)]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember(Order = 2)]
        public int NroCuota
        {
            get { return _NroCuota; }
            set { _NroCuota = value; }
        }
        
        [DataMember(Order = 3)]
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        [DataMember(Order = 4)]
        public String NroDocumento
        {
            get { return _NroDocumento; }
            set { _NroDocumento = value; }
        }

        [DataMember(Order = 5)]
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        [DataMember(Order = 6)]
        public String Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        [DataMember(Order = 7)]
        public String Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        [DataMember(Order = 8)]
        public String Impuesto
        {
            get { return _Impuesto; }
            set { _Impuesto = value; }
        }

        [DataMember(Order = 9)]
        public String ImporteCancelado
        {
            get { return _ImporteCancelado; }
            set { _ImporteCancelado = value; }
        }

        [DataMember(Order = 10)]
        public String Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }

        [DataMember(Order = 11)]
        public String FecEmision
        {
            get { return _FecEmision; }
            set { _FecEmision = value; }
        }

        [DataMember(Order = 12)]
        public String FecVencimiento
        {
            get { return _FecVencimiento; }
            set { _FecVencimiento = value; }
        }

        [DataMember(Order = 13)]
        public String Mora
        {
            get { return _Mora; }
            set { _Mora = value; }
        }

        [DataMember(Order = 14)]
        public String Total
        {
            get { return _Total; }
            set { _Total = value; }
        }




        

        

    }

    /*[DataContract]
    public class PagoPendienteDC
    {
        private int _CodPagoPendiente;
        private String _TipoDocumento;
        private String _CodAlumno;
        private int _NroCuota;
        private String _NroDocumento;
        private String _Moneda;//MonedaDocumento
        private String _Importe;//MontoTotal
        private String _Descuento;
        private String _Impuesto;
        private String _ImporteCancelado;//MontoPagado
        private String _Saldo;//MontoAdelantoSaldo (restar y calcular contra monto pagado y monto total)
        private String _FecEmision;// FechaDocumento  listo
        private String _FecVencimiento; // FechaVencimiento listo
        private String _Mora;
        private String _Total;//--Total o falta pagar o con mora MontoTotal

        [DataMember]
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }
        [DataMember]
        public int CodPagoPendiente
        {
            get { return _CodPagoPendiente; }
            set { _CodPagoPendiente = value; }
        }

        [DataMember]
        public String CodAlumno
        {
            get { return _CodAlumno; }
            set { _CodAlumno = value; }
        }

        [DataMember]
        public int NroCuota
        {
            get { return _NroCuota; }
            set { _NroCuota = value; }
        }

        [DataMember]
        public String NroDocumento
        {
            get { return _NroDocumento; }
            set { _NroDocumento = value; }
        }

        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        [DataMember]
        public String Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }

        [DataMember]
        public String Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        public String Impuesto
        {
            get { return _Impuesto; }
            set { _Impuesto = value; }
        }

        public String ImporteCancelado
        {
            get { return _ImporteCancelado; }
            set { _ImporteCancelado = value; }
        }

        [DataMember]
        public String Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }

        [DataMember]
        public String FecEmision
        {
            get { return _FecEmision; }
            set { _FecEmision = value; }
        }

        [DataMember]
        public String FecVencimiento
        {
            get { return _FecVencimiento; }
            set { _FecVencimiento = value; }
        }

        [DataMember]
        public String Mora
        {
            get { return _Mora; }
            set { _Mora = value; }
        }

        [DataMember]
        public String Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
    }*/
}
