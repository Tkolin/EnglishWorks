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
        public AddEditTaskPage(Tasks task)
        {
            InitializeComponent();
            this.task = task;
            add = false;
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
            typeCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Tasks.ToList();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message.ToString(), "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}

