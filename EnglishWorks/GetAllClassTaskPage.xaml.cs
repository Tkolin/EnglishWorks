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
    /// Логика взаимодействия для GetAllClassTaskPage.xaml
    /// </summary>
    public partial class GetAllClassTaskPage : Page
    {
        Teachers teachers;
        ClassGroup classGroup;
        bool type;
        public GetAllClassTaskPage(Teachers teachers)
        {
            InitializeComponent();
            this.teachers = teachers;
            type = true;
            DataContext = this;
        }
        public GetAllClassTaskPage(ClassGroup classGroup)
        {
            InitializeComponent();
            this.classGroup = classGroup;
            type = false;
            DataContext = this;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            classCbox.SelectedValuePath = "ID";
            classCbox.DisplayMemberPath = "Number";
            if (type)
                classCbox.ItemsSource = EnglishKlassBDEntities.GetContext().ClassGroup.
                    Where(c => c.Teacher_ID == teachers.ID).ToList();
            else
                classCbox.ItemsSource = EnglishKlassBDEntities.GetContext().ClassGroup.ToList();
     

            if (classGroup != null)
            {
                classCbox.SelectedItem = classGroup;
                classCbox.IsEnabled = false;
            }
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
            if (classCbox.SelectedValue == null ||
                taskCbox.SelectedValue == null)
            {
                MessageBox.Show("Не все поля были заполнены ", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int _id = (int)classCbox.SelectedValue;
            List<Students> students = 
                EnglishKlassBDEntities.GetContext().Students.Where(s=>s.Class_ID == _id).ToList();
            foreach(Students st in students)
            {
                AccountingForTasks taskAcc = new AccountingForTasks();
                taskAcc.Task_ID = (int)taskCbox.SelectedValue;
                taskAcc.Student_ID = st.ID;
                taskAcc.DateStart = startDPicer.SelectedDate;
                taskAcc.DateEnd = endDPicer.SelectedDate;

                EnglishKlassBDEntities.GetContext().AccountingForTasks.Add(taskAcc);
            }

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


    }
}


