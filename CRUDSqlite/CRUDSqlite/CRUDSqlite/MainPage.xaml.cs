using CRUDSqlite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRUDSqlite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }


        // Click para el insert.
        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Alumno alum = new Alumno // Objeto que se almacena en base de datos.
                {
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Email = txtEmail.Text,
                };
                await App.SQLiteDB.SaveAlumnoAsync(alum);
                await DisplayAlert("Registro", "Se guardo de manera exitosa el alumno", "Ok");
                limpiarControles();
                llenarDatos();

            }
            else
            {
                await DisplayAlert("Advertinecia", "Ingresar todos los datos", "Ok");
            }
        }


        public async void llenarDatos()
        {

            var alumnoList = await App.SQLiteDB.GetAlumnosAsync();

            if (alumnoList != null)
            {
                LstAlumnos.ItemsSource = alumnoList;
            }
        }


        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoPaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidoMaterno.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void LstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumno)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdAlumno.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdAlumno.ToString()))
            {
                var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(obj.IdAlumno);
                if (alumno != null)
                {
                    txtIdAlumno.Text = alumno.IdAlumno.ToString();
                    txtNombre.Text = alumno.Nombre;
                    txtApellidoPaterno.Text = alumno.ApellidoPaterno;
                    txtApellidoMaterno.Text = alumno.ApellidoMaterno;
                    txtEdad.Text = alumno.Edad.ToString();
                    txtEmail.Text = alumno.Email;
                }
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAlumno.Text))
            {
                Alumno alumno = new Alumno()
                {
                    IdAlumno = int.Parse(txtIdAlumno.Text),
                    Nombre = txtNombre.Text,
                    ApellidoPaterno = txtApellidoPaterno.Text,
                    ApellidoMaterno = txtApellidoMaterno.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Email = txtEmail.Text
                };

                await App.SQLiteDB.SaveAlumnoAsync(alumno);
                await DisplayAlert("Registro", "Se Actualizo de manera exitosa el alumno", "ok");
                limpiarControles();
                txtIdAlumno.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                llenarDatos();

            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {

            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtIdAlumno.Text));

            if (alumno != null)
            {
                await App.SQLiteDB.DeleteAlumnoAsync(alumno);
                await DisplayAlert("Alumno", "Se elimino de manera exitosa", "ok");
                limpiarControles();
                llenarDatos();
                txtIdAlumno.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;
            }
        }


        // Método para limpiar controles.
        public void limpiarControles()
        {
            txtIdAlumno.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";
        }
    }
}
