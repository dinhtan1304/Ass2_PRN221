using Q1.Models;
using Q1.Repository;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEmpRepository empRepository;
        public MainWindow(IEmpRepository repo)
        {
            empRepository = repo;
            InitializeComponent();
            LoadEmpList();
        }
        private Employee GetEmpObject()
        {

            string gender = "Male";
            if ((bool)RadioFemale.IsChecked)
            {
                gender = "Female";
            }
            Employee employee = null;
            try
            {
                employee = new Employee()
                {
                    Id = int.Parse(txtEmployeeId.Text),
                    Name = txtEmployeeName.Text,
                    Gender = gender,
                    Dob = DateTime.Parse(txtDate.Text),
                    Phone = txtPhone.Text,
                    Idnumber = txtIdNumber.Text
                };
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message, "Get employee");
            }
            return employee;
        }
        private Employee GetEmpObjects()
        {

            string gender = "Male";
            if ((bool)RadioFemale.IsChecked)
            {
                gender = "Female";
            }
            Employee employee = null;
            try
            {
                employee = new Employee()
                {
                    Name = txtEmployeeName.Text,
                    Gender = gender,
                    Dob = DateTime.Parse(txtDate.Text),
                    Phone = txtPhone.Text,
                    Idnumber = txtIdNumber.Text
                };
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message, "Get employee");
            }
            return employee;
        }
        public void LoadEmpList()
        {
            lvEmployee.ItemsSource = empRepository.GetEmps();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadEmpList();
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message, "Load employee list");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = GetEmpObject();
                empRepository.UpdateEmp(emp);
                LoadEmpList();
                MessageBox.Show($"{emp.Name} updated successfully", "Update employee");
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message, "Update employee");

            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = GetEmpObjects();
                empRepository.InsertEmp(emp);
                LoadEmpList();
                MessageBox.Show($"{emp.Name} insertsted successfully", "Insert employee");
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message, "Insert employee");

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = GetEmpObject();
                empRepository.DeleteEmp(emp);
                LoadEmpList();
                MessageBox.Show($"{emp.Name} deleted successfully", "Delete employee");
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message, "Delete employee");

            }
        }

        private void lvEmployee_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var gender = ((Employee)item).Gender;
                if (!string.IsNullOrEmpty(gender))
                {
                    if (gender.ToLower() == "female")
                    {
                        RadioFemale.IsChecked = true;
                    }
                    else
                    {
                        RadioMale.IsChecked = true;
                    }
                }
            }
        }
    }
}
