using System.ComponentModel.DataAnnotations;


namespace Ergasiomanis.Models
{
    [MetadataType(typeof(jobsMetadata))]
    public partial class jobs
    { 
    }

    [MetadataType(typeof(authorsMetadata))]
    public partial class authors
    {
    }

    [MetadataType(typeof(discountsMetadata))]
    public partial class discounts 
    { 
    }

    [MetadataType(typeof(storesMetadata))]
    public partial class stores 
    { 
    }

    [MetadataType(typeof(salesMetadata))]
    public partial class sales
    {
    }

    [MetadataType(typeof(titlesMetadata))]
    public partial class titles
    {
    }

    [MetadataType(typeof(roychedsMetadata))]
    public partial class roysched
    {
    }

    [MetadataType(typeof(titleauthorsMetadata))]
    public partial class titleauthor
    {
    }

    [MetadataType(typeof(employeesMetadata))]
    public partial class employee
    {
    }
}