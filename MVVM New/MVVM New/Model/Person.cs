using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_New.Model
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _username;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _department;

        public string Username 
        {
            get { return _username; }
            set { _username = value;  OnPropertyChanged("Username"); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged("FirstName"); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("LastName"); }
        }

        public string Department
        {
            get { return _department; }
            set { _department = value; OnPropertyChanged("Department"); }
        }

        public void OnPropertyChanged(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Data Error Info
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string PropertyName]
        {
            get
            {
                string result = string.Empty;

                switch (PropertyName)
                {
                    case "Username":
                        if (string.IsNullOrEmpty(Username))
                        {
                            result = "Username is required";
                        }
                        break;

                    case "Password":
                        if (string.IsNullOrEmpty(Password))
                        {
                            result = "Password is required";
                        }
                        break;

                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "First Name is required";
                        }
                        break;

                    case "Department":
                        if (string.IsNullOrEmpty(Department))
                        {
                            result = "Department is required";
                        }
                        break;

                    case "LastName":
                        if (string.IsNullOrEmpty(LastName))
                        {
                            result = "Last Name is required";
                        }
                        break;
                }
                return result;

            }
        }
    }
}
