using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Acceso.Modelos;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace Ugroup.Vistas
{
    public partial class CrearGrupos : ContentPage
    {
        private List<Tema> _temas;
        private Tema? _temaSeleccionado;

        public Tema TemaSeleccionado
        {
            get { return _temaSeleccionado; }
            set
            {
                _temaSeleccionado = value;
                OnPropertyChanged(nameof(TemaSeleccionado));
            }
        }

        public CrearGrupos()
        {
            InitializeComponent();

            _temas = new List<Tema>
            {
                new Tema { Id = 1, Descripcion = "games" },
                new Tema { Id = 2, Descripcion = "deporte" },
                new Tema { Id = 3, Descripcion = "cocina" },
                new Tema { Id = 4, Descripcion = "literatura" },
                new Tema { Id = 5, Descripcion = "moda" },
                new Tema { Id = 6, Descripcion = "animales" },
                new Tema { Id = 7, Descripcion = "salud" },
                new Tema { Id = 8, Descripcion = "manualidades" },
                new Tema { Id = 9, Descripcion = "tecnologia" },
                new Tema { Id = 10, Descripcion = "cine" }
            };

            temaPicker.ItemsSource = _temas;
            temaPicker.ItemDisplayBinding = new Binding("Descripcion");

            temaPicker.SelectedIndexChanged += (sender, e) =>
            {
                if (temaPicker.SelectedItem != null)
                {
                    TemaSeleccionado = (Tema)temaPicker.SelectedItem;
                }
            };
        }

        private async void CrearGrupo_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verificar si se ha seleccionado un tema
                if (TemaSeleccionado == null)
                {
                    await DisplayAlert("Error", "Por favor, selecciona un tema", "Aceptar");
                    return;
                }

                // Obtener el ID del tema seleccionado
                int temaId = TemaSeleccionado.Id;

                // Crear el objeto Grupo con los datos ingresados
                CrearGrupo grupo = new CrearGrupo
                {
                    NOMBRE_GRUPO = nombreGrupo.Text,
                    DESCRIPCION = descripcionGrupo.Text,
                    ID_TEMAS = temaId,
                    ID_USUARIO = Ugroup.Vistas.Login.userid
                };

                // Enviar grupo a la api
                var response = await Api.Acceso.Grupos.Crear(grupo);
                if (!response)
                {
                    await DisplayAlert("Error", "Fallo al crear grupo", "Aceptar");
                    return;
                }

                // Limpiar los campos después de enviar el grupo
                LimpiarCampos();

                await DisplayAlert("Éxito", "El grupo se ha creado correctamente", "Aceptar");
                await Navigation.PushAsync(new MisGrupos());
                this.Close();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Hubo un fallo en la aplicación", "Aceptar");
            }
        }



        private void LimpiarCampos()
        {
            nombreGrupo.Text = string.Empty;
            descripcionGrupo.Text = string.Empty;
            temaPicker.SelectedIndex = -1;
            TemaSeleccionado = null;
        }
    }
}