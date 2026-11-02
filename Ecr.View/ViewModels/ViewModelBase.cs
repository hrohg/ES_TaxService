using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ECR.Controls.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var propertyBody = property.Body as MemberExpression;

            if (propertyBody == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyBody.Member.Name));
        }
    }
}
