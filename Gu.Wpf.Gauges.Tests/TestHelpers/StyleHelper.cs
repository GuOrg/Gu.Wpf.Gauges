namespace Gu.Wpf.Gauges.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Windows;
    using System.Windows.Baml2006;
    using System.Xaml;

    public static class StyleHelper
    {
        private static readonly ConcurrentDictionary<Type, Style> Cache = new ConcurrentDictionary<Type, Style>();

        public static Style DefaultStyle<T>()
            where T : FrameworkElement
        {
            return Cache.GetOrAdd(typeof(T), ReadDefaultStyle);
        }

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        private static Style ReadDefaultStyle(Type type)
        {
            var assembly = type.Assembly;
            using var resourceReader = new ResourceReader(assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().Single()));
            foreach (DictionaryEntry entry in resourceReader)
            {
                using var reader = new Baml2006Reader((Stream)entry.Value);
                using var writer = new XamlObjectWriter(reader.SchemaContext);
                while (reader.Read())
                {
                    writer.WriteNode(reader);
                }

                var resourceDictionary = (ResourceDictionary)writer.Result;
                if (resourceDictionary.Contains(type))
                {
                    return (Style)resourceDictionary[type];
                }
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
