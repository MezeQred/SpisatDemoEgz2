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
using WpfApp5.DB;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAutorization_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = pbPassword.Password;
            using (var context = new testEntities())
            {
                var пользователь = context.Students.FirstOrDefault(u => u.FirstName == login.Trim() && u.LastName == password.Trim());
                PersonalCabWindow personalCabWindow = new PersonalCabWindow();
                if (пользователь != null)
                {
                    MessageBox.Show("Успешная авторизация!!");
                    personalCabWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!!");
                }
            }    
        }

        private void btnRegistr_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new testEntities()) 
            {
                if (context.Students.Any(u => u.FirstName == tbLoginReg.Text)) 
                {
                    MessageBox.Show("Пользователь с таким логином существует");
                }
                if (tbLoginReg.Text == null || pbPasswordReg.Password == null || pbPasswordReg.Password != pbPasswordRepeat.Password)
                {
                    MessageBox.Show("Заполните данные правильно!!");
                }
                else
                {
                    Students пользователь = new Students();
                    пользователь.FirstName = tbLoginReg.Text;
                    пользователь.LastName = pbPasswordReg.Password;
                    context.Students.Add(пользователь);
                    context.SaveChanges();
                    MessageBox.Show("Пользователь зарегистрирован");
                }
            }
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
