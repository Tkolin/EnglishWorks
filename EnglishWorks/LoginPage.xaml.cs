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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        List<Users> users;
        public LoginPage()
        {
            InitializeComponent();
            users = EnglishKlassBDEntities.GetContext().Users.ToList();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loginSearch() != -1)
                NavigationService.Navigate(new MenyPage());
            else
                MessageBox.Show("Ошибка входа");
        }


        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private int loginSearch()
        {
            foreach (Users checkUser in users)
            {
                if (checkUser.Login.ToLower() == TBoxLogin.Text.ToLower() &&
                    checkUser.Password == TBoxPassword.Text)
                {
                    return (int)checkUser.Role_ID;
                }

            }
            return -1;
        }
    }
}
