using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaugeBox
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using Annotations;

    public class CodeVm : INotifyPropertyChanged
    {
        private readonly ObservableCollection<AddOwner> addOwners = new ObservableCollection<AddOwner>();
        private Type type;

        public CodeVm()
        {
            Type = typeof (Control);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<Type> AllTypes
        {
            get
            {
                var types = typeof(Control).Assembly.GetTypes();
                var enumerable = types.Where(x => x.IsSubclassOf(typeof(UIElement))).ToArray();
                return enumerable
                            .OrderBy(x => x.Name);
            }
        }
        public Type Type
        {
            get { return this.type; }
            set
            {
                if (value == this.type)
                {
                    return;
                }
                this.type = value;
                AddOwners.Clear();
                foreach (var code in GeneratedCode(this.type))
                {
                    AddOwners.Add(code);
                }

                OnPropertyChanged();
            }
        }
        private IEnumerable<AddOwner> GeneratedCode(Type oldOwner)
        {
            var template = new AddOwnerTemplate();
            FieldInfo[] fieldInfos = oldOwner.GetFields(BindingFlags.Static|BindingFlags.DeclaredOnly|BindingFlags.Public);
            foreach (var fieldInfo in fieldInfos)
            {
                string propName = fieldInfo.Name.Replace("Property", "");
                PropertyInfo propertyInfo = oldOwner.GetProperty(propName);
                if (propertyInfo == null)
                {
                    continue;
                }
                var code = template.WriteCode(oldOwner.Name, "Gauge", propName, propertyInfo.PropertyType.Name);
                yield return new AddOwner(propName, code);
            }
        }

        public ObservableCollection<AddOwner> AddOwners
        {
            get { return this.addOwners; }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class AddOwner
    {
        public AddOwner(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }
        public string Name { get; private set; }
        public string Code { get; private set; }
    }
}
