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
    /// Логика взаимодействия для AddEditTaskPage.xaml
    /// </summary>
    public partial class AddEditTaskPage : Page
    {
        Tasks task;
        bool add;
        bool isVrite;
        public AddEditTaskPage(Tasks task)
        {
            InitializeComponent();
            this.task = task;
            add = false;
            DataContext = this;
        }
        public AddEditTaskPage(Tasks task, bool isVrite)
        {
            InitializeComponent();
            this.task = task;
            this.isVrite = isVrite;
            DataContext = this;

        }
        public AddEditTaskPage()
        {
            InitializeComponent();
            this.task = new Tasks();
            add = true;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            typeCbox.SelectedValuePath = "ID";
            typeCbox.DisplayMemberPath = "Name";
            typeCbox.ItemsSource = EnglishKlassBDEntities.GetContext().TypeTasks.ToList();

            if (!add)
            {
                nameTBox.Text = task.Name;
                contentTBox.Text = task.ContentTasks;
                descTBox.Text = task.Description;
                typeCbox.SelectedItem = task.TypeTasks;

                nameTBox.IsEnabled = false;
                contentTBox.IsEnabled = false;
                descTBox.IsEnabled = false;
                typeCbox.IsEnabled = false;
                saveBtn.Visibility = Visibility.Collapsed;
            }

        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTBox.Text.Length == 0 || contentTBox.Text.Length == 0 ||
                typeCbox.SelectedValue == null)
            {
                MessageBox.Show("Не все поля были заполнены ", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            task.Name = nameTBox.Text;
            task.ContentTasks = contentTBox.Text;
            task.Description = descTBox.Text;
            task.Type_ID = (int)typeCbox.SelectedValue;

            if (add)
                EnglishKlassBDEntities.GetContext().Tasks.Add(task);

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


    }
}

