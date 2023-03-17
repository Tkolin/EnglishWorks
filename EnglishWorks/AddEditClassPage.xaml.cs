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
    /// Логика взаимодействия для AddEditClassPage.xaml
    /// </summary>
    public partial class AddEditClassPage : Page
    {
        ClassGroup classGroup;
        bool add;
        public AddEditClassPage(ClassGroup classGroup)
        {
            InitializeComponent();
            this.classGroup = classGroup;
            add = false;
        }
        public AddEditClassPage()
        {
            InitializeComponent();
            this.classGroup = new ClassGroup();
            add = true;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            teacherCbox.SelectedValuePath = "ID";
            teacherCbox.DisplayMemberPath = "Lastname";
            teacherCbox.ItemsSource = EnglishKlassBDEntities.GetContext().Teachers.ToList();
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameClassTBox.Text.Length == 0 || numberClassTBox.Text.Length == 0)
            {
                MessageBox.Show("Не все поля были заполнены ", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            classGroup.Name = nameClassTBox.Text;
            classGroup.Number = Convert.ToInt32(numberClassTBox.Text);
            classGroup.Teacher_ID = (int)teacherCbox.SelectedValue;

            if (add)
                EnglishKlassBDEntities.GetContext().ClassGroup.Add(classGroup);
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
