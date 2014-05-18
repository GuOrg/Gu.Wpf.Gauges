namespace GaugeBox
{
    using System;
    using System.Reflection;
    using System.Windows.Controls.Primitives;

    public class CodeGenerator
    {
        ////[Test]
        public void TestNameTest()
        {
            var template = new AddOwnerTemplate();
            Type oldOwner = typeof(TickBar);
            FieldInfo[] fieldInfos = oldOwner.GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
                string propName = fieldInfo.Name.Replace("Property", "");
                PropertyInfo propertyInfo = oldOwner.GetProperty(propName);
                Console.Write(template.WriteCode(oldOwner.Name, "Gauge", propName, propertyInfo.PropertyType.Name));
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
