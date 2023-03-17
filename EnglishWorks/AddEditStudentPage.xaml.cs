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
    /// Логика взаимодействия для AddEditStudentPage.xaml
    /// </summary>
    public partial class AddEditStudentPage : Page
    {
        Students students;
        bool add;
        public AddEditStudentPage(Students students)
        {
            InitializeComponent();
            this.students = students;
            add = false;
        }
        public AddEditStudentPage()
        {
            InitializeComponent();
            this.students = new Students();
            add = true;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            genderCbox.SelectedValuePath = "ID";
            genderCbox.DisplayMemberPath = "Name";
            genderCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Genders.ToList();

            classCharCbox.SelectedValuePath = "ID";
            classCharCbox.DisplayMemberPath = "ID";
            classCharCbox.ItemsSource = EnglishKlassBDEntities.GetContext().ClassGroup.ToList();

            lkCbox.SelectedValuePath = "Login";
            lkCbox.DisplayMemberPath = "Login";
            lkCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Users
                .Where(u => u.Role_ID.Equals(2)).ToList();

            if (!add)
            {

                firstNameTBox.Text =students.Firstname;
                lastNameTBox.Text= students.Lastname;
                patronymicTBox.Text= students.Patronymic ;
                birthDPicer.SelectedDate =students.DateBirth;
                genderCbox.SelectedValue= students.Gender_ID;
                classCharCbox.SelectedValue =students.Class_ID;

                if (students.Users.Role_ID != 2)
                    students.Users_ID = null;
                lkCbox.SelectedValue =students.Users_ID;


            }
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (firstNameTBox.Text.Length == 0 || lastNameTBox.Text.Length == 0 ||
                birthDPicer.SelectedDate == null || classCharCbox.SelectedValue == null ||
                genderCbox.SelectedValue == null|| lkCbox.SelectedValue == null)
            {
                MessageBox.Show("Не все поля были заполнены ", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            students.Firstname = firstNameTBox.Text;
            students.Lastname = lastNameTBox.Text;
            students.Patronymic = patronymicTBox.Text;
            students.DateBirth = birthDPicer.SelectedDate.Value;
            students.Gender_ID = (int)genderCbox.SelectedValue;
            students.Class_ID = (int)classCharCbox.SelectedValue;
            students.Users_ID = (string)lkCbox.SelectedValue;

            if (add)
                EnglishKlassBDEntities.GetContext().Students.Add(students);
            try
            {
                EnglishKlassBDEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            NavigationService.GoBack();
        }

        private void classCharCbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Изменение класса может привести к потере доступа к ученику." , "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
