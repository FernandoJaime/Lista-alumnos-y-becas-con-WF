using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecasAlumnos
{
    public class Asignada 
    {
        // Atributos privada 
        private int _documento;
        private string _apellido2;
        private string _code;
        private double _pago;

        // Propiedades
        public int Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }
        public string Apellido2
        {
            get { return _apellido2; }
            set { _apellido2 = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public double Pago
        {
            get { return _pago; }
            set { _pago = value; }
        }

        // Constructor
        public Asignada (int documento, string apellido2, string code, double pago)
        {
            this._documento = documento;
            this._apellido2 = apellido2;
            this._code = code;
            this._pago = pago;
        }
    }
}
