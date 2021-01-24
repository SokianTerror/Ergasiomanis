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
}