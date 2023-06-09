﻿using System;
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
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        Students students;
        Teachers teachers;
        public TasksPage()
        {
            InitializeComponent();
        }
        public TasksPage(Students students)
        {
            InitializeComponent();
            this.students = students;

            studentSearchPanel.Visibility = Visibility.Collapsed;
            BtnEdit.Visibility = Visibility.Collapsed;
            BtnDel.Visibility = Visibility.Collapsed;
            BtnAdd.Visibility = Visibility.Collapsed;
        }
        public TasksPage(Teachers teachers)
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

            taskCbox.SelectedValuePath = "ID";
            taskCbox.DisplayMemberPath = "Name";
            taskCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Tasks.ToList();

            dataGrid.ItemsSource = getFilterDB();
        }
        public List<AccountingForTasks> getDB()
        {
            List<AccountingForTasks> tasks = new List<AccountingForTasks>();
            if (students != null)
                tasks = EnglishKlassBDEntities.GetContext().AccountingForTasks
                    .Where(t => t.Student_ID == students.ID).ToList();
            else if(teachers != null)
            {
                tasks = EnglishKlassBDEntities.GetContext().AccountingForTasks
    .Where(t => t.Students.ClassGroup.Teacher_ID == teachers.ID).ToList();
            }
            else
                tasks = EnglishKlassBDEntities.GetContext().AccountingForTasks.ToList();
            return tasks;
        }
        public List<AccountingForTasks> getFilterDB()
        {
            List<AccountingForTasks> tasks = getDB();
            if(DPicerStart.SelectedDate != null)
                tasks = tasks.Where(t=>t.DateStart >= DPicerStart.SelectedDate).ToList();
            if(DPicerEnd.SelectedDate != null)
                tasks = tasks.Where(t=>t.DateEnd <= DPicerEnd.SelectedDate).ToList();
            if (CBoxNumberClass.SelectedItem != null) 
            {
                int _id = (int)CBoxNumberClass.SelectedValue;
                tasks = tasks.Where(t => t.Students.ClassGroup.Number == _id).ToList();
            }
            if (CBoxCharClass.SelectedItem != null)
            {
                string _id = (string)CBoxCharClass.SelectedValue;
                tasks = tasks.Where(t => t.Students.ClassGroup.Name == _id).ToList();
            }
            if (taskCbox.SelectedItem != null)
            {
                int _id = (int)taskCbox.SelectedValue;
                tasks = tasks.Where(t => t.Task_ID == _id).ToList();
            }
            if(eventNameBox.Text.Length > 0)
                tasks = tasks.Where(t=> t.Students.Firstname.ToLower().Contains(eventNameBox.Text.ToLower()) ||
                      t.Students.Lastname.ToLower().Contains(eventNameBox.Text.ToLower()) ||
                      t.Students.Patronymic.ToLower().Contains(eventNameBox.Text.ToLower())).ToList();

            return tasks;
        }

        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            DPicerEnd.SelectedDate = null;
            DPicerStart.SelectedDate = null;
            CBoxNumberClass.SelectedItem = null;
            CBoxCharClass.SelectedItem = null;
            taskCbox.SelectedItem = null;
            eventNameBox.Text = "";

            dataGrid.ItemsSource = getFilterDB();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnglishKlassBDEntities.GetContext().AccountingForTasks.Remove(((AccountingForTasks)dataGrid.SelectedValue));
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
            NavigationService.Navigate(new GetStudentQuast(teachers));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedValue == null)
                return;
            NavigationService.Navigate(new GetStudentQuast((AccountingForTasks)dataGrid.SelectedValue, teachers));
        }



        private void CBoxNumberClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void CBoxCharClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void taskCbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void DPicerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void DPicerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void eventNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = getFilterDB();
        }

        private void viewTask_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedValue == null)
                return;
            if (students != null)
                NavigationService.Navigate(new AddEditTaskPage(((AccountingForTasks)dataGrid.SelectedValue).Tasks, true));
            else
                NavigationService.Navigate(new AddEditTaskPage(((AccountingForTasks)dataGrid.SelectedValue).Tasks));

        }
    }
}
