public class EditorData
{
    public static EditorData Instance => _instance ?? (_instance = new EditorData());
    static EditorData _instance;
    
    //List public fields for the data container here

    EditorData()
    {
        ReloadData();
    }

    public void ReloadData()
    {
        //Load data here i.e. using my JsonHelper
    }
}
