using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PatientApp.Helpers;
using PatientApp.Models;
using PatientApp.Models.Contexts;
using PatientApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PatientApp.ViewModels
{
    public class PatientsViewModel : INotifyPropertyChanged
    {
        public DatabaseContext Database { get; set; }

        private ObservableCollection<Patient> _Models;
        public ObservableCollection<Patient> Models
        {
            get => _Models;
            set
            {
                if (_Models != value)
                {
                    _Models = value;
                    OnPropertyChanged(() => Models);
                }
            }
        }


        private Patient? _SelectedItem;
        public Patient? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(() => SelectedItem);
                }
            }
        }


        private string? _SearchInput;
        public string? SearchInput
        {
            get => _SearchInput;
            set
            {
                if (_SearchInput != value)
                {
                    _SearchInput = value;
                    OnPropertyChanged(() => SearchInput);
                }
            }
        }


        public string SearchColumn { get; set; }
        public string SortColumn { get; set; }
        public bool SortDescending { get; set; }
        public List<ComboBoxHelper> SearchandOrderColumns { get; set; }

        public ICommand GetModelsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand MenuButtonCommand { get; set; }

        private UserControl currentView;
        public UserControl CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
        public PatientsViewModel()
        {
            Database = new();
            CurrentView = new PatientView();
            GetSearchModels();
            GetModelsCommand = new BaseCommand(() => GetSearchModels());
            DeleteCommand = new BaseCommand(() => Delete());
            EditCommand = new BaseCommand(() => Edit());
            MenuButtonCommand = new BaseCommand(() => Refresh());
            SearchandOrderColumns = GetSearchColumns();
            WeakReferenceMessenger.Default.Register<RefreshMessage<Patient>>(this, (recipient, message) => Refresh());
            WeakReferenceMessenger.Default.Register<ClearMessenger<Patient>>(this, (recipient, message) => RefreshAdd());
        }

        public void RefreshAdd()
        {
            CurrentView = new PatientView();
        }
        public IQueryable<Patient> GetModels()
        {
           return new DatabaseContext().Patients.Where(item => item.IsActive == true);
        }
        public void Delete()
        {
            if (SelectedItem != null)
            {
                DeleteFromDatabase();
                Models.Remove(SelectedItem);
            }

        }
        public void Edit()
        {
            WeakReferenceMessenger.Default.Send(new EdditorMessenger<Patient>(1, SelectedItem));
        }

        private void GetSearchModels()
        {
           IQueryable<Patient> modelTypes = GetModels();
           if (!(string.IsNullOrEmpty(SearchInput)))
           {
               modelTypes = Search(modelTypes);
           }
           modelTypes = Sort(modelTypes);
           Models = new ObservableCollection<Patient>(modelTypes);
        }

        public IQueryable<Patient> Search(IQueryable<Patient> models)
        {
            switch(SearchColumn)
            {
                case nameof(Patient.FirstName):
                    return models.Where(item => item.FirstName.Contains(SearchInput));
                case nameof(Patient.LastName):
                    return models.Where(item => item.LastName.Contains(SearchInput));
                case nameof(Patient.Pesel):
                    return models.Where(item => item.Pesel.Contains(SearchInput));
                case nameof(Patient.City):
                    return models.Where(item => item.City.ToString() == SearchInput);
                case nameof(Patient.Street):
                    return models.Where(item => item.Street.Contains(SearchInput));
                case nameof(Patient.Zipcode):
                    return models.Where(item => item.Zipcode.Contains(SearchInput));
                default:
                    return models;
            }
        }

        protected IQueryable<Patient> Sort(IQueryable<Patient> models)
        {
            switch (SortColumn)
            {
                case nameof(Patient.FirstName):
                    return SortDescending ? models.OrderByDescending(item => item.FirstName) : models.OrderBy(item => item.FirstName);
                case nameof(Patient.LastName):
                    return SortDescending ? models.OrderByDescending(item => item.LastName) : models.OrderBy(item => item.LastName);
                case nameof(Patient.Pesel):
                    return SortDescending ? models.OrderByDescending(item => item.Pesel) : models.OrderBy(item => item.LastName);
                case nameof(Patient.City):
                    return SortDescending ? models.OrderByDescending(item => item.City) : models.OrderBy(item => item.LastName);
                case nameof(Patient.Street):
                    return SortDescending ? models.OrderByDescending(item => item.Street) : models.OrderBy(item => item.LastName);
                case nameof(Patient.Zipcode):
                    return SortDescending ? models.OrderByDescending(item => item.Zipcode) : models.OrderBy(item => item.LastName);
                default: return models;
            }
        }
        public List<ComboBoxHelper> GetSearchColumns()
        {
            return new()
            {
                new(1,nameof(Patient.FirstName)),
                new(2,nameof(Patient.LastName)),
                new(3,nameof(Patient.Pesel)),
                new(4,nameof(Patient.City)),
                new(5,nameof(Patient.Street)),
                new(5,nameof(Patient.Zipcode))
            };
        }
        public void DeleteFromDatabase()
        {
            if (SelectedItem != null)
            {
                Patient? model = Database.Patients.FirstOrDefault(item => item.IsActive == true && item.Id == SelectedItem.Id);
                if (model != null)
                {
                    model.IsActive = false;
                    Database.SaveChanges();
                }
            }
        }

        public void Refresh()
        {
            Models = new ObservableCollection<Patient>(GetModels());
            CurrentView = new PatientView();
        }
        #region PropertyChanged
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)

            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
