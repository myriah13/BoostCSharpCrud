using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient; 

namespace Wpf_BoostCSharp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private GestionChansons _repository = new GestionChansons();
        private Chansons _selectedMusic;

        public MainWindow()
        {
            InitializeComponent();
            LoadChansons(); 
        }

        private void LoadChansons()
        {
            List_chansons.ItemsSource = _repository.GetAllMusic();
        }

        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            var music = new Chansons
            {
                Titre = titre.Text,
                Auteurs = auteurs.Text,
                Type = type.Text,
                Instruments = instruments.Text
            };
            _repository.AddMusic(music);
            LoadChansons();
            ClearInputs();
            MessageBox.Show("Musique ajoutée avec succès !");
        }

        private void ClearInputs()
        {
            titre.Text = "";
            auteurs.Text = "";
            type.Text = "";
            instruments.Text = "";
            _selectedMusic = null;
        }

        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMusic != null)
            {
                _selectedMusic.Titre = titre.Text;
                _selectedMusic.Auteurs = auteurs.Text;
                _selectedMusic.Type = type.Text;
                _selectedMusic.Instruments = instruments.Text;
                _repository.UpdateMusic(_selectedMusic);
                LoadChansons();
                ClearInputs();
                MessageBox.Show("Musique modifiée avec succès !");
            }
            else
            {
                MessageBox.Show("Sélectionnez une musique à modifier.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMusic != null)
            {
                _repository.DeleteMusic(_selectedMusic.Id);
                LoadChansons();
                ClearInputs();
                MessageBox.Show("Musique supprimée avec succès !");
            }
            else
            {
               MessageBox.Show("Sélectionnez une musique à supprimer.");
            }
        }

        private void List_chansons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_chansons.SelectedItem is Chansons)
            {
                var selected = (Chansons)List_chansons.SelectedItem;
                _selectedMusic = selected;
                titre.Text = selected.Titre ?? "";
                auteurs.Text = selected.Auteurs ?? "";
                type.Text = selected.Type ?? "";
                instruments.Text = selected.Instruments ?? "";
                btn_ajout.IsEnabled = false;
            }
            else
            {
                _selectedMusic = null;
                titre.Text = "";
                auteurs.Text = "";
                type.Text = "";
                instruments.Text = "";
                btn_ajout.IsEnabled = true; 
            }
        }

    }
}
