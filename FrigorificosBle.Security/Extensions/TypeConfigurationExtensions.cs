using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

internal static class TypeConfigurationExtensions
{
    public static PrimitivePropertyConfiguration HasUniqueIndexAnnotation(
        this PrimitivePropertyConfiguration property,
        string indexName,
        int columnOrder)
    {
        var indexAttribute = new IndexAttribute(indexName, columnOrder) { IsUnique = true };
        var indexAnnotation = new IndexAnnotation(indexAttribute);

        return property.HasColumnAnnotation(IndexAnnotation.AnnotationName, indexAnnotation);
    }
}