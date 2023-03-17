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
    /// Логика взаимодействия для MenyPage.xaml
    /// </summary>
    public partial class MenyPage : Page
    {
        Users users;
        Students students;
        Teachers teacher;
        public MenyPage(Users users)
        {
            InitializeComponent();
            this.users = users;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch (users.Role_ID)
            {
                case 0:
                    getClassTaskBtn.Visibility = Visibility.Collapsed;
                    getStudentTaskBtn.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    teacher = EnglishKlassBDEntities.GetContext().Teachers.Where(t=> t.Users == users).First();
                        classAddBtn.Visibility = Visibility.Collapsed;
                        studentAddBtn.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    classBtn.Visibility = Visibility.Collapsed;

                    taskAddBtn.Visibility = Visibility.Collapsed;
                    classAddBtn.Visibility = Visibility.Collapsed;
                    studentAddBtn.Visibility = Visibility.Collapsed;

                    getClassTaskBtn.Visibility = Visibility.Collapsed;
                    getStudentTaskBtn.Visibility = Visibility.Collapsed;

                    students = EnglishKlassBDEntities.GetContext().Students.Where(t => t.Users == users).First();
                    break;
            }
        }


        private void classBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClassPage());

        }

        private void studentBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentsPage(teacher));

        }

        private void taskBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage());

        }

        private void taskAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditTaskPage());
        }

        private void studentAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditStudentPage());

        }

        private void classAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditClassPage());

        }

        private void getClassTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllClassTaskPage(teacher));

        }

        private void getStudentTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetStudentQuast(teacher));

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }


    }
}
