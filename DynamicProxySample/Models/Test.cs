using FreeSql.DataAnnotations;

namespace DynamicProxySample.Models;

[Table(Name = "test")]
public class Test
{
    [Column(Name = "id", IsPrimary = true, IsIdentity = true)]
    public long Id { get; set; } = default!;

    [Column(Name = "name")]
    public string Name { get; set; } = default!;
}