using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Shapes;
using WpfApp5.DB;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для redWindow.xaml
    /// </summary>
    public partial class redWindow : Window
    {
        private Students _students = new Students();
        public redWindow(Students students)
        {
            InitializeComponent();
            if(_students != null)
            {
                _students = students;
            }    
            DataContext = _students;
            using (var context = new testEntities())
            {
             var stud = context.Students.ToList();
            }
        
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PersonalCabWindow personalCabWindow = new PersonalCabWindow();
            personalCabWindow.Show();
            Close();
        }

        private void SaveBack_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new testEntities())
            {
                if (_students.StudentID == 0)
                {
                    context.Students.Add(_students);
                    context.SaveChanges();
                    MessageBox.Show("fds14234123rfffrfrqqwdfdas");
                    PersonalCabWindow personalCabWindow = new PersonalCabWindow();
                    personalCabWindow.Show();
                    Close();
                }
            }
        }
    }
}
