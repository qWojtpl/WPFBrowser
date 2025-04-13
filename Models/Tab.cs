using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WPFBrowser.Data;
using WPFBrowser.Services;

namespace WPFBrowser.Models;

[Table("Tabs")]
public class Tab : GenericPropertyChanged
{
    
    [Key]
    public int Id { get; set; }

    public Uri? Uri
    {
        get => _uri;
        set
        {
            if (_uri != value)
            {
                _uri = value;
                TabsService.CurrentUri = _uri.ToString();
                TabsService.SaveTab(this);
                OnPropertyChanged();
            }
        }
    }

    private Uri _uri;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isSelected;
    public int SmallHistoryPointer { get; set; } = 0;
    public List<string> SmallHistory { get; set; } = new();
    
    [NotMapped]
    public TabsService TabsService { get; set; }

}