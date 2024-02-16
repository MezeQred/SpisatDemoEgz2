     <Grid>
     <TabControl>
         <TabItem Header="Авторизация">
             <Grid>
                 <Grid.ColumnDefinitions>
                     <ColumnDefinition/>
                     <ColumnDefinition/>
                 </Grid.ColumnDefinitions>
                 <Grid.RowDefinitions>
                     <RowDefinition Height="75"/>
                     <RowDefinition/>
                     <RowDefinition/>
                 </Grid.RowDefinitions>
                 <Label VerticalAlignment="Center" HorizontalAlignment="Center" Content="Авторизация" Grid.ColumnSpan="2" FontSize="40"/>
                 <StackPanel Grid.Row="1" VerticalAlignment="Center" HorizontalAlignment="Center">
                     <Label Content="Логин:" FontSize="30"/>
                     <Label Content="Пароль:" FontSize="30"/>
                 </StackPanel>
                 <StackPanel Grid.Row="1" Grid.Column="1" VerticalAlignment="Center" HorizontalAlignment="Center">
                     <TextBox x:Name="tbLogin" FontSize="30"/>
                     <PasswordBox x:Name="pbPassword" FontSize="30" Width="150" Margin="0,5,0,0"/>
                 </StackPanel>
                 <Button Height="50" x:Name="btnAutorization" Content="Войти" HorizontalAlignment="Center" VerticalAlignment="Center" Grid.ColumnSpan="2" FontSize="30" Grid.Row="2" Click="btnAutorization_Click"/>
             </Grid>
         </TabItem>
         <TabItem Header="Регистрация">
             <Grid>
                 <Grid.ColumnDefinitions>
                     <ColumnDefinition/>
                     <ColumnDefinition/>
                 </Grid.ColumnDefinitions>
                 <Grid.RowDefinitions>
                     <RowDefinition Height="75"/>
                     <RowDefinition/>
                     <RowDefinition/>
                 </Grid.RowDefinitions>
                 <Label VerticalAlignment="Center" HorizontalAlignment="Center" Content="Регистрация" Grid.ColumnSpan="2" FontSize="40"/>
                 <StackPanel Grid.Row="1" VerticalAlignment="Center" HorizontalAlignment="Center">
                     <Label Content="Логин:" FontSize="30"/>
                     <Label Content="Пароль:" FontSize="30"/>
                     <Label Content="Повторите:" FontSize="30"/>
                 </StackPanel>
                 <StackPanel Grid.Row="1" Grid.Column="1" VerticalAlignment="Center" HorizontalAlignment="Center">
                     <TextBox x:Name="tbLoginReg" FontSize="30"/>
                     <PasswordBox x:Name="pbPasswordReg" FontSize="30" Width="150" Margin="0,5,0,0"/>
                     <PasswordBox x:Name="pbPasswordRepeat" FontSize="30" Width="150" Margin="0,5,0,0"/>
                 </StackPanel>
                 <Button Height="50" x:Name="btnRegistr" Content="Зарегистрироваться" HorizontalAlignment="Center" VerticalAlignment="Center" Grid.ColumnSpan="2" FontSize="30" Grid.Row="2" Click="btnRegistr_Click"/>
             </Grid>
         </TabItem>
     </TabControl>
    </Grid>
                  
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
}

    <Grid>
     <TabControl Grid.Row="1" x:Name="tcImtex">
         <TabItem Header="Студенты" x:Name="tiDgstud">
             <DataGrid Grid.Row="1" AutoGenerateColumns="False" x:Name="dgStud" IsReadOnly="True">
                 <DataGrid.Columns>
                     <DataGridTextColumn Header="Имя" Binding="{Binding FirstName}"/>
                     <DataGridTextColumn Header="Фамилия" Binding="{Binding LastName}"/>
                     <DataGridTextColumn Header="Фамилия" Binding="{Binding StudentID}"/>
                     <DataGridTemplateColumn>
                         <DataGridTemplateColumn.CellTemplate>
                             <DataTemplate>
                                 <Button x:Name="btnred" Content="Редач)" Click="btnred_Click" FontSize="10"/>
                             </DataTemplate>
                         </DataGridTemplateColumn.CellTemplate>
                     </DataGridTemplateColumn>
                 </DataGrid.Columns>
             </DataGrid>
         </TabItem>
     </TabControl>
     <StackPanel Orientation="Horizontal">
         <Label Content="Поиск:" HorizontalAlignment="Left" VerticalAlignment="Center"/>
         <TextBox x:Name="tbSeatch" Grid.Row="0" HorizontalAlignment="Left" VerticalAlignment="Center" FontSize="20" Width="200" TextChanged="tbSeatch_TextChanged"/>
     </StackPanel>
     <Button x:Name="btnDell" Content="Удалить!!" HorizontalAlignment="Right" VerticalAlignment="Center" Click="btnDell_Click" Grid.Row="2" Margin="5,5,50,5"/>
     <Button x:Name="btnAdd" Content="Добавить" HorizontalAlignment="Left" VerticalAlignment="Center" Click="btnAdd_Click" Grid.Row="2" Margin="50,5,5,5"/>
     <Grid.RowDefinitions>
         <RowDefinition Height="50"/>
         <RowDefinition/>
         <RowDefinition Height="75"/>
     </Grid.RowDefinitions>
    </Grid>


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

    <Grid Cursor="">
    <Grid.ColumnDefinitions>
        <ColumnDefinition/>
        <ColumnDefinition/>
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition/>
        <RowDefinition Height="60"/>
    </Grid.RowDefinitions>
    <Button x:Name="btnBack" Content="Назад" HorizontalAlignment="Right" VerticalAlignment="Top" Click="btnBack_Click" Grid.Column="1" Margin="5,5,5,5"/>
    <Button x:Name="SaveBack" Content="Сохранить" HorizontalAlignment="Center" VerticalAlignment="Center" Click="SaveBack_Click" Grid.Row="2" Grid.ColumnSpan="2" />
    <StackPanel VerticalAlignment="Center" HorizontalAlignment="Right">
        <Label Content="ID:" FontSize="25"/>
        <Label Content="Имя:" FontSize="25"/>
        <Label Content="Фамилия:" FontSize="25"/>
    </StackPanel>
    <StackPanel VerticalAlignment="Center" HorizontalAlignment="Left" Grid.Column="1">
        <TextBox Text="{Binding StudentID}" x:Name="tbID" Width="150" FontSize="25"/>
        <TextBox Text="{Binding FirstName}" x:Name="tbName" Width="150" FontSize="25"/>
        <TextBox Text="{Binding LastName}" x:Name="tbFam" Width="150" FontSize="25"/>
    </StackPanel>
    </Grid>

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
