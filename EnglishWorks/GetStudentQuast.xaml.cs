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
    /// Логика взаимодействия для GetStudentQuast.xaml
    /// </summary>
    public partial class GetStudentQuast : Page
    {
        Teachers teachers;
        AccountingForTasks accountingForTasks;
        bool add;
        public GetStudentQuast(Teachers teachers)
        {
            InitializeComponent();
            this.teachers = teachers;
            this.accountingForTasks = new AccountingForTasks();
            DataContext = this;
            add = true;
        }
        public GetStudentQuast(AccountingForTasks accountingForTasks, Teachers teachers)
        {
            InitializeComponent();
            this.teachers = teachers;
            this.accountingForTasks = accountingForTasks;
            DataContext = this;
            add = false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            studentCbox.SelectedValuePath = "ID";
            studentCbox.DisplayMemberPath = "LastName";
            studentCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Students.
                Where(c => c.ClassGroup.Teacher_ID == teachers.ID).ToList();

            taskCbox.SelectedValuePath = "ID";
            taskCbox.DisplayMemberPath = "Name";
            taskCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Tasks.ToList();
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (studentCbox.SelectedValue == null ||
                taskCbox.SelectedValue == null)
            {
                MessageBox.Show("Не все поля были заполнены ", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
           
            accountingForTasks.Task_ID = (int)taskCbox.SelectedValue;
            accountingForTasks.Student_ID = (int)studentCbox.SelectedValue;
            accountingForTasks.DateStart = startDPicer.SelectedDate.Value;
            accountingForTasks.DateEnd = endDPicer.SelectedDate.Value;
            if (add)
            {
                try
                {
                    EnglishKlassBDEntities.GetContext().AccountingForTasks.Add(accountingForTasks);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            try
            {
                EnglishKlassBDEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}


