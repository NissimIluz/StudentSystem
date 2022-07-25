using Constracts.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Services.Services
{
    public class ConvertorService : IConvertorService
    {
        public List<T> TableConvert<T>(DataTable dataTable, PropertyInfo[] properties = null)

        {
            List<T> _retVal = new List<T>();
            if (properties == null)
            {
                properties = typeof(T).GetProperties();
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                T _nowObject = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo property in properties)
                {
                    string _data = dataTable.Rows[i][property.Name].ToString().Replace(@"-", "");
                    if (_data != null && _data != "")

                    {
                        setObjectPropertyValue(ref _nowObject, property, _data);
                    }
                }
                _retVal.Add((T)_nowObject);
            }
            return _retVal;
        }
        private void setObjectPropertyValue<T>(ref T objectToSet, PropertyInfo property, string value)
        {
            try
            {
                PropertyInfo _propertyInfo = objectToSet.GetType().GetProperty(property.Name);
                if (_propertyInfo == null)
                {
                    errorHandler(ref objectToSet, $"Property :{property.Name} not found");
                }
                else
                {
                    Type _propertType = Nullable.GetUnderlyingType(_propertyInfo.PropertyType) ?? _propertyInfo.PropertyType;
                    switch (_propertType.Name)
                    {
                        case nameof(Boolean):
                            _propertyInfo.SetValue(objectToSet, value == "1", null);
                            break;
                        case nameof(String):
                            _propertyInfo.SetValue(objectToSet, System.Convert.ChangeType(value, _propertType), null);
                            break;
                        case nameof(DateTime):
                            _propertyInfo.SetValue(objectToSet, System.Convert.ChangeType(value, _propertType), null);
                            break;
                        case nameof(Int32):
                            _propertyInfo.SetValue(objectToSet, Int32.Parse(value), null);
                            break;
                        case nameof(Decimal):
                            _propertyInfo.SetValue(objectToSet, Decimal.Parse(value), null);
                            break;
                        case nameof(Double):
                            _propertyInfo.SetValue(objectToSet, Double.Parse(value), null);
                            break;
                        default:
                            try
                            {
                                System.Enum.Parse(_propertType, value);
                                _propertyInfo.SetValue(objectToSet, System.Enum.Parse(_propertType, value), null);
                            }
                            catch (Exception ex)
                            {
                                errorHandler(ref objectToSet, ex.Message);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                errorHandler(ref objectToSet, ex.Message);
            }
        }

        private bool tryGetProperty(Type type, string propertyName, out PropertyInfo property)
        {
            try
            {
                property = type.GetProperty(propertyName);
                return property != null;
            }
            catch
            {
                property = null;
                return false;
            }
        }

        private void errorHandler<T>(ref T objectRef, string message)
        {
            Console.WriteLine(message);
            if (tryGetProperty(typeof(T), "Status", out PropertyInfo _propertyStatus))
            {
                _propertyStatus.SetValue(objectRef, false);
            }
            if (tryGetProperty(typeof(T), "Message", out PropertyInfo _propertyMessage))
            {
                string _oldyMessage = (string)_propertyMessage.GetValue(objectRef);
                message = _oldyMessage == null ? (message + ". ") : string.Format(_oldyMessage + message + ". ");
                _propertyMessage.SetValue(objectRef, message);
            }
        }

        public List<Student> ConvertToStudents(string data)
        {
            try
            {
                data = data.Trim();
                List<Student> students = new List<Student>();
                string[] studentsStringArray = data.Split("\r\n");

                for(int i =1; i< studentsStringArray.Length; i++)
                {

                    string[] studentStringArray = studentsStringArray[i].Split(",");
                    if (studentStringArray.Length == 12)
                    {
                        Student student = new Student()
                        {
                            Id = int.Parse(studentStringArray[0]),
                            School = studentStringArray[1],
                            LastName = studentStringArray[2],
                            FirstName = studentStringArray[3],
                            MaleOrFemale = int.Parse(studentStringArray[4]),
                            HomePhone = studentStringArray[5],
                            MobilePhone = studentStringArray[6],
                            Email = studentStringArray[7],
                            BirthDate = DateTime.Parse(studentStringArray[8]),
                            CountryOfBirth = studentStringArray[9],
                            ImmigrationDate = DateTime.Parse(studentStringArray[10]),
                            Nation = studentStringArray[11]
                        };
                        students.Add(student);
                    }
                }
                return students;
            }
            catch
            {
                return null;
            }
        }
    }
}