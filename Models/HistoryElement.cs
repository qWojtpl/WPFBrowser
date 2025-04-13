using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFBrowser.Models;

[Table("History")]
public class HistoryElement
{
    
    [Key]
    public int Id { get; set; }
    public string Uri { get; set; }
    public DateTime Date { get; set; }
    
}