using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; // Libreria para validar expresiones regulares de C#

namespace BecasAlumnos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        // Defino las listas de objetos a utilizar
        List<Alumnos> ListaAlumnos = new List<Alumnos>();
        List<Becas> ListaBecas = new List<Becas>();
        List<Asignada> ListaAsignadas = new List<Asignada>();

        // Verificacion de campos vacios en "Crear beca"
        public bool VerificaciónBecas()
        {
            if (txtCodigo.Text == "" || numImporte.TextAlign.ToString() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Verificacion de campos vacios en "Crear beneficiario"
        public bool VerificacionAlumnos()
        {
            if (txtNombre.Text == "" || txtApellido.Text == "" || numDNI.TextAlign.ToString() == "" || numCuota.TextAlign.ToString() == "" || BoxBeneficiarios.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Verificacion del codigo en "Crear beca" (aca se utiliza la libreria agregada)
        public bool VerificarCódigo()
        {
            string code = txtCodigo.Text;
            string patron = @"^\d{4}[a-zA-Z]{2}$";
            bool veri = Regex.IsMatch(code, patron);
            if(veri)
            {
                return true;
            }
            else
            {
            
                return false;
            }
        }

        // Boton crear beca
        private void btnCrearBeca_Click(object sender, EventArgs e)
        {
            if (VerificaciónBecas() == true)
            {
                MessageBox.Show("Debe completar todos los campos para continuar...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (VerificarCódigo() == false)
            {
                MessageBox.Show("El código de la beca debe de contener 4 números y 2 letras", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarCasillas();
            }
            else if (ImporteBeca() == true)
            {
                MessageBox.Show("El importe de la beca debe ser mayor a cero", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                AgregarBeca();
                LimpiarCasillas();
            }
        }
        
        // Funcion para agregar una beca, utilizada en el boton "crear" de beca
        public void AgregarBeca()
        {
            string Código = txtCodigo.Text;
            string fecha = DateTime.Now.ToString("MM/dd HH:mm");
            double imp = ((double)numImporte.Value);
            // Creo objeto 
            Becas obj = new Becas(Código,fecha,imp); 
            // Agrego a la lista
            ListaBecas.Add(obj); 
            // Agrego al DGV
            DGVbeca.Rows.Add(obj.Código,obj.FechaOtorga,obj.Importe); 
        }
        
        // Funcion para limpiar los campos escritos una vez agregada la beca
        public void LimpiarCasillas()
        {
            txtCodigo.Text = "";
            numImporte.Value = 0;
            DGVbeca.ClearSelection();
        }

        // Boton para agregar alumnos
        private void btnCrearBenef_Click(object sender, EventArgs e)
        {
            if (VerificacionAlumnos() == true)
            {
                MessageBox.Show("Debe completar todos los campos para continuar...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (numCuota.Value < 20000)
            {
                MessageBox.Show("El minimo de cuota en esta institucion es de $20000", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Convierto los valores decimales del NumericUpDown a double/int
                double cuota = ((double)numCuota.Value);
                int dni = ((int)numDNI.Value);
                // Genero numero random
                Random ran = new Random();
                int leg = ran.Next(100000, 999999);
              
                // Creo el objetos con los datos correspondientes
                Alumnos alumno = new Alumnos(txtNombre.Text, txtApellido.Text, leg, dni, cuota, BoxBeneficiarios.Text, false);
                // Agrego a la lista
                ListaAlumnos.Add(alumno);

                // Agrego al DGV
                DGVbeneficiario.Rows.Add(alumno.Legajo, alumno.Nombre, alumno.Apellido, alumno.DNI);

                CleanAlumnos();
            }
        }

        // Funcion para limpiar los campos escritos una vez agregado el beneficiario
        public void CleanAlumnos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            numDNI.Value = 0;
            numCuota.Value = 0;
            BoxBeneficiarios.Text = "";
            DGVbeneficiario.ClearSelection();
        }

        // Funcion para tomar los datos del DGV seleccionado
        public void MostrarAlumno(int index)
        {
            txtNombre.Text = ListaAlumnos[index].Nombre;
            txtApellido.Text = ListaAlumnos[index].Apellido;
            numDNI.Value = ListaAlumnos[index].DNI;
            BoxBeneficiarios.Text = ListaAlumnos[index].Tipo;
            numCuota.Value = (decimal)ListaAlumnos[index].Cuota;
        }

        private void btnDeleteBeca_Click(object sender, EventArgs e)
        {
            
            if(DGVbeca.SelectedRows.Count > 0) // Verifico que exista fila seleccionada
            {
                int ind = DGVbeca.CurrentRow.Index; // Obtengo incide de la fila
                Becas bequita = ListaBecas[ind];
                ListaBecas.Remove(bequita); // Elimino de la lista el objeto 
                DGVbeca.Rows.RemoveAt(ind); // Elimino del DGV
                DGVbeca.Refresh();
            }
            else
            {
                MessageBox.Show("No existen becas para eliminar ó no hay beca seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGVbeneficiario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.RowIndex >= 0) // Verifico que este seleccionando una fila
            {
                MostrarAlumno(index);
            }
        }

        private void btnModBenef_Click(object sender, EventArgs e)
        {
            if (DGVbeneficiario.SelectedRows.Count > 0) // Si hay fila seleccionada
            {
                int select = DGVbeneficiario.CurrentRow.Index; // Fila seleccionada
                // Objeto tipo Alumnos de la fila seleccionada
                Alumnos alumnoSeleccionado = ListaAlumnos[select];

                if (VerificacionAlumnos() == true)
                {
                    MessageBox.Show("Debe completar todos los campos para continuar...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (numCuota.Value < 20000)
                {
                    MessageBox.Show("El minimo de cuota en esta institucion es de $20000", "Recuerde:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Convierto los valores decimales del NumericUpDown a double/int
                    double cuota = ((double)numCuota.Value);
                    int dni = ((int)numDNI.Value);

                    // Actualizo los datos del objeto
                    alumnoSeleccionado.Nombre = txtNombre.Text;
                    alumnoSeleccionado.Apellido = txtApellido.Text;
                    alumnoSeleccionado.DNI = dni;
                    alumnoSeleccionado.Tipo = BoxBeneficiarios.Text;
                    alumnoSeleccionado.Cuota = cuota;

                    // Actualizo el DGV
                    DGVbeneficiario.Rows[select].Cells[1].Value = txtNombre.Text;
                    DGVbeneficiario.Rows[select].Cells[2].Value = txtApellido.Text;
                    DGVbeneficiario.Rows[select].Cells[3].Value = dni;

                    CleanAlumnos();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un alumno para utilizar esta función...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteBenef_Click(object sender, EventArgs e)
        {
            if (DGVbeneficiario.SelectedRows.Count > 0) // Si hay fila seleccionada
            {
                int select = DGVbeneficiario.CurrentRow.Index; // Fila seleccionada
                if (Verificaciondeeliminacion(ListaAlumnos[select].Becas) == false)
                {
                    ListaAlumnos[select] = null; // Asigno null al objeto seleccionado de la lista

                    ListaAlumnos.RemoveAt(select); // Elimino el objeto seleccionado de la lista

                    DGVbeneficiario.Rows.RemoveAt(select); // Elimino a la persona del DGV

                    CleanAlumnos();
                }
                else
                {
                    MessageBox.Show("Lo sentimos el alumno que desea eliminar posee una beca asociada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un alumno para utilizar esta función...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Boton modificar beca
        private void btnModBeca_Click(object sender, EventArgs e)
        {
            if (DGVbeca.SelectedRows.Count > 0)
            {
                DataGridViewRow Filasele = DGVbeca.SelectedRows[0];
                int filaselected = DGVbeca.CurrentRow.Index;
                if (Filasele != null) // Verifico si existen becas en el DGV
                {
                    if (VerificaciónBecas() == true) // Verifico que no haya campos vacios
                    {
                        MessageBox.Show("Debe completar todos los campos para continuar...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (VerificarCódigo() == false) // Verifico codigo 
                    {
                        MessageBox.Show("El código debe ser 4 números seguido de 2 letras", "Recuerde:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (ImporteBeca() == true) // Verifico importe > 0
                    {
                        MessageBox.Show("El importe de la beca debe ser mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Completo el DGV con los datos
                        string Fecha = DateTime.Now.ToString("MM/dd HH:mm"); // Fecha del momento 
                        Becas Bequita = new Becas(txtCodigo.Text, Fecha, (double)numImporte.Value);
                        DGVbeca.Rows[filaselected].Cells["Codigo"].Value = txtCodigo.Text;
                        DGVbeca.Rows[filaselected].Cells["Fecha"].Value = Fecha;
                        DGVbeca.Rows[filaselected].Cells["Importe"].Value = numImporte.Value;
                        ListaBecas[filaselected] = Bequita;
                        DGVbeca.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una beca para utilizar esta función...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

       // Boton asignar beca a alumno
       private void btnAsignar_Click(object sender, EventArgs e)
       {
            if (DGVbeneficiario.SelectedRows.Count > 0 && DGVbeca.SelectedRows.Count > 0) // Si hay filas seleccionadas
            {
                // Fila seleccionada
                double pago = 0;
                int selectAlumno = DGVbeneficiario.CurrentRow.Index; 
                int selectBeca = DGVbeca.CurrentRow.Index;
                ProcesarPago(ref  pago);
                double import = (double)DGVbeca.Rows[selectBeca].Cells["Importe"].Value;
                if (ListaAlumnos[selectAlumno].Becas == false) // Si el alumno no tiene becas
                {
                    if (ListaAlumnos[selectAlumno].Cuota > import) // Si la cuota del alumno es menor al importe de la beca
                    {
                        ListaAlumnos[selectAlumno].Becas = true;
                        string codes = DGVbeca.Rows[selectBeca].Cells["Codigo"].Value.ToString();
                        Asignada asignada = new Asignada(ListaAlumnos[selectAlumno].DNI, ListaAlumnos[selectAlumno].Apellido, codes, pago);

                        // Agrego a la lista
                        ListaAsignadas.Add(asignada);

                        // Agrego al DGV           
                        dgtvAlumnosyBecas.Rows.Add(asignada.Documento, asignada.Apellido2, asignada.Code, asignada.Pago);
                        DGVbeca.Rows.RemoveAt(selectBeca);
                        DGVbeca.Refresh();
                        CleanAlumnos();
                        dgtvAlumnosyBecas.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show("El importe de esta beca supera el 100% de la cuota del alumno seleccionado", "Error al asignar la beca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El alumno seleccionado ya posee una beca", "Error al asignar la beca", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un alumno y una beca para utilizar esta función...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
       }

        // Función para verificar importe válido
        public bool ImporteBeca()
        {
            if (numImporte.Value == 0)
            {
                return true;
            }
            else return false;
        }

        // Funcion para que dependiendo el tipo de alumno y importe de beca diga cuanto debe pagar
        public void ProcesarPago(ref double pago)
        {
            
            int filaselecbeca = DGVbeca.CurrentRow.Index;
            int filaselecalumno = DGVbeneficiario.CurrentRow.Index;
            switch (ListaAlumnos[filaselecalumno].Tipo) // Se Plantea distintas ejecuciones de códigos depende el valor que tome el atributo "Tipo"
            {
               
                case "Ingresantes": // 10% de descuento neto
                    pago = ListaAlumnos[filaselecalumno].Cuota - ListaBecas[filaselecbeca].Importe; // Cálculo porcentaje
                    double decuento = (pago*10) / 100;
                    pago = pago - decuento;
                    break;
                case "Grado": // 5% de descuento neto
                    pago = ListaAlumnos[filaselecalumno].Cuota - ListaBecas[filaselecbeca].Importe;
                    double desc = (pago*5) / 100;
                    pago = pago - desc;
                    break;
                case "Posgrado": // 1% de descuento neto
                    pago = ListaAlumnos[filaselecalumno].Cuota - ListaBecas[filaselecbeca].Importe;
                    double des = (pago) / 100;
                    pago = pago - des;  
                    break;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgtvAlumnosyBecas.SelectedRows.Count > 0) // Si hay seleccionado 
            {
                int select = dgtvAlumnosyBecas.CurrentRow.Index; // Fila seleccionada

                string beca = dgtvAlumnosyBecas.Rows[select].Cells["code"].Value.ToString();
                int indice = ListaBecas.FindIndex(p => p.Código == beca);

                int doc = (int)dgtvAlumnosyBecas.Rows[select].Cells["documento"].Value;
                int Indice = ListaAlumnos.FindIndex(p => p.DNI == doc);
                ListaAlumnos[Indice].Becas = false;

                ListaAsignadas[select] = null; // Asigno null al objeto seleccionado de la lista

                ListaAsignadas.RemoveAt(select); // Elimino el objeto seleccionado de la lista

                dgtvAlumnosyBecas.Rows.RemoveAt(select); // Elimino a la persona del DGV

                DGVbeca.Rows.Add(ListaBecas[indice].Código, ListaBecas[indice].FechaOtorga, ListaBecas[indice].Importe);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para utilizar esta función...", "Atencion!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Función para la verificación que no elimine un alumno cuando éste tiene una beca asociada a su nombre
        public bool Verificaciondeeliminacion(bool dato)
        {
            if (dato == false)
            { 
                return false; 
            }
            else
            { 
                return true; 
            }
        }
    }
}

