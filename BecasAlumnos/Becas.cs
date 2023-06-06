using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecasAlumnos
{
    public class Becas
    {
        // Atributos privados
        private string _codigo;
        private string _fechaotorga;
        private double _importe;

        // Propiedades 
        public string Código
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public string FechaOtorga
        {
            get { return _fechaotorga; }
            set { _fechaotorga = value; }
        }
        public double Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }

        // Constructor
        public Becas(string pcodigo, string pfecha, double importe)
        {
            this.Código = pcodigo;
            this.FechaOtorga = pfecha;
            this.Importe = importe;
        }
    }
}
