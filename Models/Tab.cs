using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WPFBrowser.Data;

namespace WPFBrowser.Models;

[Table("Tabs")]
public class Tab : GenericPropertyChanged
{
    
    [Key]
    public int Id { get; set; }

    public Uri Uri
    {
        get => _uri;
        set
        {
            if (_uri != value)
            {
                _uri = value;
                OnPropertyChanged();
            }
        }
    }

    private Uri _uri;
    public int SmallHistoryPointer { get; set; } = 0;
    public List<string> SmallHistory { get; set; } = new();

}