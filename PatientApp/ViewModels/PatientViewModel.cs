using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using PatientApp.Helpers;
using PatientApp.Models;
using PatientApp.Models.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PatientApp.ViewModels
{
    public class PatientViewModel : BaseSingleViewModel<Patient>
    {
        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (FirstName != value)
                {
                    Model.FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }
        public string LastName
        {
            get => Model.LastName;
            set
            {
                if (LastName != value)
                {
                    Model.LastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }
        public string Pesel
        {
            get => Model.Pesel;
            set
            {
                if (Pesel != value)
                {
                    Model.Pesel = value;
                    OnPropertyChanged(() => Pesel);
                }
            }
        }
        public string City
        {
            get => Model.City;
            set
            {
                if (City != value)
                {
                    Model.City = value;
                    OnPropertyChanged(() => City);
                }
            }
        }
        public string Street
        {
            get => Model.Street;
            set
            {
                if (Street != value)
                {
                    Model.Street = value;
                    OnPropertyChanged(() => Street);
                }
            }
        }
        public string Zipcode
        {
            get => Model.Zipcode;
            set
            {
                if (Zipcode != value)
                {
                    Model.Zipcode = value;
                    OnPropertyChanged(() => Zipcode);
                }
            }
        }

        public PatientViewModel()
        {

        }

        protected override DbSet<Patient> GetDBTable()
        {
            return Database.Patients;
        }

        protected override void Edit(EdditorMessenger<Patient> message)
        {
            Model = Database.Patients.FirstOrDefault(item => item.Id == message.Item.Id && item.IsActive == true);
            Refresh();
        }

        protected override Patient InitializeModel()
        {
            return new Patient()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Pesel = string.Empty,
                City = string.Empty,
                Street = string.Empty,
                Zipcode = string.Empty,
                IsActive = true,
                CreatedAt = DateTime.Now,
            };
        }
    }
} 





