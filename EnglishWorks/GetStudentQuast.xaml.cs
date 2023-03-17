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
        Students students;
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
        public GetStudentQuast( Teachers teachers, Students students)
        {
            InitializeComponent();
            this.teachers = teachers;
            this.students = students;
            DataContext = this;
            add = true;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            studentCbox.SelectedValuePath = "ID";
            studentCbox.DisplayMemberPath = "Lastname";
            if (teachers != null)
                studentCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Students.
                    Where(c => c.ClassGroup.Teacher_ID == teachers.ID).ToList();
            else
                studentCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Students.ToList();

            taskCbox.SelectedValuePath = "ID";
            taskCbox.DisplayMemberPath = "Name";
            taskCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Tasks.ToList();

            if (!add)
            {
                taskCbox.SelectedValue=accountingForTasks.Task_ID;
                studentCbox.SelectedValue= accountingForTasks.Student_ID;
                startDPicer.SelectedDate = accountingForTasks.DateStart;
                endDPicer.SelectedDate = accountingForTasks.DateEnd;
            }

            if (students != null)
            {
                studentCbox.SelectedItem = students;
                studentCbox.IsEnabled = false;
            }
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
            if(startDPicer.SelectedDate != null)
                accountingForTasks.DateStart = startDPicer.SelectedDate.Value;
            if (endDPicer.SelectedDate != null)
                accountingForTasks.DateEnd = endDPicer.SelectedDate.Value;
            if (add)
                EnglishKlassBDEntities.GetContext().AccountingForTasks.Add(accountingForTasks);
            {
                try
                {
                    
                    try
                    {
                        EnglishKlassBDEntities.GetContext().SaveChanges();
                        NavigationService.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка сохранения: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
    }
}


