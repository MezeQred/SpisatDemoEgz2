using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.RightsManagement;
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
using System.Data.Entity.Migrations;

namespace WpfApp5.DB
{
    /// <summary>
    /// Логика взаимодействия для PersonalCabWindow.xaml
    /// </summary>
    public partial class PersonalCabWindow : Window
    {
        public PersonalCabWindow()
        {
            InitializeComponent();
            try
            {
                using (var _context = new testEntities())
                {
                    dgStud.ItemsSource = _context.Students.ToList();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnDell_Click(object sender, RoutedEventArgs e)
        {        
            
                using (var context = new testEntities())
                {
                    var removeRows = dgStud.SelectedItems.Cast<Students>().ToList();
                    if (removeRows.Count() == 0)
                    {
                        MessageBox.Show("Нужно выбрать строки!");
                        return;
                    }
                    if (MessageBox.Show($"Точно удалить:{removeRows.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var UsersID = removeRows.Select(p => p.StudentID).ToList();
                            var RemoveUsers = context.Students.Where(u => UsersID.Contains(u.StudentID)).ToList();
                            context.Students.RemoveRange(RemoveUsers);
                            context.SaveChanges();
                            MessageBox.Show("Пользоователи Удалены!!!");
                            dgStud.ItemsSource = context.Students.ToList();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    }
                }                   
        }
        private void tbSeatch_TextChanged(object sender, TextChangedEventArgs e)
        {
                using (var context = new testEntities())
                {
                    dgStud.ItemsSource = context.Students.Where(a => a.FirstName.Contains(tbSeatch.Text) || a.LastName.Contains(tbSeatch.Text)).ToList();
                }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            redWindow redWindow = new redWindow(null);
            redWindow.Show();
            Close();
        }

        private void btnred_Click(object sender, RoutedEventArgs e)
        {
            redWindow redWindow = new redWindow((sender as Button).DataContext as Students);
            redWindow.Show(); 
            Close();
        }
    }
}
