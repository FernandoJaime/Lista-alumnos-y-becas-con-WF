using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecasAlumnos
{
    public class Alumnos
    {
        // Atributos privados
        private string _nombre;
        private string _apellido;
        private int _legajo;
        private int _dni;
        private double _cuota;
        private string _tipo;
        private bool _becas;

        // Propiedades
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        public int Legajo
        {
            get { return _legajo; }
            set { _legajo = value; }
        }
        public int DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }
        public double Cuota
        {
            get { return _cuota; }
            set { _cuota = value; }
        }
        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public bool Becas
        {
            get { return _becas; }
            set { _becas = value; }
        }

        // Constructor
        public Alumnos(string nombre, string apellido, int legajo, int dni, double cuota, string tipo, bool beca)
        {
            this._nombre = nombre;
            this._apellido = apellido;
            this._legajo = legajo;
            this._dni = dni;
            this._cuota = cuota;
            this._tipo = tipo;
            this._becas = beca;
        }
    }
}