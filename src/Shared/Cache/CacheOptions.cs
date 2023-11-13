using System.ComponentModel.DataAnnotations;

namespace ServiceCache;

public class CacheOptions
{
    [Required]
    public int SlidingExpirationToNowInMinutes { get; set; }
}
