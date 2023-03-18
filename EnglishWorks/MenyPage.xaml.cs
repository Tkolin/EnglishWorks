using Microsoft.Office.Interop.Excel;
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
    public partial class MenyPage : System.Windows.Controls.Page
    {
        Users users;
        Students students;
        Teachers teacher;
        public MenyPage(Users users)
        {
            InitializeComponent();
            this.users = users;
            roleTBlock.Text = "Для пользователя: "+users.Roles.Name;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string id = users.Login;
            switch (users.Role_ID)
            {
                case 0:
                    getClassTaskBtn.Visibility = Visibility.Collapsed;
                    getStudentTaskBtn.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Добро пожаловать администратор!");
                    break;
                case 1:
                    if (EnglishKlassBDEntities.GetContext().Teachers.Where(t => t.Users_ID == id).ToList().Count() == 0)
                    {
                        NavigationService.GoBack();
                        MessageBox.Show("Данная учётная запись не приаязана к учителю!");
                        return;
                    }
                    teacher = EnglishKlassBDEntities.GetContext().Teachers.Where(t=> t.Users_ID == id).First();
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
                    otchetForTaskBtn.Visibility= Visibility.Collapsed;
                    if (EnglishKlassBDEntities.GetContext().Students.Where(t => t.Users_ID == id).ToList().Count() == 0)
                    {
                        NavigationService.GoBack();
                        MessageBox.Show("Данная учётная запись не приаязана к ученику!");
                        return;
                    } 
                    students = EnglishKlassBDEntities.GetContext().Students.Where(t => t.Users_ID == id).First();
                    break;
            }
        }


        private void classBtn_Click(object sender, RoutedEventArgs e)
        {
            if (teacher != null)
                NavigationService.Navigate(new ClassPage(teacher));
            else 
                NavigationService.Navigate(new ClassPage());

        }

        private void studentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (teacher != null)
                NavigationService.Navigate(new StudentsPage(teacher));
            else if (students != null)
                NavigationService.Navigate(new StudentsPage(students));
            else
                NavigationService.Navigate(new StudentsPage());

        }

        private void taskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (teacher != null)
                NavigationService.Navigate(new TasksPage(teacher));
            else if (students != null)
                NavigationService.Navigate(new TasksPage(students));
            else
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

        private void otchetForTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            getOtchetTask();
        }
        public void getOtchetTask()
        {
            //подключение таблиц
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;

            //создание страницы
            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];

            //18
            //форматирование текста
            ws.StandardWidth = 18;

            ws.Range["B1:F1"].Merge(); ws.Range["B2:F2"].Merge(); ws.Range["C4:F4"].Merge();
            ws.Range["B1"].Value = "Расписание заданий учителя";
            if(teacher != null)
            ws.Range["B2"].Value = teacher.Firstname+" "+ teacher.Lastname+" " + teacher.Patronymic;
            ws.Range["B4"].Value = "На дату:";
            ws.Range["C4"].Value = DateTime.Now.Year+"."+DateTime.Now.Month+"."+DateTime.Now.Day;

            ws.Range["A6"].Value = "Класс:"; ws.Range["A6"].ColumnWidth = 6;
            ws.Range["B6"].Value = "Имя студента"; 
            ws.Range["C6"].Value = "Фамилия студента";
            ws.Range["D6"].Value = "Отчество студента";
            ws.Range["E6"].Value = "Наименование задания"; ws.Range["E6"].ColumnWidth = 23;
            ws.Range["F6"].Value = "Дата выдачи"; ws.Range["f6"].ColumnWidth = 12;
            ws.Range["G6"].Value = "Дата сдачи"; ws.Range["g6"].ColumnWidth = 12;

            //Строки - начала и конца таблицы данных
            int startRow = 7;
            int endRow = 7;


            //Спиоск классов у данного руководителя
            List<ClassGroup> clasGrupList = new List<ClassGroup>();
            if (teacher != null)
            clasGrupList = 
                EnglishKlassBDEntities.GetContext().ClassGroup
                .Where(cg => cg.Teacher_ID == teacher.ID).ToList();
            else
                clasGrupList = 
                    EnglishKlassBDEntities.GetContext().ClassGroup.ToList();


            foreach (ClassGroup cl in clasGrupList)
            {

                //Список актуальных заданий в данном классе 
                List<AccountingForTasks> accForTask = EnglishKlassBDEntities.GetContext().AccountingForTasks
                    .Where(t => t.Students.ClassGroup.ID == cl.ID)
                    .Where(ac => ac.DateStart > DateTime.Now).ToList();

                if (accForTask.ToList().Count == 0)
                    continue;

                //запись данных
                ws.Range["A" + endRow].Value = cl.Name + cl.Number.ToString();
                foreach(AccountingForTasks aft in accForTask)
                {
                    ws.Range["B"+endRow].Value = aft.Students.Firstname;
                    ws.Range["C" + endRow].Value = aft.Students.Lastname;
                    ws.Range["D" + endRow].Value = aft.Students.Patronymic;
                    ws.Range["E" + endRow].Value = aft.Tasks.Name;
                    ws.Range["F" + endRow].Value = aft.DateStart;
                    ws.Range["G" + endRow].Value = aft.DateEnd;
                    endRow++;
                }
                
            }
        }
    }
}
