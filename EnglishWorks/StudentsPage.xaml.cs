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
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        Students students;
        Teachers teachers;
        public StudentsPage(Students students)
        {
            InitializeComponent();
        }
        public StudentsPage(Teachers teachers)
        {
            InitializeComponent();
        }

        public List<Students> getDB()
        {
            List<Students> tasks = new List<Students>();
            if (students != null)
                tasks = EnglishKlassBDEntities.GetContext().Students
                    .Where(t => t.Class_ID == students.Class_ID).ToList();
            else
                tasks = EnglishKlassBDEntities.GetContext().Students.ToList();
            return tasks;
        }
        public List<Students> getFilterDB()
        {
            List<Students> tasks = getDB();
            if (CBoxNumberClass.SelectedItem != null)
            {
                int _id = (int)CBoxNumberClass.SelectedValue;
                tasks = tasks.Where(t => t.Class_ID == _id).ToList();
            }
            if (genderCbox.SelectedItem != null)
            {
                int _id = (int)genderCbox.SelectedValue;
                tasks = tasks.Where(t => t.Gender_ID == _id).ToList();
            }
            if (eventNameBox.Text.Length > 0)
                tasks = tasks.Where(t => t.Firstname.ToLower() == eventNameBox.Text.ToLower() ||
                      t.Lastname.ToLower() == eventNameBox.Text.ToLower() ||
                      t.Patronymic.ToLower() == eventNameBox.Text.ToLower()).ToList();

            return tasks;
        }

        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            CBoxNumberClass.SelectedValue = null;
            genderCbox.SelectedValue = null;
            eventNameBox.Text = null;

        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnglishKlassBDEntities.GetContext().Students.Remove(((Students)dataGrid.SelectedValue));
                EnglishKlassBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены! ");
                dataGrid.ItemsSource = getFilterDB();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditStudentPage());

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedValue == null)
                return;
            NavigationService.Navigate(new AddEditStudentPage((Students)dataGrid.SelectedValue));

        }

        private void BtnAddTaskClassDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddTaskStudent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnViewTaskClass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
