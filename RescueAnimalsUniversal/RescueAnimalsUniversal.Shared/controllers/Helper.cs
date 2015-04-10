using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Storage;


public static class StaticLink
{
    public static Uri GoldStarUri = new Uri("ms-appx:///images/icons/gold_star.png");
    public static Uri BlackStarUri = new Uri("ms-appx:///images/icons/black_star.png");
    public static Uri BlackImageUri = new Uri("ms-appx:///images/systems/z.png");
    public static Uri SoundCorrectUri = new Uri("ms-appx:///sounds/systems/correct.wav");
    public static Uri SoundWrongUri = new Uri("ms-appx:///sounds/systems/wrong.wav");
}


public static class Helper
{

    public static Random Rand = new Random();

    #region Sử dụng cho User thôi
    public static async Task<string> ReadFileApplicationDatAsync(string fileName)
    {
        StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
        String content = await FileIO.ReadTextAsync(file);
        return content;
    }
    public static async Task WriteToFileApplicationDataAsync(string[] lines, string fileName)
    {
        StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        await FileIO.WriteLinesAsync(file, lines);
    }
    #endregion

    #region file
    public static async Task<string> ReadFileAsync(string fileUrl)
    {
        StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(fileUrl);
        String content = await FileIO.ReadTextAsync(file);
        return content;
    }

    public static async Task ResetFile()
    {
        var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
        foreach (var file in files)
            await file.DeleteAsync();
    }
    #endregion

}


