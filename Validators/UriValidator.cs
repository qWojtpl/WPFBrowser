namespace WPFBrowser.Validators;

public static class UriValidator
{
    
    public static Uri? ValidateUri(string strUri)
    {
        Uri? uri;
        try
        {
            uri = new Uri(strUri);
        }
        catch (Exception)
        {
            try
            {
                uri = new Uri("https://" + strUri);
            }
            catch (Exception)
            {
                uri = null;
            }
        }
        return uri;
    }
    
}