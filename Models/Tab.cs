using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFBrowser.Models;

[Table("Tabs")]
public class Tab
{
    
    [Key]
    public int Id { get; set; }
    public string Uri { get; set; }
    
}