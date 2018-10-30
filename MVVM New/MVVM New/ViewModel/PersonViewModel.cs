using MVVM_New.Command;
using MVVM_New.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_New.ViewModel
{
    class PersonViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// List for person detail
        /// </summary>
        private ObservableCollection<Person> _personDetailList;

        public ObservableCollection<Person> PersonDetailList
        {
            get { return _personDetailList; }
            set { _personDetailList = value; NotifyChangeProperty("PersonDetailList"); }
        }
        /// <summary>
        /// Already existing detail of employee as database
        /// </summary>
        public PersonViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            Person = new Person();
            PersonDetailList = new ObservableCollection<Person>();
            PersonDetailList.Add(new Person() { Username = "rhtgore83", Password = "msd", FirstName = "Rohit", LastName = "Gore", Department = "AFC BO" });
            PersonDetailList.Add(new Person() { Username = "rishabh", Password = "barua", FirstName = "Rishabh", LastName = "Barua", Department = "AFC BO" });
        }

        /// <summary>
        /// Selection of person from the list view
        /// </summary>
        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                NotifyChangeProperty("SelectedPerson");
                
                
            }
        }


        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { _person = value; NotifyChangeProperty("Person"); }
        }

        /// <summary>
        /// Submit command
        /// </summary>
        private ICommand _addCommand;

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    this._addCommand = new RelayCommand(AddMethod, CanAddExecute);
                }
                return _addCommand;
            }
        }

        private bool CanAddExecute(object arg)
        {
            if (string.IsNullOrEmpty(SelectedPerson.FirstName) || string.IsNullOrEmpty(SelectedPerson.LastName) || string.IsNullOrEmpty(SelectedPerson.Username) || string.IsNullOrEmpty(SelectedPerson.Password) || string.IsNullOrEmpty(SelectedPerson.Department))
            {
                return false;
            }
            else
            {
                return true;
            }
            //if (PersonDetailList.Contains(new Person { Username = "", Password = "", FirstName = "", LastName = "", Department = "" }))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            //return true;
        }

        private void AddMethod(object obj)
        {
            PersonDetailList.Add(Person);
            Person = new Person();
            SelectedPerson = (Person.Username==null)? PersonDetailList.LastOrDefault():null;
            
           
        }
        /// <summary>
        /// Deleting the selected item from the list
        /// </summary>
        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(DeleteMethod, CanDeleteExecute);
                }
                return _deleteCommand;
            }
        }

        private bool CanDeleteExecute(object arg)
        {
            return PersonDetailList.Count > 0 && SelectedPerson != null;
        }

        private void DeleteMethod(object obj)
        {
            var deletedPerson = PersonDetailList.Where(c => c.Username == SelectedPerson.Username).FirstOrDefault();
            PersonDetailList.Remove(deletedPerson);
            SelectedPerson = (PersonDetailList.Count > 0) ? PersonDetailList.FirstOrDefault() : null;
        }

        /// <summary>
        /// update the existing person's detail.
        /// </summary>
        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(SaveMethod, CanSaveExecute);
                }
                return _saveCommand;
            }
        }

        private bool CanSaveExecute(object arg)
        {
            if (string.IsNullOrEmpty(SelectedPerson.FirstName) || string.IsNullOrEmpty(SelectedPerson.LastName) || string.IsNullOrEmpty(SelectedPerson.Username) || string.IsNullOrEmpty(SelectedPerson.Password) || string.IsNullOrEmpty(SelectedPerson.Department))
            {
                return false;
            }
            else
            {
                return true;
            }
            //if (string.IsNullOrEmpty(Person.FirstName) || string.IsNullOrEmpty(Person.LastName) || string.IsNullOrEmpty(Person.Username) || string.IsNullOrEmpty(Person.Password) || string.IsNullOrEmpty(Person.Department))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            //return PersonDetailList.Count > 0 && SelectedPerson != null;
            //return false;
        }

        private void SaveMethod(object obj)
        {
            //Person = new Person();
            var deletedPerson = PersonDetailList.Where(c => c.Username == SelectedPerson.Username).FirstOrDefault();
            deletedPerson.Username = SelectedPerson.Username;
            deletedPerson.FirstName = SelectedPerson.FirstName;
            deletedPerson.Password = SelectedPerson.Password;
            deletedPerson.LastName = SelectedPerson.LastName;
            deletedPerson.Department = SelectedPerson.Department;
            //PersonDetailList.Insert(PersonDetailList.IndexOf(deletedPerson),Person);
            //PersonDetailList.Remove(deletedPerson);
            //SelectedPerson = (PersonDetailList.Count > 0) ? PersonDetailList.FirstOrDefault() : null;
        }

        /// <summary>
        /// Notify to the UI as property changed to update the detail
        /// </summary>
        /// <param name="Property"></param>
        public void NotifyChangeProperty(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
      
    }
}
