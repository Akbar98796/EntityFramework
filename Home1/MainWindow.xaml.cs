using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using WpfApp4.Data.Dbcontexts;
using WpfApp4.Data.Models;
using WpfApp4.Data.Service.Classes;
using WpfApp4.Data.Service.Interfaces;

namespace WpfApp4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window , INotifyPropertyChanged
{
    public ObservableCollection<CarModel> Cars { get; set; }
    public CarModel Car { get; set; }
    public List<CarModel> DeletedCars { get; set; }
    private readonly ICarDbContextManagerService _managerService;
    public MainWindow()
    {
        InitializeComponent();
        Cars = new();
        Car = new CarModel();
        DeletedCars = new();
        _managerService = new CarDbContextManagerService();
    }

    private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var ListCar = _managerService.GetListCar() ?? null;
            if (ListCar != null)
            {
                Cars = new(ListCar);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw;
        }
    }

    private void ButtonDelete_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (CarList.SelectedItem is not null)
            {
                DeletedCars.Add((CarList.SelectedItem as CarModel)!);
                Cars.RemoveAt(CarList.SelectedIndex);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw;
        }
     }

    private void ButtonSaveChanges_Click(object sender, RoutedEventArgs e)
    {
        try
        {
           _managerService.SaveCarsDb(Cars , DeletedCars);
            DeletedCars.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw;
        }
    }

    private void ButtonAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _managerService.AddCar(Car);
            Cars.Add(Car);
            Car = new();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw;
        }
    }
}