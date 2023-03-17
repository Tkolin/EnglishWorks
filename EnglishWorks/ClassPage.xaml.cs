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

namespace EnglishWorks
{
    /// <summary>
    /// Логика взаимодействия для ClassPage.xaml
    /// </summary>
    public partial class ClassPage : Page
    {
        public Teachers teachers;
        public ClassPage()
        {
            InitializeComponent();
        }
        public ClassPage(Teachers teachers)
        {
            InitializeComponent();
            this.teachers = teachers;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBoxNumberClass.SelectedValuePath = "Number";
            CBoxNumberClass.DisplayMemberPath = "Number";
            CBoxNumberClass.ItemsSource = EnglishKlassBDEntities.GetContext().ClassGroup.ToList();

            CBoxCharClass.SelectedValuePath = "Name";
            CBoxCharClass.DisplayMemberPath = "Name";
            CBoxCharClass.ItemsSource = EnglishKlassBDEntities.GetContext().ClassGroup.ToList();

            dataGrid.ItemsSource = getFilterDB();
        }
        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnglishKlassBDEntities.GetContext().ClassGroup.Remove(((ClassGroup)dataGrid.SelectedValue));
                EnglishKlassBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены! ");
                dataGrid.ItemsSource = getFilterDB();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
        public List<ClassGroup> getDB()
        {
            List<ClassGroup> tasks = new List<ClassGroup>();
            if (teachers != null)
                tasks = EnglishKlassBDEntities.GetContext().ClassGroup
                    .Where(t => t.Teacher_ID == teachers.ID).ToList();
            else
                tasks = EnglishKlassBDEntities.GetContext().ClassGroup.ToList();
            return tasks;
        }
        public List<ClassGroup> getFilterDB()
        {
            List<ClassGroup> tasks = getDB();
            if (CBoxNumberClass.SelectedValue != null) {
                int id = (int)CBoxNumberClass.SelectedValue;
            tasks = tasks.Where(t => t.Number == id).ToList(); 
            }
            if (CBoxCharClass.SelectedValue != null)
            {
                string id = (string)CBoxCharClass.SelectedValue;
                tasks = tasks.Where(t => t.Name == id).ToList();
            }
            if (eventNameBox.Text.Length > 0)
                tasks = tasks.Where(t => t.Teachers.Firstname.ToLower().Contains(eventNameBox.Text.ToLower()) ||
                      t.Teachers.Lastname.ToLower().Contains( eventNameBox.Text.ToLower()) ||
                      t.Teachers.Patronymic.ToLower().Contains(eventNameBox.Text.ToLower())).ToList();

            return tasks;
        }
        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            CBoxNumberClass.SelectedValue = null;
            CBoxCharClass.SelectedValue = null;
            eventNameBox.Text = null;

            dataGrid.SelectedValue = getDB();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditClassPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedValue == null)
                return;
            NavigationService.Navigate(new AddEditClassPage((ClassGroup)dataGrid.SelectedValue));
        }



        private void CBoxCharClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void CBoxNumberClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void eventNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void BtnAddTaskClass_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedValue == null)
                return;
            NavigationService.Navigate(new GetAllClassTaskPage((ClassGroup)dataGrid.SelectedValue));
        }
    }
}
