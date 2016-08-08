using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace LEAP.Apps.ReadViewerDesktop.Core.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _values;

        protected BaseViewModel()
        {
            _values = new Dictionary<string, object>();
        }
        /// <summary>
        /// Sets the value of a property.
        /// </summary>
        /// <typeparam name="TP">The type of the property value.</typeparam>
        /// <param name="propertySelector">Expression tree contains the property definition.</param>
        /// <param name="value">The property value.</param>
        protected void SetValue<TP>(Expression<Func<TP>> propertySelector, TP value)
        {
            string propertyName = GetPropertyName(propertySelector);

            SetValue<TP>(propertyName, value);
        }

        /// <summary>
        /// Sets the value of a property.
        /// </summary>
        /// <typeparam name="TP">The type of the property value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The property value.</param>
        protected void SetValue<TP>(string propertyName, TP value)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            _values[propertyName] = value;
            NotifyPropertyChanged(propertyName);
        }

        protected void SetValue<TP>(Expression<Func<TP>> propertySelector, TP value, params string[] propertiesToNotify)
        {
            string propertyName = GetPropertyName(propertySelector);
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            _values[propertyName] = value;
            foreach (var property in propertiesToNotify)
                NotifyPropertyChanged(property);
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="TP">The type of the property value.</typeparam>
        /// <param name="propertySelector">Expression tree contains the property definition.</param>
        /// <returns>The value of the property or default value if not exist.</returns>
        protected TP GetValue<TP>(Expression<Func<TP>> propertySelector)
        {
            string propertyName = GetPropertyName(propertySelector);

            return GetValue<TP>(propertyName);
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="TP">The type of the property value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property or default value if not exist.</returns>
        protected TP GetValue<TP>(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }

            object value;
            if (!_values.TryGetValue(propertyName, out value))
            {
                value = default(TP);
                _values.Add(propertyName, value);
            }

            return (TP)value;
        }

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected void NotifyPropertyChanged<TP>(Expression<Func<TP>> propertySelector)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                string propertyName = GetPropertyName(propertySelector);
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Privates

        private string GetPropertyName(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new InvalidOperationException();
            }

            return memberExpression.Member.Name;
        }

        protected object GetValue(string propertyName)
        {
            object value;
            if (!_values.TryGetValue(propertyName, out value))
            {
                var propertyDescriptor = TypeDescriptor.GetProperties(GetType()).Find(propertyName, false);
                if (propertyDescriptor == null)
                {
                    throw new ArgumentException("Invalid property name", propertyName);
                }

                value = propertyDescriptor.GetValue(this);
                _values.Add(propertyName, value);
            }

            return value;
        }

        #endregion
    }
}
